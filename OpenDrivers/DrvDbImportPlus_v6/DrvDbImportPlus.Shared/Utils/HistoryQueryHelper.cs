using Scada.Lang;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    /// <summary>
    /// Builds historical SQL windows from comments and datetime placeholders.
    /// <para>Формирует окна исторического SQL-запроса по комментариям и шаблонам даты.</para>
    /// </summary>
    public static class HistoryQueryHelper
    {
        public const string HistoryStartKey = "HISTORY_START";
        public const string HistoryEndKey = "HISTORY_END";
        public const string WindowStartPlaceholder = "{WINDOW_START}";
        public const string WindowEndPlaceholder = "{WINDOW_END}";

        private const string SqlTimestampFormat = "yyyy-MM-dd HH:mm:ss.ffffff";
        private static readonly string[] DateTimeFormats =
        {
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-MM-dd HH:mm:ss.fff",
            "yyyy-MM-dd HH:mm:ss.ffffff",
            "yyyy-MM-ddTHH:mm:ss",
            "yyyy-MM-ddTHH:mm:ss.fff",
            "yyyy-MM-ddTHH:mm:ss.ffffff"
        };

        /// <summary>
        /// Parses HISTORY_START and HISTORY_END comments from SQL.
        /// </summary>
        /// <summary>
        /// Checks that SQL contains both HISTORY_START and HISTORY_END comments.
        /// </summary>
        public static bool HasPeriodComments(string sql)
        {
            return TryGetCommentValue(sql, HistoryStartKey, out _) &&
                TryGetCommentValue(sql, HistoryEndKey, out _);
        }
        public static bool TryParsePeriod(string sql, out DateTime startTime, out DateTime endTime, out string errMsg)
        {
            startTime = DateTime.MinValue;
            endTime = DateTime.MinValue;
            errMsg = string.Empty;

            if (!TryGetCommentValue(sql, HistoryStartKey, out string startText) ||
                !TryGetCommentValue(sql, HistoryEndKey, out string endText))
            {
                errMsg = Locale.IsRussian ?
                    "Для исторического импорта укажите комментарии HISTORY_START и HISTORY_END." :
                    "Specify HISTORY_START and HISTORY_END comments for historical import.";
                return false;
            }

            if (!TryParseDateTime(startText, out startTime))
            {
                errMsg = string.Format(Locale.IsRussian ?
                    "Не удалось разобрать HISTORY_START: {0}" :
                    "Unable to parse HISTORY_START: {0}", startText);
                return false;
            }

            if (!TryParseDateTime(endText, out endTime))
            {
                errMsg = string.Format(Locale.IsRussian ?
                    "Не удалось разобрать HISTORY_END: {0}" :
                    "Unable to parse HISTORY_END: {0}", endText);
                return false;
            }

            if (endTime <= startTime)
            {
                errMsg = Locale.IsRussian ?
                    "HISTORY_END должен быть больше HISTORY_START." :
                    "HISTORY_END must be greater than HISTORY_START.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Creates half-open windows [StartTime, EndTime) that do not cross day boundaries.
        /// </summary>
        public static List<HistoryQueryWindow> BuildWindows(DateTime startTime, DateTime endTime, int windowMinutes)
        {
            List<HistoryQueryWindow> windows = new List<HistoryQueryWindow>();

            if (endTime <= startTime)
            {
                return windows;
            }

            int effectiveMinutes = windowMinutes > 0 ? windowMinutes : 60;
            TimeSpan windowSize = TimeSpan.FromMinutes(effectiveMinutes);
            DateTime windowStart = startTime;

            while (windowStart < endTime)
            {
                DateTime nextBySize = windowStart.Add(windowSize);
                DateTime nextDay = windowStart.Date.AddDays(1);
                DateTime windowEnd = Min(nextBySize, nextDay, endTime);

                windows.Add(new HistoryQueryWindow(windowStart, windowEnd));
                windowStart = windowEnd;
            }

            return windows;
        }

        /// <summary>
        /// Replaces window placeholders and resolves date patterns against the window start.
        /// </summary>
        public static string RenderWindowQuery(string sql, HistoryQueryWindow window)
        {
            string query = sql ?? string.Empty;
            query = query.Replace(WindowStartPlaceholder, ToSqlLiteral(window.StartTime));
            query = query.Replace(WindowEndPlaceholder, ToSqlLiteral(window.EndTime));
            query = Regex.Replace(query, @"(?<![A-Za-z0-9_])WINDOW_START(?![A-Za-z0-9_])", ToSqlLiteral(window.StartTime));
            query = Regex.Replace(query, @"(?<![A-Za-z0-9_])WINDOW_END(?![A-Za-z0-9_])", ToSqlLiteral(window.EndTime));
            return DriverUtils.ResolveDateTimePatterns(query, window.StartTime);
        }

        public static string ToSqlLiteral(DateTime dateTime)
        {
            return "'" + dateTime.ToString(SqlTimestampFormat, CultureInfo.InvariantCulture) + "'";
        }

        private static bool TryGetCommentValue(string sql, string key, out string value)
        {
            Match match = Regex.Match(
                sql ?? string.Empty,
                @"^\s*--\s*" + Regex.Escape(key) + @"\s*=\s*(?<value>.+?)\s*$",
                RegexOptions.Multiline | RegexOptions.IgnoreCase);

            value = match.Success ? match.Groups["value"].Value.Trim() : string.Empty;
            return match.Success;
        }

        private static bool TryParseDateTime(string text, out DateTime dateTime)
        {
            return DateTime.TryParseExact(
                    text,
                    DateTimeFormats,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AllowWhiteSpaces,
                    out dateTime) ||
                DateTime.TryParse(
                    text,
                    CultureInfo.CurrentCulture,
                    DateTimeStyles.AllowWhiteSpaces,
                    out dateTime);
        }

        private static DateTime Min(DateTime value1, DateTime value2, DateTime value3)
        {
            DateTime min = value1 < value2 ? value1 : value2;
            return min < value3 ? min : value3;
        }
    }

    public readonly struct HistoryQueryWindow
    {
        public HistoryQueryWindow(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public DateTime StartTime { get; }

        public DateTime EndTime { get; }
    }
}




