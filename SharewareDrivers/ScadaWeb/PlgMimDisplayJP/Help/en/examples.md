# PlgMimDisplayJP — Minimal examples

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/examples.md)

## Numeric voltage indicator

Place `SegmentDisplay` and set the following properties. Channel **101** is illustrative: substitute a real measured input from your project.

| Property | Value |
| --- | --- |
| `inCnlNum` | 101 |
| `displayStyle` | `SevenSegment` |
| `digitCount` | 6 |
| `decimalPlaces` | 2 |
| `unitText` | `V` |
| `previewValue` | 230.15 |

Preview shows 230.15 in the editor; runtime shows input 101. It has no output command.

## Conditional level text

For `ValueTextDisplay` on illustrative input **102**, order three rules as follows. Translate the displayed labels for your project; the condition identifiers stay unchanged.

| Order | Condition fields | Text |
| --- | --- | --- |
| 1 | `comparisonOper1 = LessThan`, `comparisonArg1 = 20` | Low |
| 2 | `comparisonOper1 = GreaterThanEqual`, `comparisonArg1 = 20`, `logicalOper = And`, `comparisonOper2 = LessThan`, `comparisonArg2 = 80` | Normal |
| 3 | `comparisonOper1 = GreaterThanEqual`, `comparisonArg1 = 80` | High |

Set `defaultText` for unmatched good values and `noDataText` for unusable data. Test `previewValue` at 19, 20, 79 and 80; runtime uses the channel, not the preview.

## Two-row table

Create two rows and two columns with `size = 1`. Put a `StaticText` title at `row = 1`, `column = 1`, `columnSpan = 2`. Put a static label at row 2, column 1 and a `ChannelValue` cell on illustrative input **101** at row 2, column 2.

Enable the value cell's `trendSelectable` and add an optional `DrawChart` action. Leave its `inCnlNums` empty to use the cell input. No output channel or command action is needed. The host must have a chart feature assigned.
