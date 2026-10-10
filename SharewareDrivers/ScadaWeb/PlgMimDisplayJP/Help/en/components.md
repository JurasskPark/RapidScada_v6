# PlgMimDisplayJP — Component catalog and parameters

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/components.md)

The ordinary palette contains six types. Stable type names remain unchanged in both languages.

| Type | Description | Settings |
| --- | --- | --- |
| `SegmentDisplay` | Seven-segment, text LED and LCD panels | [Reference](segment-display.md) |
| `DotMatrixDisplay` | Dot-matrix digits with quality labels | [Reference](dot-matrix-display.md) |
| `MechanicalCounter` | Numeric wheels and fractional precision | [Reference](mechanical-counter.md) |
| `MarqueeDisplay` | Arbitrary text and overflow scrolling | [Reference](marquee-display.md) |
| `DataTableDisplay` | Merged cells, measurements and actions | [Reference](data-table-display.md) |
| `ValueTextDisplay` | Ordered value rules, text and colors | [Reference](value-text-display.md) |

The independent [DisplayDemo](demo.md) uses private samples of all six types. Shared references: [numeric formatting](numeric-format.md), [channels and quality](channels.md), [table cells](table-cells.md), [actions](actions.md) and [value rules](rules.md).

Properties inherited from standard Mimic components retain the host's contracts. The four read-only indicators intentionally hide interactive and generic appearance properties; the flexible table and conditional text have the contracts described on their own pages.
