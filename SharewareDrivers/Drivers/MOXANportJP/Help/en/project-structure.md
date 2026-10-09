# DrvMOXANportJP — Project Structure

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/project-structure.md)

```text
DrvMOXANportJP/
├── DrvMOXANportJP.sln                    # Solution file
├── StartСompiling.bat                    # Build and prepare Publish folder
├── ProtectWithReactor.bat                # Build, publish and protect DLLs with .NET Reactor
├── README.md                             # This file
│
├── DrvMOXANportJP.Logic/                 # Runtime driver for ScadaComm
│   ├── DrvMOXANportJP.Logic.csproj
│   └── DevMOXANportJPLogic.cs            # Driver logic entry point
│
├── DrvMOXANportJP.View/                  # ScadaAdmin driver UI
│   ├── DrvMOXANportJP.View.csproj
│   ├── DrvMOXANportJP.View.cs            # Driver view entry point
│   ├── Forms/                            # Configuration, device, search and command forms
│   └── Lang/                             # English and Russian language XML files
│
├── DrvMOXANportJP.Shared/                # Shared code used by Logic, View and tools
│   ├── Moxa/                             # MOXA UDP, DSCI, firmware and command logic
│   ├── Ping/                             # Network scan and polling helpers
│   ├── Project/                          # Driver XML configuration
│   └── Configuration/                    # Rapid SCADA channel prototypes
│
├── DrvMOXANportJP.WinForm/               # Standalone test/admin UI build
├── DrvMOXANportJP.Debug/                 # Console diagnostics and protocol research
├── DrvMOXANportJP.DsciProbe/             # DSCI probing utility
│
├── Libraries/                            # Rapid SCADA, LicenseJP and MOXA native DLLs
├── Publish/                              # Generated publish output
├── Release/                              # Release package output
├── samples/                              # Vendor examples and protocol samples
├── protocol/                             # Protocol notes and research materials
└── screen/                               # Screenshots
```
