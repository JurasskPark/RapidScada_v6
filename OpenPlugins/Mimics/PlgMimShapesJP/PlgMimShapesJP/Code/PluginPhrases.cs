using Scada.Lang;

namespace Scada.Web.Plugins.PlgMimShapesJP.Code
{
    /// <summary>
    /// Provides localized phrases for the plugin.
    /// <para>Предоставляет локализованные фразы для плагина.</para>
    /// </summary>
    internal class PluginPhrases
    {
        #region Variable

        private static string shapesGroup;                          // shapes group name
        private static string rectangleComponent;                   // rectangle component name
        private static string squareComponent;                      // square component name
        private static string ellipseComponent;                     // ellipse component name
        private static string circleComponent;                      // circle component name
        private static string roundedRectComponent;                 // rounded rectangle component name
        private static string polygonComponent;                     // polygon component name
        private static string triangleComponent;                    // triangle component name
        private static string diamondComponent;                     // diamond component name
        private static string hexagonComponent;                     // hexagon component name
        private static string parallelogramComponent;               // parallelogram component name
        private static string trapezoidComponent;                   // trapezoid component name
        private static string crossComponent;                       // cross component name
        private static string halfCircleComponent;                  // half circle component name
        private static string donutComponent;                       // donut component name
        private static string pieComponent;                         // pie component name
        private static string starComponent;                        // star component name
        private static string arrowComponent;                       // arrow component name
        private static string lineComponent;                        // line component name
        private static string polylineComponent;                    // polyline component name

        #endregion Variable

        #region Property

        /// <summary>
        /// Gets the shapes group name.
        /// <para>Возвращает название группы фигур.</para>
        /// </summary>
        public static string ShapesGroup => shapesGroup;

        /// <summary>
        /// Gets the rectangle component name.
        /// <para>Возвращает название компонента прямоугольника.</para>
        /// </summary>
        public static string RectangleComponent => rectangleComponent;

        /// <summary>
        /// Gets the square component name.
        /// <para>Возвращает название компонента квадрата.</para>
        /// </summary>
        public static string SquareComponent => squareComponent;

        /// <summary>
        /// Gets the ellipse component name.
        /// <para>Возвращает название компонента эллипса.</para>
        /// </summary>
        public static string EllipseComponent => ellipseComponent;

        /// <summary>
        /// Gets the circle component name.
        /// <para>Возвращает название компонента круга.</para>
        /// </summary>
        public static string CircleComponent => circleComponent;

        /// <summary>
        /// Gets the rounded rectangle component name.
        /// <para>Возвращает название компонента скругленного прямоугольника.</para>
        /// </summary>
        public static string RoundedRectComponent => roundedRectComponent;

        /// <summary>
        /// Gets the polygon component name.
        /// <para>Возвращает название компонента многоугольника.</para>
        /// </summary>
        public static string PolygonComponent => polygonComponent;

        /// <summary>
        /// Gets the triangle component name.
        /// <para>Возвращает название компонента треугольника.</para>
        /// </summary>
        public static string TriangleComponent => triangleComponent;

        /// <summary>
        /// Gets the diamond component name.
        /// <para>Возвращает название компонента ромба.</para>
        /// </summary>
        public static string DiamondComponent => diamondComponent;

        /// <summary>
        /// Gets the hexagon component name.
        /// <para>Возвращает название компонента шестиугольника.</para>
        /// </summary>
        public static string HexagonComponent => hexagonComponent;

        /// <summary>
        /// Gets the parallelogram component name.
        /// <para>Возвращает название компонента параллелограмма.</para>
        /// </summary>
        public static string ParallelogramComponent => parallelogramComponent;

        /// <summary>
        /// Gets the trapezoid component name.
        /// <para>Возвращает название компонента трапеции.</para>
        /// </summary>
        public static string TrapezoidComponent => trapezoidComponent;

        /// <summary>
        /// Gets the cross component name.
        /// <para>Возвращает название компонента креста.</para>
        /// </summary>
        public static string CrossComponent => crossComponent;

        /// <summary>
        /// Gets the half circle component name.
        /// <para>Возвращает название компонента полукруга.</para>
        /// </summary>
        public static string HalfCircleComponent => halfCircleComponent;

        /// <summary>
        /// Gets the donut component name.
        /// <para>Возвращает название компонента пончика.</para>
        /// </summary>
        public static string DonutComponent => donutComponent;

        /// <summary>
        /// Gets the pie component name.
        /// <para>Возвращает название компонента сектора.</para>
        /// </summary>
        public static string PieComponent => pieComponent;

        /// <summary>
        /// Gets the star component name.
        /// <para>Возвращает название компонента звезды.</para>
        /// </summary>
        public static string StarComponent => starComponent;

        /// <summary>
        /// Gets the arrow component name.
        /// <para>Возвращает название компонента стрелки.</para>
        /// </summary>
        public static string ArrowComponent => arrowComponent;

        /// <summary>
        /// Gets the line component name.
        /// <para>Возвращает название компонента линии.</para>
        /// </summary>
        public static string LineComponent => lineComponent;

        /// <summary>
        /// Gets the polyline component name.
        /// <para>Возвращает название компонента полилинии.</para>
        /// </summary>
        public static string PolylineComponent => polylineComponent;

        #endregion Property

        #region Basic

        /// <summary>
        /// Initializes the phrases from the locale dictionary.
        /// <para>Инициализирует фразы из словаря локализации.</para>
        /// </summary>
        public static void Init()
        {
            LocaleDict dict = Locale.GetDictionary("Scada.Web.Plugins.PlgMimShapesJP.Code.ShapesComponentGroup");
            shapesGroup = dict[nameof(ShapesGroup)];
            rectangleComponent = dict[nameof(RectangleComponent)];
            squareComponent = dict[nameof(SquareComponent)];
            ellipseComponent = dict[nameof(EllipseComponent)];
            circleComponent = dict[nameof(CircleComponent)];
            roundedRectComponent = dict[nameof(RoundedRectComponent)];
            polygonComponent = dict[nameof(PolygonComponent)];
            triangleComponent = dict[nameof(TriangleComponent)];
            diamondComponent = dict[nameof(DiamondComponent)];
            hexagonComponent = dict[nameof(HexagonComponent)];
            parallelogramComponent = dict[nameof(ParallelogramComponent)];
            trapezoidComponent = dict[nameof(TrapezoidComponent)];
            crossComponent = dict[nameof(CrossComponent)];
            halfCircleComponent = dict[nameof(HalfCircleComponent)];
            donutComponent = dict[nameof(DonutComponent)];
            pieComponent = dict[nameof(PieComponent)];
            starComponent = dict[nameof(StarComponent)];
            arrowComponent = dict[nameof(ArrowComponent)];
            lineComponent = dict[nameof(LineComponent)];
            polylineComponent = dict[nameof(PolylineComponent)];
        }

        #endregion Basic
    }
}
