# PlgMimSVGAnimationJP — Scene document and limits

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/scene-document.md)

`sceneDocument` stores a versioned JSON string. The top-level members are `schemaVersion`, `canvas`, `nodes`, `resources`, `roles`, `cards` and `actions`. Nodes form a flat array with parent identifiers.

Node types: `rect`, `ellipse`, `line`, `polyline`, `polygon`, `text`, `path` and `g`. Resources support local `linearGradient`, `radialGradient` and `clipPath`. Resource IDs receive instance-specific prefixes at rendering, preventing collisions between symbols.

| Validation boundary | Limit |
| --- | --- |
| SVG import | `2097152` bytes (2 MiB) |
| Scene JSON | `8388608` characters |
| Visible and clipping nodes, including expanded local references | `2000` |
| Hierarchy depth | `32` |
| Path coordinate tokens / polyline points | `100000`; conservative complexity budget |
| Resources | `2000` |
| Gradient stops per gradient | `100` |
| Animation cards | `22000`; still one card per node/property |
| Actions | `20000` |
| Terms per condition / variants per card | `100` / `100` |
| Canvas dimension | Greater than `0` and at most `100000` |
| Conditional text value | `10000` characters |
| Persisted identifier | `^[\w-]{1,100}$`; unique IDs in the document |

Transforms use finite numbers; `position` and `scale` require two-number arrays, `visible` requires a Boolean, colors and text require strings. Invalid roles, duplicate cards, missing parents, hierarchy cycles and invalid resources are rejected.

A malformed or unsupported-version scene is preserved as its original string. The designer offers read-only diagnostics/export for recovery instead of silently replacing it with an empty drawing. Keep the original document before manual repairs.

Live samples, selection, timer state, animation phases and command pending state are not part of the persisted scene. [Parameter reference](parameters.md) · [Element code](element-code.md)
