# DrvDebug — Safety Notes

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/safety-notes.md)

- DrvDebug is a diagnostic driver. Use it carefully on production communication lines because it can send arbitrary byte payloads.
- In slave and mixed modes, the first enabled configured command is used as the default response payload.
- Stop condition settings must match the protocol under test. Incorrect length or marker settings can delay reads until timeout.
- `SendStr` and `SendBin` telecontrol commands write directly to the current connection.
- Detailed transport logging can write raw protocol data to log files. Avoid permanent detailed logging for sensitive payloads.
- Simulation can hide real decode failures when `DecodeAndSimulate` and `SimulateOnDecodeError` are enabled.
