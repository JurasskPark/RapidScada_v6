# PlgMimSVGAnimationJP — Build and development

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/build.md)

These commands are for the separate `scada-web-v6-develop` source checkout; this documentation directory has no source project. They were inspected, not executed during this documentation update. [General repository build help](../../../../../Help/en/build.md)

## Entry points

For the repository's Mimics family, run from its root:

~~~powershell
Plugins\Mimics\Build-Release.bat -Project PlgMimSVGAnimationJP -Plan
~~~

`-Plan` displays the plan. Run the appropriate release mode when you actually intend to create deliverables; the family release location is `Releases/Mimics`.

The isolated plugin wrapper from `Plugins/Mimics/PlgMimSVGAnimationJP` creates a Release package:

~~~powershell
.\BuildAll.bat
~~~

The underlying script also supports building without packaging, optional browser tests and publishing:

~~~powershell
powershell -NoProfile -ExecutionPolicy Bypass -File Scripts/BuildPackage.ps1 -Configuration Release
powershell -NoProfile -ExecutionPolicy Bypass -File Scripts/BuildPackage.ps1 -Configuration Release -Package -BrowserTests
powershell -NoProfile -ExecutionPolicy Bypass -File Scripts/BuildPackage.ps1 -Configuration Release -Package -Publish
~~~

## Source ownership and prerequisites

Use Node.js, .NET 10 SDK and the matching Rapid SCADA host/shared projects. License runtime lookup is `LICENSEJP_RUNTIME`, then `LICENSEJP_LITE_RUNTIME`, then `System/ThirdParty/LicenseJPLite`. The script passes neutral `-p:RuntimeIdentifier=`.

Edit `wwwroot/plugins/MimSVGAnimationJP/js/src` modules and the EN/RU phrase sources. `Scripts/BuildAssets.mjs` assembles the JavaScript and generates locale JSON. Do not edit generated bundles or dictionaries manually. View and Web metadata must stay consistent.

The script checks JavaScript syntax, runs engine/designer/license tests and builds both projects. `-BrowserTests` additionally runs smoke, acceptance, regressions, faceplates and lifecycle fixtures. Focused tests from the plugin directory:

~~~powershell
node --test Tests/engine.test.cjs Tests/designer.test.cjs Tests/license.test.cjs
~~~

## Packaging

The isolated script writes `Package/SCADA`, a package README and a versioned ZIP; `-Publish` copies these to `Plugins/Mimics/Publish/PlgMimSVGAnimationJP` while retaining older versioned ZIPs. Portable layout uses `ScadaAdmin/Lib` and lower-case `ScadaWeb/lang`.

Required assets include both `js/svg-animation.bundle.js` and `js/zz-runtime-license.js`, CSS, the icon and both locale files. Web DLL protection runs before archive/publication and fails the workflow on error; the View assembly remains unprotected.

Portable component imports rely on matching host-owned shared assemblies. The native runtime licensing policy requires a matching `PlgMimic.Common.dll` in native deployments; do not mix that policy with portable-import contents or overwrite host core libraries.

These descriptions do not attest a new successful build, protected DLL or verified ZIP. [Installation](installation.md) · [Source boundaries](sources.md)
