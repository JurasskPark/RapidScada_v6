# PlgMimTankJP — Full Layered Level Indicators

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/full-layered-level-indicators.md)

`TankV2` draws layers vertically from bottom to top. `LayerProgress` draws them horizontally from left to right. Both components use the same physical-level, quality, dead-zone, overflow and alarm calculations.

## Layer Source Modes

| Mode | Runtime behavior |
| --- | --- |
| `Disabled` | The layer is completely excluded from totals, alarms and rendering. |
| `Static` | `liquidNLevelMeters` is used as the real value. No channel is required. |
| `Channel` | Runtime reads `liquidNInCnlNum`; `liquidNLevelMeters` is only the editor preview. |

The fixed layer order is `Liquid 1`, `Liquid 2`, `Liquid 3`. Layer 1 is the lowest or leftmost layer. A disabled layer is omitted without changing the order of the remaining layers.

## Shared Properties

| Property | Default | Purpose |
| --- | ---: | --- |
| `tankHeightMeters` | `8` | Physical capacity in metres. Must be greater than zero. |
| `deadZoneMeters` | `0` | Sediment or unusable bottom zone. |
| `decimalPlaces` | `1` | Value precision from `0` to `6`. |
| `showScale` | `true` | Displays scale ticks and labels. |
| `showTotalLevel` | `true` | Displays the corrected total level. |
| `showTotalPercent` | `true` | Displays percentage of useful height. |
| `showAlarms` | `true` | Enables alarm markers and external alarm bindings. |
| `showLegend` | `true` for `TankV2`, `false` for `LayerProgress` | Displays layer names, colors and values. |
| `alarmScale` | `Percent` | Selects percent or metre thresholds. |
| `emptyColor` | `#e2e8f0` | Empty scale color. |
| `warningColor` | `#f59e0b` | `L` and `H` warning color. |
| `alarmColor` | `#dc2626` | `LL`, `HH` and overflow alarm color. |
| `clickAction` | Empty | Standard Mimic action. The plugin does not send its own commands. |

Each of the three layers has these properties:

| Property pattern | Purpose |
| --- | --- |
| `liquidNSourceMode` | Selects `Disabled`, `Static` or `Channel`. |
| `liquidNInCnlNum` | Input channel used in `Channel` mode. |
| `liquidNName` | Caption shown in the legend or vessel value card. |
| `liquidNColor` | HTML layer color. |
| `liquidNLevelMeters` | Editor preview and the runtime value in `Static` mode. |

Default preview levels are `2 / 3 / 2 m`. Default colors are blue `#0066cc`, brown `#5c3a21` and dark `#1a1a2e`.

## TankV2 Appearance

`TankV2` additionally provides:

- `legendPosition`: `Top`, `Right`, `Bottom` or `Left`; the default is `Right`;
- `showBubbles`: independently enables rising bubbles;
- `animateWaves`: independently enables liquid-wave movement.

Animations are disabled in edit mode and when the operating system requests reduced motion.
