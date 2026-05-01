# PlgMimCalendarJP

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
├── StartСompiling.bat                # Build and deploy script
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

## Build and Deploy / Сборка и развёртывание

### Prerequisites / Требования

- .NET 8.0 SDK
- Rapid SCADA installed to `C:\Program Files\SCADA`
- Administrator rights for deployment

### Quick Deploy / Быстрое развёртывание

Run `StartСompiling.bat` as Administrator. The script will:

1. Build the web plugin (`PlgMimCalendarJP`)
2. Build the admin view plugin (`PlgMimCalendarJP.View`)
3. Stop the ScadaWeb service
4. Copy binaries to the SCADA installation
5. Deploy language files and web resources
6. Verify the dictionary key in the deployed language file
7. Start the ScadaWeb service

Запустите `StartСompiling.bat` от имени Администратора. Скрипт выполнит сборку, развёртывание и перезапуск службы.

### Manual Build / Ручная сборка

```bash
dotnet build PlgMimCalendarJP/PlgMimCalendarJP.csproj -c Release
dotnet build PlgMimCalendarJP.View/PlgMimCalendarJP.View.csproj -c Release
```

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

## License / Лицензия

This project is part of the Rapid SCADA ecosystem.  
Данный проект является частью экосистемы Rapid SCADA.
