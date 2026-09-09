# PlgMimCalendarJP

![PlgMimCalendarJP](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=PlgMimCalendarJP&color=4bb60e)

![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=10.0&color=purple)
![Platform](https://jurasskpark.ru/service/budges/?label=platform&message=Windows%20%2F%20Linux&color=lightgrey)


**Mimic Calendar** — a Rapid SCADA plugin that provides calendar components for mimic diagrams.  
**Календарь мнемосхем** — плагин Rapid SCADA, добавляющий календарные компоненты для мнемосхем.

## Overview / Обзор

PlgMimCalendarJP adds six calendar components to the mimic editor toolbox. Each component allows operators to view and set date/time values that are sent to SCADA channels.

Плагин добавляет шесть календарных компонентов на панель инструментов редактора мнемосхем. Каждый компонент позволяет просматривать и устанавливать значения даты/времени, которые отправляются в каналы SCADA.

## Components / Компоненты

| Component | Description | Описание |
|---|---|---|
| **CalendarAuto** | Date input with auto-send on change | Поле даты с автоотправкой при изменении |
| **CalendarInput** | Standalone date input | Отдельное поле ввода даты |
| **CalendarButton** | Date input + button on the right | Поле даты + кнопка справа |
| **CalendarRange** | Date input + button below | Поле даты + кнопка снизу |
| **CalendarRangeBottom** | Two date inputs + button below | Два поля даты + кнопка снизу |
| **CalendarRangeSide** | Two date inputs + button on the right | Два поля даты + кнопка справа |

## Features / Возможности

- **Command formats** — supports Double (OADate), Text (ISO 8601), and Hex (little-endian double) transport formats
- **Channel inheritance** — components inherit channel numbers from parent faceplate components
- **Debug logging** — built-in console debug output for channel binding troubleshooting
- **Localization** — English and Russian language support
- **Customizable appearance** — label, input, and button styles configurable in the mimic editor

- **Форматы команд** — поддержка Double (OADate), Text (ISO 8601) и Hex (little-endian double)
- **Наследование каналов** — компоненты наследуют номера каналов от родительских faceplate-компонентов
- **Отладка** — встроенный вывод в консоль для диагностики привязки каналов
- **Локализация** — поддержка английского и русского языков
- **Настройка внешнего вида** — стили подписи, поля даты и кнопки настраиваются в редакторе мнемосхем

## Project Structure / Структура проекта

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

## Build / Сборка

Requires Windows, PowerShell 7.2+ and the .NET 10 SDK. Run from this product folder:

Нужны Windows, PowerShell 7.2+ и .NET 10 SDK. Запуск из папки продукта:

```cmd
StartСompiling.bat -Runtime win-x64
```

Without `-Runtime`, all platforms listed in `release.json` are built. ZIP archives
and SHA-256 files are written to the repository's `Releases` directory.
Each ZIP contains `SCADA` and an automatically generated `readme.txt`.

Без `-Runtime` собираются все платформы из `release.json`. Готовые ZIP и SHA-256
сохраняются в корневой папке `Releases`. Каждый ZIP содержит `SCADA` и автоматически
сформированный `readme.txt`. Установка в SCADA выполняется отдельно.

Options, package layout and README metadata: [release packaging](../../../Doc/RELEASE_PACKAGING.md).
Параметры, структура пакетов и данные README: [сборка пакетов](../../../Doc/RELEASE_PACKAGING.md).

## Channel Binding / Привязка каналов

Each component can be bound to input and output channels:

- **Input channel** — reads the current date/time value from the SCADA channel
- **Output channel** — sends the selected date/time value as a command

For double-range components (CalendarRangeBottom, CalendarRangeSide), a second pair of channels is available.

Каждый компонент может быть привязан к входным и выходным каналам:

- **Входной канал** — читает текущее значение даты/времени из канала SCADA
- **Выходной канал** — отправляет выбранное значение даты/времени как команду

Для компонентов с двумя датами доступна вторая пара каналов.

## Command Formats / Форматы команд

| Format | Description | Описание |
|---|---|---|
| **Double** | OLE Automation date (double) | Дата OLE Automation (double) |
| **Text** | ISO 8601 string | Строка ISO 8601 |
| **Hex** | Little-endian double as hex string | Little-endian double в виде hex-строки |


## Screenshots / Скриншоты

![PlgMimCalendarJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/OpenPlugins/Source/PlgMimCalendarJP_001.png)

## License / Лицензия

This project is part of the Rapid SCADA ecosystem.  
Данный проект является частью экосистемы Rapid SCADA.

## SAST Tools

[PVS-Studio](https://pvs-studio.ru/ru/pvs-studio/?utm_source=website&utm_medium=github&utm_campaign=open_source) - static analyzer for C, C++, C#, and Java code.
