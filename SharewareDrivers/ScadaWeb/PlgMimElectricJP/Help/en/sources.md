# PlgMimElectricJP — Source materials and boundaries

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/sources.md)

This guide was checked against the local `scada-web-v6-develop` source checkout on **2026-10-10**, rather than inferred from a different plugin's README.

| Material in the source checkout | Information used |
| --- | --- |
| `Plugins/Mimics/PlgMimElectricJP/README.md` | Project layout and asset checks |
| `PlgMimElectricJP/component.json`, project files and `Code/PluginConst.cs` | Version 6.5.0.3, .NET 10, asset base and license filenames |
| `Code/ElectricalComponentCatalog.cs` and EN/RU XML dictionaries | Nine groups, all 101 type names, captions and sizes |
| `wwwroot/plugins/MimElectricJP/js/electrical-defs.js` | State mappings, profiles, metadata, port and URL contracts |
| Descriptors, factories, renderers and `electrical-demo/` scripts | Editing, binding priorities, migration and demo behavior |
| `Code/ElectricalPluginOptions.cs` and license implementation | Optional groups, configuration and activation |
| `Doc/MIMIC_COMPONENT_PLUGINS.md` and `Doc/MIMIC_RUNTIME_LICENSING.md` | Component and free-authoring/licensed-execution contracts |
| `Plugins/Mimics/BuildPublish_PlgMimElectricJP.bat` and release publisher | Portable package contents and source build workflow |

Paths beginning with `Code/` or `wwwroot/` in this table are relative to the source's Web project, `Plugins/Mimics/PlgMimElectricJP/PlgMimElectricJP`.

No confirmed product video or dedicated support link was found in the examined materials, so those blocks are omitted. These pages are not a release archive and do not establish acceptance on a user's licensed server. The source checkout, compiled files and release archives were not changed.

Twelve PNG screenshots of the supplied local Webstation views 41001–41012 were captured on **2026-10-10** at 100% scale. [Browse the screenshots](screenshots.md). They show the supplied view layouts and captured data states; they do not replace release or installed-runtime acceptance checks.
