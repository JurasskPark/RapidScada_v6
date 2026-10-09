# DrvDDEJP — Channel Prototypes

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/channel-prototypes.md)

ScadaAdmin generates one channel prototype for each configured tag ordered by `Order`. The prototype name is the tag name, and the tag code is generated from the tag name.

String formats use string Rapid SCADA data types: `Ascii` maps to ASCII, `Unicode` maps to Unicode. `Int64` and `UInt64` map to Int64, while other numeric and boolean formats map to Double. The prototype channel type is created as `InputOutput`, but runtime command sending is disabled.
