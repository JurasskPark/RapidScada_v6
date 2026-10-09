# DrvDebug — Data Formats

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/data-formats.md)

| Format | Runtime behavior |
| --- | --- |
| `Bool` | `0` is false, any non-zero first byte is true |
| `Int16`, `UInt16` | Decodes 2-byte integer with selected byte order |
| `Int32`, `UInt32` | Decodes 4-byte integer with selected byte order |
| `Int64`, `UInt64` | Decodes 8-byte integer with selected byte order |
| `Float` | Decodes 4-byte float with selected byte order |
| `Double` | Decodes 8-byte double with selected byte order |
| `Ascii` | ASCII string, trailing `NUL` removed |
| `Unicode` | UTF-16 Unicode string, byte order can be adjusted |
| `HexString` | Byte segment converted to hex text |
