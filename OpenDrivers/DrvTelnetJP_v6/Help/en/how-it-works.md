# DrvTelnetJP — How It Works

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/how-it-works.md)

The driver does not require a persistent communication connection to an external device. During each polling session, `DevTelnetJPLogic` calls `NetworkInformation.RunTelnet()` and passes the configured tag list.

For every enabled tag, the driver resolves the host name to an IP address if the configured address is not already an IP address. Then it creates a TCP socket and starts an asynchronous connection attempt to the configured `IP:Port`. If the connection is established before the tag timeout expires, the tag is treated as open. If the timeout expires, the tag is treated as closed.
