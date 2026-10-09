# DrvDDEJP — Safety Notes

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/safety-notes.md)

- DDE is a Windows technology. The driver view is built for `net10.0-windows`, and practical DDE operation requires Windows.
- DDE communication depends on the target application, its service name, topic syntax and Windows session/access context.
- If a topic fails, the driver disconnects the DDE client for that topic and suppresses repeated error messages until `ReconnectDelay` expires.
- Empty or invalid returned values are logged and are not written to device data.
- `RequestTimeout` should be long enough for the DDE server but short enough not to block the polling cycle for too long.
- Detailed logging writes DDE requests, responses and decoded values to the ScadaComm log. Avoid permanent detailed logging for sensitive data.
- Rapid SCADA telecontrol command sending is not implemented by the driver runtime.
