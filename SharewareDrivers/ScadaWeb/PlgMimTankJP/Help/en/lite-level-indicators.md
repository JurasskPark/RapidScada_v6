# PlgMimTankJP — Lite Level Indicators

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/lite-level-indicators.md)

`VerticalLevelLite` and `HorizontalLevelLite` are compact indicators for layouts where only the colored fill is required. They intentionally omit values, captions, percentages, quality text, alarms, legend and dead-zone compensation.

| Property | Default | Purpose |
| --- | ---: | --- |
| `tankHeightMeters` | `8` | Total indicator capacity. |
| `activeLayerCount` | `3` | Uses the first `1`, `2` or `3` layers. |
| `liquidNInCnlNum` | `0` | Runtime input channel for the layer. |
| `liquidNColor` | Blue, brown, dark | Layer fill color. |
| `clickAction` | Empty | Standard Mimic action. |

The editor always uses the fixed `2 / 3 / 2 m` preview. Runtime reads channel values. A bad or missing layer is drawn as zero because Lite components intentionally have no text status. Overflow is clipped to the configured height.
