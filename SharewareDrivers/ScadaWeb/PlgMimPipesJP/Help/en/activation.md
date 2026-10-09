# PlgMimPipesJP — Activation

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/activation.md)

The pipe plugin uses its own license. A `MimicEditorJP` license does not activate `PlgMimPipesJP`.

| Host | Activation request | License |
| --- | --- | --- |
| SCADA Web | `C:\Program Files\SCADA\ScadaWeb\config\PlgMimPipesJP_Activation.bin` | `C:\Program Files\SCADA\ScadaWeb\config\PlgMimPipesJP.bin` |
| ScadaAdminWebJP | `C:\Program Files\SCADA\ScadaAdminWebJP\License\PlgMimPipesJP_Activation.bin` | `C:\Program Files\SCADA\ScadaAdminWebJP\License\PlgMimPipesJP.bin` |

1. Start the application without a pipe license.
2. The plugin creates `PlgMimPipesJP_Activation.bin` in the license directory. An existing request is not overwritten.
3. Send this activation request to the license provider.
4. The generated license must preserve the request UID and the exact application name `PlgMimPipesJP`.
5. Save the received key as `PlgMimPipesJP.bin` in the license directory used by the application.
6. Restart SCADA Web or ScadaAdminWebJP. A browser refresh alone is not sufficient.
7. If both applications are used, put a valid license into each directory because each host reads only its own license location.

If the license is missing or invalid, existing pipe components continue to load and display, but the pipe toolbox group is hidden and new components cannot be placed.
