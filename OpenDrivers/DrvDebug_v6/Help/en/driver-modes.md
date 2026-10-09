# DrvDebug — Driver Modes

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/driver-modes.md)

| Mode | Runtime behavior |
| --- | --- |
| `Master` | Sends configured commands during polling sessions and reads responses |
| `Slave` | Waits for incoming requests, decodes them and sends the first configured command as response |
| `Mixed` | Combines master polling and incoming request processing |

The driver advertises support for all three channel behaviors to Rapid SCADA.
