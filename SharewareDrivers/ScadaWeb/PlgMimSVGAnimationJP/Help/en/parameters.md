# PlgMimSVGAnimationJP — Property reference and defaults

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/parameters.md)

## Component and canvas

| Property | Default / meaning |
| --- | --- |
| Component size | `160 × 120` |
| `inCnlNum`, `outCnlNum` | `0`; channels are assigned through roles |
| `clickAction` | Empty |
| Border width / background | `0` / transparent |
| `sceneDocument` | Serialized blank scene |
| `schemaVersion` | `1` |
| `canvas.width`, `canvas.height` | `320`, `240` |
| `canvas.background` | `none` |

The component's inherited channel/action/state editors are hidden; use the dedicated drawing-and-animation editor. The component size and internal canvas size are separate.

## Detail fields

| Field | Default / meaning |
| --- | --- |
| `id`, `type`, `name`, `parent` | Stable identifier, shape type, localized initial name, parent group or `null` |
| `x`, `y` | `0`, `0` |
| `width`, `height` | `80`, `60` |
| `boundsX`, `boundsY` | `0`, `0` |
| `angle`, `scaleX`, `scaleY` | `0`, `1`, `1` |
| `pivotX`, `pivotY` | `40`, `30` |
| `matrix` | `[1, 0, 0, 1, 0, 0]` |
| `fill` | `#e2e8f0`; `none` for line/polyline |
| `stroke`, `strokeWidth` | `#334155`, `2` |
| `opacity`, `visible` | `1`, `true` |
| `fillOpacity`, `strokeOpacity`, `fillRule` | `1`, `1`, `nonzero` |
| `rx` | `0`; rectangle corner radius |
| `points` | `[[0, 0], [80, 60]]` |
| `text` | Localized initial text |
| `fontSize`, `fontFamily`, `bold`, `textAlign` | `20`, `sans-serif`, `false`, `start` |
| `dash`, `linecap`, `linejoin` | Empty, `round`, `round` |
| `arrowStart`, `arrowEnd` | `false`, `false` |
| `level`, `levelDirection`, `levelColor` | `100`, `bottom`, `#38bdf8` |
| `path`, `clip` | Empty |
| `locked`, `editorHidden` | `false`, `false`; editor-only state |

Level directions are `bottom`, `top`, `left` and `right`. Local resource references must resolve within the scene. Paint, geometry and hierarchy are validated before execution.

## Card defaults

| Field | Default |
| --- | --- |
| `id`, `node`, `property` | Card identity, target and property |
| `mode` | First supported mode of the selected property |
| `duration`, `easing` | `0`, `linear` |
| `role` | Empty |
| `min`, `max` | `0`, `100` |
| `from`, `to` | Base value of the property |
| `variants` | One variant with the default condition and base value |
| `condition` | Default condition |
| `period`, `direction` | `2` seconds, `1` |
| `decimals`, `prefix`, `suffix` | `1`, empty, empty |

The base animation values for `position`, `scale` and `angle` / `flow` are `[0, 0]`, `[1, 1]` and `0`. Other properties start from the node's stored value.

[Mode compatibility](animation.md) · [Conditions](conditions.md) · [Action fields](actions.md) · [Scene validation](scene-document.md)
