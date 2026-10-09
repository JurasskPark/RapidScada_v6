# Rapid SCADA — Release packaging

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/release-packaging.md)

Requires Windows, PowerShell 7.2+ and .NET 10 SDK. Rapid SCADA and FastColoredTextBox libraries come from existing project references. Administrator privileges are not required.

## Commands

From the repository root:

```cmd
Build-Release.bat -List
Build-Release.bat -Project DrvFtpJP -Runtime win-x64
Build-Release.bat -All
```

Without arguments, `Build-Release.bat` builds every package for all platforms listed in each `release.json`. A product's BAT builds the same product:

```cmd
OpenDrivers\DrvFtpJP_v6\StartСompilingDrvFtpJP.bat -Runtime win-x64
OpenDrivers\DrvDbImportPlus_v6\StartСompiling.bat
```

These wrappers call root `Build-Release.ps1`. `FluentFTP/restore.bat` and `SampleServer/uninstall_service.bat` retain their separate purposes.

Direct PowerShell calls:

```powershell
pwsh -NoProfile -File .\Build-Release.ps1 -Project DrvDbImportPlus -Runtime win-x64
pwsh -NoProfile -File .\Build-Release.ps1 -All -Runtime win-x64
pwsh -NoProfile -File .\Build-Release.ps1 -Project DrvFtpJP -Runtime win-x64 -IncludeApp
pwsh -NoProfile -File .\Build-Release.ps1 -Project DrvFtpJP -Runtime win-x64 -OutputDirectory "C:\Temp\SCADA packages" -Date 2026-09-08
```

Default configuration is `Release`; use `-Configuration Debug` for debugging. `-KeepStaging` retains intermediate publishing output. Legacy `--package-only` is accepted: every build creates a package without installing it into a running SCADA instance.

## Output and installation

Files are created in root `Releases`:

```text
<code>_<AssemblyVersion>_<platform>.zip
<code>_<AssemblyVersion>_<platform>.zip.sha256
```

ZIP roots contain `SCADA` and `readme.txt`, without an extra `Release` or archive-name directory. A driver package uses:

```text
readme.txt
SCADA/
  ScadaAdmin/
    Lang/<code>.ru-RU.xml
    Lang/<code>.en-GB.xml
    Lib/<code>.View.dll
    Lib/<code>.View/<dependencies>
  ScadaComm/
    Drv/<code>.Logic.dll
    Drv/<code>.Logic/<dependencies>
```

Other destinations come from `release.json`: server modules use `ScadaServer/Mod`, Administrator extensions use `ScadaAdmin/Lib`, and web plugins use `ScadaWeb` with `lang` and `wwwroot`. MicrosoftSqlStorage is supplied for Server, Communicator and Webstation. `DrvDDEJP.DDE.dll` is placed beside `DrvDDEJP.Logic.dll`.

Core Rapid SCADA DLLs are not packaged. FastColoredTextBox, FluentFTP, SQL clients and their dependencies are placed in the relevant module directories. Linux packages still include Windows configuration DLLs because Administrator runs on Windows; their logic DLLs target Linux.

`-IncludeApp` adds `App` for products with standalone WinForms applications. Linux packages skip WinForms applications. These applications require .NET 10 Desktop Runtime; run an AnyCPU application with `dotnet <name>.dll`.

Match package directories with the installed instance and enable the module in Rapid SCADA configuration. Build scripts do not deploy, restart services or change user configuration.

## Generated package README

`Build/readme.template.txt` contains Russian and English parts, authors, description, version, date, platform, source and forum links. Output is UTF-8 with BOM for Windows Cyrillic text.

| Field | Source |
| --- | --- |
| Driver code | `DriverUtils.DriverCode`, checked against `release.json.id` |
| Driver name and description | `NameRu`, `NameEn`, `DescriptionRu`, `DescriptionEn` constants in `DriverUtils` |
| Other product name and description | `display` in `release.json` |
| Version | Computed MSBuild `AssemblyVersion` of `versionProject` |
| Date | Build date or `-Date` |
| Authors, source, forums and notes | `authors`, `sourceUrl`, `forums`, `notes` in `release.json` |

Do not duplicate the version in BAT or `DriverUtils`: `DriverUtils.Version` returns the assembly version. With only `Version` set, SDK computes `AssemblyVersion`. If both are set, ZIP and README versions follow `AssemblyVersion`.

Names and descriptions are stable across builds. To change a driver description, edit the corresponding ordinary C# string constants in `DriverUtils`; its `Name` and `Description` methods and README generation use the same data.

FTP, Telnet and FST forums use the supplied topic links. The Russian Ping topic is `DrvPing`; the English topic is `DrvPingJP`. ExtDepAgent uses the general support section. SQL storage links to the related ExtDepMicrosoftSqlJP topic.

## Products and package platforms

| Products | Platforms |
| --- | --- |
| DrvDbDataTransferJP, DrvDbImportPlus | win-x64, win-x86, linux-x64 |
| DrvDDEJP, DrvDebug, ExtDepAgent | win-x64, win-x86, anycpu |
| DrvFreeDiskSpaceJP, DrvFSTJP, DrvFtpJP, DrvPingJP, DrvTelnetJP | win-x64, win-x86, linux-x64, anycpu |
| ExtDepMicrosoftSqlJP | win-x64, win-x86 |
| MicrosoftSqlStorage, ModArcMicrosoftSqlJP | win-x64, win-x86, linux-x64 |
| PlgMimCalendarJP, PlgMimShapesJP | win-x64, win-x86, linux-x64, anycpu |

The release catalog contains 15 products and 51 product/platform combinations. All 44 source `.csproj` files, including helpers, converters, tests and samples, remain covered by `Build-OpenSource.ps1`. Helper projects are not independent Rapid SCADA modules.

SQL AnyCPU ZIPs are disabled based on package validation: publishing without a RID leaves a portable `Microsoft.Data.SqlClient` placeholder DLL at the root and OS implementations under `runtimes`. The Rapid SCADA loader does not select these from a module's `.deps.json`. Creating `SqlConnection` then throws `PlatformNotSupportedException`. Explicit-RID packages contain the checked implementation and matching native SNI DLL. This packaging limitation does not forbid Any CPU source-project configuration in Visual Studio.

## Verification and failures

```powershell
pwsh -NoProfile -File .\Test-Release.ps1
pwsh -NoProfile -File .\Test-Release.ps1 -Project DrvFtpJP -Runtime win-x64
pwsh -NoProfile -File .\Tests\Test-ReleaseFailure.ps1
```

Checks compare generated README with the template, assembly versions with projects, SHA-256, ZIP layout, language files, configurations and web assets. Types, FastColoredTextBox and SQL clients are loaded from unpacked ZIPs in a separate process; image keys and pixels are checked. x86 checks require .NET 10 Desktop Runtime x86. No database, FTP or DDE server connections are opened. Linux checks cover contents and platform DLLs; logic execution requires a Linux host.

The documented checks on 8 September 2026 covered 51 main ZIPs, 88 Windows component loads, all 128 original icons and eight additional `App` packages for `win-x64`. The expected publication failure test preserved the previous ZIP and propagated a nonzero exit code through BAT.

Publishing logs and JSON results are stored in `artifacts/release-results`; a different `-OutputDirectory` uses a separate results subdirectory. On publishing or validation failure, the script returns nonzero, shows the error log and retains staging under `artifacts/release-builds`. An existing ZIP is replaced only after validation of the new archive.
