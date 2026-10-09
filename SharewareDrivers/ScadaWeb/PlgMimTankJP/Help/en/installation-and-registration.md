# PlgMimTankJP — Installation and Registration

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/installation-and-registration.md)

Requirements:

- Rapid SCADA 6.x;
- the .NET 8 runtime used by SCADA Web;
- Mimic diagrams and a compatible Mimic Editor;
- configured input channels for runtime data;
- a valid `PlgMimTankJP` product license for placing new components;
- a package matching the installed Rapid SCADA build.
- Rapid SCADA 6.x;

Installation:

1. Copy the supplied `SCADA` package over the Rapid SCADA installation directory while preserving the directory structure. The package includes the required LicenseJPLite runtime files.
2. Enable `PlgMimTankJP` in the Webstation plugin configuration.
3. On Windows, install the supplied `PlgMimTankJP.View.dll` in `ScadaAdmin\Lib` when the classic Administrator must recognize the plugin.
4. Restart SCADA Web, its service or the IIS site. A browser refresh alone does not reload plugin assemblies.
5. Open a mimic editor and check that the **TANKS** group contains thirteen components.
6. After an update, perform a hard browser refresh if old styles remain cached.

Required Webstation plugin entry:

```xml
<Plugins>
  <Plugin code="PlgMimTankJP" />
</Plugins>
```

The public browser asset path is `/plugins/MimTank`. Do not rename `PlgMimTankJP.dll`, `PlgMimTankJP.View.dll` or the `MimTank` static directory.
