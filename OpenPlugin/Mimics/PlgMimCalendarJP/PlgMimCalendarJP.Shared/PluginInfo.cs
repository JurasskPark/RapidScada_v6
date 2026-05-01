using Scada.Lang;

namespace Scada.Web.Plugins.PlgMimCalendarJP
{
    /// <summary>
    /// Represents information about a plugin.
    /// <para>Представляет информацию о плагине.</para>
    /// </summary>
    internal class PluginInfo : LibraryInfo
    {
        /// <summary>
        /// Gets the plugin code.
        /// <para>Возвращает код плагина.</para>
        /// </summary>
        public override string Code => "PlgMimCalendarJP";

        /// <summary>
        /// Gets the plugin name.
        /// <para>Возвращает название плагина.</para>
        /// </summary>
        public override string Name => Locale.IsRussian
            ? "Календарь мнемосхем"
            : "Mimic Calendar";

        /// <summary>
        /// Gets the plugin description.
        /// <para>Возвращает описание плагина.</para>
        /// </summary>
        public override string Descr => Locale.IsRussian
            ? "Календарные компоненты мнемосхем с передачей даты и времени в каналы."
            : "Calendar mimic components that send date and time to channels.";
    }
}
