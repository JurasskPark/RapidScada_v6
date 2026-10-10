# PlgMimDisplayJP — Activation

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/activation.md)

Ordinary runtime execution uses LicenseJPLite validation for the exact application name **PlgMimDisplayJP**. File presence alone is insufficient: signature, server identity, validity period and product identity are validated.

| Item | Name / location |
| --- | --- |
| License file | `PlgMimDisplayJP_License.bin` |
| Activation request | `PlgMimDisplayJP_Activation.bin` |
| Standard host directory | `AppDirs.ConfigDir` of the running Webstation |
| Provider contract | Version 1 |

1. Install the complete runtime package and enable the plugin.
2. Start Webstation. If the license is missing or invalid and LicenseJPLite is available, the plugin prepares its activation request in the configuration directory.
3. Submit this product's activation request through your agreed license-issuance channel.
4. Place the issued `PlgMimDisplayJP_License.bin` in the running host's configuration directory.
5. Restart Webstation and inspect the plugin license message.

The server log records the validation result. Status codes are `valid`, `missing`, `invalid` and `error`. Failure to generate a request is reported together with the validation error; check directory access and the complete LicenseJPLite runtime. An existing request is preserved.

A `Single` license remains server-bound. The editor does not require a second local component license. Licenses from another plugin do not enable this product. Price, issuance contacts and additional commercial terms are not specified by the inspected materials; use your supplied purchase information.
