# PlgMimDisplayJP — Numeric formatting

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/numeric-format.md)

The three numeric indicators share fixed digit slots. `digitCount` accepts **1–12**; `decimalPlaces` accepts **0–6** for segment/matrix displays and **0–3** for the mechanical counter. Values outside these ranges are clamped.

Values are rounded with fixed decimal precision and right-aligned. A minus sign consumes a slot; a decimal point does not. Negative zero after rounding is normalized to zero. Missing/bad data, non-finite numbers and overflow display one dash per slot instead of truncated process data.

For example, six slots and two decimal places can display `1234.56` or `-123.45`, but `12345.67` needs seven slots and becomes six dashes. The mechanical counter does not draw a decimal separator: its final fractional wheels use the accent color. With seven wheels, one decimal place and leading zeroes, `1284.7` appears as `0012847` with the final wheel accented.

Table cells use a different contract. `displayTemplate` defaults to `###.##` for numeric previews and fallback formatting. The count of `#`/`0` characters after the dot sets precision, capped at 12. With only `#`, trailing fractional zeroes are removed; any `0` keeps fixed precision. Runtime prefers the host's `df.dispVal` when supplied, so the template does not override that host formatting.

See [numeric component settings](components.md) and [table cells](table-cells.md).
