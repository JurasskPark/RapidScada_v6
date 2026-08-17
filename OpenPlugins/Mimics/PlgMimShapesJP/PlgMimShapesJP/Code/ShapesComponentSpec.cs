using Scada.Web.Plugins.PlgMimic.Components;

namespace Scada.Web.Plugins.PlgMimShapesJP.Code
{
    /// <summary>
    /// Provides shape component groups and client resources.
    /// <para>Предоставляет группы компонентов фигур и клиентские ресурсы.</para>
    /// </summary>
    public class ShapesComponentSpec : IComponentSpec
    {
        #region Variable

        private const string PolylineEditorCode = "PlgMimicJP"; // editor that supports polyline point editing

        private bool polylineEnabled; // indicates whether the toolbox exposes the polyline component

        #endregion Variable

        #region Property

        /// <summary>
        /// Gets the component groups.
        /// <para>Возвращает группы компонентов.</para>
        /// </summary>
        public List<ComponentGroup> ComponentGroups => [new ShapesComponentGroup(polylineEnabled)];

        /// <summary>
        /// Gets the subtype groups.
        /// <para>Возвращает группы подтипов.</para>
        /// </summary>
        public List<SubtypeGroup> SubtypeGroups => [new ShapesSubtypeGroup()];

        /// <summary>
        /// Gets the style URLs.
        /// <para>Возвращает URL-адреса стилей.</para>
        /// </summary>
        public List<string> StyleUrls =>
        [
            Versioned("~/plugins/MimShapesJP/css/shapes.min.css")
        ];

        /// <summary>
        /// Gets the script URLs.
        /// <para>Возвращает URL-адреса скриптов.</para>
        /// </summary>
        public List<string> ScriptUrls =>
        [
            Versioned("~/plugins/MimShapesJP/js/shapes-bundle.js"),
            Versioned("~/plugins/MimShapesJP/js/shapes-lang.js")
        ];

        #endregion Property

        #region Basic

        /// <summary>
        /// Configures editor-specific component capabilities.
        /// <para>Настраивает возможности компонентов для указанного редактора.</para>
        /// </summary>
        public void ConfigureEditor(string editorCode)
        {
            polylineEnabled = string.Equals(editorCode, PolylineEditorCode, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Appends the current resource version to a browser asset URL.
        /// <para>Добавляет текущую версию ресурсов к URL-адресу браузерного ресурса.</para>
        /// </summary>
        internal static string Versioned(string url)
        {
            return $"{url}?v={PluginConst.ResourceVersion}";
        }

        #endregion Basic
    }
}
