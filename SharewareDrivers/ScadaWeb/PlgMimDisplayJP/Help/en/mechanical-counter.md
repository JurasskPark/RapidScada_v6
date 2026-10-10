# PlgMimDisplayJP — Mechanical counter

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/mechanical-counter.md)

`MechanicalCounter` displays a finite channel value with fixed numeric wheels. It does not integrate flow or calculate a total; the source channel supplies the required value.

| Property | Default / range | Meaning |
| --- | --- | --- |
| `size` | 420 × 150 px | Initial dimensions |
| `inCnlNum` | 0 | Primary input channel |
| `previewValue` | 1284.7 | Editor preview |
| `digitCount` | 7 / 1–12 | Wheel count |
| `decimalPlaces` | 1 / 0–3 | Accented fractional wheels |
| `leadingZeroes` | `true` | Pad unused leading slots with zeroes |
| `unitText` | Empty | Unit caption; an empty unit reserves no space |
| `showCaption` | `true` | Show the counter caption |
| `captionText` | Empty | Override the localized total caption |
| `animateChanges` | `true` | Animate wheel changes |
| `animationDurationMs` | 350 / 100–1500 ms | Wheel transition duration |
| `useCustomColors` | `false` | Enable custom colors |
| `digitColor` | `#f3f1e8` | Digits |
| `panelColor` | `#202326` | Panel |
| `accentColor` | `#2d6f9f` | Fractional wheels |

The final `decimalPlaces` wheels have accent backgrounds; no decimal-point glyph is drawn. Frame, padding, gaps, wheels, caption and unit scale together. Reduced-motion preferences disable wheel animation. Bad/missing values and overflow remain dashes, including with leading zeroes enabled. See [formatting](numeric-format.md).

[![Counters with different dimensions and precision](../../Source/PlgMimDisplayJP_003.png)](../../Source/PlgMimDisplayJP_003.png)
