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

// Polyline временно отключен - будет доработан после добавления точек привязки в редакторе
//rs.mimic.ShapePolylineDescriptor = class extends rs.mimic.RegularComponentDescriptor {
//    constructor() {
//        super();
//        const KnownCategory = rs.mimic.KnownCategory;
//        const BasicType = rs.mimic.BasicType;
//        const PropertyDescriptor = rs.mimic.PropertyDescriptor;
//        configureLineDescriptor(this, true);
//
//        this.add(new PropertyDescriptor({
//            name: "pointCount",
//            displayName: "Point count",
//            category: KnownCategory.APPEARANCE,
//            type: BasicType.INT
//        }));
//
//        this.add(new PropertyDescriptor({
//            name: "snapToAxis",
//            displayName: "Snap to axis",
//            category: KnownCategory.APPEARANCE,
//            type: BasicType.BOOL
//        }));
//
//        this.add(new PropertyDescriptor({
//            name: "snapThreshold",
//            displayName: "Snap threshold",
//            category: KnownCategory.APPEARANCE,
//            type: BasicType.INT
//        }));
//    }
//};

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
    //componentDescriptors.set("ShapePolyline", new rs.mimic.ShapePolylineDescriptor());
}

registerShapeDescriptors();
