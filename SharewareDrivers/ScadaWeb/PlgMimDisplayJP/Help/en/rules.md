# PlgMimDisplayJP — Value-to-text rules

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/rules.md)

`ValueTextRule` inherits the standard Mimic two-comparison condition and adds `text`, `foreColor`, `backColor` and `imageName`. The list is ordered with no hard rule-count limit: the first satisfied rule wins.

| Field | Meaning |
| --- | --- |
| `comparisonOper1`, `comparisonArg1` | First comparison and numeric argument |
| `logicalOper` | `And` or `Or` for two comparisons |
| `comparisonOper2`, `comparisonArg2` | Optional second comparison |
| `text` | Matched text |
| `foreColor`, `backColor` | Matched visual colors |
| `imageName` | Matched Mimic image |

Comparison identifiers are `None`, `Equal`, `NotEqual`, `LessThan`, `LessThanEqual`, `GreaterThan` and `GreaterThanEqual`. Use one comparison for an exact value, or combine two comparisons for a range. Leave the second comparison `None` when unused.

Place specific cases before broad ranges. For `ValueTextDisplay`, no match uses its default state and bad/missing input uses its no-data state. For table `ValueText` cells, no match uses the cell's `text`; unusable input uses `noDataText`.

An empty matched text or visual field falls back to the component/cell's configured value. Rules change presentation rather than writing to channels. [Examples](examples.md) provide a minimal range mapping.
