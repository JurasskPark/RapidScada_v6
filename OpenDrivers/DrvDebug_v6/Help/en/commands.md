# DrvDebug — Commands

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/commands.md)

Configured commands are stored in the project and ordered by `Order`. Only enabled commands are executed.

Command fields:

| Field | Description |
| --- | --- |
| `Enabled` | Whether the command is active |
| `Name` | Command name used in logs |
| `DataKind` | Payload format in configuration. Runtime handles `Ascii` and `Unicode` explicitly; other values are converted as HEX bytes |
| `Payload` | Command data |
| `DelayMs` | Pause after command execution |
| `Note` | Comment |

Rapid SCADA telecontrol commands are also supported:

- `SendStr` writes command data as a text line through the current connection;
- `SendBin` writes binary command data through the current connection.
