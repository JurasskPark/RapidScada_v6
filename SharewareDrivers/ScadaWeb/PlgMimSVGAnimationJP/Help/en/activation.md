# PlgMimSVGAnimationJP — Activation

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/activation.md)

Runtime activation uses the product name `PlgMimSVGAnimationJP` and these exact filenames:

| File | Purpose |
| --- | --- |
| `PlgMimSVGAnimationJP_Activation.bin` | Activation request |
| `PlgMimSVGAnimationJP_License.bin` | Issued signed license |

1. Install the complete plugin and restart the host.
2. Open the host's JP license interface when the plugin's provider is registered. The native manifest declares contract version `1` and `SvgAnimationLicenseStatusProvider`.
3. Generate the activation request for the target server and submit it through the supplier's established licensing process.
4. Place the issued license in the application's standard configuration directory, `ConfigDir`.
5. Restart the affected host and inspect the plugin's license status.

The license is server-bound in Single mode. Moving to another server or using a key issued for the old product name requires the supplier's appropriate activation process. Renaming a signed file does not change its product identity.

If the host has no registered license provider/interface, check the native manifest registration and installation before generating files manually. No separate key is required for editing, applying drawings or the editor simulator.

[Execution policy](license.md) · [Migration](migration.md) · [Troubleshooting](troubleshooting.md)
