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
