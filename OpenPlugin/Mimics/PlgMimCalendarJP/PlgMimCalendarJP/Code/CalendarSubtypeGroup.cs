using Scada.Web.Plugins.PlgMimic.Components;

namespace Scada.Web.Plugins.PlgMimCalendarJP.Code
{
    /// <summary>
    /// Registers subtype groups for the calendar plugin.
    /// <para>Регистрирует группы подтипов для плагина календаря.</para>
    /// </summary>
    public class CalendarSubtypeGroup : SubtypeGroup
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public CalendarSubtypeGroup()
        {
            DictionaryPrefix = PluginConst.ComponentModelPrefix;
            EnumNames.Add("CalendarCommandFormat");
        }
    }
}
