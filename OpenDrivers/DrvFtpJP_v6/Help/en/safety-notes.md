# DrvFtpJP — Safety Notes

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/safety-notes.md)

- The driver can delete local files, local directories, remote FTP files and remote FTP directories. Test scenarios on non-critical paths before enabling them in production.
- Passwords are encrypted before saving by `ScadaUtils.Encrypt` and decrypted during loading by `ScadaUtils.Decrypt`.
- The runtime connection sets `ValidateAnyCertificate` to `false`; FTPS certificate validation depends on the certificate being accepted by the environment and FluentFTP.
- `CanSendCommands` is disabled. The driver does not process Rapid SCADA telecontrol commands.
- If a connection attempt fails, the session is marked unsuccessful and data is invalidated, but there are no generated channels in the current implementation.
