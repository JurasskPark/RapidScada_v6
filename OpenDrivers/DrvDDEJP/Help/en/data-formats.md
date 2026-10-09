# DrvDDEJP — Data Formats

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/data-formats.md)

| Format | Runtime behavior |
| --- | --- |
| `Bool` | Accepts `1`, `TRUE`, `ON`, `YES`, `0`, `FALSE`, `OFF`, `NO` |
| `Int16`, `UInt16`, `Int32`, `UInt32`, `Int64`, `UInt64` | Parses the returned text as a number and writes it to Rapid SCADA |
| `Float`, `Double` | Parses decimal values using invariant, current and Russian-culture fallbacks |
| `Ascii` | Writes the returned text as ASCII string data |
| `Unicode` | Writes the returned text as Unicode string data |
| `HexString` | Writes the returned text as ASCII string data in the current runtime |

Incoming values are trimmed and trailing `NUL`, CR and LF characters are removed before decoding.
