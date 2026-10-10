# PlgMimDisplayJP — Data table

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/data-table-display.md)

`DataTableDisplay` stores lists of relative `rows`, `columns` and positioned `cells`. There is no hard list-count limit; practical size depends on the browser and diagram. Cells support row/column spans, text, values, ordered value rules, labels, units and Mimic images.

| Property | Default / range | Meaning |
| --- | --- | --- |
| `size` | 260 × 180 px | Initial dimensions |
| `rows`, `columns` | Two equal tracks each | Relative grid sizes |
| `cells` | Title and two data-row cells | Starter layout |
| `cellPadding` | Top/bottom 4, left/right 6 px | Default inner padding |
| `defaultTextDirection` | `Horizontal` | Default text direction |
| `defaultTextAlign` | `MiddleLeft` | Default alignment |
| `defaultWordWrap` | `true` | Default wrapping |
| `showHorizontalLines`, `showVerticalLines` | `true` | Independent dividers |
| `gridLineWidth` | 1 px / editor 0–12 | Divider width; parsing keeps non-negative values |
| `gridLineColor` | `#667085` | Divider color |
| `backColor`, `foreColor` | `#344054`, `#e5e7eb` | Base background/text |
| `border` | Width 1 px, color `#667085` | Outer outline |
| `showTrendSelection` | `true` | Channel-selection markers |
| `trendButtonText` | Localized chart caption | Aggregate chart button |
| `trendChartArgs` | Empty | Arguments for the selected-channel chart |

Cell coordinates start at **1**. Out-of-grid cells and overlaps are skipped; the first configured cell owns an occupied area. A span must fit the complete grid. The outer outline comes from the standard component border, independently of horizontal/vertical dividers.

The component's root `inCnlNum` and `outCnlNum` are zero and its root `clickAction` is empty. Bindings and [actions](actions.md) belong to [cells](table-cells.md). An explicit empty saved cell list stays empty; it does not recreate the starter cells.

[![Data tables with merged cells and actions](../../Source/PlgMimDisplayJP_005.png)](../../Source/PlgMimDisplayJP_005.png)
