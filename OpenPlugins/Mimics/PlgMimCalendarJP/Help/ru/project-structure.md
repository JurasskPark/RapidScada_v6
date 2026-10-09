# PlgMimCalendarJP — Структура проекта

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/project-structure.md)

```
PlgMimCalendarJP/
├── PlgMimCalendarJP.sln              # Файл решения
├── StartСompiling.bat                # Сборка ZIP-пакета
├── README.md                         # Главная страница
│
├── PlgMimCalendarJP/                 # Проект веб-плагина
│   ├── PlgMimCalendarJP.csproj
│   ├── PlgMimCalendarJPLogic.cs      # Точка входа логики плагина
│   ├── Code/
│   │   ├── CalendarComponentGroup.cs # Группа компонентов палитры
│   │   ├── CalendarSubtypeGroup.cs   # Регистрация группы подтипов
│   │   ├── PluginConst.cs            # Константы плагина
│   │   └── PluginPhrases.cs          # Локализованные фразы
│   ├── lang/                         # Языковые XML-файлы
│   │   ├── PlgMimCalendarJP.en-GB.xml
│   │   └── PlgMimCalendarJP.ru-RU.xml
│   ├── examples/                     # Примеры фейсплейтов (.fp)
│   └── wwwroot/plugins/MimCalendarJP/
│       ├── css/
│       │   ├── calendar.scss         # Исходный SCSS
│       │   ├── calendar.css          # Скомпилированный CSS
│       │   └── calendar.min.css      # Минифицированный CSS
│       └── js/
│           ├── calendar-descr.js     # Описания свойств
│           ├── calendar-factory.js   # Фабрики и скрипты
│           ├── calendar-render.js    # Средства отрисовки
│           └── calendar-bundle.js    # Общий JS-пакет перечисленных файлов
│
├── PlgMimCalendarJP.Shared/          # Общая библиотека
│   └── PluginInfo.cs                 # Метаданные плагина
│
└── PlgMimCalendarJP.View/            # Модуль представления Администратора
    ├── PlgMimCalendarJP.View.csproj
    └── PlgMimCalendarJPView.cs       # Точка входа представления плагина
```
