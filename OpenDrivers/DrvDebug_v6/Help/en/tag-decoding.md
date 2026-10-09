# DrvDebug — Tag Decoding

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/tag-decoding.md)

Each enabled tag reads its data from the received byte array by `ArrayIndex` and `DataLength`. The selected `DataFormat` determines how the byte segment is converted to text and then written to Rapid SCADA.

Numeric formats apply byte order, coefficient, offset and precision. String formats are written as ASCII or Unicode data. `HexString` converts the selected byte segment to a spaced hex string.

Tag codes are generated from the tag name by trimming it and replacing spaces with underscores. If the name is empty, the fallback code is built from channel and tag ID.
