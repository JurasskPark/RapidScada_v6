# PlgMimShapesJP — Project Structure

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/project-structure.md)

```
PlgMimShapesJP/
├── PlgMimShapesJP.sln                    # Solution file
├── StartСompiling.bat                    # Build ZIP package
├── ../BuildPublish_PlgMimShapesJP.bat    # Portable package script
├── README.md                             # This file
│
├── PlgMimShapesJP/                       # Web plugin project
│   ├── PlgMimShapesJP.csproj
│   ├── component.json                    # Component manifest
│   ├── PlgMimShapesJPLogic.cs            # Plugin logic entry point
│   ├── Code/
│   │   ├── ShapesComponentGroup.cs       # Toolbox component group
│   │   ├── ShapesComponentSpec.cs        # Component specification
│   │   ├── ShapesSubtypeGroup.cs         # Subtype group registration
│   │   ├── PluginConst.cs                # Plugin constants
│   │   └── PluginPhrases.cs              # Localized phrases
│   ├── lang/                             # Language XML files
│   │   ├── PlgMimShapesJP.en-GB.xml
│   │   └── PlgMimShapesJP.ru-RU.xml
│   └── wwwroot/plugins/MimShapesJP/
│       ├── css/
│       │   ├── shapes.scss               # SCSS source
│       │   ├── shapes.css                # Compiled CSS
│       │   └── shapes.min.css            # Minified CSS
│       ├── images/                       # SVG icons for components
│       └── js/
│           ├── shapes-descr.js           # Property descriptors
│           ├── shapes-factory.js         # Factories and scripts
│           ├── shapes-render.js          # Renderers
│           ├── shapes-subtypes.js        # Subtype definitions
│           ├── shapes-bundle.js          # Runtime bundle (all above)
│           └── shapes-lang.js            # XML-backed browser localization
│
├── PlgMimShapesJP.Shared/                # Shared library
│   └── PluginInfo.cs                     # Plugin metadata
│
└── PlgMimShapesJP.View/                  # Admin view plugin
    ├── PlgMimShapesJP.View.csproj
    └── PlgMimShapesJPView.cs             # Plugin view entry point
```
