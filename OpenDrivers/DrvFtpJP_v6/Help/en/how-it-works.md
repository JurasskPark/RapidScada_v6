# DrvFtpJP — How It Works

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/how-it-works.md)

On communication line start the driver loads `DrvFtpJP_<device number>.xml` from the Rapid SCADA configuration directory and initializes a `DriverClient` with FTP connection settings and scenarios.

During each polling session the driver creates a FluentFTP client, connects to the configured FTP server, executes all enabled scenarios, disconnects and disposes the client. The default polling period returned by the View part is 5 seconds.

Scenarios are executed in the order stored in the configuration. Inside each enabled scenario, actions are also executed in order. Disabled scenarios and disabled actions are skipped.
