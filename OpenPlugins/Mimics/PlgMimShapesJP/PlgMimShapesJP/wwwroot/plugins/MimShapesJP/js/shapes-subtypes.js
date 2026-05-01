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
