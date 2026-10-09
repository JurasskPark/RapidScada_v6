# PlgMimCalendarJP — Project Structure

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/project-structure.md)

```
PlgMimCalendarJP/
├── PlgMimCalendarJP.sln              # Solution file
├── StartСompiling.bat                # Build ZIP package
├── README.md                         # This file
│
├── PlgMimCalendarJP/                 # Web plugin project
│   ├── PlgMimCalendarJP.csproj
│   ├── PlgMimCalendarJPLogic.cs      # Plugin logic entry point
│   ├── Code/
│   │   ├── CalendarComponentGroup.cs # Toolbox component group
│   │   ├── CalendarSubtypeGroup.cs   # Subtype group registration
│   │   ├── PluginConst.cs            # Plugin constants
│   │   └── PluginPhrases.cs          # Localized phrases
│   ├── lang/                         # Language XML files
│   │   ├── PlgMimCalendarJP.en-GB.xml
│   │   └── PlgMimCalendarJP.ru-RU.xml
│   ├── examples/                     # Example faceplate files (.fp)
│   └── wwwroot/plugins/MimCalendarJP/
│       ├── css/
│       │   ├── calendar.scss         # SCSS source
│       │   ├── calendar.css          # Compiled CSS
│       │   └── calendar.min.css      # Minified CSS
│       └── js/
│           ├── calendar-descr.js     # Property descriptors
│           ├── calendar-factory.js   # Factories and scripts
│           ├── calendar-render.js    # Renderers
│           └── calendar-bundle.js    # Bundled JS (all above)
│
├── PlgMimCalendarJP.Shared/          # Shared library
│   └── PluginInfo.cs                 # Plugin metadata
│
└── PlgMimCalendarJP.View/            # Admin view plugin
    ├── PlgMimCalendarJP.View.csproj
    └── PlgMimCalendarJPView.cs       # Plugin view entry point
```
