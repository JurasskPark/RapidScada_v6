# PlgMimTankJP — Activation

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/activation.md)

The tank plugin uses its own installation-specific license. A `MimicEditorJP`, `PlgMimPipesJP` or another product license does not activate `PlgMimTankJP`.

| Host | Activation request | License |
| --- | --- | --- |
| SCADA Web | `C:\Program Files\SCADA\ScadaWeb\config\PlgMimTankJP_Activation.bin` | `C:\Program Files\SCADA\ScadaWeb\config\PlgMimTankJP.bin` |
| ScadaAdminWebJP | `C:\Program Files\SCADA\ScadaAdminWebJP\License\PlgMimTankJP_Activation.bin` | `C:\Program Files\SCADA\ScadaAdminWebJP\License\PlgMimTankJP.bin` |

1. Start SCADA Web or ScadaAdminWebJP without a TankJP license.
2. The plugin creates `PlgMimTankJP_Activation.bin` in the host license directory. An existing request is not overwritten.
3. Send the activation request to the license provider.
4. The generated license must preserve the request UID and the exact application name `PlgMimTankJP`.
5. Save the received key as `PlgMimTankJP.bin` in the same host license directory.
6. Restart SCADA Web or ScadaAdminWebJP. A browser refresh alone is not sufficient.
7. If both applications are used, place a valid license in each directory because each host reads only its own license location.

If the license is missing, invalid or issued for another `AppName`, existing TankJP components continue to load and display. The **TANKS** toolbox group is hidden and direct placement of new components is rejected until a valid license is installed.
