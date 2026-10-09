# DrvPingJP — How It Works

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/how-it-works.md)

The driver does not require a communication connection to an external device. On startup, it loads the device configuration file from the Rapid SCADA configuration directory using the device number and the driver code.

During a polling session, `DevPingJPLogic` calls `DriverClient.Ping()`. The client selects the ping mode from the project settings and passes the resulting tag values back to the device logic. The device logic writes values to Rapid SCADA tags by channel code when it is specified, otherwise by tag index.
