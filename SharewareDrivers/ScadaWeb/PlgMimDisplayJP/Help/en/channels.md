# PlgMimDisplayJP — Channels and data quality

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/channels.md)

`SegmentDisplay`, `DotMatrixDisplay`, `MechanicalCounter` and `ValueTextDisplay` use the finite raw numeric value. `MarqueeDisplay` uses the host-formatted input text and can display arbitrary text.

The effective primary input is a positive bound `inCnlNum`, otherwise the component's `inCnlNum`. No positive input means no data. A record belongs to the requested channel, and `stat > 0` indicates usable quality; numeric displays additionally require a finite number.

The primary input reader respects `InCnlProps.JoinLen` through `inCnlProps.joinLen`, so Unicode/ASCII text spanning multiple eight-byte records is not cut to the first record. Scalar channels keep the host's default length. Table cells have their own numeric channel reader and automatically generated, deduplicated `propertyBindings`; changing `cells` refreshes those bindings.

| State | Numeric indicators | Marquee | Table / conditional text |
| --- | --- | --- | --- |
| Good usable data | Formatted raw number | Host-formatted text | Channel value or matching rule |
| Bad data or missing input | Dashes across all digit slots | `noDataText` | Cell `noDataText` or component no-data state |
| Numeric overflow | Dashes, never truncated digits | Not applicable | No fixed digit-slot limit |
| Edit mode | `previewValue` | `previewText` | Each preview value and configured rules |

Live values, formatted text, quality, selections and animations are transient; they are not saved into the Mimic XML. With an invalid runtime license, ordinary components do not process their input at all; see [licensing](license.md).
