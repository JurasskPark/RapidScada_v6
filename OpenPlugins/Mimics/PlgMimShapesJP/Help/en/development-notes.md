# PlgMimShapesJP — Development Notes

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/development-notes.md)

## Point Handles (Anchor Points)

PlgMimicJP supports point handles for polygons, lines and polylines. It enables the polyline through the optional `ConfigureEditor("PlgMimicJP")` component convention. Other editors keep the polyline hidden because they do not provide the required point-editing workflow.

Drag a handle to move it, Alt-click the selected shape to insert a point into the nearest segment, and Shift-click a handle to remove it. Right-click finishes drawing a new polyline. At least two points are required; no artificial upper limit is imposed.

## Polyline

Polyline is available only in PlgMimicJP and supports moving, adding and removing points.

## Browser Assets

The production runtime loads `shapes-bundle.js` followed by `shapes-lang.js`. The bundle is generated from the four source files listed in `bundleconfig.json`; loading those sources a second time is intentionally avoided.
