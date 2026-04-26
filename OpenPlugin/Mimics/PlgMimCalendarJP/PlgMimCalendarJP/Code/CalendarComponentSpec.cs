using Scada.Web.Plugins.PlgMimic.Components;

namespace Scada.Web.Plugins.PlgMimCalendarJP.Code
{
    /// <summary>
    /// Provides calendar component groups and client resources.
    /// <para>Предоставляет группы компонентов календаря и клиентские ресурсы.</para>
    /// </summary>
    public class CalendarComponentSpec : IComponentSpec
    {
        /// <summary>
        /// Gets the component groups.
        /// </summary>
        public List<ComponentGroup> ComponentGroups => [new CalendarComponentGroup()];

        /// <summary>
        /// Gets the subtype groups.
        /// </summary>
        public List<SubtypeGroup> SubtypeGroups => [new CalendarSubtypeGroup()];

        /// <summary>
        /// Gets the component style URLs.
        /// </summary>
        public List<string> StyleUrls => [
            "~/plugins/MimCalendarJP/css/calendar.min.css"
        ];

        /// <summary>
        /// Gets the component script URLs.
        /// </summary>
        public List<string> ScriptUrls => [
            "~/plugins/MimCalendarJP/js/calendar-bundle.js"
        ];
    }
}
