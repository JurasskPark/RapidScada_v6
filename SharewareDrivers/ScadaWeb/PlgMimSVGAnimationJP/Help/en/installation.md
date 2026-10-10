# PlgMimSVGAnimationJP — Installation and update

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/installation.md)

## Matching versions

The inspected manifest, Web assembly and View assembly use version `6.5.0.3` and target .NET 10 / Rapid SCADA 6.5. Use matching host and plugin builds. Compatibility with Webstation 6.3 has not been established for this version.

Obtain a complete package built from the corresponding source version. This documentation directory contains Markdown and PNG images, not an installable ZIP.

## Package placement

| Location | Required content |
| --- | --- |
| `ScadaWeb` | `PlgMimSVGAnimationJP.dll` and a matching host `PlgMimic.Common` dependency |
| `ScadaWeb/lang` | Plugin phrase files |
| `ScadaWeb/wwwroot/plugins/MimSVGAnimationJP` | CSS, `js/svg-animation.bundle.js`, `js/zz-runtime-license.js`, icons and `locales/en-GB.json` / `locales/ru-RU.json` |
| `ScadaAdmin/Lib` | The plugin View assembly and matching packaged dependencies |

The portable package uses `ScadaAdmin/Lib` for the View DLL. Native host deployment may use its own plugin directory; follow that package layout. Portable imports rely on the host's matching shared dependencies. Use the host's supported plugin registration/import mechanism. The native manifest is `plg-mim-svg-animation-jp`; the component capability is `mimic.component.svg-animation`. Do not replace host `ScadaCommon` or `ScadaWebCommon` with arbitrary copies.

## Verify installation

1. Register the plugin, restart the affected host and reopen the editor.
2. Confirm `SvgAnimation` appears in the palette and the designer opens.
3. Confirm both JavaScript bundles and the matching locale load without HTTP errors.
4. Apply a test symbol and save it.
5. Check license status and access to assigned channels before runtime verification.

Close editor sessions before updating packages. Keep a backup of project documents and the previous package. The old `PlgSVGAnimationJP` registration must not coexist with the renamed plugin: see [migration](migration.md).

[Activation](activation.md) · [Build and packaging](build.md) · [Troubleshooting](troubleshooting.md)
