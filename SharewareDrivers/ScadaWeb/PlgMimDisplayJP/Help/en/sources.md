# PlgMimDisplayJP — Sources and documentation boundaries

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/sources.md)

This guide was checked on **2026-10-10** against the local `scada-web-v6-develop/Plugins/Mimics/PlgMimDisplayJP` source checkout.

| Material | Used for |
| --- | --- |
| `PlgMimDisplayJP/component.json` | Version 6.5.0.3, asset base and runtime resources |
| Web / View project files and shared constants | .NET 10, product/version metadata and activation filenames |
| `Code/DisplayComponentGroup.cs`, `DisplayComponentSpec.cs` and subtype registration | Six ordinary types, demo palette and host contracts |
| `wwwroot/plugins/MimDisplayJP/js/` descriptors, factories, helpers and renderers | Property names/defaults, bounds, formatting, channels, rules and actions |
| EN/RU XML dictionaries and demo localization generator | Captions and demo translation behavior |
| License manager/provider and shared runtime guard | Server validation, authoring and execution boundary |
| `fonts/FONT_SOURCE.md` and `OFL.txt` | Bundled font provenance and license |
| `Doc/MIMIC_COMPONENT_PLUGINS.md` and `Doc/MIMIC_RUNTIME_LICENSING.md` | Component design and current licensing policy |
| Family release script and portable publishing wrapper | Source build entry points and output layouts |
| Local Webstation views 51001–51006 | Six real [PNG captures](screenshots.md) |

Older feature-history numbers in source design notes are not the current package version. The manifest and project files supply the documented current version.

No earlier product README existed in this public folder to migrate. Source projects, runtime DLLs and releases were not copied here. The diagrams show the installed local host; its binary version and successful signed-license validation were not separately audited.

## Remaining material gaps

- No confirmed video or cover was found in the inspected project materials.
- A distributable ZIP was not produced or verified by this documentation task.
- Price, license-issuance contact, support URL and additional commercial terms were not confirmed, so they are not invented.
- Compatibility with Webstation 6.3 requires a corresponding build and separate installed-host verification.

All six component contracts, demo behavior and known limitations are covered in both languages.
