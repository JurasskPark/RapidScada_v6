# PlgMimTankJP — Linear Gauge

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/linear-gauge.md)

`LinearGauge` is a separate read-only zonal scale driven by the standard input channel. It is not a layered liquid indicator and does not calculate tank alarms.

| Property | Default |
| --- | ---: |
| Standard input channel | `0` |
| `orientation` | `Horizontal` |
| `unit` | Empty |
| `decimalPlaces` | `1` |
| `minimum` | `0` |
| `lowAlarmLimit` | `10` |
| `lowWarningLimit` | `20` |
| `highWarningLimit` | `80` |
| `highAlarmLimit` | `90` |
| `maximum` | `100` |
| `workingColor` | `#22c55e` |
| `warningColor` | `#f59e0b` |
| `alarmColor` | `#dc2626` |

Limits are normalized into ascending order between minimum and maximum. Changing orientation exchanges a landscape preview size for a useful portrait size when appropriate. Missing or bad-quality data is shown as `#.#`. The component has no output channel, command sending or click action.
