# Rapid SCADA — Build

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/build.md)

Open-source products use .NET 10. Build on Windows with PowerShell 7.2+ and a stable .NET 10 SDK selected by `global.json`. Visual Studio must support .NET 10.

From the repository root:

```powershell
pwsh -NoProfile -File .\Build-OpenSource.ps1
pwsh -NoProfile -File .\Test-OpenSource.ps1
```

Both scripts default to `Release`. Add `-Configuration Debug` for debugging. Build logs and `results.json` are stored under `artifacts/build-net10-Release`.

Create installation packages with:

```cmd
Build-Release.bat -List
Build-Release.bat -Project DrvFtpJP -Runtime win-x64
Build-Release.bat -All
```

[Package options and checks](release-packaging.md) · [.NET 10 migration and historical validation](net10-migration.md)

A .NET 10 module requires a Rapid SCADA host running on .NET 10. Installing the SDK beside a .NET 8 host does not change that host's runtime. Main Rapid SCADA application sources are not part of this repository.

Shareware pages describe the runtime of each published product separately. Their binaries were not rebuilt by the open-source migration.
