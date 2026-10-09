# DrvTelnetJP — Tag Values

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/tag-values.md)

| Check result | Value | Status | Format |
| --- | ---: | ---: | --- |
| TCP port is open | `1` | `1` | Off/On |
| TCP port is closed or connection timed out | `0` | `1` | Off/On |
| Host name cannot be resolved | `0` | `0` | Off/On |

The driver writes values by channel code when `TagCode` is specified. If the code is empty, values are written by the tag index.
