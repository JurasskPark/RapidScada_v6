// Registers plugin-owned property and enumeration translations in the editor model.
(() => {
    const commonShapeTypes = [
        "ShapeRectangle",
        "ShapeSquare",
        "ShapeEllipse",
        "ShapeCircle",
        "ShapeTriangle",
        "ShapeDiamond",
        "ShapeHexagon",
        "ShapeParallelogram",
        "ShapeTrapezoid",
        "ShapeCross",
        "ShapeHalfCircle"
    ];
    let registrationTimer = 0;

    // Returns a translated component phrase after the host dictionary is ready.
    function translate(key) {
        const api = globalThis.ScadaAdminWebJP?.mimicEditor;
        const value = api && typeof api.tr === "function" ? api.tr(key) : "";
        return value && value !== key ? value : "";
    }

    // Builds a property dictionary in the format expected by the editor model.
    function buildProperties(entries) {
        const properties = {};

        for (const [propertyName, phraseKey] of entries) {
            const displayName = translate(phraseKey);

            if (!displayName) {
                return null;
            }

            properties[propertyName] = displayName;
        }

        return properties;
    }

    // Builds an option map in the format expected by Tweakpane.
    function buildOptions(entries) {
        const options = {};

        for (const [phraseKey, value] of entries) {
            const label = translate(phraseKey);

            if (!label) {
                return null;
            }

            options[label] = value;
        }

        return options;
    }

    // Builds the complete client dictionary from component-owned XML phrases.
    function buildDictionary() {
        const shapeProperties = buildProperties([
            ["fillColor", "ShapePropertyFillColor"],
            ["strokeColor", "ShapePropertyStrokeColor"],
            ["strokeWidth", "ShapePropertyStrokeWidth"],
            ["strokeDasharray", "ShapePropertyStrokeDash"],
            ["opacity", "ShapePropertyOpacity"],
            ["rotation", "ShapePropertyRotation"],
            ["backgroundColor", "ShapePropertyBackgroundColor"],
            ["imageName", "ShapePropertyImage"],
            ["imageOpacity", "ShapePropertyImageOpacity"]
        ]);
        const lineProperties = buildProperties([
            ["strokeColor", "ShapePropertyLineColor"],
            ["strokeWidth", "ShapePropertyLineWidth"],
            ["strokeDasharray", "ShapePropertyLineDash"],
            ["opacity", "ShapePropertyOpacity"],
            ["rotation", "ShapePropertyRotation"]
        ]);

        if (!shapeProperties || !lineProperties) {
            return null;
        }

        const componentProperties = new Map();

        for (const typeName of commonShapeTypes) {
            componentProperties.set(typeName, shapeProperties);
        }

        const propertySets = {
            ShapeRoundedRect: [
                ["borderRadius", "ShapePropertyBorderRadius"]
            ],
            ShapePolygon: [
                ["points", "ShapePropertyPoints"],
                ["pointMode", "ShapePropertyPointMode"],
                ["pointCount", "ShapePropertyPointCount"]
            ],
            ShapeDonut: [
                ["holeSize", "ShapePropertyHoleSize"]
            ],
            ShapePie: [
                ["startAngle", "ShapePropertyStartAngle"],
                ["sweepAngle", "ShapePropertySweepAngle"]
            ],
            ShapeArrow: [
                ["direction", "ShapePropertyDirection"]
            ]
        };

        for (const [typeName, entries] of Object.entries(propertySets)) {
            const extraProperties = buildProperties(entries);

            if (!extraProperties) {
                return null;
            }

            componentProperties.set(typeName, { ...shapeProperties, ...extraProperties });
        }

        const lineExtras = buildProperties([
            ["orientation", "ShapePropertyOrientation"],
            ["x1", "ShapePropertyX1"],
            ["y1", "ShapePropertyY1"],
            ["x2", "ShapePropertyX2"],
            ["y2", "ShapePropertyY2"]
        ]);
        const polylineExtras = buildProperties([
            ["points", "ShapePropertyPoints"],
            ["pointCount", "ShapePropertyPointCount"],
            ["snapToAxis", "ShapePropertySnapToAxis"],
            ["snapThreshold", "ShapePropertySnapThreshold"]
        ]);

        if (!lineExtras || !polylineExtras) {
            return null;
        }

        componentProperties.set("ShapeLine", { ...lineProperties, ...lineExtras });
        componentProperties.set("ShapePolyline", { ...lineProperties, ...polylineExtras });

        const ShapePolygonPointMode = rs.mimic.ShapePolygonPointMode;
        const ShapeLineOrientation = rs.mimic.ShapeLineOrientation;
        const ShapeArrowDirection = rs.mimic.ShapeArrowDirection;
        const enumerationEntries = {
            ShapePolygonPointMode: [
                ["ShapeEnumPointModeAuto", ShapePolygonPointMode.AUTO],
                ["ShapeEnumPointModeCustom", ShapePolygonPointMode.CUSTOM]
            ],
            ShapeLineOrientation: [
                ["ShapeEnumLineOrientationDiagonal", ShapeLineOrientation.DIAGONAL],
                ["ShapeEnumLineOrientationHorizontal", ShapeLineOrientation.HORIZONTAL],
                ["ShapeEnumLineOrientationVertical", ShapeLineOrientation.VERTICAL],
                ["ShapeEnumLineOrientationCustom", ShapeLineOrientation.CUSTOM]
            ],
            ShapeArrowDirection: [
                ["ShapeEnumArrowDirectionRight", ShapeArrowDirection.RIGHT],
                ["ShapeEnumArrowDirectionLeft", ShapeArrowDirection.LEFT],
                ["ShapeEnumArrowDirectionUp", ShapeArrowDirection.UP],
                ["ShapeEnumArrowDirectionDown", ShapeArrowDirection.DOWN]
            ]
        };
        const enumerations = new Map();

        for (const [enumName, entries] of Object.entries(enumerationEntries)) {
            const options = buildOptions(entries);

            if (!options) {
                return null;
            }

            enumerations.set(enumName, options);
        }

        return { componentProperties, enumerations };
    }

    // Applies labels directly so descriptors remain localized after asynchronous loading.
    function applyDescriptorTranslation(dictionary) {
        const descriptors = globalThis.rs?.mimic?.DescriptorSet?.componentDescriptors;

        if (!(descriptors instanceof Map)) {
            return false;
        }

        for (const [typeName, properties] of dictionary.componentProperties) {
            const descriptor = descriptors.get(typeName);

            if (!descriptor?.propertyDescriptors) {
                continue;
            }

            for (const [propertyName, displayName] of Object.entries(properties)) {
                const propertyDescriptor = descriptor.get(propertyName);

                if (propertyDescriptor) {
                    propertyDescriptor.displayName = displayName;
                }
            }

            for (const propertyDescriptor of descriptor.propertyDescriptors.values()) {
                const options = dictionary.enumerations.get(propertyDescriptor.subtype);

                if (options) {
                    propertyDescriptor.tweakpaneOptions ??= {};
                    propertyDescriptor.tweakpaneOptions.options = options;
                }
            }
        }

        return true;
    }

    // Merges shape labels using the standard editor translation model contract.
    function mergeTranslation() {
        const translationModel = globalThis.translation?.model;

        if (!(translationModel?.components instanceof Map) ||
            !(translationModel?.enumerations instanceof Map) ||
            !globalThis.rs?.mimic?.ShapeSubtype) {
            return false;
        }

        const dictionary = buildDictionary();

        if (!dictionary) {
            return false;
        }

        for (const [typeName, properties] of dictionary.componentProperties) {
            translationModel.components.set(typeName, properties);
        }

        for (const [enumName, options] of dictionary.enumerations) {
            translationModel.enumerations.set(enumName, options);
        }

        applyDescriptorTranslation(dictionary);
        return true;
    }

    // Hooks the normal editor translation pass for any late editor initialization.
    function patchPropGridTranslation() {
        if (typeof PropGridHelper === "undefined" ||
            typeof PropGridHelper.translateDescriptors !== "function" ||
            PropGridHelper.__shapesTranslationPatched) {
            return false;
        }

        const originalTranslateDescriptors = PropGridHelper.translateDescriptors;
        PropGridHelper.translateDescriptors = function (translationModel) {
            mergeTranslation();
            return originalTranslateDescriptors.call(this, translationModel);
        };
        PropGridHelper.__shapesTranslationPatched = true;
        return true;
    }

    // Refreshes the currently visible object after asynchronous host localization completes.
    function refreshPropertyGrid() {
        if (typeof propGrid !== "undefined" && typeof propGrid?.refresh === "function") {
            propGrid.refresh();
        }
    }

    // Waits until both the editor model and component-owned host phrases are available.
    function registerWhenReady() {
        patchPropGridTranslation();

        if (mergeTranslation()) {
            return;
        }

        if (registrationTimer) {
            return;
        }

        registrationTimer = globalThis.setInterval(() => {
            patchPropGridTranslation();

            if (mergeTranslation()) {
                globalThis.clearInterval(registrationTimer);
                registrationTimer = 0;
            }
        }, 25);

        globalThis.setTimeout(() => {
            if (registrationTimer) {
                globalThis.clearInterval(registrationTimer);
                registrationTimer = 0;
            }
        }, 3000);
    }

    document.addEventListener("scada-i18n-loaded", () => {
        const helper = typeof PropGridHelper === "undefined" ? null : PropGridHelper;

        if (mergeTranslation() && typeof helper?.translateDescriptors === "function") {
            helper.translateDescriptors(globalThis.translation.model);
            refreshPropertyGrid();
        }
    });

    registerWhenReady();

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", registerWhenReady, { once: true, capture: true });
    }
})();
