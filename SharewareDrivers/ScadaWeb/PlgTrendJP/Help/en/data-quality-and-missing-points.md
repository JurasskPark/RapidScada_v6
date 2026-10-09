# PlgTrendJP — Data Quality and Missing Points

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/data-quality-and-missing-points.md)

A point is drawn only when its SCADA status is positive and its value is a valid finite number. Bad-quality, missing or invalid points break line, stepped, smooth and area paths. TrendJP intentionally does not connect the good values on opposite sides of a bad interval.

Tooltips, group ranges and Excel summary statistics use the same good-quality values. A gap in the chart therefore usually indicates missing or bad-quality archive data rather than a drawing error.

Large periods and many channels require more time and data. Choose an archive resolution appropriate to the operator task: use detailed archives for short diagnostics and coarser archives for long-term analysis.
