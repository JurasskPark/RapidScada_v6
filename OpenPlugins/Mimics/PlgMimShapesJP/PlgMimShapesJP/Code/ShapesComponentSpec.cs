using Scada.Web.Plugins.PlgMimic.Components;

namespace Scada.Web.Plugins.PlgMimShapesJP.Code
{
    /// <summary>
    /// Provides shape component groups and client resources.
    /// <para>Предоставляет группы компонентов фигур и клиентские ресурсы.</para>
    /// </summary>
    public class ShapesComponentSpec : IComponentSpec
    {
        #region Property

        /// <summary>
        /// Gets the component groups.
        /// <para>Возвращает группы компонентов.</para>
        /// </summary>
        public List<ComponentGroup> ComponentGroups => [new ShapesComponentGroup()];

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
            "~/plugins/MimShapesJP/css/shapes.min.css"
        ];

        /// <summary>
        /// Gets the script URLs.
        /// <para>Возвращает URL-адреса скриптов.</para>
        /// </summary>
        public List<string> ScriptUrls =>
        [
            // Load the bundle first (for compatibility), then override with separate scripts.
            // This order ensures our latest descriptors, factories and renderers win.
            "~/plugins/MimShapesJP/js/shapes-bundle.js",
            "~/plugins/MimShapesJP/js/shapes-subtypes.js",
            "~/plugins/MimShapesJP/js/shapes-descr.js",
            "~/plugins/MimShapesJP/js/shapes-factory.js",
            "~/plugins/MimShapesJP/js/shapes-render.js"
        ];

        #endregion Property
    }
}
