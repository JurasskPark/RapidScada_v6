# DrvPingJP — Tag Values

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/tag-values.md)

| Ping result | Value | Status | Format |
| --- | ---: | ---: | --- |
| Host is available | `1` | `1` | Off/On |
| Host is unavailable or host name cannot be resolved | `0` | `0` | Off/On |

The driver resolves host names before pinging when the configured address is not an IP address. If the DNS name cannot be resolved to an IP address, the tag receives value `0` and status `0`.
