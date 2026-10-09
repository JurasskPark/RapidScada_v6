# ModArcMicrosoftSqlJP — Project Structure

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/project-structure.md)

```text
ScadaServer/OpenModules/
├── OpenModules.sln
├── README.md
├── ModArcMicrosoftSqlJP.Logic/
│   ├── ModArcMicrosoftSqlJP.Logic.csproj
│   ├── ModArcMicrosoftSqlJPLogic.cs
│   ├── MicrosoftSqlCAL.cs
│   ├── MicrosoftSqlHAL.cs
│   ├── MicrosoftSqlEAL.cs
│   ├── QueryBuilder.cs
│   ├── PointQueue.cs
│   └── EventQueue.cs
├── ModArcMicrosoftSqlJP.Shared/
│   ├── ModArcMicrosoftSqlJP.Shared.projitems
│   ├── ModArcMicrosoftSqlJP.Shared.shproj
│   ├── ModuleUtils.cs
│   ├── ModulePhrases.cs
│   └── Config/
│       ├── ModArcMicrosoftSqlJP.xml
│       ├── ModuleConfig.cs
│       ├── MicrosoftSqlCAO.cs
│       ├── MicrosoftSqlHAO.cs
│       └── MicrosoftSqlEAO.cs
└── ModArcMicrosoftSqlJP.View/
    ├── ModArcMicrosoftSqlJP.View.csproj
    ├── ModArcMicrosoftSqlJPView.cs
    ├── MicrosoftSqlArchiveView.cs
    ├── Forms/
    ├── Controls/
    └── Lang/
        ├── ModArcMicrosoftSqlJP.en-GB.xml
        └── ModArcMicrosoftSqlJP.ru-RU.xml

ScadaAdmin/OpenExtensions/
├── OpenExtensions.sln
└── ExtDepMicrosoftSqlJP/
    ├── ExtDepMicrosoftSqlJP.csproj
    ├── ExtDepMicrosoftSqlJPLogic.cs
    ├── Downloader.cs
    ├── Uploader.cs
    ├── Config/
    │   └── ExtDepMicrosoftSqlJP.xml
    └── Lang/
        ├── ExtDepMicrosoftSqlJP.en-GB.xml
        └── ExtDepMicrosoftSqlJP.ru-RU.xml
```
