using Scada.Lang;

namespace Scada.Web.Plugins.PlgMimShapesJP
{
    /// <summary>
    /// Provides information about the plugin.
    /// <para>Предоставляет информацию о плагине.</para>
    /// </summary>
    internal class PluginInfo : LibraryInfo
    {
        #region Property

        /// <summary>
        /// Gets the plugin code.
        /// <para>Возвращает код плагина.</para>
        /// </summary>
        public override string Code => "PlgMimShapesJP";

        /// <summary>
        /// Gets the plugin name.
        /// <para>Возвращает название плагина.</para>
        /// </summary>
        public override string Name => Locale.IsRussian
            ? "Фигуры мнемосхем"
            : "Mimic Shapes";

        /// <summary>
        /// Gets the plugin description.
        /// <para>Возвращает описание плагина.</para>
        /// </summary>
        public override string Descr => Locale.IsRussian
            ? "Компоненты мнемосхем для отображения геометрических примитивов."
            : "Mimic components for displaying geometric primitives.";

        #endregion Property
    }
}
