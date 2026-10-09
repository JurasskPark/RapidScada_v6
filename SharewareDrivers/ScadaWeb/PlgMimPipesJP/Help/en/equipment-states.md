# PlgMimPipesJP — Equipment States

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/equipment-states.md)

The pump, gate valve and pressure gauge obtain their state from the standard input channel binding. The state is calculated at runtime and cannot be selected manually.

## Pump and Gate Valve

| Channel data | Display |
| --- | --- |
| `stat <= 0`, missing channel or non-numeric value | Unknown: amber indicator |
| `value = 0` | Off: red indicator |
| `value = 1` | Running: green indicator; pump animation is active |
| `value = -1` or `value = 2` | Alarm: blinking red indicator |
| any other value | Unknown: amber indicator |

Only the valve handwheel and pump motor show the equipment state. The pipe body keeps the selected pipe color.

## Pressure Gauge

| Channel data | Display |
| --- | --- |
| `stat <= 0`, missing channel, `NaN` or negative value | Unknown: housing remains visible and the scale is hidden |
| `value = 0` | Zero state with a fixed needle |
| `value > 0` | Active state with a white dial and short repeating needle oscillation |

The current gauge implementation indicates zero, active or unknown state. It does not use the numeric pressure value as a proportional scale position.
