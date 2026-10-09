# DrvDebug — Channel Prototypes

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/channel-prototypes.md)

ScadaAdmin generates one `InputOutput` channel prototype for each configured tag ordered by `Order`. The prototype name is the tag name, the tag code is generated from the tag name, and `DataLen` is taken from `DataLength`.

`Ascii` and `Unicode` use string data types, `Int64` and `UInt64` use Int64, and other formats use Double.
