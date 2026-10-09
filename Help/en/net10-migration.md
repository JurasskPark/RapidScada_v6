# Rapid SCADA — .NET 10 migration

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/net10-migration.md)

The 44 projects in OpenDrivers, OpenExtensions, OpenModules and OpenPlugins target `net10.0` or `net10.0-windows`. This includes configuration modules, standalone applications, converters and samples, including FluentFTP and DdeNet source projects.

## Build and runtime compatibility

Use Windows, PowerShell 7 and .NET 10 SDK. `global.json` chooses an installed stable 10.0 SDK. Visual Studio must support .NET 10.

```powershell
pwsh -NoProfile -File .\Build-OpenSource.ps1
pwsh -NoProfile -File .\Test-OpenSource.ps1
```

Scripts default to Release; use `-Configuration Debug` for Debug. Build logs and `results.json` are stored under `artifacts/build-net10-Release`. Builds do not install modules into a running SCADA instance. `StartСompiling*.bat` creates ZIPs through the shared `Build-Release.ps1`; see [packaging](release-packaging.md).

.NET 10 modules require a Rapid SCADA host running on .NET 10. Installing the SDK does not migrate a host still running on .NET 8. The main Rapid SCADA application sources are not included in this checkout, so their build and deployment were outside this migration.

## WinForms images and resources

| Form | Images | Size | Storage |
| --- | ---: | --- | --- |
| DrvDbDataTransferJP — FrmProject | 10 | 16 × 16 | Form-resource ImageList |
| DrvDbImportPlus — FrmProject | 10 | 16 × 16 | Form-resource ImageList |
| DrvFtpJP — FrmAbout | 4 | 32 × 32 | Form-resource ImageList |
| DrvFtpJP — FrmFilesManager | 104 | 16 × 16 | Form-resource ImageList |

All four lists use the original `.resx` `ImageStream`, loaded in `InitializeComponent`. Edit images through `ImageList.Images` in Designer. Additional `*.Images.cs` files and PNG strips were removed. x86 checks found pixel differences after importing PNGs; restoring original streams fixed this and simplified resource editing.

WinForms on .NET 10 reads `ImageListStreamer` without BinaryFormatter even when its `.resx` MIME type is `application/x-microsoft.net.object.binary.base64`. See the [Microsoft migration documentation](https://learn.microsoft.com/en-us/dotnet/standard/serialization/binaryformatter-migration-guide/winforms-applications).

Five binary `FastColoredTextBox.ServiceColors` records were replaced with the same colors supplied by the editor constructor. Null assignments overriding them were removed. ServiceColors uses content serialization; other control properties have explicit serialization settings. WFO1000 is not disabled and BinaryFormatter is not enabled.

`Tests/ResourceSmoke` checks resource loading of built WinForms libraries, construction of changed forms, editor colors and TreeView drawing. All 128 icons are checked for dimensions, keys and SHA-256 of native rendering on white and black backgrounds. `ImageBaseline.json` was captured from the original ImageStreams before migration with Windows visual styles enabled.

Checks do not invoke form Load handlers or connect to FTP/databases. Binary `.resx` records are allowed only for a successfully loaded and rendered ImageListStreamer. Both BinaryFormatter compatibility settings are explicitly off in the check process; other binary types, including old ServiceColors, are rejected.

## Dependencies

Third-party package versions were preserved. Microsoft.Data.SqlClient package directories such as `runtimes/.../net8.0` are package contents and are not renamed with a project TargetFramework change.

Bundled FluentFTP automatic NuGet packaging was disabled when upstream package files were missing. NLog/Serilog samples use a local IFtpLogger adapter. The DDE library path was updated to `net10.0`; PlgMimShapesJP.View uses the existing `Libraries/ScadaWebCommon.dll`.

Previously published archives and binary versions and SharewareDrivers were preserved. Real connections and SCADA deployment are separate from build/resource checks.

## Documented validation on 8 September 2026

The migration document records checks with .NET SDK 10.0.301 on Windows:

- Release build: 44/44 projects.
- Resource loading: 13/13 WinForms libraries.
- All 128 icons retained keys and native rendering on both backgrounds.
- Existing DrvDbDataTransferJP tests: 6/6.
- Packages: 51 ZIPs; 88 Windows x64/x86 component-load checks.
- `git diff --check` passed.

Warnings remained: NU1510 for packages supplied by the platform, CA2022 in legacy EncodingDetector and NU1902 for log4net 2.0.15 in FluentFTP samples. Dependency updates and stream-reading changes were outside the platform migration. These are historical validation results, not new tests performed during the documentation restructuring.
