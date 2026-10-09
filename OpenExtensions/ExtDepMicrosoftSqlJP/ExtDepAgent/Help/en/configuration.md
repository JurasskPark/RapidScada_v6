# ExtDepAgent — Configuration

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/configuration.md)

The extension uses the selected `DeploymentProfile` rather than a separate extension configuration file.

- `AgentEnabled` must be enabled; upload and download reject a profile with Agent disabled.
- `AgentConnectionOptions` supply the Agent connection settings. Their `Timeout` also configures service control timeouts.
- `UploadOptions` and `DownloadOptions` select the configuration database (`IncludeBase`), view files (`IncludeView`), Server (`IncludeServer`), Communicator (`IncludeComm`) and Webstation (`IncludeWeb`) settings.
- Application settings are included only if that application is enabled in the project instance.
- Upload uses `ObjectFilter` to filter channel/view data and associated views, and `IgnoreRegKeys` to exclude registration keys.
- Service restart choices are controlled by `RestartServer`, `RestartComm` and `RestartWeb` in upload options.
