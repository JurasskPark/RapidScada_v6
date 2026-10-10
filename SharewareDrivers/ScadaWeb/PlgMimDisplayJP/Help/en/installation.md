# PlgMimDisplayJP — Installation

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/installation.md)

Install the complete package for your Rapid SCADA branch, preserving the `SCADA` directory layout.

| Package destination | Contents |
| --- | --- |
| `ScadaWeb/` | `PlgMimDisplayJP.dll`, matching dependencies and package metadata |
| `ScadaWeb/lang/` | `PlgMimDisplayJP.en-GB.xml` and `PlgMimDisplayJP.ru-RU.xml` |
| `ScadaWeb/wwwroot/plugins/MimDisplayJP/` | JavaScript, CSS, icons and the Russo One font with `OFL.txt` |
| `ScadaAdmin/Lib/` | `PlgMimDisplayJP.View.dll` for Classic Administrator |

1. Stop the Webstation instance being updated and copy the complete package into its SCADA installation.
2. Register and enable `PlgMimDisplayJP` in Webstation plugin configuration, then transfer the configuration to the running host.
3. Install the matching View module on a Classic Administrator editing workstation if used.
4. Install the [server license](activation.md) for ordinary runtime components.
5. Restart Webstation, reopen the editor and reload browser resources.

Keep LicenseJPLite dependencies, the matching `PlgMimic.Common.dll` and the generated `js/zz-runtime-license.js`. Without the guard resource, ordinary registrations are not loaded in unlicensed runtime. Do not substitute core DLLs from another branch.

The browser base is `/plugins/MimDisplayJP`. Preserve directory and file case on case-sensitive hosts. A JP browser editor needs its own matching component registration/package; it is not installed by copying the Classic Administrator View DLL.
