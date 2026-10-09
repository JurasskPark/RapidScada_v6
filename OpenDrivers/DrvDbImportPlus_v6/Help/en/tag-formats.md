# DrvDbImportPlus — Tag Formats

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/tag-formats.md)

| Driver format | Runtime behavior |
| --- | --- |
| `Float` | Converts to double and applies numeric format `N{NumberDecimalPlaces}` |
| `Integer` | Writes integer values with integer format |
| `DateTime` | Writes DateTime values as Rapid SCADA DateTime |
| `String` | Writes Unicode string values |
| `Boolean` | Writes values with Off/On format |

If the imported value is `null` or `DBNull`, the corresponding Rapid SCADA tag data is invalidated.
