# DrvDDEJP — Configuration

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/configuration.md)

The driver stores device configuration in XML files named by device number:

- `DrvDDEJP.xml` for device number `0`;
- `DrvDDEJP_001.xml`, `DrvDDEJP_002.xml`, and so on for normal device numbers.

Main project settings:

| Setting | Default | Description |
| --- | ---: | --- |
| `ServiceName` | `ServiceName` | DDE service name, for example an application service name |
| `DefaultTopic` | `DefaultTopic` | Topic used when a tag does not define its own topic |
| `RequestTimeout` | `5000` | DDE request timeout in milliseconds, minimum `100` |
| `ReconnectDelay` | `2000` | Minimum interval between repeated error log messages for the same topic |
| `WriteLogDriver` | `true` | Enables driver messages in the ScadaComm log |
| `MessageTypeLogDriver` | `Action` | Rapid SCADA log message type used by the driver |
