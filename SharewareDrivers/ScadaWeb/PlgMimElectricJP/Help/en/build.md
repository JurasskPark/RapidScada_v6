# PlgMimElectricJP — Source build and development

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/build.md)

The implementation belongs to the separate `scada-web-v6-develop` checkout, under `Plugins/Mimics/PlgMimElectricJP`. This public product folder contains documentation and illustrations, not those projects or a release package.

The Web project and Classic Administrator View target .NET 10. `PlgMimElectricJP.Shared` owns shared metadata. For general build preparation see the [repository build guide](../../../../../Help/en/build.md); source-specific host/dependency rules remain in `scada-web-v6-develop/Doc/BUILD.md`.

## Read-only asset checks

From `scada-web-v6-develop/Plugins/Mimics/PlgMimElectricJP`:

```powershell
pwsh -NoProfile -File Scripts/BuildElectricalAssets.ps1 -Check
node Scripts/ValidateElectricalPlugin.mjs
pwsh -NoProfile -File PlgMimElectricJP/Scripts/BuildDemoLocalization.ps1 -Check
```

The editable designs are `Design/ElectricalSymbolsCatalog.svg` and `Design/UniversalSymbolsCatalog.svg`. The latter is a symbol-definition library, not a rendered overview. Runtime assets are standalone SVGs under `PlgMimElectricJP/wwwroot/plugins/MimElectricJP/images/symbols`.

## Portable package

On Windows, use the entry point from the source repository root:

```bat
Plugins\Mimics\BuildPublish_PlgMimElectricJP.bat
```

The wrapper uses the parent Mimics NuGet/CLI environment, generates demo localization, checks assets, runs the plugin's focused JavaScript tests, builds Web and View in Release and publishes `Plugins/Mimics/Publish/PlgMimElectricJP/SCADA`.

Packaging requires the LicenseJPLite runtime: `LICENSEJP_RUNTIME`, then `LICENSEJP_LITE_RUNTIME`, or the default `System/ThirdParty/LicenseJPLite` directory. Licensed Web DLL protection also requires the configured .NET Reactor tool. The View DLL remains unprotected. The payload includes the required licensing dependencies and the generated runtime guard.

Change component catalog, definitions, descriptors, factories, renderers and EN/RU dictionaries together. Keep semantic type names and migration contracts stable. Rebuild generated localization and asset outputs using the source scripts; verify all required manifest assets and runtime-license packaging.

These commands describe the source workflow. No plugin build, protected package creation or installed-runtime acceptance was performed as part of adding this documentation.
