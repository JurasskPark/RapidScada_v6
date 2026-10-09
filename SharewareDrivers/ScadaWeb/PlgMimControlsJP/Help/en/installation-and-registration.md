# PlgMimControlsJP — Installation and Registration

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/installation-and-registration.md)

Requirements:

- a compatible Rapid SCADA 6.5 build;
- the .NET 10 runtime used by the current SCADA Web package;
- Mimic diagrams and a compatible Mimic Editor;
- configured input and output channels;
- operator control rights for command components;
- a valid server-side `PlgMimControlsJP` license for executing ordinary components;
- a package matching the installed Rapid SCADA build.

No local component license is required for authoring. Current packages target .NET 10 and the 6.5 branch; compatibility with older Webstation builds, including 6.3, requires a matching build and separate verification.

Installation:

1. Copy the supplied `SCADA` package over the Rapid SCADA installation directory while preserving its directory structure. The package includes the required LicenseJPLite runtime files.
2. Enable `PlgMimControlsJP` in the Webstation plugin configuration.
3. On Windows, install the supplied `PlgMimControlsJP.View.dll` in `ScadaAdmin\Lib` when the classic Administrator must recognize the plugin.
4. Restart SCADA Web, its service or the IIS site. A browser refresh alone does not reload plugin assemblies.
5. Open a mimic editor and verify that **CONTROLS** contains nineteen ordinary component types and the separate `ControlsDemo` entry without requiring a local component license.
6. After an update, perform a hard browser refresh if old scripts or styles remain cached.

Required Webstation plugin entry:

```xml
<Plugins>
  <Plugin code="PlgMimControlsJP" />
</Plugins>
```

The public browser asset path is `/plugins/MimControlsJP`. Do not rename `PlgMimControlsJP.dll`, `PlgMimControlsJP.View.dll` or the `MimControlsJP` static directory. The portable package does not replace a Mimic Editor or host-owned shared assemblies.
