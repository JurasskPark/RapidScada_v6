# PlgMimDisplayJP — Segment display

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/segment-display.md)

`SegmentDisplay` is a read-only numeric panel. Choose `displayStyle` from `SevenSegment` (seven LED segments), `LedText` (Russo One text) or `IndustrialLcd` (LCD). All styles share the same numeric contract.

| Property | Default / range | Meaning |
| --- | --- | --- |
| `size` | 300 × 96 px | Initial dimensions |
| `displayStyle` | `SevenSegment` | Drawing style |
| `inCnlNum` | 0 | Primary input channel |
| `previewValue` | 123.45 | Editor preview |
| `digitCount` | 6 / 1–12 | Fixed digit slots |
| `decimalPlaces` | 2 / 0–6 | Fractional precision |
| `unitText` | Empty | Unit caption |
| `useCustomColors` | `false` | Use the following colors instead of the style palette |
| `displayColor` | `#59ff91` | Digits |
| `panelColor` | `#07130d` | Panel |
| `showGlow` | `true` | Backlight/glow |

Unsupported style names fall back to `SevenSegment`. Decimal points do not consume slots; the minus sign does. Bad/missing data and overflow produce dashes. See [numeric formatting](numeric-format.md).

[![Three styles of numeric indicators](../../Source/PlgMimDisplayJP_001.png)](../../Source/PlgMimDisplayJP_001.png)
