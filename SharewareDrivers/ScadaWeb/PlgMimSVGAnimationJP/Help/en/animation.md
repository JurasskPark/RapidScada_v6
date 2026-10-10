# PlgMimSVGAnimationJP — Animation cards

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/animation.md)

One card controls one property of one node. Adding the same property again opens the existing card. Copying cards to multiple selected details makes independent compatible copies and replaces existing cards for the same property in one undo operation.

| Mode | Behavior |
| --- | --- |
| `condition` | First matching variant wins; otherwise use the base value |
| `range` | Linear interpolation between `min` and `max`; values outside the interval are clamped |
| `cycle` | Continuous rotation, flow, blinking or pulsing while its condition is active |
| `value` | Numeric text with decimal places, prefix and suffix |

## Property and mode compatibility

| Property | Supported modes | Value |
| --- | --- | --- |
| `fill` | `condition` | Color |
| `stroke` | `condition` | Color |
| `strokeWidth` | `condition`, `range` | Number |
| `opacity` | `condition`, `range`, `cycle` | Number |
| `visible` | `condition` | Boolean |
| `position` | `condition`, `range` | `[x, y]` |
| `scale` | `condition`, `range`, `cycle` | `[x, y]` |
| `angle` | `condition`, `range`, `cycle` | Angle |
| `level` | `condition`, `range` | Percent |
| `text` | `condition`, `value` | Text |
| `flow` | `cycle` | Moving dashed line |

Groups accept only `position`, `scale`, `angle`, `opacity` and `visible`. Path geometry remains a single object; animation does not edit individual path points.

## Transitions and cycles

`duration = 0` changes immediately. `linear` and `smooth` transitions continue towards the same target without restarting. When a new target arrives, the transition starts from the currently displayed value; intermediate targets are not queued. Position, scale and angle are relative to base geometry and use the node's pivot.

Cycles use `period` (default `2` seconds) and `direction` (default `1`), with an optional activation condition. Rotation and flow retain their phase when stopped. Inactive blinking and pulsing return to the base appearance. Reduced-motion mode displays a static activity indication instead of continuous cycles.

Numeric text uses `decimals` (default `1`, allowed `0–10`), `prefix` and `suffix`. Conditions always evaluate the original numeric value, rather than the formatted text.

One shared animation scheduler processes active instances. Static symbols are not continuously scheduled; updates reuse their SVG nodes. Removing an instance or an ancestor, including a faceplate, disposes its animation resources.

[Conditions](conditions.md) · [Data quality](quality.md) · [Defaults](parameters.md)
