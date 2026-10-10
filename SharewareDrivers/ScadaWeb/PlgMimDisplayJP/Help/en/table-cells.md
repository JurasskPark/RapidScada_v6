# PlgMimDisplayJP — Table cells and tracks

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/table-cells.md)

Each `DisplayTableTrack` has `size`, a positive relative weight (default 1). For columns with weights 1 and 2, widths divide as one third and two thirds. Zero/negative track sizes become 1; an empty row/column list restores at least one track.

`DisplayTableCell` properties:

| Property | Default / range | Meaning |
| --- | --- | --- |
| `row`, `column` | 1 / positive integers | Position |
| `rowSpan`, `columnSpan` | 1 / positive integers | Span |
| `contentType` | `StaticText` | `StaticText`, `ChannelValue` or `ValueText` |
| `text` | Empty | Static text or unmatched-rule fallback |
| `label`, `unit` | Empty | Caption before / unit after the value |
| `inCnlNum` | 0 / non-negative | Cell input |
| `previewValue` | 0 | Editor preview |
| `displayTemplate` | `###.##` | Numeric preview/fallback format |
| `noDataText` | `#.#` | Bad/missing-data placeholder |
| `imageName` | Empty | Image from the Mimic image collection |
| `rules` | Empty list | Ordered [ValueTextRule](rules.md) list |
| `useDefaultStyle` | `true` | Inherit component cell styling |
| `textDirection` | `Horizontal` | Own horizontal or 90/270-degree direction |
| `textAlign` | `MiddleLeft` | Own alignment |
| `wordWrap` | `true` | Own wrapping |
| `padding` | Standard padding structure | Own padding |
| `font` | Inherited | Own font if inheritance is disabled |
| `foreColor`, `backColor` | Empty | Local colors |
| `trendSelectable` | `false` | Allow chart-channel selection |
| `actions` | Empty list | Ordered [DisplayCellAction](actions.md) list |

Disable `useDefaultStyle` to customize cell padding, font, alignment, direction and wrapping. Color/image rules can provide conditional overrides. Text is rendered as text, not executable HTML.

`ChannelValue` uses the host-formatted runtime value if available and template formatting otherwise. `ValueText` selects the first matching rule; if none matches it uses `text`. Unusable numeric data use `noDataText`. `StaticText` stays visible without a channel. The renderer reports skipped cells in the browser console; check coordinates and spans before diagnosing absent data.
