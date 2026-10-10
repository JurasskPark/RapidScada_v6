# PlgMimDisplayJP — Conditional value text

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/value-text-display.md)

`ValueTextDisplay` maps a numeric input to text, colors and an optional Mimic image. It retains the standard component's `inCnlNum` and single `clickAction`.

| Property | Default / range | Meaning |
| --- | --- | --- |
| `size` | 150 × 56 px | Initial dimensions |
| `inCnlNum` | 0 | Primary input channel |
| `previewValue` | 1 | Editor preview |
| `rules` | One rule: value > 0 | Localized on text with green background |
| `defaultText` | Localized off text | Unmatched good-value text |
| `baseForeColor` | `#ffffff` | Base text color |
| `baseBackColor` | `#475467` | Base background |
| `defaultImageName` | Empty | Unmatched/default image |
| `noDataText` | `#.#` | Bad/missing-data text |
| `noDataForeColor` | `#fef2f2` | Bad/missing-data text color |
| `noDataBackColor` | `#991b1b` | Bad/missing-data background |
| `noDataImageName` | Empty | Bad/missing-data image |
| `padding` | Top/bottom 4, left/right 8 px | Inner padding |
| `textDirection` | `Horizontal` | Text direction |
| `textAlign` | `MiddleCenter` | Alignment |
| `wordWrap` | `true` | Wrapping |

The first matching [rule](rules.md) wins. Good values with no matching rule use the default state; bad/missing/non-finite inputs use the separate no-data state. Editor preview evaluates rules with `previewValue`.

Rule fields that are empty fall back to component values. Old files' `defaultForeColor`/`defaultBackColor` overrides remain readable. Editing `baseForeColor`/`baseBackColor` establishes the corresponding base color and clears its legacy override.

[![Conditional text, colors and images](../../Source/PlgMimDisplayJP_006.png)](../../Source/PlgMimDisplayJP_006.png)
