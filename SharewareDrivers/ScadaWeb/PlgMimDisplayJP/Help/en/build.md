# PlgMimDisplayJP — Source build and development

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/build.md)

The implementation belongs to the separate `scada-web-v6-develop` checkout under `Plugins/Mimics/PlgMimDisplayJP`. This public folder contains documentation and PNG captures. Web and Classic Administrator View projects target `net10.0` and share product metadata.

For general preparation see the [repository build guide](../../../../../Help/en/build.md). Source-specific contracts are described by that checkout's `Doc/BUILD.md`, `Doc/JS_BUILD_RULES.md` and `Doc/MIMIC_RUNTIME_LICENSING.md`.

From the source repository root, inspect the canonical family plan:

~~~bat
Plugins\Mimics\Build-Release.bat -Project PlgMimDisplayJP -Plan
~~~

Remove `-Plan` to build the selected family release. The current family script writes verified packages under `Releases/Mimics` by default and preserves prior releases. The separate portable wrapper `Plugins/Mimics/BuildPublish_PlgMimDisplayJP.bat` prepares `Plugins/Mimics/PlgMimDisplayJP/Publish/SCADA`. Follow the selected entry point's own layout.

Focused source checks:

~~~text
node Tests/Js/PlgMimDisplayJP/index.js
powershell -NoProfile -File Plugins/Mimics/PlgMimDisplayJP/PlgMimDisplayJP/Scripts/BuildDemoLocalization.ps1 -Check
dotnet test Tests/CompiledUnitTests/PlgMimDisplayJP.Tests/PlgMimDisplayJP.Tests.csproj -c Release
node Tests/BrowserSmoke/display-demo-fixture.mjs --serve
~~~

Demo localization is generated from the EN/RU XML dictionaries into `display-demo-lang.js`. Keep the manifest, component/subtype registration, descriptors, factories, renderers and both dictionaries consistent.

LicenseJPLite defaults to `System/ThirdParty/LicenseJPLite`; build configuration can supply `LICENSEJP_LITE_RUNTIME`. The generated runtime bundle is mandatory. Preserve required `runtimes/win/lib/net8.0` licensing dependency files even though the plugin targets .NET 10. Protected release packaging requires its configured .NET Reactor tool.

These are documented source commands. No build, ZIP creation or signed-license acceptance was performed while preparing this product page.
