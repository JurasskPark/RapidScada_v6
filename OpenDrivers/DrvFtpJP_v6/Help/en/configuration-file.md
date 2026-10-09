# DrvFtpJP — Configuration File

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/configuration-file.md)

The driver stores settings in the Rapid SCADA configuration directory. The file name is generated from the driver code and device number, for example `DrvFtpJP_001.xml`.

The main XML blocks are:

- `FtpClientSettings` - FTP connection profile.
- `Scenarios` - ordered scenario list.
- `Actions` - ordered action list inside a scenario.
- `DeviceTags` - saved tag metadata, although runtime channel prototypes are not generated in the current code.
- `DebugerSettings` - log path, log writing flag and log retention days.
- `LanguageIsRussian` - language flag used by the configuration tool.
