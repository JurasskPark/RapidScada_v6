# DrvMOXANportJP — Safety Notes

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/safety-notes.md)

- Firmware upload and restart operations are executed through the vendor DSCI library.
- Configuration export is safe for mass backup.
- Configuration import should be used only for a single explicitly selected device, because a copied configuration can contain network settings.
- Diagnostic commands are separated by kind: read-only, state-change and research.
- Some devices or firmware versions may not return all fields. For example, NP6250 firmware 2.3 returns no payload for the UDP uptime command `0x56`; in this case uptime tags remain inactive.
