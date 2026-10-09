# DrvDDEJP — Project Structure

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/project-structure.md)

```text
DrvDDEJP/
├── DrvDDEJP.sln                    # Solution file
├── StartСompiling.bat              # Build ZIP package
├── README.md                       # This file
│
├── DdeNet/                         # Embedded DDE client/server library sources
├── DrvDDEJP.DDE/                   # DDE client wrapper used by the driver
├── DrvDDEJP.Logic/                 # Runtime driver for ScadaComm
├── DrvDDEJP.Shared/                # Shared configuration, tags, utilities and language helpers
├── DrvDDEJP.View/                  # ScadaAdmin configuration UI
│   ├── Forms/                      # Project and tag forms
│   └── Lang/                       # English and Russian language XML files
├── DrvDDEJP.WinForm/               # Standalone configuration/test UI host
├── Hex.Shared/                     # Shared conversion helpers
├── Libraries/                      # Rapid SCADA referenced libraries
└── SampleServer/                   # Sample DDE server utility
```
