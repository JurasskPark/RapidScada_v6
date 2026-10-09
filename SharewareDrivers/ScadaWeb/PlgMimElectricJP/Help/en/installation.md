# PlgMimElectricJP — Installation

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/installation.md)

Use a complete portable package for the matching Rapid SCADA branch. The native package has a `SCADA` directory; preserve its directory structure when copying to the installation.

| Package destination | Contents |
| --- | --- |
| `ScadaWeb/` | `PlgMimElectricJP.dll`, matching dependencies and package metadata |
| `ScadaWeb/lang/` | `PlgMimElectricJP.en-GB.xml` and `PlgMimElectricJP.ru-RU.xml` |
| `ScadaWeb/wwwroot/plugins/MimElectricJP/` | JavaScript, styles and symbol images |
| `ScadaAdmin/Lib/` | `PlgMimElectricJP.View.dll` for Classic Administrator |

1. Stop the Webstation instance being updated and copy the complete package into its SCADA directory.
2. Register and enable `PlgMimElectricJP` in the Webstation plugin configuration, then transfer the configuration to the running host.
3. If using Classic Administrator, install the View module on the editing workstation from the same package.
4. Configure [optional groups](configuration.md) and install the [runtime license](activation.md).
5. Restart Webstation. Reopen the editor and reload the browser resources.

Do not deploy only the entry DLL. The package includes LicenseJPLite and its dependencies, matching `PlgMimic.Common.dll`, dictionaries and web assets. The generated `js/zz-runtime-license.js` is required for unlicensed execution. Its absence prevents ordinary registrations for this plugin from loading.

The browser asset base is `/plugins/MimElectricJP`, not `/plugins/PlgMimElectricJP`. Keep file and directory case when hosting on a case-sensitive system.

This guide describes the native portable package. A JP browser editor needs its matching host registration and package; paths from another plugin or an older release should not be substituted. See [build](build.md) for the source packaging entry point.
