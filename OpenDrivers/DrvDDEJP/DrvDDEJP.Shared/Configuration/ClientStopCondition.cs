using Scada.Comm.Channels;
using System;

namespace Scada.Comm.Drivers.DrvDDEJP
{
    /// <summary>
    /// Implements a configurable stop condition for incoming data.
    /// <para>Реализует настраиваемое условие остановки для входящих данных.</para>
    /// </summary>
    public class ClientStopCondition : BinStopCondition
    {
        #region Variable

        private readonly bool isLengthMode;                         // indicates whether the stop condition is based on length
        private readonly int checkAddress;                          // address to check for marker or length
        private readonly int checkLength;                           // length of the marker or length field
        private readonly TypeCode checkFormat;                      // data format of the field being checked
        private readonly object markerValue;                        // marker value for sequence mode
        private readonly bool lengthIncludesItself;                 // indicates whether the length value includes the length field itself

        private int bytesRead;                                      // number of bytes read so far
        private byte[] buffer;                                      // buffer to store read data
        private bool conditionMet;                                  // indicates whether the stop condition has been met
        private int expectedTotalLength;                            // expected total length of data in length mode
        private bool lengthDetermined;                              // indicates whether the total length has been determined

        #endregion Variable

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public ClientStopCondition() : base(0)
        {
            StopSeq = null;
        }

        /// <summary>
        /// Initializes a new instance using configuration parameters.
        /// <para>Инициализирует экземпляр с параметрами конфигурации.</para>
        /// </summary>
        public ClientStopCondition(
            int checkAddress,
            int checkLength,
            TypeCode checkFormat,
            bool isLengthMode,
            object markerValue = null,
            bool lengthIncludesItself = false) : base(0)
        {
            StopSeq = null;
            this.isLengthMode = isLengthMode;

            if (isLengthMode)
            {
                this.checkAddress = checkAddress;
                this.checkLength = checkLength;
                this.checkFormat = checkFormat;
                this.lengthIncludesItself = lengthIncludesItself;
            }
            else
            {
                this.checkAddress = checkAddress;
                this.checkLength = checkLength;
                this.checkFormat = checkFormat;
                this.markerValue = markerValue ?? false;
            }

            Reset();
        }

        /// <summary>
        /// Checks whether the stop condition is met for incoming data.
        /// <para>Проверяет, выполнено ли условие остановки при приёме данных.</para>
        /// </summary>
        public override bool CheckCondition(byte[] buffer, int index)
        {
            if (index < 0 || index >= buffer.Length)
            {
                return false;
            }

            bytesRead++;
            this.buffer = buffer;

            return isLengthMode
                ? CheckLengthMode(index)
                : CheckMarkerMode(index);
        }

        /// <summary>
        /// Resets the internal state of the condition.
        /// <para>Сбрасывает внутреннее состояние условия.</para>
        /// </summary>
        public void Reset()
        {
            bytesRead = 0;
            conditionMet = false;
            lengthDetermined = false;
            expectedTotalLength = -1;
            buffer = null;
        }

        #endregion Basic

        #region Private Methods

        /// <summary>
        /// Marker mode logic: checks if marker value is present at expected address.
        /// <para>Логика режима маркера: проверяет наличие маркера по адресу.</para>
        /// </summary>
        private bool CheckMarkerMode(int index)
        {
            int lastNeededIndex = checkAddress + checkLength - 1;

            if (!conditionMet && index >= lastNeededIndex)
            {
                conditionMet = CheckMarker();
                if (conditionMet)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Length mode logic: reads the length field and waits for the complete message.
        /// <para>Логика режима длины: считывает поле длины и ждёт полного сообщения.</para>
        /// </summary>
        private bool CheckLengthMode(int index)
        {
            int lastLengthByteIndex = checkAddress + checkLength - 1;

            if (!lengthDetermined && index >= lastLengthByteIndex)
            {
                expectedTotalLength = ReadLengthValue();
                lengthDetermined = true;
            }

            return lengthDetermined && bytesRead >= expectedTotalLength;
        }

        /// <summary>
        /// Parses the length field from the buffer.
        /// <para>Парсит поле длины из буфера.</para>
        /// </summary>
        private int ReadLengthValue()
        {
            if (buffer == null || checkAddress + checkLength > buffer.Length)
            {
                return 0;
            }

            try
            {
                byte[] bytes = new byte[checkLength];
                Array.Copy(buffer, checkAddress, bytes, 0, checkLength);

                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(bytes);
                }

                int lengthValue = checkFormat switch
                {
                    TypeCode.Byte => bytes[0],
                    TypeCode.UInt16 => BitConverter.ToUInt16(bytes, 0),
                    TypeCode.UInt32 => (int)BitConverter.ToUInt32(bytes, 0),
                    _ => 0
                };

                if (!lengthIncludesItself)
                {
                    lengthValue += checkLength;
                }

                return lengthValue;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Checks whether the marker value matches.
        /// <para>Проверяет, совпадает ли маркер.</para>
        /// </summary>
        private bool CheckMarker()
        {
            if (buffer == null || checkAddress + checkLength > buffer.Length)
            {
                return false;
            }

            try
            {
                byte[] bytes = new byte[checkLength];
                Array.Copy(buffer, checkAddress, bytes, 0, checkLength);

                if (BitConverter.IsLittleEndian && checkLength > 1)
                {
                    Array.Reverse(bytes);
                }

                ulong actualValue = checkFormat switch
                {
                    TypeCode.Byte => bytes[0],
                    TypeCode.UInt16 => BitConverter.ToUInt16(bytes, 0),
                    TypeCode.UInt32 => BitConverter.ToUInt32(bytes, 0),
                    TypeCode.UInt64 => BitConverter.ToUInt64(bytes, 0),
                    _ => 0
                };

                if (markerValue is bool boolValue)
                {
                    return (actualValue != 0) == boolValue;
                }

                return actualValue == Convert.ToUInt64(markerValue);
            }
            catch
            {
                return false;
            }
        }

        #endregion Private Methods
    }
}
