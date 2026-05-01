// Contains subtypes for shape components.

rs.mimic = rs.mimic ?? {};

rs.mimic.ShapeSubtype = class {
    static POLYGON_POINT_MODE = "ShapePolygonPointMode";
    static LINE_ORIENTATION = "ShapeLineOrientation";
    static ARROW_DIRECTION = "ShapeArrowDirection";
    static STAR_POINTS = "ShapeStarPoints";
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
    constructor() {
        super();
        configureShapeDescriptor(this, false);
    }
};

rs.mimic.ShapeSquareDescriptor = class extends rs.mimic.ShapeRectangleDescriptor {};
rs.mimic.ShapeEllipseDescriptor = class extends rs.mimic.ShapeRectangleDescriptor {};
rs.mimic.ShapeCircleDescriptor = class extends rs.mimic.ShapeRectangleDescriptor {};

rs.mimic.ShapeRoundedRectDescriptor = class extends rs.mimic.RegularComponentDescriptor {
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
    constructor() {
        super();
        configureShapeDescriptor(this, false);
    }
};

rs.mimic.ShapeDiamondDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    constructor() {
        super();
        configureShapeDescriptor(this, false);
    }
};

rs.mimic.ShapeHexagonDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    constructor() {
        super();
        configureShapeDescriptor(this, false);
    }
};

rs.mimic.ShapeParallelogramDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    constructor() {
        super();
        configureShapeDescriptor(this, false);
    }
};

rs.mimic.ShapeTrapezoidDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    constructor() {
        super();
        configureShapeDescriptor(this, false);
    }
};

rs.mimic.ShapeCrossDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    constructor() {
        super();
        configureShapeDescriptor(this, false);
    }
};

rs.mimic.ShapeHalfCircleDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    constructor() {
        super();
        configureShapeDescriptor(this, false);
    }
};

rs.mimic.ShapeDonutDescriptor = class extends rs.mimic.RegularComponentDescriptor {
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

rs.mimic.ShapeStarDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    constructor() {
        super();
        const KnownCategory = rs.mimic.KnownCategory;
        const BasicType = rs.mimic.BasicType;
        const PropertyDescriptor = rs.mimic.PropertyDescriptor;
        configureShapeDescriptor(this, false);

        this.add(new PropertyDescriptor({
            name: "pointCount",
            displayName: "Point count",
            category: KnownCategory.APPEARANCE,
            type: BasicType.INT
        }));

        this.add(new PropertyDescriptor({
            name: "innerRadius",
            displayName: "Inner radius",
            category: KnownCategory.APPEARANCE,
            type: BasicType.INT
        }));
    }
};

rs.mimic.ShapeArrowDescriptor = class extends rs.mimic.RegularComponentDescriptor {
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

// Polyline - работает без точек привязки. Код точек привязки закомментирован ниже.
rs.mimic.ShapePolylineDescriptor = class extends rs.mimic.RegularComponentDescriptor {
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
    componentDescriptors.set("ShapeStar", new rs.mimic.ShapeStarDescriptor());
    componentDescriptors.set("ShapeArrow", new rs.mimic.ShapeArrowDescriptor());
    componentDescriptors.set("ShapeLine", new rs.mimic.ShapeLineDescriptor());
    componentDescriptors.set("ShapePolyline", new rs.mimic.ShapePolylineDescriptor());
}

registerShapeDescriptors();
// Contains factories for shape components.

rs.mimic.ShapeFactoryBase = class extends rs.mimic.RegularComponentFactory {
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
        if (!Number.isFinite(props.strokeWidth)) {
            props.strokeWidth = 1;
        }
        if (!Number.isFinite(props.imageOpacity)) {
            props.imageOpacity = 100;
        }
        if (!Number.isFinite(props.opacity)) {
            props.opacity = 100;
        }
        if (!Number.isFinite(props.rotation)) {
            props.rotation = 0;
        }
    }

    createProperties() {
        let props = super.createProperties();
        this._setShapeDefaults(props);
        return props;
    }

    parseProperties(sourceProps) {
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        this._parseShapeProperties(props, sourceProps);
        return props;
    }
};

rs.mimic.ShapeRectangleFactory = class extends rs.mimic.ShapeFactoryBase {
    createComponent() {
        return super.createComponent("ShapeRectangle");
    }
};

rs.mimic.ShapeSquareFactory = class extends rs.mimic.ShapeFactoryBase {
    createProperties() {
        let props = super.createProperties();
        props.size.width = 80;
        props.size.height = 80;
        return props;
    }

    createComponent() {
        return super.createComponent("ShapeSquare");
    }
};

rs.mimic.ShapeEllipseFactory = class extends rs.mimic.ShapeFactoryBase {
    createComponent() {
        return super.createComponent("ShapeEllipse");
    }
};

rs.mimic.ShapeCircleFactory = class extends rs.mimic.ShapeFactoryBase {
    createProperties() {
        let props = super.createProperties();
        props.size.width = 80;
        props.size.height = 80;
        return props;
    }

    createComponent() {
        return super.createComponent("ShapeCircle");
    }
};

rs.mimic.ShapeRoundedRectFactory = class extends rs.mimic.ShapeFactoryBase {
    createProperties() {
        let props = super.createProperties();
        props.borderRadius = 10;
        return props;
    }

    parseProperties(sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        props.borderRadius = PropertyParser.parseInt(sourceProps.borderRadius);
        if (!Number.isFinite(props.borderRadius)) {
            props.borderRadius = 10;
        }
        return props;
    }

    createComponent() {
        return super.createComponent("ShapeRoundedRect");
    }
};

rs.mimic.ShapePolygonFactory = class extends rs.mimic.ShapeFactoryBase {
    createProperties() {
        let props = super.createProperties();
        props.size.width = 120;
        props.size.height = 100;
        props.pointMode = rs.mimic.ShapePolygonPointMode.AUTO;
        props.pointCount = 5;
        props.points = "50,0 100,38 82,100 18,100 0,38";
        return props;
    }

    parseProperties(sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        props.pointMode = PropertyParser.parseString(sourceProps.pointMode) || rs.mimic.ShapePolygonPointMode.AUTO;
        props.pointCount = PropertyParser.parseInt(sourceProps.pointCount);
        if (!Number.isFinite(props.pointCount)) {
            props.pointCount = 5;
        }
        props.points = PropertyParser.parseString(sourceProps.points) || "50,0 100,38 82,100 18,100 0,38";
        return props;
    }

    createComponent() {
        return super.createComponent("ShapePolygon");
    }
};

rs.mimic.ShapeTriangleFactory = class extends rs.mimic.ShapeFactoryBase {
    createComponent() {
        return super.createComponent("ShapeTriangle");
    }
};

rs.mimic.ShapeDiamondFactory = class extends rs.mimic.ShapeFactoryBase {
    createComponent() {
        return super.createComponent("ShapeDiamond");
    }
};

rs.mimic.ShapeHexagonFactory = class extends rs.mimic.ShapeFactoryBase {
    createComponent() {
        return super.createComponent("ShapeHexagon");
    }
};

rs.mimic.ShapeParallelogramFactory = class extends rs.mimic.ShapeFactoryBase {
    createComponent() {
        return super.createComponent("ShapeParallelogram");
    }
};

rs.mimic.ShapeTrapezoidFactory = class extends rs.mimic.ShapeFactoryBase {
    createComponent() {
        return super.createComponent("ShapeTrapezoid");
    }
};

rs.mimic.ShapeCrossFactory = class extends rs.mimic.ShapeFactoryBase {
    createComponent() {
        return super.createComponent("ShapeCross");
    }
};

rs.mimic.ShapeHalfCircleFactory = class extends rs.mimic.ShapeFactoryBase {
    createComponent() {
        return super.createComponent("ShapeHalfCircle");
    }
};

rs.mimic.ShapeDonutFactory = class extends rs.mimic.ShapeFactoryBase {
    createProperties() {
        let props = super.createProperties();
        props.holeSize = 30;
        return props;
    }

    parseProperties(sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        props.holeSize = PropertyParser.parseInt(sourceProps.holeSize);
        if (!Number.isFinite(props.holeSize)) {
            props.holeSize = 30;
        }
        return props;
    }

    createComponent() {
        return super.createComponent("ShapeDonut");
    }
};

rs.mimic.ShapePieFactory = class extends rs.mimic.ShapeFactoryBase {
    createProperties() {
        let props = super.createProperties();
        props.startAngle = 0;
        props.sweepAngle = 90;
        return props;
    }

    parseProperties(sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        props.startAngle = PropertyParser.parseInt(sourceProps.startAngle);
        props.sweepAngle = PropertyParser.parseInt(sourceProps.sweepAngle);
        if (!Number.isFinite(props.startAngle)) props.startAngle = 0;
        if (!Number.isFinite(props.sweepAngle)) props.sweepAngle = 90;
        return props;
    }

    createComponent() {
        return super.createComponent("ShapePie");
    }
};

rs.mimic.ShapeStarFactory = class extends rs.mimic.ShapeFactoryBase {
    createProperties() {
        let props = super.createProperties();
        props.size.width = 120;
        props.size.height = 120;
        props.pointCount = 5;
        props.innerRadius = 25;
        return props;
    }

    parseProperties(sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        props.pointCount = PropertyParser.parseInt(sourceProps.pointCount);
        props.innerRadius = PropertyParser.parseInt(sourceProps.innerRadius);
        if (!Number.isFinite(props.pointCount)) props.pointCount = 5;
        if (!Number.isFinite(props.innerRadius)) props.innerRadius = 25;
        return props;
    }

    createComponent() {
        return super.createComponent("ShapeStar");
    }
};

rs.mimic.ShapeArrowFactory = class extends rs.mimic.ShapeFactoryBase {
    createProperties() {
        let props = super.createProperties();
        props.size.width = 120;
        props.size.height = 80;
        props.direction = rs.mimic.ShapeArrowDirection.RIGHT;
        return props;
    }

    parseProperties(sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        props.direction = PropertyParser.parseString(sourceProps.direction) || rs.mimic.ShapeArrowDirection.RIGHT;
        return props;
    }

    createComponent() {
        return super.createComponent("ShapeArrow");
    }
};

rs.mimic.ShapeLineFactory = class extends rs.mimic.RegularComponentFactory {
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

    parseProperties(sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        props.strokeColor = PropertyParser.parseString(sourceProps.strokeColor) || "Black";
        props.strokeWidth = PropertyParser.parseInt(sourceProps.strokeWidth);
        props.strokeDasharray = PropertyParser.parseString(sourceProps.strokeDasharray) || "";
        props.opacity = PropertyParser.parseInt(sourceProps.opacity);
        props.rotation = PropertyParser.parseInt(sourceProps.rotation);
        if (!Number.isFinite(props.strokeWidth)) {
            props.strokeWidth = 2;
        }
        if (!Number.isFinite(props.opacity)) {
            props.opacity = 100;
        }
        if (!Number.isFinite(props.rotation)) {
            props.rotation = 0;
        }
        props.orientation = PropertyParser.parseString(sourceProps.orientation) || rs.mimic.ShapeLineOrientation.DIAGONAL;
        props.x1 = PropertyParser.parseInt(sourceProps.x1);
        props.y1 = PropertyParser.parseInt(sourceProps.y1);
        props.x2 = PropertyParser.parseInt(sourceProps.x2);
        props.y2 = PropertyParser.parseInt(sourceProps.y2);
        if (!Number.isFinite(props.x1)) props.x1 = 0;
        if (!Number.isFinite(props.y1)) props.y1 = 100;
        if (!Number.isFinite(props.x2)) props.x2 = 100;
        if (!Number.isFinite(props.y2)) props.y2 = 0;
        return props;
    }

    createComponent() {
        return super.createComponent("ShapeLine");
    }
};

// Polyline - работает без точек привязки. Код точек привязки закомментирован ниже.
rs.mimic.ShapePolylineFactory = class extends rs.mimic.ShapeLineFactory {
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

    parseProperties(sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        props.pointCount = PropertyParser.parseInt(sourceProps.pointCount);
        if (!Number.isFinite(props.pointCount)) {
            props.pointCount = 4;
        }
        props.snapToAxis = PropertyParser.parseBool(sourceProps.snapToAxis, true);
        props.snapThreshold = PropertyParser.parseInt(sourceProps.snapThreshold);
        if (!Number.isFinite(props.snapThreshold)) {
            props.snapThreshold = 10;
        }
        props.points = PropertyParser.parseString(sourceProps.points) || "0,80 35,20 70,70 100,10";
        return props;
    }

    createComponent() {
        return rs.mimic.RegularComponentFactory.prototype.createComponent.call(this, "ShapePolyline");
    }
};

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
    componentFactories.set("ShapeStar", new rs.mimic.ShapeStarFactory());
    componentFactories.set("ShapeArrow", new rs.mimic.ShapeArrowFactory());
    componentFactories.set("ShapeLine", new rs.mimic.ShapeLineFactory());
    componentFactories.set("ShapePolyline", new rs.mimic.ShapePolylineFactory());
}

registerShapeFactories();
// Contains renderers for shape components.

rs.mimic.ShapeRendererBase = class extends rs.mimic.RegularComponentRenderer {
    _setBorder(jqObj, border) {
        jqObj.css("border", "");
    }

    _completeDom(componentElem, component, renderContext) {
        componentElem.append("<svg class='shape-svg' xmlns='http://www.w3.org/2000/svg' preserveAspectRatio='none'></svg>" +
            "<div class='shape-resize-handles'>" +
            "<span class='nw'></span><span class='n'></span><span class='ne'></span>" +
            "<span class='e'></span><span class='se'></span><span class='s'></span>" +
            "<span class='sw'></span><span class='w'></span></div>");
    }

    _setClasses(componentElem, component, renderContext) {
        super._setClasses(componentElem, component, renderContext);
        componentElem.addClass("shape-comp");
    }

    _imageHref(renderContext, imageName) {
        return imageName && renderContext.getImage ? renderContext.getImage(imageName) : "";
    }

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

    _createBackground(width, height, color) {
        return $(document.createElementNS("http://www.w3.org/2000/svg", "rect"))
            .attr("x", 0)
            .attr("y", 0)
            .attr("width", width)
            .attr("height", height)
            .attr("fill", color || "transparent");
    }

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

    _pointsToSvg(points, width, height) {
        return points.map(p => (p.x / 100 * width) + "," + (p.y / 100 * height)).join(" ");
    }

    // JP добавлено начало (закомментировано) //
    // _clampPercent(value) {
    //     return Math.max(0, Math.min(100, Math.round(value)));
    // }
    // 
    // _formatPercentPoints(points) {
    //     return points
    //         .map(p => this._clampPercent(p.x) + "," + this._clampPercent(p.y))
    //         .join(" ");
    // }
    // 
    // _mimicPointToComponentPercent(component, mimicPoint) {
    //     let width = Math.max(1, component.width);
    //     let height = Math.max(1, component.height);
    //     return {
    //         x: this._clampPercent((mimicPoint.x - component.x) / width * 100),
    //         y: this._clampPercent((mimicPoint.y - component.y) / height * 100)
    //     };
    // }
    // 
    // _componentPointToPercent(component, componentPoint) {
    //     let width = Math.max(1, component.width);
    //     let height = Math.max(1, component.height);
    //     return {
    //         x: this._clampPercent(componentPoint.x / width * 100),
    //         y: this._clampPercent(componentPoint.y / height * 100)
    //     };
    // }
    // 
    // _renderPointHandles(componentElem, component, points) {
    //     let handlesElem = componentElem.children(".shape-point-handles:first");
    // 
    //     if (handlesElem.length === 0) {
    //         handlesElem = $("<div class='shape-point-handles'></div>").appendTo(componentElem);
    //     }
    // 
    //     handlesElem.empty();
    // 
    //     if (!component.isSelected || !Array.isArray(points)) {
    //         return;
    //     }
    // 
    //     for (let i = 0; i < points.length; i++) {
    //         $("<span class='jp-point-handle'></span>")
    //             .attr("title", "Point " + (i + 1))
    //             .attr("data-point-index", i)
    //             .css({
    //                 left: points[i].x + "%",
    //                 top: points[i].y + "%"
    //             })
    //             .appendTo(handlesElem);
    //     }
    // }
    // JP добавлено конец (закомментировано) //

    _setProps(componentElem, component, renderContext) {
        super._setProps(componentElem, component, renderContext);
        component._shapeRenderContext = renderContext;
        this._applyTransform(componentElem, component.properties);
        this._renderShape(componentElem, component, renderContext);
    }

    setSize(component, width, height) {
        super.setSize(component, width, height);
        this._renderShape(component.dom, component, component._shapeRenderContext || {});
    }
};

rs.mimic.ShapeRectangleRenderer = class extends rs.mimic.ShapeRendererBase {
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
    setSize(component, width, height) {
        let side = Math.max(width, height);
        super.setSize(component, side, side);
    }

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
    setSize(component, width, height) {
        let side = Math.max(width, height);
        super.setSize(component, side, side);
    }

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

    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let polygon = $(document.createElementNS("http://www.w3.org/2000/svg", "polygon"))
            .attr("points", this._normalizePoints(props, size.width, size.height));
        this._applyPaint(polygon, props);
        svg.append(polygon);
        // JP добавлено начало (закомментировано) //
        // this._renderPointHandles(componentElem, component, this._getEditablePoints(props));
        // JP добавлено конец (закомментировано) //
    }

    // JP добавлено начало (закомментировано) //
    // _getEditablePoints(props) {
    //     let pointCount = Math.max(2, Math.min(12, Number.parseInt(props.pointCount) || 5));
    //     let points = props.pointMode === rs.mimic.ShapePolygonPointMode.CUSTOM
    //         ? this._parsePercentPoints(props.points)
    //         : this._autoPoints(pointCount);
    //     let defaults = this._autoPoints(pointCount);
    // 
    //     if (points.length < 2) {
    //         points = defaults;
    //     }
    // 
    //     while (points.length < pointCount) {
    //         points.push(defaults[points.length]);
    //     }
    // 
    //     return points.slice(0, pointCount);
    // }
    // 
    // startPointEdit(component, pointIndex, mimicPoint) {
    //     let props = component.properties;
    //     let points = this._getEditablePoints(props);
    // 
    //     props.pointMode = rs.mimic.ShapePolygonPointMode.CUSTOM;
    //     props.pointCount = points.length;
    //     props.points = this._formatPercentPoints(points);
    // 
    //     return {
    //         component,
    //         pointIndex: Number.parseInt(pointIndex),
    //         propertyName: "points",
    //         oldValue: props.points,
    //         moved: false
    //     };
    // }
    // 
    // movePointEdit(component, action, mimicPoint) {
    //     let props = component.properties;
    //     let points = this._getEditablePoints(props);
    //     let index = Math.max(0, Math.min(points.length - 1, action.pointIndex));
    //     points[index] = this._mimicPointToComponentPercent(component, mimicPoint);
    //     props.pointMode = rs.mimic.ShapePolygonPointMode.CUSTOM;
    //     props.points = this._formatPercentPoints(points);
    //     return true;
    // }
    // 
    // finishPointEdit(component, action) {
    //     return {
    //         properties: {
    //             pointMode: component.properties.pointMode,
    //             pointCount: component.properties.pointCount,
    //             points: component.properties.points
    //         }
    //     };
    // }
    // 
    // addPointEdit(component, componentPoint) {
    //     let props = component.properties;
    //     let points = this._getEditablePoints(props);
    // 
    //     if (points.length >= 12) {
    //         return null;
    //     }
    // 
    //     points.push(this._componentPointToPercent(component, componentPoint));
    //     props.pointMode = rs.mimic.ShapePolygonPointMode.CUSTOM;
    //     props.pointCount = points.length;
    //     props.points = this._formatPercentPoints(points);
    // 
    //     return {
    //         properties: {
    //             pointMode: props.pointMode,
    //             pointCount: props.pointCount,
    //             points: props.points
    //         }
    //     };
    // }
    // 
    // removePointEdit(component, pointIndex) {
    //     let props = component.properties;
    //     let points = this._getEditablePoints(props);
    // 
    //     if (points.length <= 2) {
    //         return null;
    //     }
    // 
    //     points.splice(Number.parseInt(pointIndex), 1);
    //     props.pointMode = rs.mimic.ShapePolygonPointMode.CUSTOM;
    //     props.pointCount = points.length;
    //     props.points = this._formatPercentPoints(points);
    // 
    //     return {
    //         properties: {
    //             pointMode: props.pointMode,
    //             pointCount: props.pointCount,
    //             points: props.points
    //         }
    //     };
    // }
    // JP добавлено конец (закомментировано) //
};

rs.mimic.ShapeTriangleRenderer = class extends rs.mimic.ShapeRendererBase {
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
    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let w = size.width;
        let h = size.height;
        let qw = w / 4;
        let hw = w / 2;
        let qh = h / 4;
        let hh = h / 2;
        let polygon = $(document.createElementNS("http://www.w3.org/2000/svg", "polygon"))
            .attr("points", qw + ",0 " + (w - qw) + ",0 " + w + "," + hh + " " +
                (w - qw) + "," + h + " " + qw + "," + h + " 0," + hh);
        this._applyPaint(polygon, props);
        svg.append(polygon);
    }
};

rs.mimic.ShapeParallelogramRenderer = class extends rs.mimic.ShapeRendererBase {
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
    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let w = size.width;
        let h = size.height;
        let cx = w / 2;
        let cy = h / 2;
        let r = Math.min(w, h) / 2;
        let startAngle = (Number.parseInt(props.startAngle) || 0) * Math.PI / 180;
        let sweepAngle = (Number.parseInt(props.sweepAngle) || 90) * Math.PI / 180;
        let endAngle = startAngle + sweepAngle;

        let x1 = cx + r * Math.cos(startAngle);
        let y1 = cy + r * Math.sin(startAngle);
        let x2 = cx + r * Math.cos(endAngle);
        let y2 = cy + r * Math.sin(endAngle);
        let largeArc = sweepAngle > Math.PI ? 1 : 0;

        let path = $(document.createElementNS("http://www.w3.org/2000/svg", "path"))
            .attr("d", "M " + cx + "," + cy + " L " + x1 + "," + y1 +
                " A " + r + "," + r + " 0 " + largeArc + ",1 " + x2 + "," + y2 + " Z");
        this._applyPaint(path, props);
        svg.append(path);
    }
};

rs.mimic.ShapeStarRenderer = class extends rs.mimic.ShapeRendererBase {
    _starPoints(props, width, height) {
        let pointCount = Math.max(3, Math.min(12, Number.parseInt(props.pointCount) || 5));
        let innerRadius = Math.max(10, Math.min(50, Number.parseInt(props.innerRadius) || 40));
        let outerRadius = 50;
        let cx = 50;
        let cy = 50;
        let points = [];

        for (let i = 0; i < pointCount * 2; i++) {
            let angle = -Math.PI / 2 + i * Math.PI / pointCount;
            let r = i % 2 === 0 ? outerRadius : innerRadius;
            points.push({
                x: cx + r * Math.cos(angle),
                y: cy + r * Math.sin(angle)
            });
        }

        return this._pointsToSvg(points, width, height);
    }

    _renderShape(componentElem, component, renderContext) {
        let props = component.properties;
        let svg = componentElem.children("svg.shape-svg:first");
        let size = this._setCommonSvg(svg, component, renderContext);
        let polygon = $(document.createElementNS("http://www.w3.org/2000/svg", "polygon"))
            .attr("points", this._starPoints(props, size.width, size.height));
        this._applyPaint(polygon, props);
        svg.append(polygon);
    }
};

rs.mimic.ShapeArrowRenderer = class extends rs.mimic.ShapeRendererBase {
    _arrowPoints(props, width, height) {
        const ShapeArrowDirection = rs.mimic.ShapeArrowDirection;
        let dir = props.direction || ShapeArrowDirection.RIGHT;
        let w = width;
        let h = height;
        let shaftW = Math.max(1, w * 0.5);
        let shaftH = Math.max(1, h * 0.4);
        let headW = Math.max(1, w * 0.5);
        let headH = Math.max(1, h * 0.5);

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
                return ((w - shaftH) / 2) + "," + h + " " + ((w - shaftH) / 2) + "," + headH + " " +
                    "0," + headH + " " + (w / 2) + ",0 " +
                    w + "," + headH + " " + ((w + shaftH) / 2) + "," + headH + " " +
                    ((w + shaftH) / 2) + "," + h;
            case ShapeArrowDirection.DOWN:
                return ((w - shaftH) / 2) + ",0 " + ((w - shaftH) / 2) + "," + (h - headH) + " " +
                    "0," + (h - headH) + " " + (w / 2) + "," + h + " " +
                    w + "," + (h - headH) + " " + ((w + shaftH) / 2) + "," + (h - headH) + " " +
                    ((w + shaftH) / 2) + ",0";
            default:
                return "0," + ((h - shaftH) / 2) + " " + (w - headW) + "," + ((h - shaftH) / 2) + " " +
                    (w - headW) + ",0 " + w + "," + (h / 2) + " " +
                    (w - headW) + "," + h + " " + (w - headW) + "," + ((h + shaftH) / 2) + " " +
                    "0," + ((h + shaftH) / 2);
        }
    }

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
    _setCommonSvg(svg, component, renderContext) {
        let props = component.properties;
        let width = Math.max(1, component.innerWidth || component.width || props.size.width);
        let height = Math.max(1, component.innerHeight || component.height || props.size.height);
        svg.attr("viewBox", "0 0 " + width + " " + height);
        svg.empty();
        return { width, height };
    }

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
        // JP добавлено начало (закомментировано) //
        // this._renderPointHandles(componentElem, component, [
        //     { x: p.x1, y: p.y1 },
        //     { x: p.x2, y: p.y2 }
        // ]);
        // JP добавлено конец (закомментировано) //
    }

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

    // JP добавлено начало (закомментировано) //
    // startPointEdit(component, pointIndex, mimicPoint) {
    //     component.properties.orientation = rs.mimic.ShapeLineOrientation.CUSTOM;
    //     return {
    //         component,
    //         pointIndex: Number.parseInt(pointIndex),
    //         propertyName: "orientation",
    //         oldValue: component.properties.orientation,
    //         moved: false
    //     };
    // }
    // 
    // movePointEdit(component, action, mimicPoint) {
    //     let point = this._mimicPointToComponentPercent(component, mimicPoint);
    // 
    //     if (action.pointIndex === 0) {
    //         component.properties.x1 = point.x;
    //         component.properties.y1 = point.y;
    //     } else {
    //         component.properties.x2 = point.x;
    //         component.properties.y2 = point.y;
    //     }
    // 
    //     component.properties.orientation = rs.mimic.ShapeLineOrientation.CUSTOM;
    //     return true;
    // }
    // 
    // finishPointEdit(component, action) {
    //     return {
    //         properties: {
    //             orientation: component.properties.orientation,
    //             x1: component.properties.x1,
    //             y1: component.properties.y1,
    //             x2: component.properties.x2,
    //             y2: component.properties.y2
    //         }
    //     };
    // }
    // JP добавлено конец (закомментировано) //
};

// Polyline - работает без точек привязки. Код точек привязки закомментирован ниже.
rs.mimic.ShapePolylineRenderer = class extends rs.mimic.ShapeLineRenderer {
    _defaultPoints(pointCount) {
        pointCount = Math.max(2, Math.min(12, Number.parseInt(pointCount) || 4));
        let result = [];
        for (let i = 0; i < pointCount; i++) {
            let x = pointCount === 1 ? 0 : i * 100 / (pointCount - 1);
            let y = i % 2 === 0 ? 75 : 25;
            result.push({ x, y });
        }
        return result;
    }

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

    _normalizePoints(props, width, height) {
        let pointCount = Math.max(2, Math.min(12, Number.parseInt(props.pointCount) || 4));
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
        // this._renderPointHandles(componentElem, component, this._getEditablePoints(props));
    }

    // _getEditablePoints(props) {
    //     let pointCount = Math.max(2, Math.min(12, Number.parseInt(props.pointCount) || 4));
    //     let points = this._parsePercentPoints(props.points);
    //     let defaults = this._defaultPoints(pointCount);
    //
    //     if (points.length < 2) {
    //         points = defaults;
    //     }
    //
    //     while (points.length < pointCount) {
    //         points.push(defaults[points.length]);
    //     }
    //
    //     points = points.slice(0, pointCount);
    //
    //     if (props.snapToAxis) {
    //         points = this._snapPointsToAxis(points, props.snapThreshold);
    //     }
    //
    //     return points;
    // }
    //
    // startPointEdit(component, pointIndex, mimicPoint) {
    //     return {
    //         component,
    //         pointIndex: Number.parseInt(pointIndex),
    //         propertyName: "points",
    //         oldValue: component.properties.points,
    //         moved: false
    //     };
    // }
    //
    // movePointEdit(component, action, mimicPoint) {
    //     let props = component.properties;
    //     let points = this._getEditablePoints(props);
    //     let index = Math.max(0, Math.min(points.length - 1, action.pointIndex));
    //     points[index] = this._mimicPointToComponentPercent(component, mimicPoint);
    //
    //     if (props.snapToAxis) {
    //         points = this._snapPointsToAxis(points, props.snapThreshold);
    //     }
    //
    //     props.pointCount = points.length;
    //     props.points = this._formatPercentPoints(points);
    //     return true;
    // }
    //
    // finishPointEdit(component, action) {
    //     return {
    //         properties: {
    //             pointCount: component.properties.pointCount,
    //             points: component.properties.points
    //         }
    //     };
    // }
    //
    // addPointEdit(component, componentPoint) {
    //     let props = component.properties;
    //     let points = this._getEditablePoints(props);
    //
    //     if (points.length >= 12) {
    //         return null;
    //     }
    //
    //     points.push(this._componentPointToPercent(component, componentPoint));
    //
    //     if (props.snapToAxis) {
    //         points = this._snapPointsToAxis(points, props.snapThreshold);
    //     }
    //
    //     props.pointCount = points.length;
    //     props.points = this._formatPercentPoints(points);
    //
    //     return {
    //         properties: {
    //             pointCount: props.pointCount,
    //             points: props.points
    //         }
    //     };
    // }
    //
    // removePointEdit(component, pointIndex) {
    //     let props = component.properties;
    //     let points = this._getEditablePoints(props);
    //
    //     if (points.length <= 2) {
    //         return null;
    //     }
    //
    //     points.splice(Number.parseInt(pointIndex), 1);
    //     props.pointCount = points.length;
    //     props.points = this._formatPercentPoints(points);
    //
    //     return {
    //         properties: {
    //             pointCount: props.pointCount,
    //             points: props.points
    //         }
    //     };
    // }
};

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
    componentRenderers.set("ShapeStar", new rs.mimic.ShapeStarRenderer());
    componentRenderers.set("ShapeArrow", new rs.mimic.ShapeArrowRenderer());
    componentRenderers.set("ShapeLine", new rs.mimic.ShapeLineRenderer());
    componentRenderers.set("ShapePolyline", new rs.mimic.ShapePolylineRenderer());
}

registerShapeRenderers();
