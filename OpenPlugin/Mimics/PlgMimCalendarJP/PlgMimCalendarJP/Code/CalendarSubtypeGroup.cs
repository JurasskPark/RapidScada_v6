using Scada.Web.Plugins.PlgMimic.Components;

namespace Scada.Web.Plugins.PlgMimCalendarJP.Code
{
    public class CalendarSubtypeGroup : SubtypeGroup
    {
        public CalendarSubtypeGroup()
        {
            DictionaryPrefix = PluginConst.ComponentModelPrefix;
            EnumNames.Add("CalendarCommandFormat");
        }
    }
}
