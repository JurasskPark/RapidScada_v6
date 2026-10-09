# DrvDbImportPlus — Project Structure

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/project-structure.md)

```text
DrvDbImportPlus_v6/
├── DrvDbImportPlus.sln                  # Solution file
├── StartСompiling.bat                   # Build ZIP package
├── README.md                            # This file
│
├── DrvDbImportPlus.Logic/               # Runtime driver for ScadaComm
│   ├── DrvDbImportPlus.Logic.csproj
│   └── DevDbImportPlusLogic.cs          # Device runtime logic
│
├── DrvDbImportPlus.View/                # ScadaAdmin driver UI
│   ├── DrvDbImportPlus.View.csproj
│   ├── DevDbImportPlusView.cs           # Device view entry point
│   ├── Forms/                           # Project, import, export and tag forms
│   ├── FastColorTextBox/                # SQL editor control
│   └── Lang/                            # English and Russian language XML files
│
├── DrvDbImportPlus.Shared/              # Shared logic used by Logic, View and WinForms
│   ├── Client/                          # Polling client
│   ├── Data/                            # Database and InfluxDB data sources
│   ├── Database/                        # Query execution helpers
│   ├── Settings/                        # XML configuration classes
│   └── Tags/                            # Tag and channel prototype generation
│
└── DrvDbImportPlus.Winform/             # Standalone WinForms test/admin build
```
