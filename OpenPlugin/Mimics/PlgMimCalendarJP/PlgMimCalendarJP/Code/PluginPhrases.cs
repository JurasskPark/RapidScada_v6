using Scada.Lang;

namespace Scada.Web.Plugins.PlgMimCalendarJP.Code
{
    internal class PluginPhrases
    {
        public static string CalendarGroup { get; private set; }
        public static string CalendarAutoComponent { get; private set; }
        public static string CalendarButtonComponent { get; private set; }
        public static string CalendarRangeComponent { get; private set; }
        public static string CalendarRangeBottomComponent { get; private set; }
        public static string CalendarRangeSideComponent { get; private set; }

        public static void Init()
        {
            LocaleDict dict = Locale.GetDictionary("Scada.Web.Plugins.PlgMimCalendarJP.Code.CalendarComponentGroup");
            CalendarGroup = dict[nameof(CalendarGroup)];
            CalendarAutoComponent = dict[nameof(CalendarAutoComponent)];
            CalendarButtonComponent = dict[nameof(CalendarButtonComponent)];
            CalendarRangeComponent = dict[nameof(CalendarRangeComponent)];
            CalendarRangeBottomComponent = dict[nameof(CalendarRangeBottomComponent)];
            CalendarRangeSideComponent = dict[nameof(CalendarRangeSideComponent)];
        }
    }
}
