using System;
using System.Collections.Generic;
using System.Linq;

namespace HexTools
{
    /// <summary>
    /// Provides methods for working with DateTime in hex format.
    /// <para>Предоставляет методы для работы с DateTime в шестнадцатеричном формате.</para>
    /// Supports various PLC and industrial formats including the year rule (0-79 = 2000+, 80-99 = 1900+).
    /// </summary>
    public static class DateTime
    {
        #region Constants

        private const int DateBytesLength = 4;  // DD.MM.YY or YY.MM.DD.
        private const int TimeBytesLength = 4;  // HH.MM.SS.ms.
        private const int DateTimeBytesLength = 8; // Date (4) + Time (4).
        private const int UnixEpochYear = 1970;

        #endregion

        #region Date Formats

        /// <summary>
        /// Date format for PLC/industrial systems
        /// </summary>
        public enum DateFormat
        {
            /// <summary>DD.MM.YY (day, month, year) - common in many PLCs</summary>
            DayMonthYear,
            /// <summary>MM.DD.YY (month, day, year) - US format</summary>
            MonthDayYear,
            /// <summary>YY.MM.DD (year, month, day) - ISO format</summary>
            YearMonthDay
        }

        /// <summary>
        /// Time format for PLC/industrial systems
        /// </summary>
        public enum TimeFormat
        {
            /// <summary>HH.MM.SS (hours, minutes, seconds)</summary>
            HoursMinutesSeconds,
            /// <summary>HH.MM (hours, minutes) - seconds omitted</summary>
            HoursMinutes,
            /// <summary>MM.SS (minutes, seconds) - hours omitted</summary>
            MinutesSeconds
        }

        #endregion

        #region Year Conversion Helpers

        /// <summary>
        /// Converts PLC-style year byte to full year
        /// Years 0-79 -> 2000-2079, Years 80-99 -> 1980-1999
        /// </summary>
        /// <param name="yearByte">Year byte (0-99)</param>
        /// <returns>Full year (4 digits)</returns>
        private static int PlcYearToFullYear(byte yearByte)
        {
            if (yearByte <= 79)
            {
                return 2000 + yearByte;
            }
            else
            {
                return 1900 + yearByte;
            }
        }

        /// <summary>
        /// Converts full year to PLC-style year byte
        /// </summary>
        /// <param name="fullYear">Full year (4 digits)</param>
        /// <returns>Year byte (0-99)</returns>
        private static byte FullYearToPlcYear(int fullYear)
        {
            if (fullYear >= 2000 && fullYear <= 2079)
            {
                return (byte)(fullYear - 2000);
            }
            else if (fullYear >= 1980 && fullYear <= 1999)
            {
                return (byte)(fullYear - 1900);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(fullYear), "Year must be between 1980-2079 for PLC format");
            }
        }

        #endregion

        #region From ByteArray (Date)

        /// <summary>
        /// Converts byte array to date (without time)
        /// </summary>
        /// <param name="bytes">Byte array (4 bytes: [day, month, year, 0] or custom format)</param>
        /// <param name="format">Date format (default: DayMonthYear)</param>
        /// <param name="yearBase">Base year (default: 2000, common in PLCs)</param>
        /// <returns>DateTime with date part only, or DateTime.MinValue on error</returns>
        public static System.DateTime FromDateByteArray(byte[]? bytes, DateFormat format = DateFormat.DayMonthYear, int yearBase = 2000)
        {
            if (bytes == null || bytes.Length < 3)
            {
                return System.DateTime.MinValue;
            }

            try
            {
                int day, month, year;

                switch (format)
                {
                    case DateFormat.DayMonthYear:
                // bytes[0] = day, bytes[1] = month, bytes[2] = year.
                        day = bytes[0];
                        month = bytes[1];
                        year = yearBase + bytes[2];
                        break;

                    case DateFormat.MonthDayYear:
                // bytes[0] = month, bytes[1] = day, bytes[2] = year.
                        month = bytes[0];
                        day = bytes[1];
                        year = yearBase + bytes[2];
                        break;

                    case DateFormat.YearMonthDay:
                // bytes[0] = year, bytes[1] = month, bytes[2] = day.
                        year = yearBase + bytes[0];
                        month = bytes[1];
                        day = bytes[2];
                        break;

                    default:
                        return System.DateTime.MinValue;
                }

            // Validate values.
                if (day < 1 || day > 31 || month < 1 || month > 12 || year < 1 || year > 9999)
                {
                    return System.DateTime.MinValue;
                }

                return new System.DateTime(year, month, day);
            }
            catch
            {
                return System.DateTime.MinValue;
            }
        }

        /// <summary>
        /// Converts byte array to date using PLC year rule (0-79=2000+, 80-99=1900+)
        /// </summary>
        /// <param name="bytes">Byte array (4 bytes: [day, month, year, 0] or custom format)</param>
        /// <param name="format">Date format (default: DayMonthYear)</param>
        /// <returns>DateTime with date part only, or DateTime.MinValue on error</returns>
        public static System.DateTime FromDateByteArrayPlc(byte[]? bytes, DateFormat format = DateFormat.DayMonthYear)
        {
            if (bytes == null || bytes.Length < 3)
            {
                return System.DateTime.MinValue;
            }

            try
            {
                int day, month, year;

                switch (format)
                {
                    case DateFormat.DayMonthYear:
                        day = bytes[0];
                        month = bytes[1];
                        year = PlcYearToFullYear(bytes[2]);
                        break;

                    case DateFormat.MonthDayYear:
                        month = bytes[0];
                        day = bytes[1];
                        year = PlcYearToFullYear(bytes[2]);
                        break;

                    case DateFormat.YearMonthDay:
                        year = PlcYearToFullYear(bytes[0]);
                        month = bytes[1];
                        day = bytes[2];
                        break;

                    default:
                        return System.DateTime.MinValue;
                }

            // Validate values.
                if (day < 1 || day > 31 || month < 1 || month > 12)
                {
                    return System.DateTime.MinValue;
                }

                return new System.DateTime(year, month, day);
            }
            catch
            {
                return System.DateTime.MinValue;
            }
        }

        /// <summary>
        /// Converts byte array to date using BCD format (common in PLCs)
        /// </summary>
        /// <param name="bytes">Byte array with BCD encoded date</param>
        /// <returns>DateTime value or MinValue on error</returns>
        public static System.DateTime FromDateBcd(byte[]? bytes)
        {
            if (bytes == null || bytes.Length < 4)
            {
                return System.DateTime.MinValue;
            }

            try
            {
            // Typical BCD format: [day, month, year, 0], where each byte is BCD.
                int day = BcdToByte(bytes[0]);
                int month = BcdToByte(bytes[1]);
                int year = 2000 + BcdToByte(bytes[2]);

                return new System.DateTime(year, month, day);
            }
            catch
            {
                return System.DateTime.MinValue;
            }
        }

        #endregion

        #region From ByteArray (Time)

        /// <summary>
        /// Converts byte array to time (without date)
        /// </summary>
        /// <param name="bytes">Byte array (4 bytes: [hour, minute, second, 0] or custom)</param>
        /// <param name="format">Time format</param>
        /// <returns>TimeSpan value or TimeSpan.Zero on error</returns>
        public static TimeSpan FromTimeByteArray(byte[]? bytes, TimeFormat format = TimeFormat.HoursMinutesSeconds)
        {
            if (bytes == null || bytes.Length < 2)
            {
                return TimeSpan.Zero;
            }

            try
            {
                int hours = 0, minutes = 0, seconds = 0;

                switch (format)
                {
                    case TimeFormat.HoursMinutesSeconds:
                        hours = (bytes.Length > 0) ? bytes[0] : 0;
                        minutes = (bytes.Length > 1) ? bytes[1] : 0;
                        seconds = (bytes.Length > 2) ? bytes[2] : 0;
                        break;

                    case TimeFormat.HoursMinutes:
                        hours = (bytes.Length > 0) ? bytes[0] : 0;
                        minutes = (bytes.Length > 1) ? bytes[1] : 0;
                        break;

                    case TimeFormat.MinutesSeconds:
                        minutes = (bytes.Length > 0) ? bytes[0] : 0;
                        seconds = (bytes.Length > 1) ? bytes[1] : 0;
                        break;
                }

            // Validate values.
                if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59 || seconds < 0 || seconds > 59)
                {
                    return TimeSpan.Zero;
                }

                return new TimeSpan(hours, minutes, seconds);
            }
            catch
            {
                return TimeSpan.Zero;
            }
        }

        /// <summary>
        /// Converts byte array to time using BCD format
        /// </summary>
        public static TimeSpan FromTimeBcd(byte[]? bytes)
        {
            if (bytes == null || bytes.Length < 3)
            {
                return TimeSpan.Zero;
            }

            try
            {
                int hours = BcdToByte(bytes[0]);
                int minutes = BcdToByte(bytes[1]);
                int seconds = BcdToByte(bytes[2]);

                return new TimeSpan(hours, minutes, seconds);
            }
            catch
            {
                return TimeSpan.Zero;
            }
        }

        #endregion

        #region From ByteArray (DateTime)

        /// <summary>
        /// Converts byte array to full date and time
        /// </summary>
        /// <param name="bytes">Byte array (8 bytes: first 4 for date, last 4 for time)</param>
        /// <param name="dateFormat">Date format</param>
        /// <param name="timeFormat">Time format</param>
        /// <param name="yearBase">Base year</param>
        /// <returns>DateTime value or MinValue on error</returns>
        public static System.DateTime FromByteArray(byte[]? bytes,
                                                    DateFormat dateFormat = DateFormat.DayMonthYear,
                                                    TimeFormat timeFormat = TimeFormat.HoursMinutesSeconds,
                                                    int yearBase = 2000)
        {
            if (bytes == null || bytes.Length < 8)
            {
                return System.DateTime.MinValue;
            }

            try
            {
            // Extract the date bytes.
                byte[] dateBytes = new byte[4];
                Array.Copy(bytes, 0, dateBytes, 0, 4);

            // Extract the time bytes.
                byte[] timeBytes = new byte[4];
                Array.Copy(bytes, 4, timeBytes, 0, 4);

                System.DateTime date = FromDateByteArray(dateBytes, dateFormat, yearBase);
                TimeSpan time = FromTimeByteArray(timeBytes, timeFormat);

                if (date == System.DateTime.MinValue)
                {
                    return System.DateTime.MinValue;
                }

                return date.Add(time);
            }
            catch
            {
                return System.DateTime.MinValue;
            }
        }

        /// <summary>
        /// Converts byte array to DateTime using PLC year rule
        /// </summary>
        /// <param name="bytes">Byte array (8 bytes: [day, month, year, 0, hour, minute, second, 0])</param>
        /// <returns>DateTime value or MinValue on error</returns>
        public static System.DateTime FromByteArrayPlc(byte[]? bytes)
        {
            if (bytes == null || bytes.Length < 8)
            {
                return System.DateTime.MinValue;
            }

            try
            {
            // Process the date part.
                int day = bytes[0];
                int month = bytes[1];
                int year = PlcYearToFullYear(bytes[2]);

            // Process the time part.
                int hour = bytes[4];
                int minute = bytes[5];
                int second = bytes[6];

            // Validate values.
                if (day < 1 || day > 31 || month < 1 || month > 12)
                {
                    return System.DateTime.MinValue;
                }

                if (hour < 0 || hour > 23 || minute < 0 || minute > 59 || second < 0 || second > 59)
                {
                    return System.DateTime.MinValue;
                }

                return new System.DateTime(year, month, day, hour, minute, second);
            }
            catch
            {
                return System.DateTime.MinValue;
            }
        }

        /// <summary>
        /// Converts byte array to DateTime using typical PLC format (DD.MM.YY HH.MM.SS) with base year
        /// </summary>
        public static System.DateTime FromByteArrayPlc(byte[]? bytes, int yearBase)
        {
            if (bytes == null || bytes.Length < 8)
            {
                return System.DateTime.MinValue;
            }

            try
            {
                return new System.DateTime(
                    yearBase + bytes[2], bytes[1], bytes[0],
                    bytes[4], bytes[5], bytes[6]
                );
            }
            catch
            {
                return System.DateTime.MinValue;
            }
        }

        /// <summary>
        /// Converts byte array to DateTime using BCD format
        /// </summary>
        public static System.DateTime FromByteArrayBcd(byte[]? bytes)
        {
            if (bytes == null || bytes.Length < 8)
            {
                return System.DateTime.MinValue;
            }

            try
            {
            // BCD format: [day, month, year, 0, hour, minute, second, 0].
                int day = BcdToByte(bytes[0]);
                int month = BcdToByte(bytes[1]);
                int year = 2000 + BcdToByte(bytes[2]);
                int hour = BcdToByte(bytes[4]);
                int minute = BcdToByte(bytes[5]);
                int second = BcdToByte(bytes[6]);

                return new System.DateTime(year, month, day, hour, minute, second);
            }
            catch
            {
                return System.DateTime.MinValue;
            }
        }

        #endregion

        #region To ByteArray

        /// <summary>
        /// Converts DateTime to byte array (date part only)
        /// </summary>
        public static byte[] ToDateByteArray(System.DateTime date, DateFormat format = DateFormat.DayMonthYear, int yearBase = 2000)
        {
            byte[] bytes = new byte[4];

            try
            {
                int yearOffset = date.Year - yearBase;
                if (yearOffset < 0 || yearOffset > 255)
                {
                    return Array.Empty<byte>();
                }

                switch (format)
                {
                    case DateFormat.DayMonthYear:
                        bytes[0] = (byte)date.Day;
                        bytes[1] = (byte)date.Month;
                        bytes[2] = (byte)yearOffset;
                        bytes[3] = 0;
                        break;

                    case DateFormat.MonthDayYear:
                        bytes[0] = (byte)date.Month;
                        bytes[1] = (byte)date.Day;
                        bytes[2] = (byte)yearOffset;
                        bytes[3] = 0;
                        break;

                    case DateFormat.YearMonthDay:
                        bytes[0] = (byte)yearOffset;
                        bytes[1] = (byte)date.Month;
                        bytes[2] = (byte)date.Day;
                        bytes[3] = 0;
                        break;
                }

                return bytes;
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        /// <summary>
        /// Converts DateTime to PLC-style byte array with year rule
        /// </summary>
        public static byte[] ToDateByteArrayPlc(System.DateTime date, DateFormat format = DateFormat.DayMonthYear)
        {
            byte[] bytes = new byte[4];

            try
            {
                byte yearByte = FullYearToPlcYear(date.Year);

                switch (format)
                {
                    case DateFormat.DayMonthYear:
                        bytes[0] = (byte)date.Day;
                        bytes[1] = (byte)date.Month;
                        bytes[2] = yearByte;
                        bytes[3] = 0;
                        break;

                    case DateFormat.MonthDayYear:
                        bytes[0] = (byte)date.Month;
                        bytes[1] = (byte)date.Day;
                        bytes[2] = yearByte;
                        bytes[3] = 0;
                        break;

                    case DateFormat.YearMonthDay:
                        bytes[0] = yearByte;
                        bytes[1] = (byte)date.Month;
                        bytes[2] = (byte)date.Day;
                        bytes[3] = 0;
                        break;
                }

                return bytes;
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        /// <summary>
        /// Converts TimeSpan to byte array (time part only)
        /// </summary>
        public static byte[] ToTimeByteArray(TimeSpan time, TimeFormat format = TimeFormat.HoursMinutesSeconds)
        {
            byte[] bytes = new byte[4];

            try
            {
                switch (format)
                {
                    case TimeFormat.HoursMinutesSeconds:
                        bytes[0] = (byte)time.Hours;
                        bytes[1] = (byte)time.Minutes;
                        bytes[2] = (byte)time.Seconds;
                        bytes[3] = 0;
                        break;

                    case TimeFormat.HoursMinutes:
                        bytes[0] = (byte)time.Hours;
                        bytes[1] = (byte)time.Minutes;
                        bytes[2] = 0;
                        bytes[3] = 0;
                        break;

                    case TimeFormat.MinutesSeconds:
                        bytes[0] = (byte)time.Minutes;
                        bytes[1] = (byte)time.Seconds;
                        bytes[2] = 0;
                        bytes[3] = 0;
                        break;
                }

                return bytes;
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        /// <summary>
        /// Converts DateTime to byte array (full date and time)
        /// </summary>
        public static byte[] ToByteArray(System.DateTime dateTime,
                                        DateFormat dateFormat = DateFormat.DayMonthYear,
                                        TimeFormat timeFormat = TimeFormat.HoursMinutesSeconds,
                                        int yearBase = 2000)
        {
            try
            {
                byte[] dateBytes = ToDateByteArray(dateTime, dateFormat, yearBase);
                byte[] timeBytes = ToTimeByteArray(dateTime.TimeOfDay, timeFormat);

                if (dateBytes.Length == 0 || timeBytes.Length == 0)
                {
                    return Array.Empty<byte>();
                }

                byte[] result = new byte[8];
                Array.Copy(dateBytes, 0, result, 0, 4);
                Array.Copy(timeBytes, 0, result, 4, 4);

                return result;
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        /// <summary>
        /// Converts DateTime to PLC-style byte array with year rule
        /// </summary>
        public static byte[] ToByteArrayPlc(System.DateTime dateTime)
        {
            try
            {
                byte[] result = new byte[8];

            // Process the date part.
                result[0] = (byte)dateTime.Day;
                result[1] = (byte)dateTime.Month;
                result[2] = FullYearToPlcYear(dateTime.Year);
                result[3] = 0;

            // Process the time part.
                result[4] = (byte)dateTime.Hour;
                result[5] = (byte)dateTime.Minute;
                result[6] = (byte)dateTime.Second;
                result[7] = 0;

                return result;
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        /// <summary>
        /// Converts DateTime to BCD byte array
        /// </summary>
        public static byte[] ToByteArrayBcd(System.DateTime dateTime)
        {
            try
            {
                byte[] bytes = new byte[8];

                bytes[0] = ByteToBcd((byte)dateTime.Day);
                bytes[1] = ByteToBcd((byte)dateTime.Month);
                bytes[2] = ByteToBcd((byte)(dateTime.Year - 2000));
                bytes[3] = 0;
                bytes[4] = ByteToBcd((byte)dateTime.Hour);
                bytes[5] = ByteToBcd((byte)dateTime.Minute);
                bytes[6] = ByteToBcd((byte)dateTime.Second);
                bytes[7] = 0;

                return bytes;
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        #endregion

        #region Unix Timestamp

        /// <summary>
        /// Converts Unix timestamp (seconds since 1970-01-01) to DateTime
        /// </summary>
        /// <param name="timestamp">Unix timestamp as 32-bit integer</param>
        /// <returns>DateTime value</returns>
        public static System.DateTime FromUnixTime(uint timestamp)
        {
            try
            {
                return System.DateTimeOffset.FromUnixTimeSeconds(timestamp).LocalDateTime;
            }
            catch
            {
                return System.DateTime.MinValue;
            }
        }

        /// <summary>
        /// Converts Unix timestamp from byte array
        /// </summary>
        public static System.DateTime FromUnixTime(byte[]? bytes, Endian.Endianness endianness = Endian.Endianness.BigEndian)
        {
            if (bytes == null || bytes.Length < 4)
            {
                return System.DateTime.MinValue;
            }

            try
            {
                uint timestamp = UInt32.FromByteArray(bytes, endianness);
                return FromUnixTime(timestamp);
            }
            catch
            {
                return System.DateTime.MinValue;
            }
        }

        /// <summary>
        /// Converts DateTime to Unix timestamp (seconds since 1970-01-01)
        /// </summary>
        public static uint ToUnixTime(System.DateTime dateTime)
        {
            try
            {
                return (uint)((dateTime.ToUniversalTime() - new System.DateTime(UnixEpochYear, 1, 1, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Converts DateTime to Unix timestamp byte array
        /// </summary>
        public static byte[] ToUnixTimeByteArray(System.DateTime dateTime, Endian.Endianness endianness = Endian.Endianness.BigEndian)
        {
            uint timestamp = ToUnixTime(dateTime);
            return UInt32.ToByteArray(timestamp, endianness);
        }

        #endregion

        #region BCD Helpers

        /// <summary>
        /// Converts BCD byte to integer (e.g. 0x12 -> 12)
        /// </summary>
        private static int BcdToByte(byte bcd)
        {
            return (bcd >> 4) * 10 + (bcd & 0x0F);
        }

        /// <summary>
        /// Converts integer to BCD byte (e.g. 12 -> 0x12)
        /// </summary>
        private static byte ByteToBcd(byte value)
        {
            return (byte)(((value / 10) << 4) | (value % 10));
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Checks if byte array contains valid date
        /// </summary>
        public static bool IsValidDate(byte[]? bytes, DateFormat format = DateFormat.DayMonthYear, int yearBase = 2000)
        {
            return FromDateByteArray(bytes, format, yearBase) != System.DateTime.MinValue;
        }

        /// <summary>
        /// Checks if byte array contains valid date using PLC year rule
        /// </summary>
        public static bool IsValidDatePlc(byte[]? bytes, DateFormat format = DateFormat.DayMonthYear)
        {
            return FromDateByteArrayPlc(bytes, format) != System.DateTime.MinValue;
        }

        /// <summary>
        /// Checks if byte array contains valid time
        /// </summary>
        public static bool IsValidTime(byte[]? bytes, TimeFormat format = TimeFormat.HoursMinutesSeconds)
        {
            return FromTimeByteArray(bytes, format) != TimeSpan.Zero;
        }

        /// <summary>
        /// Gets current date and time as byte array in specified format
        /// </summary>
        public static byte[] GetNow(DateFormat dateFormat = DateFormat.DayMonthYear,
                                   TimeFormat timeFormat = TimeFormat.HoursMinutesSeconds,
                                   int yearBase = 2000)
        {
            return ToByteArray(System.DateTime.Now, dateFormat, timeFormat, yearBase);
        }

        /// <summary>
        /// Gets current date and time as PLC-style byte array
        /// </summary>
        public static byte[] GetNowPlc()
        {
            return ToByteArrayPlc(System.DateTime.Now);
        }

        /// <summary>
        /// Gets current date and time as BCD byte array
        /// </summary>
        public static byte[] GetNowBcd()
        {
            return ToByteArrayBcd(System.DateTime.Now);
        }

        #endregion
    }
}
