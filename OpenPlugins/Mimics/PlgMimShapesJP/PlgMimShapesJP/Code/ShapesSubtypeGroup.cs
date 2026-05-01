using Scada.Web.Plugins.PlgMimic.Components;

namespace Scada.Web.Plugins.PlgMimShapesJP.Code
{
    /// <summary>
    /// Represents a group of shape subtypes for the mimic editor.
    /// <para>Представляет группу подтипов фигур для редактора мнемосхем.</para>
    /// </summary>
    public class ShapesSubtypeGroup : SubtypeGroup
    {
        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public ShapesSubtypeGroup()
        {
            DictionaryPrefix = PluginConst.ComponentModelPrefix;

            EnumNames.AddRange([
                "ShapePolygonPointMode",
                "ShapeLineOrientation",
                "ShapeArrowDirection"
            ]);
        }

        #endregion Basic
    }
}
