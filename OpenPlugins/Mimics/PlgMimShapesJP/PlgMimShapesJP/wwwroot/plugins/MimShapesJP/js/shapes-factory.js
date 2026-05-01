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

// Polyline временно отключен - будет доработан после добавления точек привязки в редакторе
//rs.mimic.ShapePolylineFactory = class extends rs.mimic.ShapeLineFactory {
//    createProperties() {
//        let props = super.createProperties();
//        props.size.width = 160;
//        props.size.height = 100;
//        props.pointCount = 4;
//        props.snapToAxis = true;
//        props.snapThreshold = 10;
//        props.points = "0,80 35,20 70,70 100,10";
//        return props;
//    }
//
//    parseProperties(sourceProps) {
//        const PropertyParser = rs.mimic.PropertyParser;
//        let props = super.parseProperties(sourceProps);
//        sourceProps ??= {};
//        props.pointCount = PropertyParser.parseInt(sourceProps.pointCount);
//        if (!Number.isFinite(props.pointCount)) {
//            props.pointCount = 4;
//        }
//        props.snapToAxis = PropertyParser.parseBool(sourceProps.snapToAxis, true);
//        props.snapThreshold = PropertyParser.parseInt(sourceProps.snapThreshold);
//        if (!Number.isFinite(props.snapThreshold)) {
//            props.snapThreshold = 10;
//        }
//        props.points = PropertyParser.parseString(sourceProps.points) || "0,80 35,20 70,70 100,10";
//        return props;
//    }
//
//    createComponent() {
//        return rs.mimic.RegularComponentFactory.prototype.createComponent.call(this, "ShapePolyline");
//    }
//};

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
    //componentFactories.set("ShapePolyline", new rs.mimic.ShapePolylineFactory());
}

registerShapeFactories();
