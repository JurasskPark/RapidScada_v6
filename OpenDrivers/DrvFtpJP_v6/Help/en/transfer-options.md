# DrvFtpJP — Transfer Options

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/transfer-options.md)

File transfers use FluentFTP options stored in each action. File upload uses `RemoteExistsMode`; file download uses `LocalExistsMode`. Directory upload and download also use `Mode`, which maps to `FtpFolderSyncMode`.

For directory transfers the driver can build FluentFTP rules:

- `Formats` creates an extension whitelist using `FtpFileExtensionRule`.
- `MaxSizeFile` creates a size rule using `FtpSizeRule` with `LessThan`.
