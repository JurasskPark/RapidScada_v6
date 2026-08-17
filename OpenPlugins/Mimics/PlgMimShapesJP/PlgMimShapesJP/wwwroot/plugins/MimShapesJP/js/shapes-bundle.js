// Contains subtypes for shape components.

rs.mimic = rs.mimic ?? {};

rs.mimic.ShapeSubtype = class {
    static POLYGON_POINT_MODE = "ShapePolygonPointMode";
    static LINE_ORIENTATION = "ShapeLineOrientation";
    static ARROW_DIRECTION = "ShapeArrowDirection";
};

rs.mimic.ShapePolygonPointMode = class {
    static AUTO = "Auto";
    static CUSTOM = "Custom";
};

rs.mimic.ShapeLineOrientation = class {
    static DIAGONAL = "Diagonal";
    static HORIZONTAL = "Horizontal";
    static VERTICAL = "Vertical";
    static CUSTOM = "Custom";
};

rs.mimic.ShapeArrowDirection = class {
    static RIGHT = "Right";
    static LEFT = "Left";
    static UP = "Up";
    static DOWN = "Down";
};

// Contains descriptors for shape components.

// Adds the shared appearance properties used by filled shapes.
function configureShapeDescriptor(descriptor, allowPoints) {
    const KnownCategory = rs.mimic.KnownCategory;
    const BasicType = rs.mimic.BasicType;
    const PropertyEditor = rs.mimic.PropertyEditor;
    const PropertyDescriptor = rs.mimic.PropertyDescriptor;

    descriptor.add(new PropertyDescriptor({
        name: "fillColor",
        displayName: "Fill color",
        category: KnownCategory.APPEARANCE,
        type: BasicType.STRING,
        editor: PropertyEditor.COLOR_DIALOG
    }));

    descriptor.add(new PropertyDescriptor({
        name: "strokeColor",
        displayName: "Stroke color",
        category: KnownCategory.APPEARANCE,
        type: BasicType.STRING,
        editor: PropertyEditor.COLOR_DIALOG
    }));

    descriptor.add(new PropertyDescriptor({
        name: "strokeWidth",
        displayName: "Stroke width",
        category: KnownCategory.APPEARANCE,
        type: BasicType.INT
    }));

    descriptor.add(new PropertyDescriptor({
        name: "strokeDasharray",
        displayName: "Stroke dash",
        category: KnownCategory.APPEARANCE,
        type: BasicType.STRING
    }));

    descriptor.add(new PropertyDescriptor({
        name: "opacity",
        displayName: "Opacity",
        category: KnownCategory.APPEARANCE,
        type: BasicType.INT
    }));

    descriptor.add(new PropertyDescriptor({
        name: "rotation",
        displayName: "Rotation",
        category: KnownCategory.APPEARANCE,
        type: BasicType.INT
    }));

    descriptor.add(new PropertyDescriptor({
        name: "backgroundColor",
        displayName: "Background color",
        category: KnownCategory.APPEARANCE,
        type: BasicType.STRING,
        editor: PropertyEditor.COLOR_DIALOG
    }));

    descriptor.add(new PropertyDescriptor({
        name: "imageName",
        displayName: "Image",
        category: KnownCategory.APPEARANCE,
        type: BasicType.STRING,
        editor: PropertyEditor.IMAGE_DIALOG
    }));

    descriptor.add(new PropertyDescriptor({
        name: "imageOpacity",
        displayName: "Image opacity",
        category: KnownCategory.APPEARANCE,
        type: BasicType.INT
    }));

    if (allowPoints) {
        descriptor.add(new PropertyDescriptor({
            name: "points",
            displayName: "Points",
            category: KnownCategory.APPEARANCE,
            type: BasicType.STRING
        }));
    }
}

// Adds the shared appearance properties used by lines.
function configureLineDescriptor(descriptor, allowPoints) {
    const KnownCategory = rs.mimic.KnownCategory;
    const BasicType = rs.mimic.BasicType;
    const PropertyEditor = rs.mimic.PropertyEditor;
    const PropertyDescriptor = rs.mimic.PropertyDescriptor;

    descriptor.add(new PropertyDescriptor({
        name: "strokeColor",
        displayName: "Line color",
        category: KnownCategory.APPEARANCE,
        type: BasicType.STRING,
        editor: PropertyEditor.COLOR_DIALOG
    }));

    descriptor.add(new PropertyDescriptor({
        name: "strokeWidth",
        displayName: "Line width",
        category: KnownCategory.APPEARANCE,
        type: BasicType.INT
    }));

    descriptor.add(new PropertyDescriptor({
        name: "strokeDasharray",
        displayName: "Stroke dash",
        category: KnownCategory.APPEARANCE,
        type: BasicType.STRING
    }));

    descriptor.add(new PropertyDescriptor({
        name: "opacity",
        displayName: "Opacity",
        category: KnownCategory.APPEARANCE,
        type: BasicType.INT
    }));

    descriptor.add(new PropertyDescriptor({
        name: "rotation",
        displayName: "Rotation",
        category: KnownCategory.APPEARANCE,
        type: BasicType.INT
    }));

    if (allowPoints) {
        descriptor.add(new PropertyDescriptor({
            name: "points",
            displayName: "Points",
            category: KnownCategory.APPEARANCE,
            type: BasicType.STRING
        }));
    }
}

rs.mimic.ShapeRectangleDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    // Initializes the rectangle property descriptor.
    constructor() {
        super();
        configureShapeDescriptor(this, false);
    }
};

rs.mimic.ShapeSquareDescriptor = class extends rs.mimic.ShapeRectangleDescriptor {};
rs.mimic.ShapeEllipseDescriptor = class extends rs.mimic.ShapeRectangleDescriptor {};
rs.mimic.ShapeCircleDescriptor = class extends rs.mimic.ShapeRectangleDescriptor {};

rs.mimic.ShapeRoundedRectDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    // Initializes rounded-rectangle properties including the corner radius.
    constructor() {
        super();
        const KnownCategory = rs.mimic.KnownCategory;
        const BasicType = rs.mimic.BasicType;
        const PropertyDescriptor = rs.mimic.PropertyDescriptor;
        configureShapeDescriptor(this, false);

        this.add(new PropertyDescriptor({
            name: "borderRadius",
            displayName: "Border radius",
            category: KnownCategory.APPEARANCE,
            type: BasicType.INT
        }));
    }
};

rs.mimic.ShapePolygonDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    // Initializes polygon point mode and point-count properties.
    constructor() {
        super();
        const KnownCategory = rs.mimic.KnownCategory;
        const BasicType = rs.mimic.BasicType;
        const ShapeSubtype = rs.mimic.ShapeSubtype;
        const ShapePolygonPointMode = rs.mimic.ShapePolygonPointMode;
        const PropertyDescriptor = rs.mimic.PropertyDescriptor;
        configureShapeDescriptor(this, true);

        this.add(new PropertyDescriptor({
            name: "pointMode",
            displayName: "Point mode",
            category: KnownCategory.APPEARANCE,
            type: BasicType.ENUM,
            subtype: ShapeSubtype.POLYGON_POINT_MODE,
            tweakpaneOptions: {
                options: {
                    Auto: ShapePolygonPointMode.AUTO,
                    Custom: ShapePolygonPointMode.CUSTOM
                }
            }
        }));

        this.add(new PropertyDescriptor({
            name: "pointCount",
            displayName: "Point count",
            category: KnownCategory.APPEARANCE,
            type: BasicType.INT
        }));
    }
};

rs.mimic.ShapeTriangleDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    // Initializes the triangle property descriptor.
    constructor() {
        super();
        configureShapeDescriptor(this, false);
    }
};

rs.mimic.ShapeDiamondDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    // Initializes the diamond property descriptor.
    constructor() {
        super();
        configureShapeDescriptor(this, false);
    }
};

rs.mimic.ShapeHexagonDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    // Initializes the hexagon property descriptor.
    constructor() {
        super();
        configureShapeDescriptor(this, false);
    }
};

rs.mimic.ShapeParallelogramDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    // Initializes the parallelogram property descriptor.
    constructor() {
        super();
        configureShapeDescriptor(this, false);
    }
};

rs.mimic.ShapeTrapezoidDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    // Initializes the trapezoid property descriptor.
    constructor() {
        super();
        configureShapeDescriptor(this, false);
    }
};

rs.mimic.ShapeCrossDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    // Initializes the cross property descriptor.
    constructor() {
        super();
        configureShapeDescriptor(this, false);
    }
};

rs.mimic.ShapeHalfCircleDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    // Initializes the half-circle property descriptor.
    constructor() {
        super();
        configureShapeDescriptor(this, false);
    }
};

rs.mimic.ShapeDonutDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    // Initializes donut properties including the hole size.
    constructor() {
        super();
        const KnownCategory = rs.mimic.KnownCategory;
        const BasicType = rs.mimic.BasicType;
        const PropertyDescriptor = rs.mimic.PropertyDescriptor;
        configureShapeDescriptor(this, false);

        this.add(new PropertyDescriptor({
            name: "holeSize",
            displayName: "Hole size",
            category: KnownCategory.APPEARANCE,
            type: BasicType.INT
        }));
    }
};

rs.mimic.ShapePieDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    // Initializes the pie start and sweep angle properties.
    constructor() {
        super();
        const KnownCategory = rs.mimic.KnownCategory;
        const BasicType = rs.mimic.BasicType;
        const PropertyDescriptor = rs.mimic.PropertyDescriptor;
        configureShapeDescriptor(this, false);

        this.add(new PropertyDescriptor({
            name: "startAngle",
            displayName: "Start angle",
            category: KnownCategory.APPEARANCE,
            type: BasicType.INT
        }));

        this.add(new PropertyDescriptor({
            name: "sweepAngle",
            displayName: "Sweep angle",
            category: KnownCategory.APPEARANCE,
            type: BasicType.INT
        }));
    }
};

rs.mimic.ShapeArrowDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    // Initializes the arrow direction property.
    constructor() {
        super();
        const KnownCategory = rs.mimic.KnownCategory;
        const BasicType = rs.mimic.BasicType;
        const ShapeSubtype = rs.mimic.ShapeSubtype;
        const ShapeArrowDirection = rs.mimic.ShapeArrowDirection;
        const PropertyDescriptor = rs.mimic.PropertyDescriptor;
        configureShapeDescriptor(this, false);

        this.add(new PropertyDescriptor({
            name: "direction",
            displayName: "Direction",
            category: KnownCategory.APPEARANCE,
            type: BasicType.ENUM,
            subtype: ShapeSubtype.ARROW_DIRECTION,
            tweakpaneOptions: {
                options: {
                    Right: ShapeArrowDirection.RIGHT,
                    Left: ShapeArrowDirection.LEFT,
                    Up: ShapeArrowDirection.UP,
                    Down: ShapeArrowDirection.DOWN
                }
            }
        }));
    }
};

rs.mimic.ShapeLineDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    // Initializes line orientation and endpoint properties.
    constructor() {
        super();
        const KnownCategory = rs.mimic.KnownCategory;
        const BasicType = rs.mimic.BasicType;
        const ShapeSubtype = rs.mimic.ShapeSubtype;
        const ShapeLineOrientation = rs.mimic.ShapeLineOrientation;
        const PropertyDescriptor = rs.mimic.PropertyDescriptor;
        configureLineDescriptor(this, false);

        this.add(new PropertyDescriptor({
            name: "orientation",
            displayName: "Orientation",
            category: KnownCategory.APPEARANCE,
            type: BasicType.ENUM,
            subtype: ShapeSubtype.LINE_ORIENTATION,
            tweakpaneOptions: {
                options: {
                    Diagonal: ShapeLineOrientation.DIAGONAL,
                    Horizontal: ShapeLineOrientation.HORIZONTAL,
                    Vertical: ShapeLineOrientation.VERTICAL,
                    Custom: ShapeLineOrientation.CUSTOM
                }
            }
        }));

        this.add(new PropertyDescriptor({ name: "x1", displayName: "X1", category: KnownCategory.APPEARANCE, type: BasicType.INT }));
        this.add(new PropertyDescriptor({ name: "y1", displayName: "Y1", category: KnownCategory.APPEARANCE, type: BasicType.INT }));
        this.add(new PropertyDescriptor({ name: "x2", displayName: "X2", category: KnownCategory.APPEARANCE, type: BasicType.INT }));
        this.add(new PropertyDescriptor({ name: "y2", displayName: "Y2", category: KnownCategory.APPEARANCE, type: BasicType.INT }));
    }
};

rs.mimic.ShapePolylineDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    // Initializes polyline point and axis-snapping properties.
    constructor() {
        super();
        const KnownCategory = rs.mimic.KnownCategory;
        const BasicType = rs.mimic.BasicType;
        const PropertyDescriptor = rs.mimic.PropertyDescriptor;
        configureLineDescriptor(this, true);

        this.add(new PropertyDescriptor({
            name: "pointCount",
            displayName: "Point count",
            category: KnownCategory.APPEARANCE,
            type: BasicType.INT
        }));

        this.add(new PropertyDescriptor({
            name: "snapToAxis",
            displayName: "Snap to axis",
            category: KnownCategory.APPEARANCE,
            type: BasicType.BOOL
        }));

        this.add(new PropertyDescriptor({
            name: "snapThreshold",
            displayName: "Snap threshold",
            category: KnownCategory.APPEARANCE,
            type: BasicType.INT
        }));
    }
};

// Registers every shape descriptor with the shared mimic descriptor set.
function registerShapeDescriptors() {
    let componentDescriptors = rs.mimic.DescriptorSet.componentDescriptors;
    componentDescriptors.set("ShapeRectangle", new rs.mimic.ShapeRectangleDescriptor());
    componentDescriptors.set("ShapeSquare", new rs.mimic.ShapeSquareDescriptor());
    componentDescriptors.set("ShapeEllipse", new rs.mimic.ShapeEllipseDescriptor());
    componentDescriptors.set("ShapeCircle", new rs.mimic.ShapeCircleDescriptor());
    componentDescriptors.set("ShapeRoundedRect", new rs.mimic.ShapeRoundedRectDescriptor());
    componentDescriptors.set("ShapePolygon", new rs.mimic.ShapePolygonDescriptor());
    componentDescriptors.set("ShapeTriangle", new rs.mimic.ShapeTriangleDescriptor());
    componentDescriptors.set("ShapeDiamond", new rs.mimic.ShapeDiamondDescriptor());
    componentDescriptors.set("ShapeHexagon", new rs.mimic.ShapeHexagonDescriptor());
    componentDescriptors.set("ShapeParallelogram", new rs.mimic.ShapeParallelogramDescriptor());
    componentDescriptors.set("ShapeTrapezoid", new rs.mimic.ShapeTrapezoidDescriptor());
    componentDescriptors.set("ShapeCross", new rs.mimic.ShapeCrossDescriptor());
    componentDescriptors.set("ShapeHalfCircle", new rs.mimic.ShapeHalfCircleDescriptor());
    componentDescriptors.set("ShapeDonut", new rs.mimic.ShapeDonutDescriptor());
    componentDescriptors.set("ShapePie", new rs.mimic.ShapePieDescriptor());
    componentDescriptors.set("ShapeArrow", new rs.mimic.ShapeArrowDescriptor());
    componentDescriptors.set("ShapeLine", new rs.mimic.ShapeLineDescriptor());
    componentDescriptors.set("ShapePolyline", new rs.mimic.ShapePolylineDescriptor());
}

registerShapeDescriptors();

// Contains factories for shape components.

rs.mimic.ShapeFactoryBase = class extends rs.mimic.RegularComponentFactory {
    // Applies the shared initial values used by filled shapes.
    _setShapeDefaults(props) {
        props.size.width = 120;
        props.size.height = 80;
        props.backColor = "Transparent";
        props.border.width = 0;
        props.fillColor = "SteelBlue";
        props.strokeColor = "Black";
        props.strokeWidth = 1;
        props.strokeDasharray = "";
        props.backgroundColor = "";
        props.imageName = "";
        props.imageOpacity = 100;
        props.opacity = 100;
        props.rotation = 0;
    }

    // Parses and normalizes the shared persisted properties of filled shapes.
    _parseShapeProperties(props, sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        props.fillColor = PropertyParser.parseString(sourceProps.fillColor) || "SteelBlue";
        props.strokeColor = PropertyParser.parseString(sourceProps.strokeColor) || "Black";
        props.strokeWidth = PropertyParser.parseInt(sourceProps.strokeWidth);
        props.strokeDasharray = PropertyParser.parseString(sourceProps.strokeDasharray) || "";
        props.backgroundColor = PropertyParser.parseString(sourceProps.backgroundColor);
        props.imageName = PropertyParser.parseString(sourceProps.imageName);
        props.imageOpacity = PropertyParser.parseInt(sourceProps.imageOpacity);
        props.opacity = PropertyParser.parseInt(sourceProps.opacity);
        props.rotation = PropertyParser.parseInt(sourceProps.rotation);
        props.strokeWidth = this._clampInteger(props.strokeWidth, 0, 100, 1);
        props.imageOpacity = this._clampInteger(props.imageOpacity, 0, 100, 100);
        props.opacity = this._clampInteger(props.opacity, 0, 100, 100);

        if (!Number.isFinite(props.rotation)) {
            props.rotation = 0;
        }
    }

    // Clamps a parsed integer and substitutes a safe fallback for invalid input.
    _clampInteger(value, minimum, maximum, fallback) {
        return Number.isFinite(value)
            ? Math.max(minimum, Math.min(maximum, value))
            : fallback;
    }

    // Creates the base shape property set with plugin defaults.
    createProperties() {
        let props = super.createProperties();
        this._setShapeDefaults(props);
        return props;
    }

    // Parses a persisted base shape property set.
    parseProperties(sourceProps) {
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        this._parseShapeProperties(props, sourceProps);
        return props;
    }
};

rs.mimic.ShapeRectangleFactory = class extends rs.mimic.ShapeFactoryBase {
    // Creates a rectangle component.
    createComponent() {
        return super.createComponent("ShapeRectangle");
    }
};

rs.mimic.ShapeSquareFactory = class extends rs.mimic.ShapeFactoryBase {
    // Creates square-specific default dimensions.
    createProperties() {
        let props = super.createProperties();
        props.size.width = 80;
        props.size.height = 80;
        return props;
    }

    // Creates a square component.
    createComponent() {
        return super.createComponent("ShapeSquare");
    }
};

rs.mimic.ShapeEllipseFactory = class extends rs.mimic.ShapeFactoryBase {
    // Creates an ellipse component.
    createComponent() {
        return super.createComponent("ShapeEllipse");
    }
};

rs.mimic.ShapeCircleFactory = class extends rs.mimic.ShapeFactoryBase {
    // Creates circle-specific default dimensions.
    createProperties() {
        let props = super.createProperties();
        props.size.width = 80;
        props.size.height = 80;
        return props;
    }

    // Creates a circle component.
    createComponent() {
        return super.createComponent("ShapeCircle");
    }
};

rs.mimic.ShapeRoundedRectFactory = class extends rs.mimic.ShapeFactoryBase {
    // Creates rounded-rectangle defaults.
    createProperties() {
        let props = super.createProperties();
        props.borderRadius = 10;
        return props;
    }

    // Parses a non-negative corner radius.
    parseProperties(sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        props.borderRadius = PropertyParser.parseInt(sourceProps.borderRadius);
        props.borderRadius = Number.isFinite(props.borderRadius) ? Math.max(0, props.borderRadius) : 10;
        return props;
    }

    // Creates a rounded-rectangle component.
    createComponent() {
        return super.createComponent("ShapeRoundedRect");
    }
};

rs.mimic.ShapePolygonFactory = class extends rs.mimic.ShapeFactoryBase {
    // Creates the default five-point polygon.
    createProperties() {
        let props = super.createProperties();
        props.size.width = 120;
        props.size.height = 100;
        props.pointMode = rs.mimic.ShapePolygonPointMode.AUTO;
        props.pointCount = 5;
        props.points = "50,0 100,38 82,100 18,100 0,38";
        return props;
    }

    // Parses polygon mode, point count and custom coordinates.
    parseProperties(sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        const pointMode = PropertyParser.parseString(sourceProps.pointMode);
        props.pointMode = pointMode === rs.mimic.ShapePolygonPointMode.CUSTOM
            ? pointMode
            : rs.mimic.ShapePolygonPointMode.AUTO;
        props.pointCount = PropertyParser.parseInt(sourceProps.pointCount);
        props.pointCount = this._clampInteger(props.pointCount, 2, 12, 5);
        props.points = PropertyParser.parseString(sourceProps.points) || "50,0 100,38 82,100 18,100 0,38";
        return props;
    }

    // Creates a polygon component.
    createComponent() {
        return super.createComponent("ShapePolygon");
    }
};

rs.mimic.ShapeTriangleFactory = class extends rs.mimic.ShapeFactoryBase {
    // Creates a triangle component.
    createComponent() {
        return super.createComponent("ShapeTriangle");
    }
};

rs.mimic.ShapeDiamondFactory = class extends rs.mimic.ShapeFactoryBase {
    // Creates a diamond component.
    createComponent() {
        return super.createComponent("ShapeDiamond");
    }
};

rs.mimic.ShapeHexagonFactory = class extends rs.mimic.ShapeFactoryBase {
    // Creates a hexagon component.
    createComponent() {
        return super.createComponent("ShapeHexagon");
    }
};

rs.mimic.ShapeParallelogramFactory = class extends rs.mimic.ShapeFactoryBase {
    // Creates a parallelogram component.
    createComponent() {
        return super.createComponent("ShapeParallelogram");
    }
};

rs.mimic.ShapeTrapezoidFactory = class extends rs.mimic.ShapeFactoryBase {
    // Creates a trapezoid component.
    createComponent() {
        return super.createComponent("ShapeTrapezoid");
    }
};

rs.mimic.ShapeCrossFactory = class extends rs.mimic.ShapeFactoryBase {
    // Creates a cross component.
    createComponent() {
        return super.createComponent("ShapeCross");
    }
};

rs.mimic.ShapeHalfCircleFactory = class extends rs.mimic.ShapeFactoryBase {
    // Creates a half-circle component.
    createComponent() {
        return super.createComponent("ShapeHalfCircle");
    }
};

rs.mimic.ShapeDonutFactory = class extends rs.mimic.ShapeFactoryBase {
    // Creates donut-specific defaults.
    createProperties() {
        let props = super.createProperties();
        props.holeSize = 30;
        return props;
    }

    // Parses and clamps the donut hole percentage.
    parseProperties(sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        props.holeSize = PropertyParser.parseInt(sourceProps.holeSize);
        props.holeSize = this._clampInteger(props.holeSize, 10, 90, 30);
        return props;
    }

    // Creates a donut component.
    createComponent() {
        return super.createComponent("ShapeDonut");
    }
};

rs.mimic.ShapePieFactory = class extends rs.mimic.ShapeFactoryBase {
    // Creates pie-sector angle defaults.
    createProperties() {
        let props = super.createProperties();
        props.startAngle = 0;
        props.sweepAngle = 90;
        return props;
    }

    // Parses pie angles and limits the sweep to one complete turn.
    parseProperties(sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        props.startAngle = PropertyParser.parseInt(sourceProps.startAngle);
        props.sweepAngle = PropertyParser.parseInt(sourceProps.sweepAngle);
        if (!Number.isFinite(props.startAngle)) {
            props.startAngle = 0;
        }

        props.sweepAngle = this._clampInteger(props.sweepAngle, -360, 360, 90);
        return props;
    }

    // Creates a pie-sector component.
    createComponent() {
        return super.createComponent("ShapePie");
    }
};

rs.mimic.ShapeArrowFactory = class extends rs.mimic.ShapeFactoryBase {
    // Creates arrow-specific defaults.
    createProperties() {
        let props = super.createProperties();
        props.size.width = 120;
        props.size.height = 80;
        props.direction = rs.mimic.ShapeArrowDirection.RIGHT;
        return props;
    }

    // Parses and validates the arrow direction.
    parseProperties(sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        const direction = PropertyParser.parseString(sourceProps.direction);
        const directions = Object.values(rs.mimic.ShapeArrowDirection);
        props.direction = directions.includes(direction) ? direction : rs.mimic.ShapeArrowDirection.RIGHT;
        return props;
    }

    // Creates an arrow component.
    createComponent() {
        return super.createComponent("ShapeArrow");
    }
};

rs.mimic.ShapeLineFactory = class extends rs.mimic.RegularComponentFactory {
    // Clamps a parsed integer and substitutes a safe fallback for invalid input.
    _clampInteger(value, minimum, maximum, fallback) {
        return Number.isFinite(value)
            ? Math.max(minimum, Math.min(maximum, value))
            : fallback;
    }

    // Creates line-specific defaults.
    createProperties() {
        let props = super.createProperties();
        props.size.width = 120;
        props.size.height = 80;
        props.backColor = "Transparent";
        props.border.width = 0;
        props.strokeColor = "Black";
        props.strokeWidth = 2;
        props.strokeDasharray = "";
        props.opacity = 100;
        props.rotation = 0;
        props.orientation = rs.mimic.ShapeLineOrientation.DIAGONAL;
        props.x1 = 0;
        props.y1 = 100;
        props.x2 = 100;
        props.y2 = 0;
        return props;
    }

    // Parses and normalizes persisted line properties.
    parseProperties(sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        props.strokeColor = PropertyParser.parseString(sourceProps.strokeColor) || "Black";
        props.strokeWidth = PropertyParser.parseInt(sourceProps.strokeWidth);
        props.strokeDasharray = PropertyParser.parseString(sourceProps.strokeDasharray) || "";
        props.opacity = PropertyParser.parseInt(sourceProps.opacity);
        props.rotation = PropertyParser.parseInt(sourceProps.rotation);
        props.strokeWidth = this._clampInteger(props.strokeWidth, 0, 100, 2);
        props.opacity = this._clampInteger(props.opacity, 0, 100, 100);

        if (!Number.isFinite(props.rotation)) {
            props.rotation = 0;
        }

        const orientation = PropertyParser.parseString(sourceProps.orientation);
        const orientations = Object.values(rs.mimic.ShapeLineOrientation);
        props.orientation = orientations.includes(orientation)
            ? orientation
            : rs.mimic.ShapeLineOrientation.DIAGONAL;
        props.x1 = PropertyParser.parseInt(sourceProps.x1);
        props.y1 = PropertyParser.parseInt(sourceProps.y1);
        props.x2 = PropertyParser.parseInt(sourceProps.x2);
        props.y2 = PropertyParser.parseInt(sourceProps.y2);
        props.x1 = this._clampInteger(props.x1, 0, 100, 0);
        props.y1 = this._clampInteger(props.y1, 0, 100, 100);
        props.x2 = this._clampInteger(props.x2, 0, 100, 100);
        props.y2 = this._clampInteger(props.y2, 0, 100, 0);
        return props;
    }

    // Creates a line component.
    createComponent() {
        return super.createComponent("ShapeLine");
    }
};

rs.mimic.ShapePolylineFactory = class extends rs.mimic.ShapeLineFactory {
    placementMode = "polyline";

    // Creates polyline-specific defaults.
    createProperties() {
        let props = super.createProperties();
        props.size.width = 160;
        props.size.height = 100;
        props.pointCount = 4;
        props.snapToAxis = true;
        props.snapThreshold = 10;
        props.points = "0,80 35,20 70,70 100,10";
        return props;
    }

    // Parses and normalizes persisted polyline properties.
    parseProperties(sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        props.pointCount = PropertyParser.parseInt(sourceProps.pointCount);
        props.pointCount = Math.max(2, props.pointCount || 4);
        props.snapToAxis = PropertyParser.parseBool(sourceProps.snapToAxis, true);
        props.snapThreshold = PropertyParser.parseInt(sourceProps.snapThreshold);
        props.snapThreshold = this._clampInteger(props.snapThreshold, 0, 50, 10);
        props.points = PropertyParser.parseString(sourceProps.points) || "0,80 35,20 70,70 100,10";
        return props;
    }

    // Creates a polyline component without inheriting the line type name.
    createComponent() {
        return rs.mimic.RegularComponentFactory.prototype.createComponent.call(this, "ShapePolyline");
    }

    // Accepts finite placement coordinates without imposing an artificial point limit.
    normalizePlacementPoint(point) {
        const x = Number(point?.x);
        const y = Number(point?.y);
        return Number.isFinite(x) && Number.isFinite(y)
            ? { x, y }
            : null;
    }

    // Validates the minimum point count and every placement coordinate.
    validatePlacementPoints(points) {
        const valid = Array.isArray(points) &&
            points.length >= 2 &&
            points.every(point => Number.isFinite(Number(point?.x)) && Number.isFinite(Number(point?.y)));
        return { valid };
    }

    // Calculates stable component bounds for absolute editor points.
    _getPlacementBounds(points) {
        const minimumSize = 8;
        let left = Math.min(...points.map(point => point.x));
        let top = Math.min(...points.map(point => point.y));
        const right = Math.max(...points.map(point => point.x));
        const bottom = Math.max(...points.map(point => point.y));
        let width = right - left;
        let height = bottom - top;

        if (width < minimumSize) {
            left -= (minimumSize - width) / 2;
            width = minimumSize;
        }

        if (height < minimumSize) {
            top -= (minimumSize - height) / 2;
            height = minimumSize;
        }

        return { left, top, width, height };
    }

    // Converts absolute editor points to component-relative percentages.
    _formatPlacementPoints(points, bounds) {
        return points
            .map(point => ({
                x: Math.round((point.x - bounds.left) / bounds.width * 10000) / 100,
                y: Math.round((point.y - bounds.top) / bounds.height * 10000) / 100
            }))
            .map(point => point.x + "," + point.y)
            .join(" ");
    }

    // Creates one persisted polyline draft from validated editor points.
    createComponentsFromPoints(points) {
        if (!this.validatePlacementPoints(points).valid) {
            throw new Error();
        }

        const component = this.createComponent();
        const bounds = this._getPlacementBounds(points);
        component.setLocation(bounds.left, bounds.top);
        component.properties.size.width = bounds.width;
        component.properties.size.height = bounds.height;
        component.properties.pointCount = points.length;
        component.properties.points = this._formatPlacementPoints(points, bounds);
        return [{ component, left: bounds.left, top: bounds.top }];
    }
};

// Registers every shape factory with the shared mimic factory set.
function registerShapeFactories() {
    let componentFactories = rs.mimic.FactorySet.componentFactories;
    componentFactories.set("ShapeRectangle", new rs.mimic.ShapeRectangleFactory());
    componentFactories.set("ShapeSquare", new rs.mimic.ShapeSquareFactory());
    componentFactories.set("ShapeEllipse", new rs.mimic.ShapeEllipseFactory());
    componentFactories.set("ShapeCircle", new rs.mimic.ShapeCircleFactory());
    componentFactories.set("ShapeRoundedRect", new rs.mimic.ShapeRoundedRectFactory());
    componentFactories.set("ShapePolygon", new rs.mimic.ShapePolygonFactory());
    componentFactories.set("ShapeTriangle", new rs.mimic.ShapeTriangleFactory());
    componentFactories.set("ShapeDiamond", new rs.mimic.ShapeDiamondFactory());
    componentFactories.set("ShapeHexagon", new rs.mimic.ShapeHexagonFactory());
    componentFactories.set("ShapeParallelogram", new rs.mimic.ShapeParallelogramFactory());
    componentFactories.set("ShapeTrapezoid", new rs.mimic.ShapeTrapezoidFactory());
    componentFactories.set("ShapeCross", new rs.mimic.ShapeCrossFactory());
    componentFactories.set("ShapeHalfCircle", new rs.mimic.ShapeHalfCircleFactory());
    componentFactories.set("ShapeDonut", new rs.mimic.ShapeDonutFactory());
    componentFactories.set("ShapePie", new rs.mimic.ShapePieFactory());
    componentFactories.set("ShapeArrow", new rs.mimic.ShapeArrowFactory());
    componentFactories.set("ShapeLine", new rs.mimic.ShapeLineFactory());
    componentFactories.set("ShapePolyline", new rs.mimic.ShapePolylineFactory());
}

registerShapeFactories();

// Contains renderers for shape components.

rs.mimic.ShapeRendererBase = class extends rs.mimic.RegularComponentRenderer {
    // Suppresses the regular HTML border because SVG renders the shape stroke.
    _setBorder(jqObj, border) {
        jqObj.css("border", "");
    }

    // Creates the SVG surface and resize handles for a shape component.
    _completeDom(componentElem, component, renderContext) {
        componentElem.append("<svg class='shape-svg' xmlns='http://www.w3.org/2000/svg' preserveAspectRatio='none'></svg>" +
            "<div class='shape-resize-handles'>" +
            "<span class='nw'></span><span class='n'></span><span class='ne'></span>" +
            "<span class='e'></span><span class='se'></span><span class='s'></span>" +
            "<span class='sw'></span><span class='w'></span></div>");
    }

    // Adds the shared shape class to the regular component element.
    _setClasses(componentElem, component, renderContext) {
        super._setClasses(componentElem, component, renderContext);
        componentElem.addClass("shape-comp");
    }

    // Resolves an optional project image through the active render context.
    _imageHref(renderContext, imageName) {
        return imageName && renderContext.getImage ? renderContext.getImage(imageName) : "";
    }

    // Resets the SVG surface and applies the shared background image.
    _setCommonSvg(svg, component, renderContext) {
        let props = component.properties;
        let width = Math.max(1, component.innerWidth || component.width || props.size.width);
        let height = Math.max(1, component.innerHeight || component.height || props.size.height);
        let imageHref = this._imageHref(renderContext, props.imageName);
        let imageOpacity = Math.max(0, Math.min(100, props.imageOpacity)) / 100;

        svg.attr("viewBox", "0 0 " + width + " " + height);
        svg.empty();
        svg.append(this._createBackground(width, height, props.backgroundColor));

        if (imageHref) {
            svg.append($(document.createElementNS("http://www.w3.org/2000/svg", "image"))
                .attr("href", imageHref)
                .attr("x", 0)
                .attr("y", 0)
                .attr("width", width)
                .attr("height", height)
                .attr("preserveAspectRatio", "none")
                .attr("opacity", imageOpacity));
        }

        return { width, height };
    }

    // Creates a background rectangle that covers the complete SVG surface.
    _createBackground(width, height, color) {
        return $(document.createElementNS("http://www.w3.org/2000/svg", "rect"))
            .attr("x", 0)
            .attr("y", 0)
            .attr("width", width)
            .attr("height", height)
            .attr("fill", color || "transparent");
    }

    // Applies fill, stroke and dash properties to an SVG geometry element.
    _applyPaint(shapeElem, props) {
        shapeElem
            .attr("fill", props.fillColor || "transparent")
            .attr("stroke", props.strokeColor || "transparent")
            .attr("stroke-width", Math.max(0, props.strokeWidth || 0))
            .attr("vector-effect", "non-scaling-stroke");

        if (props.strokeDasharray) {
            shapeElem.attr("stroke-dasharray", props.strokeDasharray);
        } else {
            shapeElem.attr("stroke-dasharray", null);
        }
    }

    // Applies component opacity and rotation to the HTML wrapper.
    _applyTransform(componentElem, props) {
        let opacity = Math.max(0, Math.min(100, props.opacity)) / 100;
        componentElem.css("opacity", opacity);

        let rotation = Number.parseInt(props.rotation) || 0;
        if (rotation !== 0) {
            componentElem.css("transform", "rotate(" + rotation + "deg)");
        } else {
            componentElem.css("transform", "");
        }
    }

    // Parses a persisted percentage point list and drops invalid coordinates.
    _parsePercentPoints(points) {
        let nums = String(points || "")
            .replace(/[;|]/g, " ")
            .split(/[\s,]+/)
            .map(s => Number(s))
            .filter(n => Number.isFinite(n));

        let result = [];
        for (let i = 0; i + 1 < nums.length; i += 2) {
            result.push({
                x: Math.max(0, Math.min(100, nums[i])),
                y: Math.max(0, Math.min(100, nums[i + 1]))
            });
        }
        return result;
    }

    // Converts percentage points to SVG pixel coordinates.
    _pointsToSvg(points, width, height) {
        return points.map(p => (p.x / 100 * width) + "," + (p.y / 100 * height)).join(" ");
    }

    // Clamps a component-relative coordinate to a percentage range.
    _clampPercent(value) {
        return Math.max(0, Math.min(100, Math.round(value * 100) / 100));
    }

    // Serializes component-relative points for persistence.
    _formatPercentPoints(points) {
        return points
            .map(point => this._clampPercent(point.x) + "," + this._clampPercent(point.y))
            .join(" ");
    }

    // Converts a parent-local editor point to component-relative percentages.
    _mimicPointToComponentPercent(component, mimicPoint) {
        const width = Math.max(1, component.width);
        const height = Math.max(1, component.height);
        return {
            x: this._clampPercent((mimicPoint.x - component.x) / width * 100),
            y: this._clampPercent((mimicPoint.y - component.y) / height * 100)
        };
    }

    // Calculates the squared pixel distance from a point to a finite segment.
    _pointSegmentDistanceSquared(component, point, start, end) {
        const scaleX = Math.max(1, component.width) / 100;
        const scaleY = Math.max(1, component.height) / 100;
        const pointX = point.x * scaleX;
        const pointY = point.y * scaleY;
        const startX = start.x * scaleX;
        const startY = start.y * scaleY;
        const deltaX = (end.x - start.x) * scaleX;
        const deltaY = (end.y - start.y) * scaleY;
        const lengthSquared = deltaX * deltaX + deltaY * deltaY;

        if (lengthSquared < 0.000001) {
            return (pointX - startX) ** 2 + (pointY - startY) ** 2;
        }

        const factor = Math.max(0, Math.min(1,
            ((pointX - startX) * deltaX + (pointY - startY) * deltaY) / lengthSquared));
        const nearestX = startX + factor * deltaX;
        const nearestY = startY + factor * deltaY;
        return (pointX - nearestX) ** 2 + (pointY - nearestY) ** 2;
    }

    // Finds the insertion index belonging to the segment nearest the new point.
    _findNearestSegmentInsertIndex(component, points, point, closed) {
        const segmentCount = closed ? points.length : points.length - 1;
        let bestIndex = points.length;
        let bestDistance = Infinity;

        for (let i = 0; i < segmentCount; i++) {
            const nextIndex = (i + 1) % points.length;
            const distance = this._pointSegmentDistanceSquared(component, point, points[i], points[nextIndex]);

            if (distance < bestDistance) {
                bestDistance = distance;
                bestIndex = i + 1;
            }
        }

        return bestIndex;
    }

    // Rebuilds the point handles consumed by the current Mimic editor contract.
    _renderPointHandles(componentElem, component, points) {
        let handlesElem = componentElem.children(".shape-point-handles:first");

        if (handlesElem.length === 0) {
            handlesElem = $("<div class='shape-point-handles'></div>").appendTo(componentElem);
        }

        handlesElem.empty();

        if (!component.isSelected || !Array.isArray(points)) {
            return;
        }

        for (let i = 0; i < points.length; i++) {
            $("<span class='shape-point-handle'></span>")
                .attr("data-point-index", i)
                .css({
                    left: points[i].x + "%",
                    top: points[i].y + "%"
                })
                .appendTo(handlesElem);
        }
    }

    // Applies shape properties and redraws its SVG geometry.
    _setProps(componentElem, component, renderContext) {
        super._setProps(componentElem, component, renderContext);
        component._shapeRenderContext = renderContext;
        this._applyTransform(componentElem, component.properties);
        this._renderShape(componentElem, component, renderContext);
    }

    // Redraws SVG geometry after a regular component resize.
    setSize(component, width, height) {
        super.setSize(component, width, height);
        this._renderShape(component.dom, component, component._shapeRenderContext || {});
    }
};

rs.mimic.ShapeRectangleRenderer = class extends rs.mimic.ShapeRendererBase {
    // Renders a rectangle inside the available SVG bounds.
    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let stroke = Math.max(0, props.strokeWidth || 0);
        let rect = $(document.createElementNS("http://www.w3.org/2000/svg", "rect"))
            .attr("x", stroke / 2)
            .attr("y", stroke / 2)
            .attr("width", Math.max(1, size.width - stroke))
            .attr("height", Math.max(1, size.height - stroke));
        this._applyPaint(rect, props);
        svg.append(rect);
    }
};

rs.mimic.ShapeSquareRenderer = class extends rs.mimic.ShapeRendererBase {
    // Keeps square width and height equal during resizing.
    setSize(component, width, height) {
        let side = Math.max(width, height);
        super.setSize(component, side, side);
    }

    // Renders a square inside the available SVG bounds.
    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let stroke = Math.max(0, props.strokeWidth || 0);
        let side = Math.max(1, Math.min(size.width, size.height) - stroke);
        let rect = $(document.createElementNS("http://www.w3.org/2000/svg", "rect"))
            .attr("x", (size.width - side) / 2)
            .attr("y", (size.height - side) / 2)
            .attr("width", side)
            .attr("height", side);
        this._applyPaint(rect, props);
        svg.append(rect);
    }
};

rs.mimic.ShapeEllipseRenderer = class extends rs.mimic.ShapeRendererBase {
    // Renders an ellipse centered in the SVG surface.
    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let stroke = Math.max(0, props.strokeWidth || 0);
        let ellipse = $(document.createElementNS("http://www.w3.org/2000/svg", "ellipse"))
            .attr("cx", size.width / 2)
            .attr("cy", size.height / 2)
            .attr("rx", Math.max(1, (size.width - stroke) / 2))
            .attr("ry", Math.max(1, (size.height - stroke) / 2));
        this._applyPaint(ellipse, props);
        svg.append(ellipse);
    }
};

rs.mimic.ShapeCircleRenderer = class extends rs.mimic.ShapeRendererBase {
    // Keeps circle width and height equal during resizing.
    setSize(component, width, height) {
        let side = Math.max(width, height);
        super.setSize(component, side, side);
    }

    // Renders a circle centered in the SVG surface.
    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let stroke = Math.max(0, props.strokeWidth || 0);
        let radius = Math.max(1, (Math.min(size.width, size.height) - stroke) / 2);
        let circle = $(document.createElementNS("http://www.w3.org/2000/svg", "circle"))
            .attr("cx", size.width / 2)
            .attr("cy", size.height / 2)
            .attr("r", radius);
        this._applyPaint(circle, props);
        svg.append(circle);
    }
};

rs.mimic.ShapeRoundedRectRenderer = class extends rs.mimic.ShapeRendererBase {
    // Renders a rectangle with a size-limited corner radius.
    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let stroke = Math.max(0, props.strokeWidth || 0);
        let borderRadius = Math.max(0, Math.min(Math.min(size.width, size.height) / 2, Number.parseInt(props.borderRadius) || 0));
        let rect = $(document.createElementNS("http://www.w3.org/2000/svg", "rect"))
            .attr("x", stroke / 2)
            .attr("y", stroke / 2)
            .attr("width", Math.max(1, size.width - stroke))
            .attr("height", Math.max(1, size.height - stroke))
            .attr("rx", borderRadius)
            .attr("ry", borderRadius);
        this._applyPaint(rect, props);
        svg.append(rect);
    }
};

rs.mimic.ShapePolygonRenderer = class extends rs.mimic.ShapeRendererBase {
    // Generates evenly distributed percentage points for an automatic polygon.
    _autoPoints(pointCount) {
        pointCount = Math.max(2, Math.min(12, Number.parseInt(pointCount) || 5));
        let result = [];

        if (pointCount === 2) {
            return [{ x: 0, y: 50 }, { x: 100, y: 50 }];
        }

        for (let i = 0; i < pointCount; i++) {
            let angle = -Math.PI / 2 + i * Math.PI * 2 / pointCount;
            result.push({
                x: 50 + Math.cos(angle) * 50,
                y: 50 + Math.sin(angle) * 50
            });
        }
        return result;
    }

    // Resolves automatic or custom polygon points for SVG rendering.
    _normalizePoints(props, width, height) {
        let pointCount = Math.max(2, Math.min(12, Number.parseInt(props.pointCount) || 5));
        let points = props.pointMode === rs.mimic.ShapePolygonPointMode.CUSTOM
            ? this._parsePercentPoints(props.points)
            : this._autoPoints(pointCount);

        if (points.length < 2) {
            points = this._autoPoints(pointCount);
        }

        if (props.pointMode === rs.mimic.ShapePolygonPointMode.CUSTOM) {
            while (points.length < pointCount) {
                points.push(this._autoPoints(pointCount)[points.length]);
            }
            points = points.slice(0, pointCount);
        }

        return this._pointsToSvg(points, width, height);
    }

    // Renders polygon geometry and its selected-state point handles.
    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let polygon = $(document.createElementNS("http://www.w3.org/2000/svg", "polygon"))
            .attr("points", this._normalizePoints(props, size.width, size.height));
        this._applyPaint(polygon, props);
        svg.append(polygon);
        this._renderPointHandles(componentElem, component, this._getEditablePoints(props));
    }

    // Returns the normalized polygon points used by editor operations.
    _getEditablePoints(props) {
        let pointCount = Math.max(2, Math.min(12, Number.parseInt(props.pointCount) || 5));
        let points = props.pointMode === rs.mimic.ShapePolygonPointMode.CUSTOM
            ? this._parsePercentPoints(props.points)
            : this._autoPoints(pointCount);
        let defaults = this._autoPoints(pointCount);

        if (points.length < 2) {
            points = defaults;
        }

        while (points.length < pointCount) {
            points.push(defaults[points.length]);
        }

        return points.slice(0, pointCount);
    }

    // Creates the editor action used to drag one polygon point.
    startPointEdit(component, pointIndex) {
        const index = Number.parseInt(pointIndex);
        const points = this._getEditablePoints(component.properties);

        if (!Number.isInteger(index) || index < 0 || index >= points.length) {
            return null;
        }

        return {
            actionType: "point-edit",
            component,
            pointIndex: index,
            moved: false,
            getCursor: () => "crosshair"
        };
    }

    // Applies a dragged parent-local point to the custom polygon geometry.
    movePointEdit(component, action, mimicPoint) {
        const props = component.properties;
        const points = this._getEditablePoints(props);
        const index = Math.max(0, Math.min(points.length - 1, action.pointIndex));
        points[index] = this._mimicPointToComponentPercent(component, mimicPoint);
        props.pointMode = rs.mimic.ShapePolygonPointMode.CUSTOM;
        props.pointCount = points.length;
        props.points = this._formatPercentPoints(points);
        return true;
    }

    // Returns the polygon properties changed by point dragging.
    finishPointEdit(component) {
        return { properties: this._pointProperties(component.properties) };
    }

    // Inserts a point into the geometrically nearest polygon edge.
    addPointEdit(component, mimicPoint) {
        const props = component.properties;
        const points = this._getEditablePoints(props);

        if (points.length >= 12) {
            return null;
        }

        const point = this._mimicPointToComponentPercent(component, mimicPoint);
        const insertIndex = this._findNearestSegmentInsertIndex(component, points, point, true);
        points.splice(insertIndex, 0, point);
        props.pointMode = rs.mimic.ShapePolygonPointMode.CUSTOM;
        props.pointCount = points.length;
        props.points = this._formatPercentPoints(points);
        return { properties: this._pointProperties(props) };
    }

    // Removes a polygon point while preserving the two-point minimum.
    removePointEdit(component, pointIndex) {
        const props = component.properties;
        const points = this._getEditablePoints(props);
        const index = Number.parseInt(pointIndex);

        if (points.length <= 2 || !Number.isInteger(index) || index < 0 || index >= points.length) {
            return null;
        }

        points.splice(index, 1);
        props.pointMode = rs.mimic.ShapePolygonPointMode.CUSTOM;
        props.pointCount = points.length;
        props.points = this._formatPercentPoints(points);
        return { properties: this._pointProperties(props) };
    }

    // Returns all persisted polygon properties changed by point operations.
    _pointProperties(props) {
        return {
            pointMode: props.pointMode,
            pointCount: props.pointCount,
            points: props.points
        };
    }
};

rs.mimic.ShapeTriangleRenderer = class extends rs.mimic.ShapeRendererBase {
    // Renders a triangle that fills the SVG surface.
    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let polygon = $(document.createElementNS("http://www.w3.org/2000/svg", "polygon"))
            .attr("points", "0," + size.height + " " + (size.width / 2) + ",0 " + size.width + "," + size.height);
        this._applyPaint(polygon, props);
        svg.append(polygon);
    }
};

rs.mimic.ShapeDiamondRenderer = class extends rs.mimic.ShapeRendererBase {
    // Renders a diamond that fills the SVG surface.
    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let polygon = $(document.createElementNS("http://www.w3.org/2000/svg", "polygon"))
            .attr("points", (size.width / 2) + ",0 " + size.width + "," + (size.height / 2) + " " +
                (size.width / 2) + "," + size.height + " 0," + (size.height / 2));
        this._applyPaint(polygon, props);
        svg.append(polygon);
    }
};

rs.mimic.ShapeHexagonRenderer = class extends rs.mimic.ShapeRendererBase {
    // Renders a regular six-sided polygon.
    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let w = size.width;
        let h = size.height;
        let qw = w / 4;
        let hh = h / 2;
        let polygon = $(document.createElementNS("http://www.w3.org/2000/svg", "polygon"))
            .attr("points", qw + ",0 " + (w - qw) + ",0 " + w + "," + hh + " " +
                (w - qw) + "," + h + " " + qw + "," + h + " 0," + hh);
        this._applyPaint(polygon, props);
        svg.append(polygon);
    }
};

rs.mimic.ShapeParallelogramRenderer = class extends rs.mimic.ShapeRendererBase {
    // Renders a right-leaning parallelogram.
    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let skew = size.width * 0.2;
        let polygon = $(document.createElementNS("http://www.w3.org/2000/svg", "polygon"))
            .attr("points", skew + ",0 " + size.width + ",0 " + (size.width - skew) + "," + size.height + " 0," + size.height);
        this._applyPaint(polygon, props);
        svg.append(polygon);
    }
};

rs.mimic.ShapeTrapezoidRenderer = class extends rs.mimic.ShapeRendererBase {
    // Renders a centered trapezoid.
    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let topInset = size.width * 0.15;
        let polygon = $(document.createElementNS("http://www.w3.org/2000/svg", "polygon"))
            .attr("points", topInset + ",0 " + (size.width - topInset) + ",0 " + size.width + "," + size.height + " 0," + size.height);
        this._applyPaint(polygon, props);
        svg.append(polygon);
    }
};

rs.mimic.ShapeCrossRenderer = class extends rs.mimic.ShapeRendererBase {
    // Renders a cross with proportional arms.
    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let w = size.width;
        let h = size.height;
        let tw = w * 0.3;
        let th = h * 0.3;
        let points = tw + ",0 " + (w - tw) + ",0 " + (w - tw) + "," + th + " " +
            w + "," + th + " " + w + "," + (h - th) + " " + (w - tw) + "," + (h - th) + " " +
            (w - tw) + "," + h + " " + tw + "," + h + " " + tw + "," + (h - th) + " " +
            "0," + (h - th) + " 0," + th + " " + tw + "," + th;
        let polygon = $(document.createElementNS("http://www.w3.org/2000/svg", "polygon"))
            .attr("points", points);
        this._applyPaint(polygon, props);
        svg.append(polygon);
    }
};

rs.mimic.ShapeHalfCircleRenderer = class extends rs.mimic.ShapeRendererBase {
    // Renders the upper half of an ellipse closed along its diameter.
    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let w = size.width;
        let h = size.height;
        let r = Math.max(1, Math.min(w, h * 2) / 2);
        let path = $(document.createElementNS("http://www.w3.org/2000/svg", "path"))
            .attr("d", "M 0," + h + " A " + r + "," + r + " 0 0,1 " + w + "," + h + " Z");
        this._applyPaint(path, props);
        svg.append(path);
    }
};

rs.mimic.ShapeDonutRenderer = class extends rs.mimic.ShapeRendererBase {
    // Renders a ring with an independently sized transparent center.
    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let w = size.width;
        let h = size.height;
        let outerR = Math.min(w, h) / 2;
        let cx = w / 2;
        let cy = h / 2;
        let holePct = Math.max(10, Math.min(90, Number.parseInt(props.holeSize) || 30)) / 100;
        let innerR = outerR * holePct;

        let path = $(document.createElementNS("http://www.w3.org/2000/svg", "path"))
            .attr("d", "M " + (cx - outerR) + "," + cy +
                " A " + outerR + "," + outerR + " 0 1,1 " + (cx + outerR) + "," + cy +
                " A " + outerR + "," + outerR + " 0 1,1 " + (cx - outerR) + "," + cy +
                " M " + (cx - innerR) + "," + cy +
                " A " + innerR + "," + innerR + " 0 1,0 " + (cx + innerR) + "," + cy +
                " A " + innerR + "," + innerR + " 0 1,0 " + (cx - innerR) + "," + cy + " Z")
            .attr("fill-rule", "evenodd");
        this._applyPaint(path, props);
        svg.append(path);
    }
};

rs.mimic.ShapePieRenderer = class extends rs.mimic.ShapeRendererBase {
    // Renders a sector or a full circle when the sweep covers a complete turn.
    _renderShape(componentElem, component, renderContext) {
        const props = component.properties;
        const svg = componentElem.children("svg.shape-svg:first");
        const size = this._setCommonSvg(svg, component, renderContext);
        const w = size.width;
        const h = size.height;
        const cx = w / 2;
        const cy = h / 2;
        const r = Math.min(w, h) / 2;
        const parsedStartAngle = Number.parseInt(props.startAngle);
        const parsedSweepAngle = Number.parseInt(props.sweepAngle);
        const startAngle = (Number.isFinite(parsedStartAngle) ? parsedStartAngle : 0) * Math.PI / 180;
        const sweepAngleDegrees = Number.isFinite(parsedSweepAngle) ? parsedSweepAngle : 90;

        if (Math.abs(sweepAngleDegrees) >= 360) {
            const circle = $(document.createElementNS("http://www.w3.org/2000/svg", "circle"))
                .attr("cx", cx)
                .attr("cy", cy)
                .attr("r", r);
            this._applyPaint(circle, props);
            svg.append(circle);
            return;
        }

        if (sweepAngleDegrees === 0) {
            return;
        }

        const sweepAngle = sweepAngleDegrees * Math.PI / 180;
        const endAngle = startAngle + sweepAngle;

        const x1 = cx + r * Math.cos(startAngle);
        const y1 = cy + r * Math.sin(startAngle);
        const x2 = cx + r * Math.cos(endAngle);
        const y2 = cy + r * Math.sin(endAngle);
        const largeArc = Math.abs(sweepAngle) > Math.PI ? 1 : 0;
        const sweepFlag = sweepAngle > 0 ? 1 : 0;

        const path = $(document.createElementNS("http://www.w3.org/2000/svg", "path"))
            .attr("d", "M " + cx + "," + cy + " L " + x1 + "," + y1 +
                " A " + r + "," + r + " 0 " + largeArc + "," + sweepFlag + " " + x2 + "," + y2 + " Z");
        this._applyPaint(path, props);
        svg.append(path);
    }
};

rs.mimic.ShapeArrowRenderer = class extends rs.mimic.ShapeRendererBase {
    // Builds direction-specific arrow points for the current component size.
    _arrowPoints(props, width, height) {
        const ShapeArrowDirection = rs.mimic.ShapeArrowDirection;
        const dir = props.direction || ShapeArrowDirection.RIGHT;
        const w = width;
        const h = height;
        const shaftW = Math.max(1, w * 0.5);
        const shaftH = Math.max(1, h * 0.4);
        const headW = Math.max(1, w * 0.5);
        const headH = Math.max(1, h * 0.5);

        switch (dir) {
            case ShapeArrowDirection.RIGHT:
                return "0," + ((h - shaftH) / 2) + " " + (w - headW) + "," + ((h - shaftH) / 2) + " " +
                    (w - headW) + ",0 " + w + "," + (h / 2) + " " +
                    (w - headW) + "," + h + " " + (w - headW) + "," + ((h + shaftH) / 2) + " " +
                    "0," + ((h + shaftH) / 2);
            case ShapeArrowDirection.LEFT:
                return w + "," + ((h - shaftH) / 2) + " " + headW + "," + ((h - shaftH) / 2) + " " +
                    headW + ",0 0," + (h / 2) + " " +
                    headW + "," + h + " " + headW + "," + ((h + shaftH) / 2) + " " +
                    w + "," + ((h + shaftH) / 2);
            case ShapeArrowDirection.UP:
                return ((w - shaftW) / 2) + "," + h + " " + ((w - shaftW) / 2) + "," + headH + " " +
                    "0," + headH + " " + (w / 2) + ",0 " +
                    w + "," + headH + " " + ((w + shaftW) / 2) + "," + headH + " " +
                    ((w + shaftW) / 2) + "," + h;
            case ShapeArrowDirection.DOWN:
                return ((w - shaftW) / 2) + ",0 " + ((w - shaftW) / 2) + "," + (h - headH) + " " +
                    "0," + (h - headH) + " " + (w / 2) + "," + h + " " +
                    w + "," + (h - headH) + " " + ((w + shaftW) / 2) + "," + (h - headH) + " " +
                    ((w + shaftW) / 2) + ",0";
            default:
                return "0," + ((h - shaftH) / 2) + " " + (w - headW) + "," + ((h - shaftH) / 2) + " " +
                    (w - headW) + ",0 " + w + "," + (h / 2) + " " +
                    (w - headW) + "," + h + " " + (w - headW) + "," + ((h + shaftH) / 2) + " " +
                    "0," + ((h + shaftH) / 2);
        }
    }

    // Renders an arrow polygon for the selected direction.
    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let polygon = $(document.createElementNS("http://www.w3.org/2000/svg", "polygon"))
            .attr("points", this._arrowPoints(props, size.width, size.height));
        this._applyPaint(polygon, props);
        svg.append(polygon);
    }
};

rs.mimic.ShapeLineRenderer = class extends rs.mimic.ShapeRendererBase {
    // Resets the line SVG without adding the filled-shape background layer.
    _setCommonSvg(svg, component, renderContext) {
        let props = component.properties;
        let width = Math.max(1, component.innerWidth || component.width || props.size.width);
        let height = Math.max(1, component.innerHeight || component.height || props.size.height);
        svg.attr("viewBox", "0 0 " + width + " " + height);
        svg.empty();
        return { width, height };
    }

    // Renders a line and its selected-state endpoint handles.
    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let stroke = Math.max(1, props.strokeWidth || 1);
        let p = this._getLinePoints(props);
        let line = $(document.createElementNS("http://www.w3.org/2000/svg", "line"))
            .attr("x1", p.x1 / 100 * size.width)
            .attr("y1", p.y1 / 100 * size.height)
            .attr("x2", p.x2 / 100 * size.width)
            .attr("y2", p.y2 / 100 * size.height)
            .attr("stroke", props.strokeColor || "Black")
            .attr("stroke-width", stroke)
            .attr("stroke-linecap", "round")
            .attr("vector-effect", "non-scaling-stroke");

        if (props.strokeDasharray) {
            line.attr("stroke-dasharray", props.strokeDasharray);
        }

        svg.append(line);
        this._renderPointHandles(componentElem, component, [
            { x: p.x1, y: p.y1 },
            { x: p.x2, y: p.y2 }
        ]);
    }

    // Resolves line endpoints for the selected orientation mode.
    _getLinePoints(props) {
        const ShapeLineOrientation = rs.mimic.ShapeLineOrientation;

        switch (props.orientation) {
            case ShapeLineOrientation.HORIZONTAL:
                return { x1: 0, y1: 50, x2: 100, y2: 50 };
            case ShapeLineOrientation.VERTICAL:
                return { x1: 50, y1: 0, x2: 50, y2: 100 };
            case ShapeLineOrientation.CUSTOM:
                return {
                    x1: Math.max(0, Math.min(100, props.x1)),
                    y1: Math.max(0, Math.min(100, props.y1)),
                    x2: Math.max(0, Math.min(100, props.x2)),
                    y2: Math.max(0, Math.min(100, props.y2))
                };
            default:
                return { x1: 0, y1: 100, x2: 100, y2: 0 };
        }
    }

    // Creates the editor action used to drag one line endpoint.
    startPointEdit(component, pointIndex) {
        const index = Number.parseInt(pointIndex);

        if (index !== 0 && index !== 1) {
            return null;
        }

        return {
            actionType: "point-edit",
            component,
            pointIndex: index,
            moved: false,
            getCursor: () => "crosshair"
        };
    }

    // Applies a dragged parent-local point to one custom line endpoint.
    movePointEdit(component, action, mimicPoint) {
        const point = this._mimicPointToComponentPercent(component, mimicPoint);

        if (action.pointIndex === 0) {
            component.properties.x1 = point.x;
            component.properties.y1 = point.y;
        } else {
            component.properties.x2 = point.x;
            component.properties.y2 = point.y;
        }

        component.properties.orientation = rs.mimic.ShapeLineOrientation.CUSTOM;
        return true;
    }

    // Returns the line properties changed by endpoint dragging.
    finishPointEdit(component) {
        return {
            properties: {
                orientation: component.properties.orientation,
                x1: component.properties.x1,
                y1: component.properties.y1,
                x2: component.properties.x2,
                y2: component.properties.y2
            }
        };
    }
};

rs.mimic.ShapePolylineRenderer = class extends rs.mimic.ShapeLineRenderer {
    // Generates the requested number of alternating default points.
    _defaultPoints(pointCount) {
        pointCount = Math.max(2, Number.parseInt(pointCount) || 4);
        let result = [];
        for (let i = 0; i < pointCount; i++) {
            let x = i * 100 / (pointCount - 1);
            let y = i % 2 === 0 ? 75 : 25;
            result.push({ x, y });
        }
        return result;
    }

    // Snaps each point to the previous point when it is near one axis.
    _snapPointsToAxis(points, threshold) {
        threshold = Math.max(0, Math.min(50, Number.parseInt(threshold) || 0));
        if (threshold <= 0 || points.length < 2) {
            return points;
        }

        let snapped = [points[0]];
        for (let i = 1; i < points.length; i++) {
            let prev = snapped[i - 1];
            let point = { ...points[i] };
            if (Math.abs(point.x - prev.x) <= threshold) {
                point.x = prev.x;
            }
            if (Math.abs(point.y - prev.y) <= threshold) {
                point.y = prev.y;
            }
            snapped.push(point);
        }
        return snapped;
    }

    // Resolves persisted polyline points and converts them to SVG coordinates.
    _normalizePoints(props, width, height) {
        let pointCount = Math.max(2, Number.parseInt(props.pointCount) || 4);
        let points = this._parsePercentPoints(props.points);
        let defaults = this._defaultPoints(pointCount);

        if (points.length < 2) {
            points = defaults;
        } else {
            while (points.length < pointCount) {
                points.push(defaults[points.length]);
            }
            points = points.slice(0, pointCount);
        }

        if (props.snapToAxis) {
            points = this._snapPointsToAxis(points, props.snapThreshold);
        }

        return this._pointsToSvg(points, width, height);
    }

    // Renders connected adjacent segments and selected-state point handles.
    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let stroke = Math.max(1, props.strokeWidth || 1);
        let polyline = $(document.createElementNS("http://www.w3.org/2000/svg", "polyline"))
            .attr("points", this._normalizePoints(props, size.width, size.height))
            .attr("fill", "none")
            .attr("stroke", props.strokeColor || "Black")
            .attr("stroke-width", stroke)
            .attr("stroke-linecap", "round")
            .attr("stroke-linejoin", "round")
            .attr("vector-effect", "non-scaling-stroke");

        if (props.strokeDasharray) {
            polyline.attr("stroke-dasharray", props.strokeDasharray);
        }

        svg.append(polyline);
        this._renderPointHandles(componentElem, component, this._getEditablePoints(props));
    }

    // Returns the normalized polyline points used by editor operations.
    _getEditablePoints(props) {
        let pointCount = Math.max(2, Number.parseInt(props.pointCount) || 4);
        let points = this._parsePercentPoints(props.points);
        let defaults = this._defaultPoints(pointCount);

        if (points.length < 2) {
            points = defaults;
        }

        while (points.length < pointCount) {
            points.push(defaults[points.length]);
        }

        points = points.slice(0, pointCount);

        if (props.snapToAxis) {
            points = this._snapPointsToAxis(points, props.snapThreshold);
        }

        return points;
    }

    // Creates the editor action used to drag one polyline point.
    startPointEdit(component, pointIndex) {
        const index = Number.parseInt(pointIndex);
        const points = this._getEditablePoints(component.properties);

        if (!Number.isInteger(index) || index < 0 || index >= points.length) {
            return null;
        }

        return {
            actionType: "point-edit",
            component,
            pointIndex: index,
            moved: false,
            getCursor: () => "crosshair"
        };
    }

    // Applies a dragged parent-local point to the polyline geometry.
    movePointEdit(component, action, mimicPoint) {
        let props = component.properties;
        let points = this._getEditablePoints(props);
        let index = Math.max(0, Math.min(points.length - 1, action.pointIndex));
        points[index] = this._mimicPointToComponentPercent(component, mimicPoint);

        if (props.snapToAxis) {
            points = this._snapPointsToAxis(points, props.snapThreshold);
        }

        props.pointCount = points.length;
        props.points = this._formatPercentPoints(points);
        return true;
    }

    // Returns the polyline properties changed by point dragging.
    finishPointEdit(component) {
        return { properties: this._pointProperties(component.properties) };
    }

    // Inserts a point into the geometrically nearest polyline segment.
    addPointEdit(component, mimicPoint) {
        let props = component.properties;
        let points = this._getEditablePoints(props);

        const point = this._mimicPointToComponentPercent(component, mimicPoint);
        const insertIndex = this._findNearestSegmentInsertIndex(component, points, point, false);
        points.splice(insertIndex, 0, point);

        if (props.snapToAxis) {
            points = this._snapPointsToAxis(points, props.snapThreshold);
        }

        props.pointCount = points.length;
        props.points = this._formatPercentPoints(points);
        return { properties: this._pointProperties(props) };
    }

    // Removes a polyline point while preserving the two-point minimum.
    removePointEdit(component, pointIndex) {
        let props = component.properties;
        let points = this._getEditablePoints(props);
        const index = Number.parseInt(pointIndex);

        if (points.length <= 2 || !Number.isInteger(index) || index < 0 || index >= points.length) {
            return null;
        }

        points.splice(index, 1);
        props.pointCount = points.length;
        props.points = this._formatPercentPoints(points);
        return { properties: this._pointProperties(props) };
    }

    // Returns all persisted polyline properties changed by point operations.
    _pointProperties(props) {
        return {
            pointCount: props.pointCount,
            points: props.points
        };
    }
};

// Registers every shape renderer with the shared mimic renderer set.
function registerShapeRenderers() {
    let componentRenderers = rs.mimic.RendererSet.componentRenderers;
    componentRenderers.set("ShapeRectangle", new rs.mimic.ShapeRectangleRenderer());
    componentRenderers.set("ShapeSquare", new rs.mimic.ShapeSquareRenderer());
    componentRenderers.set("ShapeEllipse", new rs.mimic.ShapeEllipseRenderer());
    componentRenderers.set("ShapeCircle", new rs.mimic.ShapeCircleRenderer());
    componentRenderers.set("ShapeRoundedRect", new rs.mimic.ShapeRoundedRectRenderer());
    componentRenderers.set("ShapePolygon", new rs.mimic.ShapePolygonRenderer());
    componentRenderers.set("ShapeTriangle", new rs.mimic.ShapeTriangleRenderer());
    componentRenderers.set("ShapeDiamond", new rs.mimic.ShapeDiamondRenderer());
    componentRenderers.set("ShapeHexagon", new rs.mimic.ShapeHexagonRenderer());
    componentRenderers.set("ShapeParallelogram", new rs.mimic.ShapeParallelogramRenderer());
    componentRenderers.set("ShapeTrapezoid", new rs.mimic.ShapeTrapezoidRenderer());
    componentRenderers.set("ShapeCross", new rs.mimic.ShapeCrossRenderer());
    componentRenderers.set("ShapeHalfCircle", new rs.mimic.ShapeHalfCircleRenderer());
    componentRenderers.set("ShapeDonut", new rs.mimic.ShapeDonutRenderer());
    componentRenderers.set("ShapePie", new rs.mimic.ShapePieRenderer());
    componentRenderers.set("ShapeArrow", new rs.mimic.ShapeArrowRenderer());
    componentRenderers.set("ShapeLine", new rs.mimic.ShapeLineRenderer());
    componentRenderers.set("ShapePolyline", new rs.mimic.ShapePolylineRenderer());
}

registerShapeRenderers();
