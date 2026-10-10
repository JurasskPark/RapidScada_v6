# PlgMimSVGAnimationJP — Drawing and editing

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/designer.md)

## Tools and selection

Draw `rect`, `ellipse`, `line`, `polyline`, `polygon` and `text`; imported `path` objects can be selected and animated as a whole. Combine details in `g` groups. The toolbar includes file operations, undo/redo, zoom, grid, snapping, channels and the simulator. Tooltips, keyboard hints and accessible labels are localized.

Drag a line to create it. For polylines and polygons, click successive points; finish with Enter, a double-click or the right mouse button. Finishing with the right button does not add another point. An incomplete shape is discarded without an undo entry. Esc cancels drawing; Shift constrains direction or proportions.

A click selects without changing geometry. Dragging starts after 3 screen pixels. One completed drag creates one undo entry. Alt selects a child inside an already selected group. Alignment and distribution require a common parent.

The object tree provides selection, grouping, order, editor visibility and locks. The eye and lock affect editing only; runtime visibility uses `visible`. Animation and action icons open the relevant panels and indicate directly assigned rules.

## Geometry and groups

Position, size, angle, scale and pivot are independent settings. Preserve the pivot when moving or rotating a detail. A line may have negative width or height to retain direction; other shapes use nonnegative dimensions. Animation transforms are relative to base geometry.

Groups support position, scale, angle, opacity and visibility cards. Group fill/stroke cards are unavailable, so child colors remain independent. Remove a group's actions explicitly before ungrouping it; ungrouping must not silently discard actions.

During simulation, the property panel shows the resulting animated value. Exit simulation before editing the base value.

## Keyboard

| Keys | Operation |
| --- | --- |
| `Ctrl+Z` / `Ctrl+Y` | Undo / redo |
| `Ctrl+D` | Duplicate |
| `Ctrl+C` / `Ctrl+V` | Copy / paste within the designer |
| `Delete` | Delete selected details |
| Arrow keys / `Shift` + arrow keys | Move by 1 / 10 units |
| `Enter` / right mouse button | Finish a polyline or polygon |
| `Esc` | Cancel the current drawing operation |

The designer is a draft. Apply commits it to the component; then save the outer document. Cancelling the designer leaves the stored scene unchanged.

[Element code](element-code.md) · [Import/export](import-export.md) · [Simulator](simulation.md)
