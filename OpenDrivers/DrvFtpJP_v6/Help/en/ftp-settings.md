# DrvFtpJP — FTP Settings

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/ftp-settings.md)

| Setting | English |
| --- | --- |
| `Name` | Connection profile name displayed in the configuration UI. |
| `Host` | FTP server host. Default value in code is `127.0.0.1`. |
| `Username` | FTP user name. |
| `Password` | FTP password stored in the XML configuration using `ScadaUtils.Encrypt`. |
| `Port` | FTP port. The UI uses port `21` when the default port option is enabled. |
| `FtpDataType` | Data connection type selected in the UI and applied to `client.Config.DataConnectionType`: passive, passive without route info, or active. |
| `EncryptionMode` | FluentFTP encryption mode used by runtime connection code. |
| `Encryption` | UI checkbox value for TLS. When enabled, the settings form writes `EncryptionMode = Explicit`; when disabled, it writes `EncryptionMode = None`. |
| `SshKey` | SSH key text can be stored by the UI, but FTP runtime connection code does not use it directly. |
