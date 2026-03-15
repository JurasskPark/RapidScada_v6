using DebugerLog;
using Scada.Comm.Channels;
using Scada.Lang;
using System;
using System.Diagnostics;

namespace Scada.Comm.Drivers.DrvDebug
{
    /// <summary>
    /// Implements a configurable stop condition for incoming data.
    /// <para>Реализует настраиваемое условие остановки для входящих данных.</para>
    /// </summary>
    public class ClientStopCondition : BinStopCondition
    {
        // Common fields.
        private int _bytesRead = 0;                     // Bytes read so far.
        private byte[]? _buffer;                        // Buffer with received data.
        private bool _conditionMet = false;             // Flag indicating the condition was met.

        // Fields for marker mode.
        private int _checkAddress;                      // Address to verify.
        private int _checkLength;                       // Length of data to check.
        private TypeCode _checkFormat;                  // Data format.
        private object? _markerValue;                   // Value to find.

        // Fields for length mode.
        private int _lengthAddress;                     // Address of the length field.
        private int _lengthFieldSize;                   // Size of the length field.
        private TypeCode _lengthFormat;                 // Format of the length field.
        private bool _lengthIncludesItself;             // Indicates whether the length includes itself.
        private int _expectedTotalLength = -1;          // Calculated total message length.
        private bool _isLengthMode = false;             // Indicates the active mode.
        private bool _lengthDetermined = false;         // Indicates whether the length has been read.

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ClientStopCondition() : base(0)
        {
            StopSeq = null;
        }

        /// <summary>
        /// Initializes a new instance of the class using configuration parameters.
        /// </summary>
        /// <param name="checkAddress">Address to check (for both modes)</param>
        /// <param name="checkLength">Length to check (for both modes)</param>
        /// <param name="checkFormat">Data format (for both modes)</param>
        /// <param name="isLengthMode">True for Length mode, False for Marker mode</param>
        /// <param name="markerValue">Value to find (for Marker mode, can be null)</param>
        /// <param name="lengthIncludesItself">Does length field include itself (for Length mode)</param>
        public ClientStopCondition
        (
            int checkAddress,
            int checkLength,
            TypeCode checkFormat,
            bool isLengthMode,
            object? markerValue = null,
            bool lengthIncludesItself = false
        ) : base(0)
        {
            StopSeq = null;

            _isLengthMode = isLengthMode;

            if (isLengthMode)
            {
                // Initialize length mode.
                _lengthAddress = checkAddress;
                _lengthFieldSize = checkLength;
                _lengthFormat = checkFormat;
                _lengthIncludesItself = lengthIncludesItself;
            }
            else
            {
                // Initialize marker mode.
                _checkAddress = checkAddress;
                _checkLength = checkLength;
                _checkFormat = checkFormat;
                _markerValue = markerValue ?? false; // Default to false if null.
            }
        }

        /// <summary>
        /// Initializes a new marker mode stop condition.
        /// </summary>
        public ClientStopCondition(int checkAddress, int checkLength, TypeCode checkFormat, object markerValue) : base(0)
        {
            StopSeq = null;
            _isLengthMode = false;
            _checkAddress = checkAddress;
            _checkLength = checkLength;
            _checkFormat = checkFormat;
            _markerValue = markerValue;
        }

        /// <summary>
        /// Initializes a new length mode stop condition.
        /// </summary>
        /// <param name="lengthAddress">Address of length field</param>
        /// <param name="lengthFieldSize">Size of length field in bytes</param>
        /// <param name="lengthFormat">Format of length field</param>
        /// <param name="lengthIncludesItself">True if length value includes the field itself</param>
        public ClientStopCondition(int lengthAddress, int lengthFieldSize, TypeCode lengthFormat, bool lengthIncludesItself) : base(0)
        {
            StopSeq = null;
            _isLengthMode = true;
            _lengthAddress = lengthAddress;
            _lengthFieldSize = lengthFieldSize;
            _lengthFormat = lengthFormat;
            _lengthIncludesItself = lengthIncludesItself;
        }

        /// <summary>
        /// Checks if the stop condition is met based on the received data.
        /// This method is called by the communication channel for each received byte.
        /// </summary>
        /// <param name="buffer">The receive buffer containing all received data.</param>
        /// <param name="index">The index of the current byte being processed.</param>
        /// <returns>True if the stop condition is met; otherwise, false.</returns>
        /// <summary>
        /// Checks whether the stop condition is met.
        /// </summary>
        public override bool CheckCondition(byte[] buffer, int index)
        {
            DebugerReturn debugerReturn = new DebugerReturn();

            if (index < 0 || index >= buffer.Length)
            {
                return false;
            }

            _bytesRead++;
            _buffer = buffer;

            if (_isLengthMode)
            {
                return CheckLengthMode(index);
            }
            else
            {
                return CheckMarkerMode(index);
            }
        }

        /// <summary>
        /// The Marker Mode logic.
        /// Checks if the marker value is present at the expected address.
        /// </summary>
        /// <param name="index">Current byte index.</param>
        /// <returns>True if the marker is found and condition is met.</returns>
        private bool CheckMarkerMode(int index)
        {
            int lastNeededIndex = _checkAddress + _checkLength - 1;

            if (!_conditionMet && index >= lastNeededIndex)
            {             
                _conditionMet = CheckMarker();

                if (_conditionMet)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// The Length Mode logic.
        /// Reads the length field and waits for the complete message.
        /// </summary>
        /// <param name="index">Current byte index.</param>
        /// <returns>True when the expected total length has been reached.</returns>
        private bool CheckLengthMode(int index)
        {
            int lastLengthByteIndex = _lengthAddress + _lengthFieldSize - 1;

            if (!_lengthDetermined && index >= lastLengthByteIndex)
            {
                _expectedTotalLength = ReadLengthValue();
                _lengthDetermined = true;
            }

            if (_lengthDetermined)
            {
                bool shouldStop = _bytesRead >= _expectedTotalLength;
                return shouldStop;
            }

            return false;
        }

        /// <summary>
        /// Parses the length field from the buffer and calculates the expected total message length.
        /// </summary>
        /// <returns>The expected total message length in bytes.</returns>
        private int ReadLengthValue()
        {
            DebugerReturn debugerReturn = new DebugerReturn();

            if (_buffer == null || _lengthAddress + _lengthFieldSize > _buffer.Length)
            {
                return 0;
            }

            try
            {
                byte[] bytes = new byte[_lengthFieldSize];
                Array.Copy(_buffer, _lengthAddress, bytes, 0, _lengthFieldSize);

                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(bytes);
                }

                int lengthValue = 0;

                switch (_lengthFormat)
                {
                    case TypeCode.Byte:
                        lengthValue = bytes[0];
                        break;

                    case TypeCode.UInt16:
                        lengthValue = BitConverter.ToUInt16(bytes, 0);
                        break;

                    case TypeCode.UInt32:
                        lengthValue = (int)BitConverter.ToUInt32(bytes, 0);
                        break;

                    default:
                        return 0;
                }

                if (!_lengthIncludesItself)
                {
                    lengthValue += _lengthFieldSize;
                }

                return lengthValue;
            }
            catch (Exception ex)
            {
                debugerReturn.Log($"Exception: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Checks if the marker value matches at the specified address.
        /// Handles different data formats and endianness.
        /// </summary>
        /// <returns>True if the marker value matches; otherwise, false.</returns>
        private bool CheckMarker()
        {
            DebugerReturn debugerReturn = new DebugerReturn();

            if (_buffer == null)
            {
                return false;
            }

            if (_checkAddress + _checkLength > _buffer.Length)
            {
                return false;
            }

            try
            {
                byte[] bytes = new byte[_checkLength];
                Array.Copy(_buffer, _checkAddress, bytes, 0, _checkLength);

                if (BitConverter.IsLittleEndian && _checkLength > 1)
                {
                    Array.Reverse(bytes);
                }

                ulong actualValue = 0;

                switch (_checkFormat)
                {
                    case TypeCode.Byte:
                        actualValue = bytes[0];
                        break;
                    case TypeCode.UInt16:
                        actualValue = BitConverter.ToUInt16(bytes, 0);
                        break;
                    case TypeCode.UInt32:
                        actualValue = BitConverter.ToUInt32(bytes, 0);
                        break;
                    case TypeCode.UInt64:
                        actualValue = BitConverter.ToUInt64(bytes, 0);
                        break;
                    default:
                        return false;
                }

                if (_markerValue is bool boolValue)
                {
                    bool result = (actualValue != 0) == boolValue;
                    return result;
                }
                else
                {
                    ulong markerNumeric = Convert.ToUInt64(_markerValue);
                    bool result = actualValue == markerNumeric;
                    return result;
                }
            }
            catch (Exception ex)
            {
                debugerReturn.Log($"Exception: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Resets the internal state of the condition.
        /// Should be called before starting to read a new message.
        /// </summary>
        public void Reset()
        {
            _bytesRead = 0;
            _conditionMet = false;
            _lengthDetermined = false;
            _expectedTotalLength = -1;
            _buffer = null;
        }

        #region Usage Examples

        /*
         * ===== USAGE EXAMPLES =====
         * 
         * // MARKER MODE - Find specific value
         * var markerCondition = new ClientStopCondition(
         *     checkAddress: 5,
         *     checkLength: 2,
         *     checkFormat: TypeCode.UInt16,
         *     markerValue: (ushort)0xFFFF);
         * 
         * // LENGTH MODE - Read length field
         * var lengthCondition = new ClientStopCondition(
         *     lengthAddress: 0,
         *     lengthFieldSize: 4,
         *     lengthFormat: TypeCode.UInt32,
         *     lengthIncludesItself: false);
         * 
         * // UNIVERSAL CONSTRUCTOR - From config
         * var condition = new ClientStopCondition(
         *     checkAddress: config.CheckAddress,
         *     checkLength: config.CheckLength,
         *     checkFormat: config.CheckFormat,
         *     isLengthMode: config.LengthMode,
         *     markerValue: config.CheckValue,
         *     lengthIncludesItself: config.LengthIncludesItself);
         * 
         * ===== CONFIGURATION XML EXAMPLE =====
         * 
         * <!-- MARKER MODE CONFIGURATION -->
         * <StopConditionCheckAddress>5</StopConditionCheckAddress>
         * <StopConditionCheckLength>2</StopConditionCheckLength>
         * <StopConditionCheckFormat>UInt16</StopConditionCheckFormat>
         * <StopConditionCheckValue>65535</StopConditionCheckValue>
         * <StopConditionLengthMode>false</StopConditionLengthMode>
         * <StopConditionLengthIncludesItself>false</StopConditionLengthIncludesItself>
         * 
         * <!-- LENGTH MODE CONFIGURATION -->
         * <StopConditionCheckAddress>0</StopConditionCheckAddress>
         * <StopConditionCheckLength>4</StopConditionCheckLength>
         * <StopConditionCheckFormat>UInt32</StopConditionCheckFormat>
         * <StopConditionCheckValue>0</StopConditionCheckValue> <!-- ignored -->
         * <StopConditionLengthMode>true</StopConditionLengthMode>
         * <StopConditionLengthIncludesItself>false</StopConditionLengthIncludesItself>
         */

        #endregion Usage Examples
    }
}
