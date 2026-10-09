# DrvDbImportPlus — Channel Prototypes

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/channel-prototypes.md)

The driver view generates channel prototypes from enabled import and export definitions:

| Group | Source | Description |
| --- | --- | --- |
| `Tags` | Non-string import tags | Numeric, integer, DateTime and boolean import channels |
| `String tags` | Import tags with `String` format | Unicode channels. Data length is calculated as `ceil(NumberDecimalPlaces / 4)` |
| `Command tags` | Enabled export commands | Unicode command channels. Data length is calculated as `ceil(Length / 4)` |

Duplicate tag codes and duplicate command codes are skipped while generating prototypes.
