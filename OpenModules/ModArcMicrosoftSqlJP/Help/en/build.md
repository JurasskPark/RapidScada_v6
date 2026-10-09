# ModArcMicrosoftSqlJP — Build

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/build.md)

Requires Windows, PowerShell 7.2+ and the .NET 10 SDK. Run from this product folder:

```cmd
StartСompiling.bat -Runtime win-x64
```

Without `-Runtime`, all platforms listed in `release.json` are built. ZIP archives
and SHA-256 files are written to the repository's `Releases` directory.
Each ZIP contains `SCADA` and an automatically generated `readme.txt`.

Options, package layout and README metadata: [release packaging](../../../../Help/en/release-packaging.md).

## Server Deployment

Copy the server module files to Rapid SCADA:

```text
ModArcMicrosoftSqlJP.Logic/bin/Release/net10.0/ModArcMicrosoftSqlJP.Logic.dll
  -> C:\Program Files\SCADA\ScadaServer\Mod\ModArcMicrosoftSqlJP.Logic.dll

ModArcMicrosoftSqlJP.Logic/bin/Release/net10.0/ModArcMicrosoftSqlJP.Logic/
  -> C:\Program Files\SCADA\ScadaServer\Mod\ModArcMicrosoftSqlJP.Logic\

ModArcMicrosoftSqlJP.Shared/Config/ModArcMicrosoftSqlJP.xml
  -> C:\Program Files\SCADA\ScadaServer\Config\ModArcMicrosoftSqlJP.xml
```

Add the module to `ScadaServerConfig.xml`:

```xml
<Module code="ModArcMicrosoftSqlJP" />
```

Configure archives to use the module:

```xml
<Archive active="true" code="CurCopy" name="Current data copy" kind="Current" module="ModArcMicrosoftSqlJP">
  <Option name="UseDefaultConn" value="false" />
  <Option name="Connection" value="MicrosoftSqlConn" />
  <Option name="ReadOnly" value="false" />
  <Option name="MaxQueueSize" value="1000" />
  <Option name="BatchSize" value="1000" />
</Archive>
```

When `UseDefaultConn` is `false`, the module uses a named connection from `ModArcMicrosoftSqlJP.xml`. When it is `true`, the module uses the default connection from `ScadaInstanceConfig.xml`.

## Administrator Deployment

Copy the view module and extension files:

```text
ModArcMicrosoftSqlJP.View/bin/Release/net10.0-windows/ModArcMicrosoftSqlJP.View.dll
  -> C:\Program Files\SCADA\ScadaAdmin\Lib\ModArcMicrosoftSqlJP.View.dll

ModArcMicrosoftSqlJP.View/bin/Release/net10.0-windows/ModArcMicrosoftSqlJP.View/
  -> C:\Program Files\SCADA\ScadaAdmin\Lib\ModArcMicrosoftSqlJP.View\

ModArcMicrosoftSqlJP.View/Lang/ModArcMicrosoftSqlJP.*.xml
  -> C:\Program Files\SCADA\ScadaAdmin\Lang\

ExtDepMicrosoftSqlJP/bin/Release/net10.0-windows/ExtDepMicrosoftSqlJP.dll
  -> C:\Program Files\SCADA\ScadaAdmin\Lib\ExtDepMicrosoftSqlJP.dll

ExtDepMicrosoftSqlJP/bin/Release/net10.0-windows/ExtDepMicrosoftSqlJP/
  -> C:\Program Files\SCADA\ScadaAdmin\Lib\ExtDepMicrosoftSqlJP\

ExtDepMicrosoftSqlJP/Config/ExtDepMicrosoftSqlJP.xml
  -> C:\Program Files\SCADA\ScadaAdmin\Config\ExtDepMicrosoftSqlJP.xml

ExtDepMicrosoftSqlJP/Lang/ExtDepMicrosoftSqlJP.*.xml
  -> C:\Program Files\SCADA\ScadaAdmin\Lang\
```

The extension code must be registered in the Admin configuration:

```xml
<Extension code="ExtDepMicrosoftSqlJP" />
```
