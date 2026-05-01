using Scada.Web.Plugins.PlgMimic.Components;

namespace Scada.Web.Plugins.PlgMimCalendarJP.Code
{
    /// <summary>
    /// Represents a group of calendar components in the mimic editor toolbox.
    /// <para>Представляет группу компонентов календаря на панели инструментов редактора мнемосхем.</para>
    /// </summary>
    public class CalendarComponentGroup : ComponentGroup
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public CalendarComponentGroup()
        {
            Name = PluginPhrases.CalendarGroup;
            DictionaryPrefix = PluginConst.ComponentModelPrefix;
            const string IconPath = "~/plugins/MimCalendarJP/images/";

            Items.Add(new ComponentItem
            {
                IconUrl = IconPath + "calendar.png",
                DisplayName = PluginPhrases.CalendarAutoComponent,
                TypeName = "CalendarAuto"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = IconPath + "calendar.png",
                DisplayName = PluginPhrases.CalendarInputComponent,
                TypeName = "CalendarInput"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = IconPath + "calendar.png",
                DisplayName = PluginPhrases.CalendarButtonComponent,
                TypeName = "CalendarButton"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = IconPath + "calendar.png",
                DisplayName = PluginPhrases.CalendarRangeComponent,
                TypeName = "CalendarRange"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = IconPath + "calendar.png",
                DisplayName = PluginPhrases.CalendarRangeBottomComponent,
                TypeName = "CalendarRangeBottom"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = IconPath + "calendar.png",
                DisplayName = PluginPhrases.CalendarRangeSideComponent,
                TypeName = "CalendarRangeSide"
            });

            Items.Sort();
        }
    }
}
