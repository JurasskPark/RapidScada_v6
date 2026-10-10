# PlgMimSVGAnimationJP — Operator actions

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/actions.md)

Attach actions to a detail or group. A single action opens directly; several actions show a menu, not a sequence of automatic operations. The deepest clicked detail with actions takes precedence; otherwise the nearest parent group is used. Enter and Space activate keyboard-accessible details.

| Type | Behavior |
| --- | --- |
| `command` | Opens the host's standard command-value dialog for a command role |
| `fixedCommand` | Sends a fixed numeric value after confirmation |
| `chart` | Opens the host's chart for an input role through `ChartFeature` |
| `view` | Opens the positive `viewID` |
| `link` | Opens an absolute HTTP/HTTPS `url` in a new tab |

External links use `noopener` and `noreferrer`. Relative URLs and other schemes are not accepted.

## Availability and confirmation

Runtime execution must be enabled and data quality must permit the action. An optional `condition` must be true. When `feedback` is configured, its input sample must be usable. Commands additionally require host `controlRight`, an effective positive output channel and no pending command for that channel.

Pending state is tracked per channel for `30` seconds. There is no automatic resend. A delivery response does not set the feedback value; redraw from the input channel's confirmed state. Use different roles for the commanded setpoint and measured feedback.

In the editor simulator, actions only write to a log. They do not call command/chart/navigation APIs.

## Saved fields and defaults

| Field | Meaning / default |
| --- | --- |
| `id`, `node` | Action identifier and target node |
| `type` | `command` |
| `label` | User-visible caption |
| `role` | Empty; assign an input or command role appropriate to the type |
| `feedback` | Empty; optional input feedback role |
| `value` | `1` |
| `viewID` | `1` |
| `url` | `https://`; replace with a complete URL |
| `condition` | `null`; no additional availability condition |

[Quality](quality.md) · [Channel roles](channels.md) · [Interaction examples](examples-interaction.md)
