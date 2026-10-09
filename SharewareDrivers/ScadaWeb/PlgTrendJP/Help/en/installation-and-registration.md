# PlgTrendJP — Installation and Registration

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/installation-and-registration.md)

Requirements:

- Rapid SCADA 6.x on Windows or Linux;
- the .NET 8 runtime used by SCADA Web;
- a valid `PlgTrendJP.bin` license with a positive `CountTags` value;
- access rights to the requested objects and active input channels;
- configured SCADA archives.

Installation:

1. Select the package for the target operating system and copy its `SCADA` folder over the Rapid SCADA installation directory while preserving the folder structure.
2. Enable `PlgTrendJP` in the project `ScadaWebConfig.xml`.
3. Assign `PlgTrendJP` as `ChartFeature` if standard Rapid SCADA chart actions must open TrendJP.
4. Install the license and restart SCADA Web or its IIS site. Refreshing the browser alone does not reload assemblies or the license.

Required configuration:

```xml
<Plugins>
  <Plugin code="PlgTrendJP" />
</Plugins>

<PluginAssignment>
  <ChartFeature>PlgTrendJP</ChartFeature>
</PluginAssignment>
```

The supplied project helper can add this configuration and create a timestamped backup:

```bat
RegisterTrendPluginInProject.bat -ProjectDir "C:\Program Files\SCADA\ProjectSamples\HelloWorld"
```

Use `-KeepChartFeature` to enable the plugin without replacing another chart feature. The helper also accepts `-InstanceName`, `-ConfigFileName` and `-NoBackup`.

On Windows, copy `PlgTrendJP.View.dll` to `ScadaAdmin\Lib` when the classic Administrator must recognize the `TrendJP` view type.
