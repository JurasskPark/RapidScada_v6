# PlgTrendJP — Trend Types

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/trend-types.md)

Open **Actions → Trend type** and select the presentation that matches the data.

| Value | Recommended use |
| --- | --- |
| `line` | Standard analog values. |
| `points` | Individual samples without connecting lines. |
| `line-markers` | Line with visible sample positions. |
| `stepped` | Discrete, retained and state values. |
| `smooth` | Visually smoothed process curve. |
| `area` | Filled area under a curve. |
| `bar` | Values compared as bars. |
| `multiple-axes` | Channels with different numeric ranges, grouped into up to four Y axes. |
| `limits` | Channel trends with configured low and high limits. |
| `polar` | Latest values interpreted as angles on a 360° plot. |
| `pie` | Latest good values shown as pie sectors. |
| `radial-gauge` | Latest values of up to the first six series as radial indicators. |
| `single-gauge` | Latest value of the first available series. |
| `normalized-gauge` | Latest values of up to the first six series normalized to `0–100%`. |
| `dynamogram` | The first two channels paired by equal good-quality timestamps. |

`pie`, gauge and polar modes use the latest good values rather than the complete time curve. `dynamogram` requires at least two channels.
