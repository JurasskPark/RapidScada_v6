# PlgMimPipesJP — Troubleshooting

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/troubleshooting.md)

| Symptom | Cause and action |
| --- | --- |
| The **PIPES** group is missing | Check that the plugin is enabled, `PlgMimPipesJP.bin` is in the license directory of the current host, and the application was restarted. |
| The log says the license is not for this copy | The UID does not match this computer or the license was generated from another activation request. |
| The log reports an application-name mismatch | Regenerate the license with the exact application name `PlgMimPipesJP`; a default name such as `Demo` is rejected. |
| No activation request is created | Check write permissions for `config` or `License`, the plugin log and the presence of the supplied licensing runtime. |
| Manual components work, but automatic layout is unavailable | Open the mimic in Mimic JP Editor and install the matching `PlgMimicJP` version. Automatic layout is intentionally disabled in the original editor. |
| Equipment always shows Unknown | Verify the input channel number, channel status and value. A channel with `stat <= 0` is considered unreliable. |
| New files are installed but the old UI remains | Restart the application or IIS site, then perform a hard browser refresh. |
