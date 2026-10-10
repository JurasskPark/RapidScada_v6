# PlgMimDisplayJP — Dot-matrix display

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/dot-matrix-display.md)

`DotMatrixDisplay` draws each numeric digit with a 5 × 7 cell mask and an optional quality line. The panel, digits, unit and quality label scale with actual dimensions.

| Property | Default / range | Meaning |
| --- | --- | --- |
| `size` | 380 × 132 px | Initial dimensions |
| `inCnlNum` | 0 | Primary input channel |
| `previewValue` | -12.3 | Editor preview |
| `digitCount` | 6 / 1–12 | Fixed digit slots |
| `decimalPlaces` | 2 / 0–6 | Fractional precision |
| `unitText` | Empty | Unit caption |
| `showQuality` | `true` | Display the quality line |
| `goodQualityText` | Empty | Override the localized good-data label |
| `badQualityText` | Empty | Override the localized bad-data label |
| `noDataQualityText` | Empty | Override the localized no-data label |
| `useCustomColors` | `false` | Enable custom colors |
| `displayColor` | `#55f0ad` | Active dots |
| `panelColor` | `#07130f` | Panel |
| `showGlow` | `true` | Dot glow |

Empty quality labels use the plugin dictionary. Edit mode shows good preview quality. A numeric overflow is shown with dashes and bad quality; missing input uses the no-data label. Long quality captions can be shortened with an ellipsis. See [formatting](numeric-format.md) and [quality](channels.md).

[![Dot-matrix indicators with quality labels](../../Source/PlgMimDisplayJP_002.png)](../../Source/PlgMimDisplayJP_002.png)
