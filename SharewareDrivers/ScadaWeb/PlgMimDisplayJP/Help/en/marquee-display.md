# PlgMimDisplayJP — Marquee display

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/marquee-display.md)

`MarqueeDisplay` is a read-only panel for arbitrary channel text. It uses the host-formatted input rather than restricting the value to numeric digits.

| Property | Default / range | Meaning |
| --- | --- | --- |
| `size` | 360 × 64 px | Initial dimensions |
| `inCnlNum` | 0 | Primary input channel |
| `previewText` | Bilingual system-ready sample | Editor-only text |
| `noDataText` | `#.#` | Bad/missing-data placeholder |
| `displayColor` | `#ffb43c` | Text color |
| `panelColor` | `#160d02` | Panel color |
| `scrollEnabled` | `true` | Permit scrolling |
| `direction` | `Left` / `Left`, `Right` | Scrolling direction |
| `speed` | 45 / 10–200 px/s | Scrolling speed |
| `pauseSeconds` | 1 / 0–10 s | Pause between cycles |

The source preview text is `SYSTEM READY • СИСТЕМА ГОТОВА`. Scrolling starts only for good runtime text that overflows the panel. Edit mode, bad/missing data and reduced-motion preferences keep it stationary. Short static text is centered; overflowing static text starts at the left edge. Unknown direction names fall back to `Left`.

The frame, padding, text and repeated scrolling gap scale with actual dimensions. Multi-record text is supported through the primary input's join length; see [channels](channels.md).

[![Text displays and scrolling examples](../../Source/PlgMimDisplayJP_004.png)](../../Source/PlgMimDisplayJP_004.png)
