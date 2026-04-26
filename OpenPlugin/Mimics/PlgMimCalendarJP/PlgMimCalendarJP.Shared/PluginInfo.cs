using Scada.Lang;

namespace Scada.Web.Plugins.PlgMimCalendarJP
{
    /// <summary>
    /// Represents information about a plugin.
    /// <para>Представляет информацию о плагине.</para>
    /// </summary>
    internal class PluginInfo : LibraryInfo
    {
        public override string Code => "PlgMimCalendarJP";

        public override string Name => Locale.IsRussian
            ? "Календарь мнемосхем"
            : "Mimic Calendar";

        public override string Descr => Locale.IsRussian
            ? "Календарные компоненты мнемосхем с передачей даты и времени в каналы."
            : "Calendar mimic components that send date and time to channels.";
    }
}
