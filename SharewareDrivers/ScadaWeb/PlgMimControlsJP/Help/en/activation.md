# PlgMimControlsJP — Activation

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/activation.md)

The ordinary controls use their own server-side runtime license. A `Single` license is bound to the server installation; it does not have to be copied to the engineer's computer to create or save `.mim` files. A `MimicEditorJP`, `PlgMimTankJP`, `PlgMimPipesJP` or another product license does not activate `PlgMimControlsJP`.

The separate `MimicEditorJP` license governs that editor's free-version watermark on save. It does not replace a ControlsJP runtime license and does not restrict the availability of ControlsJP components for authoring.

| Host | Activation request | License |
| --- | --- | --- |
| SCADA Web / Webstation | `ScadaWeb/config/PlgMimControlsJP_Activation.bin` | `ScadaWeb/config/PlgMimControlsJP_License.bin` |

Paths are relative to the Rapid SCADA installation directory. For example, a Windows installation may use `C:\Program Files\SCADA\ScadaWeb\config`. A different runtime component host uses its own configured license directory. An authoring-only ScadaAdminWebJP or classic Administrator installation does not require a local ControlsJP runtime license and does not generate an activation request merely by editing a mimic.

1. Install and enable the plugin on the SCADA Web server, then start or restart the application.
2. If the runtime license is missing or rejected, the plugin creates `PlgMimControlsJP_Activation.bin` in `ScadaWeb/config` when its licensing dependencies are available. An existing request is not overwritten automatically.
3. Send the activation request to the license provider.
4. The generated license must preserve the request UID and the exact application name `PlgMimControlsJP`.
5. Save the received key as `PlgMimControlsJP_License.bin` in the same server directory.
6. Restart SCADA Web so the runtime component specifications are rebuilt. A browser refresh alone is not sufficient.
7. Open an ordinary control in Webstation and verify licensed operation, channel feedback and command rights. No second component license is needed on the authoring computer.

If the server license is missing, invalid, expired, issued for another `AppName`, or cannot be validated, ordinary ControlsJP runtime components are replaced by inert localized license warnings. Their types, IDs, geometry and saved settings remain loadable, but they do not execute component scripts, process bindings/data or send commands. Other plugins and autonomous `ControlsDemo` instances continue to work. The editor palette remains available.

| Context | Ordinary controls | `ControlsDemo` |
| --- | --- | --- |
| Authoring without a local component license | Full palette, properties, copying and saving | Available in the standard editor palette; static preview |
| Licensed runtime | Real feedback and commands with normal operator rights | Previously saved demos continue to simulate |
| Unlicensed runtime | Inert license warnings | Autonomous simulated values and local actions |
