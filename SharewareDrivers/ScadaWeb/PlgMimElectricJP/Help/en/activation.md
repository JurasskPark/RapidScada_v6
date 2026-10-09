# PlgMimElectricJP — Activation

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/activation.md)

Webstation loads the license from its `ScadaWeb/config` directory. With the default Windows installation this is `C:\Program Files\SCADA\ScadaWeb\config`; other installations use their actual Webstation configuration directory.

| Item | Exact name |
| --- | --- |
| Application name | `PlgMimElectricJP` |
| Activation request | `PlgMimElectricJP_Activation.bin` |
| Signed license | `PlgMimElectricJP_License.bin` |

1. Start Webstation with the plugin enabled. If the license is missing or invalid, the plugin attempts to create an activation request. An existing request is preserved.
2. Give the request to the license provider and obtain the license for the same server UID and exact application name.
3. Save the signed file as `PlgMimElectricJP_License.bin` in this host's configuration directory.
4. Restart Webstation: its license result is cached for the running process.
5. Open a saved mimic containing an ordinary component and check the server log and displayed state.

LicenseJPLite checks the signed license, UID, expiration and product name. A key for `MimicEditorJP` or another component plugin does not activate this product. A missing dependency or validation exception also prevents licensed execution.

Editing does not require a local component key and does not create local activation requests. Without a valid server license, ordinary components become localized inert placeholders: their bindings, scripts, blinking and actions do not execute. Other plugins continue working and `ElectricalDemo` remains available. Saved mimic files are preserved.

The JP editor watermark is governed by its separate editor license. For another server host using the license-provider contract, use the license directory supplied by that host; this guide verifies the Webstation path.
