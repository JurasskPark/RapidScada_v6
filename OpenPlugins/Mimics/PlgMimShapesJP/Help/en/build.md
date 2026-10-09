# PlgMimShapesJP — Build

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/build.md)

Requires Windows, PowerShell 7.2+ and the .NET 10 SDK. Run from this product folder:

```cmd
StartСompiling.bat -Runtime win-x64
```

Without `-Runtime`, all platforms listed in `release.json` are built. ZIP archives
and SHA-256 files are written to the repository's `Releases` directory.
Each ZIP contains `SCADA` and an automatically generated `readme.txt`.

Options, package layout and README metadata: [release packaging](../../../../../Help/en/release-packaging.md).
