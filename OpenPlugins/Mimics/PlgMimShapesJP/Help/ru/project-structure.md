# PlgMimShapesJP — Структура проекта

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/project-structure.md)

```
PlgMimShapesJP/
├── PlgMimShapesJP.sln                    # Файл решения
├── StartСompiling.bat                    # Сборка ZIP-пакета
├── ../BuildPublish_PlgMimShapesJP.bat    # Скрипт переносимого пакета
├── README.md                             # Главная страница
│
├── PlgMimShapesJP/                       # Проект веб-плагина
│   ├── PlgMimShapesJP.csproj
│   ├── component.json                    # Манифест компонента
│   ├── PlgMimShapesJPLogic.cs            # Точка входа логики плагина
│   ├── Code/
│   │   ├── ShapesComponentGroup.cs       # Группа компонентов палитры
│   │   ├── ShapesComponentSpec.cs        # Спецификация компонента
│   │   ├── ShapesSubtypeGroup.cs         # Регистрация группы подтипов
│   │   ├── PluginConst.cs                # Константы плагина
│   │   └── PluginPhrases.cs              # Локализованные фразы
│   ├── lang/                             # Языковые XML-файлы
│   │   ├── PlgMimShapesJP.en-GB.xml
│   │   └── PlgMimShapesJP.ru-RU.xml
│   └── wwwroot/plugins/MimShapesJP/
│       ├── css/
│       │   ├── shapes.scss               # Исходный SCSS
│       │   ├── shapes.css                # Скомпилированный CSS
│       │   └── shapes.min.css            # Минифицированный CSS
│       ├── images/                       # SVG-иконки компонентов
│       └── js/
│           ├── shapes-descr.js           # Описания свойств
│           ├── shapes-factory.js         # Фабрики и скрипты
│           ├── shapes-render.js          # Средства отрисовки
│           ├── shapes-subtypes.js        # Определения подтипов
│           ├── shapes-bundle.js          # Общий пакет исполняемых скриптов
│           └── shapes-lang.js            # Локализация браузера из XML
│
├── PlgMimShapesJP.Shared/                # Общая библиотека
│   └── PluginInfo.cs                     # Метаданные плагина
│
└── PlgMimShapesJP.View/                  # Модуль представления Администратора
    ├── PlgMimShapesJP.View.csproj
    └── PlgMimShapesJPView.cs             # Точка входа представления плагина
```
