using Scada.Web.Plugins.PlgMimic.Components;

namespace Scada.Web.Plugins.PlgMimShapesJP.Code
{
    /// <summary>
    /// Represents a group of shape components in the mimic editor toolbox.
    /// <para>Представляет группу компонентов фигур в панели инструментов редактора мнемосхем.</para>
    /// </summary>
    public class ShapesComponentGroup : ComponentGroup
    {
        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public ShapesComponentGroup()
            : this(false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the class and optionally exposes the polyline component.
        /// <para>Инициализирует новый экземпляр класса и при необходимости добавляет компонент ломаной линии.</para>
        /// </summary>
        public ShapesComponentGroup(bool includePolyline)
        {
            Name = PluginPhrases.ShapesGroup;
            DictionaryPrefix = PluginConst.ComponentModelPrefix;
            const string IconPath = "~/plugins/MimShapesJP/images/";

            Items.Add(new ComponentItem
            {
                IconUrl = ShapesComponentSpec.Versioned(IconPath + "rectangle-icon.svg"),
                DisplayName = PluginPhrases.RectangleComponent,
                TypeName = "ShapeRectangle"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = ShapesComponentSpec.Versioned(IconPath + "square-icon.svg"),
                DisplayName = PluginPhrases.SquareComponent,
                TypeName = "ShapeSquare"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = ShapesComponentSpec.Versioned(IconPath + "ellipse-icon.svg"),
                DisplayName = PluginPhrases.EllipseComponent,
                TypeName = "ShapeEllipse"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = ShapesComponentSpec.Versioned(IconPath + "circle-icon.svg"),
                DisplayName = PluginPhrases.CircleComponent,
                TypeName = "ShapeCircle"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = ShapesComponentSpec.Versioned(IconPath + "rounded-rect-icon.svg"),
                DisplayName = PluginPhrases.RoundedRectComponent,
                TypeName = "ShapeRoundedRect"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = ShapesComponentSpec.Versioned(IconPath + "polygon-icon.svg"),
                DisplayName = PluginPhrases.PolygonComponent,
                TypeName = "ShapePolygon"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = ShapesComponentSpec.Versioned(IconPath + "triangle-icon.svg"),
                DisplayName = PluginPhrases.TriangleComponent,
                TypeName = "ShapeTriangle"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = ShapesComponentSpec.Versioned(IconPath + "diamond-icon.svg"),
                DisplayName = PluginPhrases.DiamondComponent,
                TypeName = "ShapeDiamond"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = ShapesComponentSpec.Versioned(IconPath + "hexagon-icon.svg"),
                DisplayName = PluginPhrases.HexagonComponent,
                TypeName = "ShapeHexagon"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = ShapesComponentSpec.Versioned(IconPath + "parallelogram-icon.svg"),
                DisplayName = PluginPhrases.ParallelogramComponent,
                TypeName = "ShapeParallelogram"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = ShapesComponentSpec.Versioned(IconPath + "trapezoid-icon.svg"),
                DisplayName = PluginPhrases.TrapezoidComponent,
                TypeName = "ShapeTrapezoid"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = ShapesComponentSpec.Versioned(IconPath + "cross-icon.svg"),
                DisplayName = PluginPhrases.CrossComponent,
                TypeName = "ShapeCross"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = ShapesComponentSpec.Versioned(IconPath + "half-circle-icon.svg"),
                DisplayName = PluginPhrases.HalfCircleComponent,
                TypeName = "ShapeHalfCircle"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = ShapesComponentSpec.Versioned(IconPath + "donut-icon.svg"),
                DisplayName = PluginPhrases.DonutComponent,
                TypeName = "ShapeDonut"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = ShapesComponentSpec.Versioned(IconPath + "pie-icon.svg"),
                DisplayName = PluginPhrases.PieComponent,
                TypeName = "ShapePie"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = ShapesComponentSpec.Versioned(IconPath + "arrow-icon.svg"),
                DisplayName = PluginPhrases.ArrowComponent,
                TypeName = "ShapeArrow"
            });

            Items.Add(new ComponentItem
            {
                IconUrl = ShapesComponentSpec.Versioned(IconPath + "line-icon.svg"),
                DisplayName = PluginPhrases.LineComponent,
                TypeName = "ShapeLine"
            });

            if (includePolyline)
            {
                Items.Add(new ComponentItem
                {
                    IconUrl = ShapesComponentSpec.Versioned(IconPath + "polyline-icon.svg"),
                    DisplayName = PluginPhrases.PolylineComponent,
                    TypeName = "ShapePolyline"
                });
            }

            Items.Sort();
        }

        #endregion Basic
    }
}
