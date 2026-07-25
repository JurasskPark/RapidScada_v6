# PlgTrendJP — Interactive Trends for Rapid SCADA

![Rapid SCADA](https://img.shields.io/badge/Rapid%20SCADA-6.x-blue.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![Platform](https://img.shields.io/badge/platform-Windows-lightgrey.svg)

## Overview / Обзор

`PlgTrendJP` is a Rapid SCADA Webstation plugin that provides an interactive trend page and the `TrendWindow` component for mimic diagrams. It reads current and historical channel data through the standard Rapid SCADA APIs, draws interactive Canvas charts in the browser and uses ScottPlot for server-side PNG rendering and diagnostic fallback scenarios.

The full trend page supports multiple chart types, channel and archive selection, relative time expressions, automatic refresh, several archive sources, browser-local profiles and XLSX export through MiniExcel. The embedded mimic component uses the same interactive Canvas engine and is intended for compact process dashboards.

`PlgTrendJP` — плагин Вебстанции Rapid SCADA, который добавляет интерактивную страницу трендов и компонент `TrendWindow` для мнемосхем. Плагин получает текущие и архивные данные каналов через стандартные API Rapid SCADA, рисует интерактивные Canvas-графики в браузере и использует ScottPlot для серверного PNG-рендеринга и диагностических резервных сценариев.

Полная страница тренда поддерживает разные типы графиков, выбор каналов и архивов, относительные выражения времени, автоматическое обновление, несколько источников архивов, локальные профили браузера и экспорт XLSX через MiniExcel. Компонент мнемосхемы использует тот же интерактивный Canvas-движок и предназначен для компактных технологических панелей.

## Features / Возможности

English:

- current, minute, hourly, daily and configured custom archives;
- channel lists, mixed ranges and selection limited only by the licensed `CountTags` value;
- line, point, stepped, area, bar, multi-axis, limit, polar, pie, gauge and dynamogram views;
- mouse-wheel zoom, drag panning, reset zoom, detailed tooltips and a timeline navigator;
- automatic refresh with fixed intervals or adaptive `Auto` mode;
- up to four archive sources on one full trend page;
- compact JSON transfer, browser-side typed arrays and level-of-detail rendering for large datasets;
- wide or long XLSX export, optional daily worksheets, frozen headers and a summary worksheet;
- saved last configuration and named browser-local profiles;
- white and dark themes, configurable legend, line width, marker size and marker shape;
- interactive `TrendWindow` components with an optional transparent background on mimic diagrams;
- localized English and Russian user interface;
- independent TrendJP license with a unique-channel limit enforced before SCADA reads and Excel jobs.

Русский:

- текущий, минутный, часовой, суточный и настроенные пользовательские архивы;
- списки каналов, смешанные диапазоны и выбор, ограниченный только лицензионным значением `CountTags`;
- линии, точки, ступеньки, области, столбики, несколько осей, границы, полярные диаграммы, круговые диаграммы, индикаторы и динамограммы;
- масштабирование колесом мыши, перемещение перетаскиванием, сброс масштаба, подробные подсказки и нижняя временная шкала;
- автоматическое обновление с фиксированным интервалом или адаптивным режимом `Auto`;
- до четырёх источников архивов на одной полной странице тренда;
- компактная передача JSON, типизированные массивы и уровни детализации в браузере для больших наборов данных;
- широкий или длинный экспорт XLSX, отдельные листы по суткам, закреплённые заголовки и лист сводки;
- восстановление последней конфигурации и именованные локальные профили браузера;
- светлая и тёмная темы, настраиваемая легенда, толщина линий, размер и форма маркеров;
- интерактивные компоненты `TrendWindow` с прозрачным фоном на мнемосхемах;
- локализованный английский и русский интерфейс;
- отдельная лицензия TrendJP с ограничением уникальных каналов до чтения SCADA и запуска Excel-задания.

## Requirements / Требования

English:

- Rapid SCADA 6.x on Windows;
- .NET 8 runtime used by SCADA Web;
- installed and enabled `PlgTrendJP` Webstation plugin;
- `PlgTrendJP.View.dll` in `ScadaAdmin\Lib` when the classic Administrator must recognize the TrendJP view type;
- valid `PlgTrendJP.bin` license with a positive `CountTags` value;
- user rights to every requested object and input channel;
- configured SCADA archives and file-system permissions for the account running the Server and Webstation services.

Русский:

- Rapid SCADA 6.x под Windows;
- среда .NET 8, используемая SCADA Web;
- установленный и включённый плагин Вебстанции `PlgTrendJP`;
- файл `PlgTrendJP.View.dll` в `ScadaAdmin\Lib`, если классический Администратор должен распознавать тип представления TrendJP;
- действующая лицензия `PlgTrendJP.bin` с положительным значением `CountTags`;
- права пользователя на все запрашиваемые объекты и входные каналы;
- настроенные архивы SCADA и права файловой системы у учётных записей служб Сервера и Вебстанции.

## Installation and Registration / Установка и регистрация

English:

1. Copy the supplied `SCADA` package over the Rapid SCADA installation directory while preserving the package structure.
2. Check that `PlgTrendJP.dll`, its dependencies and language files are present under `ScadaWeb`, and that `wwwroot\plugins\TrendJP` contains the plugin browser assets.
3. Copy `PlgTrendJP.View.dll` to `ScadaAdmin\Lib` if it is included in a separate package.
4. Enable `PlgTrendJP` in the project `ScadaWebConfig.xml`.
5. Assign `PlgTrendJP` as `ChartFeature` when standard Rapid SCADA chart actions must open the TrendJP page.
6. Install the license and restart SCADA Web or its IIS site. A browser refresh alone does not reload the license or plugin assemblies.

Русский:

1. Скопируйте поставляемый пакет `SCADA` поверх каталога установки Rapid SCADA с сохранением структуры папок.
2. Проверьте наличие `PlgTrendJP.dll`, его зависимостей и языковых файлов в `ScadaWeb`, а также браузерных ресурсов в `wwwroot\plugins\TrendJP`.
3. Скопируйте `PlgTrendJP.View.dll` в `ScadaAdmin\Lib`, если файл поставляется отдельным пакетом.
4. Включите `PlgTrendJP` в проектном файле `ScadaWebConfig.xml`.
5. Назначьте `PlgTrendJP` как `ChartFeature`, если стандартные команды открытия графика Rapid SCADA должны переходить на страницу TrendJP.
6. Установите лицензию и перезапустите SCADA Web или сайт IIS. Простого обновления страницы недостаточно для перезагрузки лицензии и сборок плагина.

Required configuration:

```xml
<Plugins>
  <Plugin code="PlgTrendJP" />
</Plugins>

<PluginAssignment>
  <ChartFeature>PlgTrendJP</ChartFeature>
</PluginAssignment>
```

The supplied project helper can register the plugin and create a timestamped backup:

```bat
RegisterTrendPluginInProject.bat -ProjectDir "C:\Program Files\SCADA\ProjectSamples\HelloWorld"
```

Use `-KeepChartFeature` to enable the plugin without replacing the existing chart feature. The script can also accept `-InstanceName`, `-ConfigFileName` and `-NoBackup`.

Поставляемый помощник регистрации добавляет плагин в проект и по умолчанию создаёт резервную копию с отметкой времени. Параметр `-KeepChartFeature` включает плагин без замены текущего обработчика графиков. Также поддерживаются `-InstanceName`, `-ConfigFileName` и `-NoBackup`.

## Activation and Tag Limit / Активация и лимит тегов

`PlgTrendJP` uses its own LicenseJPLite license. The signed license must contain the exact application name `PlgTrendJP` and a positive `CountTags` value.

`PlgTrendJP` использует отдельную лицензию LicenseJPLite. Подписанная лицензия должна содержать точное имя приложения `PlgTrendJP` и положительное значение `CountTags`.

| Host / Приложение | Activation request / Запрос активации | License / Лицензия |
|---|---|---|
| SCADA Web | `C:\Program Files\SCADA\ScadaWeb\config\PlgTrendJP_Activation.bin` | `C:\Program Files\SCADA\ScadaWeb\config\PlgTrendJP.bin` |
| ScadaAdminWebJP, when used / при использовании | `C:\Program Files\SCADA\ScadaAdminWebJP\License\PlgTrendJP_Activation.bin` | `C:\Program Files\SCADA\ScadaAdminWebJP\License\PlgTrendJP.bin` |

English:

1. Start the host without a TrendJP license.
2. The plugin creates `PlgTrendJP_Activation.bin` without overwriting an existing request.
3. Send the activation request to the license provider.
4. Save the received license as `PlgTrendJP.bin` in the license directory of the corresponding host.
5. Restart the host application.

Русский:

1. Запустите приложение без лицензии TrendJP.
2. Плагин создаст `PlgTrendJP_Activation.bin`, не перезаписывая существующий запрос.
3. Передайте запрос активации поставщику лицензии.
4. Сохраните полученную лицензию под именем `PlgTrendJP.bin` в папке лицензий соответствующего приложения.
5. Перезапустите приложение.

`CountTags` is the maximum number of unique channel numbers in one trend. Ranges are expanded before counting, duplicates are counted once, and the same channel used by several archive sources is still one licensed tag. The browser provides immediate feedback, while the JSON, PNG and Excel endpoints independently enforce the same limit before reading archive data or creating an export job.

`CountTags` задаёт максимальное количество уникальных номеров каналов в одном тренде. Диапазоны раскрываются до подсчёта, дубликаты учитываются один раз, а один канал в нескольких источниках архивов остаётся одним лицензируемым тегом. Браузер сразу сообщает о превышении, а конечные точки JSON, PNG и Excel независимо проверяют тот же лимит до чтения архива или создания задания экспорта.

## Standalone Trend Page / Отдельная страница тренда

The plugin registers the fileless view type `TrendJP`. A view of this type opens `/TrendJP?viewID=<ID>`. Its `Args` field contains ordinary query-string parameters without the leading question mark.

Плагин регистрирует тип представления без отдельного файла — `TrendJP`. Представление этого типа открывает `/TrendJP?viewID=<ID>`. Поле `Args` содержит обычные параметры строки запроса без начального знака вопроса.

The page provides:

- channel selection with object, device, search, selected-only and selected-archive filters;
- a single archive or up to four independently configured archive sources;
- `From` and `To` controls with second precision;
- `Auto`, `1`, `5`, `10`, `30` and `60` second timer modes;
- nearest-channel or all-channel tooltip modes;
- direct Refresh and Reset Zoom commands;
- the Actions menu with trend type, display settings, profiles and Excel export;
- a lower timeline with drag/resize navigation and previous/next calendar-day buttons.

Страница предоставляет:

- выбор каналов с фильтрами по объекту, устройству, поиску, выбранным каналам и выбранному архиву;
- один архив или до четырёх независимо настроенных источников архивов;
- поля `От` и `До` с точностью до секунды;
- режимы таймера `Auto`, `1`, `5`, `10`, `30` и `60` секунд;
- Tooltip по ближайшему каналу или по всем каналам;
- отдельные команды обновления и сброса масштаба;
- меню «Действия» с типом тренда, настройками отображения, профилями и экспортом Excel;
- нижнюю временную шкалу с перемещением, изменением диапазона и кнопками предыдущих/следующих календарных суток.

## Trend View Arguments / Аргументы представления тренда

The parameters below are supported both in `View.Args` and in a direct `/TrendJP?...` URL. Parameter names are case-sensitive. Many enumerated values are normalized by the page, but new configurations should use the lowercase canonical values shown below. Aliases are retained for compatibility.

Параметры ниже поддерживаются как в `View.Args`, так и в прямом URL `/TrendJP?...`. Имена параметров чувствительны к регистру. Многие значения перечислений нормализуются страницей, но в новых конфигурациях следует использовать показанные ниже канонические значения в нижнем регистре. Алиасы сохранены для совместимости.

### Data and Time Arguments / Аргументы данных и времени

| Parameter | Values and default / Значения и значение по умолчанию | English description | Русское описание |
|---|---|---|---|
| `cnlNums` | Channel expression; empty by default / Выражение каналов; по умолчанию пусто | Input channels. Supports individual numbers, separators and ranges. | Входные каналы. Поддерживает отдельные номера, разделители и диапазоны. |
| `archiveCode` | Archive code; `Min` by default / Код архива; по умолчанию `Min` | Archive used in single-source mode. Common codes are `Cur`, `Min`, `Hour` and `Day`; configured custom codes returned by the archive catalog are also accepted. | Архив одиночного режима. Основные коды: `Cur`, `Min`, `Hour`, `Day`; также принимаются коды пользовательских архивов из каталога архивов. |
| `archive` | Alias of `archiveCode` / Алиас `archiveCode` | Compatibility alias. | Алиас для совместимости. |
| `startTime` | Absolute or relative local time / Абсолютное или относительное локальное время | Start of the requested range. Recommended absolute format: `YYYY-MM-DDTHH:mm[:ss]`. Alias: `from`. | Начало запрашиваемого диапазона. Рекомендуемый абсолютный формат: `YYYY-MM-DDTHH:mm[:ss]`. Алиас: `from`. |
| `endTime` | Absolute or relative local time / Абсолютное или относительное локальное время | End of the requested range. Recommended absolute format: `YYYY-MM-DDTHH:mm[:ss]`. Alias: `to`. | Конец запрашиваемого диапазона. Рекомендуемый абсолютный формат: `YYYY-MM-DDTHH:mm[:ss]`. Алиас: `to`. |
| `period` | Positive number plus `s`, `m` or `h`, for example `30s`, `90m`, `8h` / Положительное число и `s`, `m` или `h` | Builds a range when one or both endpoints are omitted. | Формирует диапазон, когда одна или обе границы не указаны. |
| `hours` | Positive number of hours / Положительное число часов | Legacy compatibility equivalent of an hourly `period`. | Совместимый устаревший эквивалент периода в часах. |

If none of `startTime`, `endTime`, `period` and `hours` is specified, the page opens the complete current local day from `00:00` to the next `00:00`.

Если не указан ни один из параметров `startTime`, `endTime`, `period` и `hours`, страница открывает полные текущие локальные сутки от `00:00` до следующих `00:00`.

### Display and Behavior Arguments / Аргументы отображения и поведения

| Parameter | Supported values; default / Значения; по умолчанию | English description | Русское описание |
|---|---|---|---|
| `theme` | `default`, `dark`; `default` | Selects the page palette. | Выбирает цветовую тему страницы. |
| `trendType` | See the trend type table; `line` / См. таблицу типов; `line` | Selects the renderer. Unknown values fall back to `line`. | Выбирает способ построения. Неизвестные значения заменяются на `line`. |
| `showLegend` | Boolean; `true` | Legacy legend visibility switch. `false` disables the legend regardless of its position. | Совместимый флаг легенды. `false` отключает легенду независимо от положения. |
| `legendPosition` | `none`, `top`, `right`, `bottom`, `left`; `top` | Places the legend. `none` hides it. | Задаёт положение легенды. `none` скрывает её. |
| `markerShape` | `circle`, `triangle`, `square`; `circle` | Marker used by point modes and the active tooltip point. | Маркер в точечных режимах и у активной точки Tooltip. |
| `lineWidth` | `1`, `1.5`, `2`, `3`, `4`; `2` | Line width in CSS pixels. | Толщина линии в CSS-пикселях. |
| `markerSize` | `2`, `3`, `4`, `5`, `6`, `8`; `3` | Point marker size in CSS pixels. | Размер маркера точки в CSS-пикселях. |
| `tooltip` | `nearest`, `all`; `nearest` | Shows the nearest series or all available series at the cursor time. | Показывает ближайший ряд или все доступные ряды на времени курсора. |
| `refreshInterval` | `auto`, `1`, `5`, `10`, `30`, `60`; `30` | Presets the timer cadence. It does not start the timer by itself. | Задаёт период таймера. Самостоятельно таймер не запускает. |
| `autoRefresh` | Boolean; `false` | Starts the moving-range refresh timer when the view is ready. | Автоматически запускает обновление скользящего диапазона после открытия представления. |
| `showToolbar` | Boolean; `true` | `false` hides the complete upper toolbar, including Actions. It has priority over `showControlPanel`. | `false` полностью скрывает верхнюю панель, включая «Действия». Имеет приоритет над `showControlPanel`. |
| `showControlPanel` | Boolean; `true` | `false` hides filters, timer, Refresh and Reset Zoom but keeps the Actions menu available. | `false` скрывает фильтры, таймер, обновление и сброс масштаба, но оставляет меню «Действия». |
| `showTimeline` | Boolean; `true` | `false` hides the lower timeline and previous/next-period buttons. | `false` скрывает нижнюю временную шкалу и кнопки предыдущего/следующего периода. |
| `exportLayout` | `wide`, `long`; `wide` | Presets the Excel data layout. | Задаёт формат данных Excel по умолчанию. |
| `splitByDay` | Boolean; `false` | Presets creation of one worksheet per local calendar day. | Задаёт создание отдельного листа для каждых локальных суток. |

For Boolean arguments use `true` or `false`. The page also recognizes `1` as true and `0`, `no` or `off` as false where the general Boolean parser is used.

Для логических аргументов рекомендуется использовать `true` и `false`. Общий обработчик страницы также распознаёт `1` как истину, а `0`, `no` и `off` — как ложь.

### Multiple Archive Arguments / Аргументы нескольких архивов

The full page supports up to four sources. This mode is not available as a property of one embedded `TrendWindow`; a mimic component uses one archive and one channel list.

Полная страница поддерживает до четырёх источников. Этот режим не является свойством одного встроенного `TrendWindow`: компонент мнемосхемы использует один архив и один список каналов.

| Parameter | Values and default / Значения и значение по умолчанию | English description | Русское описание |
|---|---|---|---|
| `multiArchive` | Boolean; `false` | Enables multiple archive sources. Alias: `multi`. Source arguments also enable this mode automatically. | Включает несколько источников архивов. Алиас: `multi`. Наличие аргументов источников также включает режим автоматически. |
| `sourceNArchive` | Archive code, `N=1..4` / Код архива, `N=1..4` | Archive of source `N`. Compatibility alias: `archiveN`. | Архив источника `N`. Алиас: `archiveN`. |
| `sourceNCnlNums` | Channel expression, `N=1..4` / Выражение каналов, `N=1..4` | Channels of source `N`. Compatibility alias: `cnlNumsN`. | Каналы источника `N`. Алиас: `cnlNumsN`. |
| `sourceNEnabled` | `1` or `0`, `true` or `false`; enabled by default / По умолчанию включён | Includes or temporarily disables source `N`. Compatibility alias: `enabledN`. | Включает или временно отключает источник `N`. Алиас: `enabledN`. |

Series names are prefixed with the archive code in multiple-source mode. Data sources are loaded through parallel calls to the existing data endpoint and merged in the browser. There is no separate `data-multi` endpoint.

В режиме нескольких источников к именам рядов добавляется код архива. Источники загружаются параллельными вызовами существующей конечной точки данных и объединяются в браузере. Отдельной конечной точки `data-multi` нет.

### Channel Expression Syntax / Синтаксис списка каналов

Channel arguments accept commas, semicolons and whitespace as separators. Spaces around a dash are allowed. Ascending and descending ranges are expanded, invalid or non-positive numbers are ignored, and duplicates are removed while preserving the first occurrence order.

Аргументы каналов принимают запятые, точки с запятой и пробельные символы как разделители. Вокруг тире разрешены пробелы. Возрастающие и убывающие диапазоны раскрываются, ошибочные и неположительные номера игнорируются, а дубликаты удаляются с сохранением порядка первого появления.

| Expression / Выражение | Normalized result / Нормализованный результат |
|---|---|
| `100, 200 - 205, 310, 450-455, 600` | `100,200,201,202,203,204,205,310,450,451,452,453,454,455,600` |
| `10; 12 14-16` | `10,12,14,15,16` |
| `5-2, 3, 5` | `5,4,3,2` |

There is no fixed 12-channel parser limit. The actual maximum is the positive `CountTags` value in the current license.

Фиксированного ограничения парсера на 12 каналов нет. Фактический максимум задаётся положительным значением `CountTags` текущей лицензии.

### Relative Time Expressions / Относительные выражения времени

`startTime` and `endTime` accept an absolute local date/time or a case-insensitive relative expression. All expressions in one range are resolved against one shared browser clock value when the page opens.

`startTime` и `endTime` принимают абсолютные локальные дату и время либо регистронезависимое относительное выражение. Все выражения одного диапазона вычисляются относительно одного общего значения часов браузера при открытии страницы.

Supported bases:

| Base / Основа | Result / Результат |
|---|---|
| `NOW` | Current local time with milliseconds removed / Текущее локальное время без миллисекунд |
| `SECOND` | Beginning of the current second / Начало текущей секунды |
| `MINUTE` | Beginning of the current minute / Начало текущей минуты |
| `HOUR` | Beginning of the current hour / Начало текущего часа |
| `DAY` | Beginning of the current day / Начало текущих суток |
| `WEEK` | Monday `00:00` of the current week / Понедельник `00:00` текущей недели |
| `MONTH` | First day `00:00` of the current month / Первое число `00:00` текущего месяца |
| `YEAR` | January 1 `00:00` of the current year / 1 января `00:00` текущего года |

Supported offsets are applied from left to right:

| Suffix / Суффикс | Unit / Единица | Example / Пример |
|---|---|---|
| `S` | Seconds / Секунды | `NOW-30S` |
| `M` | Minutes / Минуты | `HOUR+15M` |
| `H` | Hours / Часы | `DAY+6H` |
| `D` | Days / Сутки | `DAY-1D` |
| `W` | Weeks / Недели | `WEEK-2W` |
| `MO` | Calendar months / Календарные месяцы | `MONTH-1MO` |
| `Y` | Calendar years / Календарные годы | `YEAR-1Y` |

Examples:

| Expression / Выражение | Meaning / Значение |
|---|---|
| `DAY` … `DAY%2B1D` | Current complete day / Полные текущие сутки |
| `DAY-1D` … `DAY` | Previous complete day / Полные предыдущие сутки |
| `DAY-2D%2B6H%2B30M` … `DAY-2D%2B18H` | Window from 06:30 to 18:00 two days ago / Окно с 06:30 до 18:00 два дня назад |
| `WEEK` … `WEEK%2B1W` | Current complete week / Полная текущая неделя |
| `MONTH-1MO` … `MONTH` | Previous complete calendar month / Полный предыдущий календарный месяц |
| `YEAR` … `NOW` | From the beginning of the year to now / От начала года до текущего момента |

Range resolution rules:

| Supplied arguments / Переданные аргументы | Result / Результат |
|---|---|
| No endpoints, `period=8h` | `NOW-8H` … `NOW` |
| `startTime=DAY`, `period=8h` | `DAY` … `DAY+8H` |
| `endTime=DAY`, `period=90m` | `DAY-90M` … `DAY` |
| Both endpoints and `period` | Explicit endpoints win; `period` is ignored / Используются явные границы; `period` игнорируется |
| Only one endpoint without `period` | Invalid configuration / Ошибочная конфигурация |
| Start greater than or equal to end | Invalid configuration / Ошибочная конфигурация |

Invalid time expressions are rejected before the data request and displayed in the localized error dialog.

Ошибочные выражения времени отклоняются до запроса данных и показываются в локализованном окне ошибки.

### Priority and XML Encoding / Приоритет и кодирование XML

Configuration priority is:

1. Direct URL query parameters.
2. The corresponding fields from `View.Args`.
3. The last configuration saved in this browser for the current `viewID`.
4. Plugin defaults.

Приоритет конфигурации:

1. Прямые параметры URL.
2. Соответствующие поля из `View.Args`.
3. Последняя конфигурация, сохранённая в этом браузере для текущего `viewID`.
4. Значения плагина по умолчанию.

Time parameters are one group: if `View.Args` contains any of `startTime`, `endTime`, `period` or `hours`, the configured time group replaces the complete saved range. Relative expressions are recalculated on every view opening and after `Ctrl+F5`; values written into the address bar by TrendJP are marked in `history.state` and do not prevent recalculation.

Параметры времени образуют одну группу: если `View.Args` содержит любой из `startTime`, `endTime`, `period` или `hours`, настроенная группа времени полностью заменяет сохранённый диапазон. Относительные выражения пересчитываются при каждом открытии представления и после `Ctrl+F5`; значения, записанные самим TrendJP в адресную строку, помечаются в `history.state` и не мешают пересчёту.

Inside XML, write `&` as `&amp;`. A literal plus sign in a relative expression must be URL-encoded as `%2B`, because an unescaped `+` is decoded as a space.

В XML записывайте `&` как `&amp;`. Знак сложения в относительном выражении необходимо кодировать как `%2B`, потому что неэкранированный `+` декодируется как пробел.

Correct XML fragment:

```xml
<Args>cnlNums=101,103-108&amp;archiveCode=Min&amp;startTime=DAY-2D%2B6H%2B30M&amp;endTime=DAY-2D%2B18H&amp;trendType=line-markers</Args>
```

Do not use a hard-coded `ViewTypeID` copied from another project. The numeric ID belongs to the current configuration database. Select the registered `TrendJP` view type in the Administrator or use the ID assigned to it in that project.

Не копируйте фиксированный `ViewTypeID` из другого проекта. Числовой идентификатор относится к текущей базе конфигурации. Выберите зарегистрированный тип представления `TrendJP` в Администраторе либо используйте идентификатор, назначенный ему в конкретном проекте.

### Ready-to-Use Argument Examples / Готовые примеры аргументов

The channel numbers in these examples must be replaced with channels available to the current user and must fit the licensed `CountTags` limit.

Номера каналов в примерах необходимо заменить на доступные текущему пользователю и уложить в лицензионный лимит `CountTags`.

#### Current Day / Текущие сутки

```text
cnlNums=101,103-108&archiveCode=Min&startTime=DAY&endTime=DAY%2B1D&trendType=line&legendPosition=right
```

XML:

```xml
<Args>cnlNums=101,103-108&amp;archiveCode=Min&amp;startTime=DAY&amp;endTime=DAY%2B1D&amp;trendType=line&amp;legendPosition=right</Args>
```

#### Previous Day with a Stepped Trend / Предыдущие сутки со ступенчатым трендом

```xml
<Args>cnlNums=101,103-108&amp;archiveCode=Min&amp;startTime=DAY-1D&amp;endTime=DAY&amp;trendType=stepped&amp;legendPosition=bottom</Args>
```

#### Last 8 Hours / Последние 8 часов

```xml
<Args>cnlNums=101,103-108&amp;archiveCode=Min&amp;period=8h&amp;trendType=line&amp;legendPosition=top</Args>
```

#### Last 90 Minutes as Points / Последние 90 минут точками

```xml
<Args>cnlNums=101,103-108&amp;archiveCode=Min&amp;period=90m&amp;trendType=points&amp;markerShape=square&amp;markerSize=4</Args>
```

#### Live 30-Second Headless Trend / Автообновляемый тренд за 30 секунд без панели

```xml
<Args>cnlNums=101,103&amp;archiveCode=Cur&amp;period=30s&amp;trendType=line-markers&amp;showToolbar=false&amp;showTimeline=false&amp;autoRefresh=true&amp;refreshInterval=1</Args>
```

#### Actions-Only Compact Mode / Компактный режим только с меню «Действия»

```xml
<Args>cnlNums=101,103-108&amp;archiveCode=Min&amp;period=8h&amp;showControlPanel=false&amp;showTimeline=false&amp;legendPosition=right</Args>
```

#### Dark Theme Without a Legend / Тёмная тема без легенды

```xml
<Args>cnlNums=101,103-108&amp;archiveCode=Min&amp;period=8h&amp;theme=dark&amp;legendPosition=none</Args>
```

#### All Channels in Tooltip and Daily Excel Sheets / Все каналы в Tooltip и отдельные листы Excel

```xml
<Args>cnlNums=101,103-108&amp;archiveCode=Min&amp;period=8h&amp;tooltip=all&amp;exportLayout=wide&amp;splitByDay=true</Args>
```

#### Two Archive Sources / Два источника архивов

```xml
<Args>multiArchive=true&amp;source1Archive=Cur&amp;source1CnlNums=101,103-105&amp;source1Enabled=1&amp;source2Archive=Min&amp;source2CnlNums=101,103-108&amp;source2Enabled=1&amp;period=8h&amp;legendPosition=right</Args>
```

#### Complex Channel List and Shifted Window / Сложный список каналов и смещённое окно

```xml
<Args>cnlNums=100, 200 - 205, 310, 450-455, 600&amp;archiveCode=Min&amp;startTime=DAY-2D%2B6H%2B30M&amp;endTime=DAY-2D%2B18H&amp;trendType=line-markers</Args>
```

## Trend Types / Типы тренда

| Value / Значение | English use | Назначение |
|---|---|---|
| `line` | Standard analog line trend. | Обычный линейный аналоговый тренд. |
| `points` | Raw samples without connecting lines. | Исходные точки без соединяющих линий. |
| `line-markers` | Line with visible sample markers. | Линия с видимыми маркерами отсчётов. |
| `stepped` | Discrete, retained or state values. | Дискретные, удерживаемые значения и состояния. |
| `smooth` | Smoothed process curve. | Сглаженная технологическая кривая. |
| `area` | Filled area under the curve. | Область с заливкой под кривой. |
| `bar` | Interval values shown as bars. | Значения интервалов в виде столбиков. |
| `multiple-axes` | Independent visual scale for each channel. | Независимая визуальная шкала каждого канала. |
| `limits` | Channel trend with configured low/high thresholds. | Тренд с настроенными нижними и верхними границами канала. |
| `polar` | Latest values interpreted as angles on a 360° plot. | Последние значения как углы на круговой диаграмме 360°. |
| `pie` | Latest good value of each series as a pie sector. | Последнее достоверное значение каждого ряда как сектор. |
| `radial-gauge` | Multiple latest values as radial indicators. | Несколько последних значений как радиальные индикаторы. |
| `single-gauge` | Latest value of the first available series. | Последнее значение первого доступного ряда. |
| `normalized-gauge` | Latest values normalized to a 0–100% scale. | Последние значения, нормализованные к шкале 0–100%. |
| `dynamogram` | First two series paired by exact good-quality timestamps. | Первые два ряда, объединённые по точному времени достоверных точек. |

`pie`, gauge and polar modes use latest values rather than the complete time curve. `dynamogram` requires at least two channels. Unsupported financial candles, heatmaps and statistical plots require other data models and are not part of this plugin.

Режимы `pie`, индикаторы и полярная диаграмма используют последние значения, а не всю временную кривую. Для `dynamogram` требуется не менее двух каналов. Финансовые свечи, тепловые карты и статистические графики требуют других моделей данных и в плагин не входят.

## Automatic Refresh / Автоматическое обновление

`autoRefresh=true` starts the timer after the view and archive list are ready. `refreshInterval` selects `auto` or a fixed supported number of seconds.

`autoRefresh=true` запускает таймер после готовности представления и списка архивов. `refreshInterval` выбирает `auto` либо поддерживаемое фиксированное число секунд.

In `Auto` mode, the current archive starts with a fast poll, backs off to at most five seconds while values remain unchanged and returns to the fast interval when they change. Historical archives use minimum reload intervals of one minute for minute archives, five minutes for hourly archives and fifteen minutes for daily archives. Explicit timer values continue to use the selected cadence.

В режиме `Auto` текущий архив сначала опрашивается быстро, при неизменных данных интервал увеличивается максимум до пяти секунд и снова уменьшается после изменения. Для исторических архивов минимальные интервалы составляют одну минуту для минутного архива, пять минут для часового и пятнадцать минут для суточного. Явно заданный интервал продолжает использовать выбранное значение.

While a timer is running, TrendJP preserves a manually selected viewport and does not force an automatic full-range reset. The loading overlay is delayed for short refreshes to prevent one-second live trends from blinking.

Во время работы таймера TrendJP сохраняет выбранный пользователем масштаб и не выполняет принудительный возврат ко всему диапазону. Индикатор загрузки показывается с задержкой для длительных операций, поэтому быстрый секундный тренд не мигает при каждом обновлении.

## Excel Export / Экспорт Excel

The full page exports the arrays already loaded for the chart. It does not request the same archive data from SCADA Server a second time. A short-lived, user-scoped export token identifies the cached trend data.

Полная страница экспортирует уже загруженные для графика массивы и не запрашивает те же архивные данные у Сервера SCADA повторно. Краткоживущий токен экспорта, связанный с пользователем, указывает на кэшированные данные тренда.

| Option / Настройка | Result / Результат |
|---|---|
| `wide` / Широкий | One shared time column plus value and quality columns for each tag / Один общий столбец времени и столбцы значения и качества для каждого тега |
| `long` / Длинный | Consecutive `Date and time`, `Tag`, `Value`, `Quality (Stat)` rows / Последовательные строки `Дата и время`, `Тег`, `Значение`, `Качество (Stat)` |
| Split by day / Разделять по суткам | One `yyyy-MM-dd` worksheet for each local calendar day / Отдельный лист `yyyy-MM-dd` для каждых локальных суток |
| Summary / Сводка | Archive, channel, tag, count, minimum, maximum and average of good-quality samples / Архив, канал, тег, количество, минимум, максимум и среднее достоверных точек |

Every worksheet freezes its first row. The workbook is created by MiniExcel and does not require Microsoft Excel on the server. One export is limited to 10,000,000 points. The licensed unique-channel limit and ownership of every export token are validated before the background job is created.

На каждом листе закрепляется первая строка. Книга создаётся MiniExcel и не требует Microsoft Excel на сервере. Один экспорт ограничен 10 000 000 точек. Лицензионный лимит уникальных каналов и принадлежность каждого токена текущему пользователю проверяются до создания фонового задания.

## TrendWindow on a Mimic Diagram / TrendWindow на мнемосхеме

`TrendWindow` is a compact interactive trend component. The Mimic Editor shows deterministic demonstration data so appearance settings are visible without archive access. At runtime the component loads real compact data, supports wheel zoom, drag panning and tooltips, and shows a small Reset Zoom button in the upper-right corner only after the viewport changes.

`TrendWindow` — компактный интерактивный компонент тренда. В редакторе мнемосхемы показываются детерминированные тестовые данные, поэтому внешний вид можно настроить без доступа к архиву. Во время выполнения компонент загружает реальные компактные данные, поддерживает масштабирование колесом, перемещение перетаскиванием и Tooltip, а небольшая кнопка сброса масштаба появляется в правом верхнем углу только после изменения диапазона.

The component intentionally has no header, channel-number badge or permanent navigation button. When `openOnClick` is enabled, an ordinary runtime click on the chart opens the full TrendJP page in a new tab. Zooming, dragging and clicking Reset Zoom do not trigger navigation. In edit mode a click only selects the component.

У компонента намеренно нет заголовка, номера канала и постоянной кнопки перехода. Если включено `openOnClick`, обычный щелчок по графику во время выполнения открывает полную страницу TrendJP в новой вкладке. Масштабирование, перемещение и сброс масштаба переход не вызывают. В режиме редактирования щелчок только выделяет компонент.

### Plugin-Specific Mimic Properties / Собственные свойства компонента

The table lists every property added by `PlgTrendJP`. Standard mimic properties such as location, size, visibility, border and property bindings are inherited from the editor and are not TrendJP-specific.

В таблице перечислены все свойства, добавляемые `PlgTrendJP`. Стандартные свойства мнемосхемы — положение, размер, видимость, рамка и привязки — наследуются от редактора и не относятся только к TrendJP.

| Property / Свойство | Category / Категория | Default / По умолчанию | English description | Русское описание |
|---|---|---|---|---|
| `channelNumbers` / **Channels / Каналы** | Data / Данные | `1` | Channel expression with the same list and range syntax as `cnlNums`. | Выражение каналов с тем же синтаксисом списков и диапазонов, что у `cnlNums`. |
| `archiveCode` / **Archive / Архив** | Data / Данные | `Min` | One archive used by this component. The editor starts with `Cur`, `Min`, `Hour`, `Day` and replaces the list with the current project archive catalog when available. | Один архив компонента. Сначала редактор предлагает `Cur`, `Min`, `Hour`, `Day`, а при доступности заменяет список актуальным каталогом архивов проекта. |
| `periodValue` / **Period / Период** | Data / Данные | `1` | Positive duration magnitude for the rolling data window. | Положительное числовое значение скользящего периода данных. |
| `periodUnit` / **Period unit / Единица периода** | Data / Данные | `h` | Duration unit: seconds (`s`), minutes (`m`) or hours (`h`). | Единица периода: секунды (`s`), минуты (`m`) или часы (`h`). |
| `preset` / **Preset / Шаблон** | Appearance / Внешний вид | `default` | White `default` or `dark` visual theme. | Светлая тема `default` или тёмная `dark`. |
| `transparentBackground` / **Transparent background / Прозрачный фон** | Appearance / Внешний вид | `true` | Makes the component canvas, plot and legend backgrounds transparent so the mimic underlay remains visible. The selected preset still controls axes, grid, labels and series colors. | Делает фон Canvas, области графика и легенды прозрачным, чтобы была видна подложка мнемосхемы. Выбранная тема продолжает задавать оси, сетку, подписи и цвета рядов. |
| `trendType` / **Trend type / Тип тренда** | Appearance / Внешний вид | `line` | One of the supported renderer values listed in the Trend Types section. | Один из типов построения из раздела «Типы тренда». |
| `showLegend` / **Show legend / Показывать легенду** | Appearance / Внешний вид | `true` | Enables the embedded legend. `false` has priority over its position. | Включает легенду компонента. `false` имеет приоритет над положением. |
| `legendPosition` / **Legend position / Положение легенды** | Appearance / Внешний вид | `top` | `none`, `top`, `right`, `bottom` or `left`; `none` also disables the legend. | `none`, `top`, `right`, `bottom` или `left`; `none` также отключает легенду. |
| `markerShape` / **Point marker / Маркер точки** | Appearance / Внешний вид | `circle` | `circle`, `triangle` or `square`. Used in point renderers and tooltip highlighting. | `circle`, `triangle` или `square`. Используется в точечных режимах и подсветке Tooltip. |
| `lineWidth` / **Line width / Толщина линии** | Appearance / Внешний вид | `2` | Independent line width: `1`, `1.5`, `2`, `3` or `4` px. | Независимая толщина линии: `1`, `1.5`, `2`, `3` или `4` px. |
| `markerSize` / **Point size / Размер точки** | Appearance / Внешний вид | `3` | Independent marker size: `2`, `3`, `4`, `5`, `6` or `8` px. | Независимый размер точки: `2`, `3`, `4`, `5`, `6` или `8` px. |
| `autoRefresh` / **Auto refresh / Автообновление** | Behavior / Поведение | `true` | Periodically requests the current rolling window at runtime. It is disabled in the editor preview. | Периодически запрашивает текущий скользящий диапазон во время выполнения. В тестовом режиме редактора не выполняется. |
| `refreshSeconds` / **Refresh, sec / Обновление, сек** | Behavior / Поведение | `30` | Refresh delay in seconds. `0` stops periodic requests even if `autoRefresh` is enabled. | Задержка обновления в секундах. `0` останавливает периодические запросы даже при включённом `autoRefresh`. |
| `openOnClick` / **Open trend page on click / Открывать страницу тренда по клику** | Behavior / Поведение | `true` | Opens the full page with the same channels, archive, period, theme, trend type and applicable display settings. | Открывает полную страницу с теми же каналами, архивом, периодом, темой, типом тренда и применимыми настройками отображения. |

The legacy persisted property `periodHours` is accepted when an old mimic is opened and is interpreted as hours. New components use `periodValue` together with `periodUnit`.

Старое сохранённое свойство `periodHours` принимается при открытии существующей мнемосхемы и интерпретируется как часы. Новые компоненты используют `periodValue` вместе с `periodUnit`.

### Recommended Mimic Configurations / Рекомендуемые конфигурации мнемосхемы

| Scenario / Сценарий | Recommended properties / Рекомендуемые свойства |
|---|---|
| Trend over a process drawing / Тренд поверх технологической схемы | `transparentBackground=true`, `preset=default` or `dark` selected for readable axes / тема подбирается для читаемых осей |
| Compact analog trend / Компактный аналоговый тренд | `trendType=line`, `showLegend=false`, `periodValue=1`, `periodUnit=h` |
| Live current values / Оперативные текущие значения | `archiveCode=Cur`, `periodValue=30`, `periodUnit=s`, `autoRefresh=true`, `refreshSeconds=1` |
| Discrete state history / История дискретного состояния | `trendType=stepped`, `archiveCode=Min` |
| Detailed samples / Подробные отсчёты | `trendType=line-markers` or `points`, increased `markerSize` / увеличенный `markerSize` |
| Read-only dashboard tile / Плитка без перехода | `openOnClick=false` |

## Data Quality, Rights and Archives / Качество, права и архивы

A point is drawable only when its SCADA status is positive and its value is finite. Bad-quality or missing points break line, stepped, smooth and area paths instead of connecting the valid samples on either side. Tooltips and summary statistics use valid data according to the same quality contract.

Точка отображается только при положительном статусе SCADA и конечном числовом значении. Недостоверные или отсутствующие точки разрывают линии, ступеньки, сглаженные кривые и области, а не соединяют достоверные отсчёты по обе стороны. Tooltip и сводная статистика используют достоверные данные по тому же правилу качества.

The channel dialog filters by current user object-view rights. The JSON and PNG endpoints repeat active-channel and object-right checks before reading data, so direct URLs cannot bypass the selection restrictions.

Диалог каналов фильтрует данные по правам текущего пользователя на объекты. Конечные точки JSON и PNG повторно проверяют активность каналов и права на объекты до чтения данных, поэтому прямой URL не обходит ограничения выбора.

The archive list is built from the current `ConfigDatabase.ArchiveTable`; the plugin does not search archive folders on the Webstation disk. Archive data may reside on another disk if the Rapid SCADA Server configuration points to it and the service account has access. Event archives are not shown. The current archive is read through `GetCurrentData`, while historical archives use `GetTrends` with the configured archive bit.

Список архивов формируется из текущей `ConfigDatabase.ArchiveTable`; плагин не сканирует папки архивов на диске Вебстанции. Архивы могут находиться на другом диске, если путь задан в конфигурации Сервера Rapid SCADA и учётная запись службы имеет доступ. Архивы событий не отображаются. Текущий архив читается через `GetCurrentData`, исторические — через `GetTrends` с настроенным битом архива.

## Performance Notes / Особенности производительности

The browser receives a compact `compact-v2` response with one shared numeric time axis, value arrays and a default quality value plus sparse status exceptions. Large series are kept in typed arrays and rendered through a prepared level-of-detail cache. Zooming and panning operate on data already loaded in the browser and do not re-read the SCADA archive.

Браузер получает компактный ответ `compact-v2` с одной общей числовой осью времени, массивами значений, качеством по умолчанию и редкими исключениями статуса. Большие ряды хранятся в типизированных массивах и рисуются через подготовленный кэш уровней детализации. Масштабирование и перемещение работают с уже загруженными в браузер данными и не перечитывают архив SCADA.

The initial load time still includes the SCADA read, compact-array preparation, HTTP transfer and browser cache construction. Very large ranges and many channels can produce large responses, so choose the archive resolution and period appropriate for the operator task.

Время первой загрузки всё равно включает чтение SCADA, подготовку компактных массивов, передачу HTTP и построение браузерного кэша. Большие диапазоны и множество каналов создают крупные ответы, поэтому разрешение архива и период следует выбирать по задаче оператора.

## Troubleshooting / Устранение неполадок

| Symptom / Признак | Cause and action / Причина и действие |
|---|---|
| TrendJP is absent from Webstation / TrendJP отсутствует в Вебстанции | Check `<Plugin code="PlgTrendJP" />`, deployed DLLs and assets, then restart SCADA Web. / Проверьте регистрацию, DLL и браузерные ресурсы, затем перезапустите SCADA Web. |
| Standard chart action opens another plugin / Стандартная команда графика открывает другой плагин | Set `<ChartFeature>PlgTrendJP</ChartFeature>`. / Назначьте `<ChartFeature>PlgTrendJP</ChartFeature>`. |
| License error / Ошибка лицензии | Check the exact file name, host license directory, `AppName=PlgTrendJP`, positive `CountTags` and restart the host. / Проверьте имя файла, папку лицензии текущего приложения, `AppName=PlgTrendJP`, положительный `CountTags` и перезапустите приложение. |
| Channel limit exceeded / Превышен лимит каналов | Count unique expanded channel numbers across all sources and reduce them or use a suitable license. / Подсчитайте уникальные раскрытые номера каналов во всех источниках, сократите их либо используйте подходящую лицензию. |
| Archive list is empty / Список архивов пуст | Check the project configuration database and archive table. The Webstation does not discover archives by scanning folders. / Проверьте базу конфигурации проекта и таблицу архивов. Вебстанция не ищет архивы сканированием папок. |
| Archive is on another disk and no data is returned / Архив на другом диске, данные не возвращаются | Verify the Server archive path and read permissions of the Rapid SCADA service account. / Проверьте путь архива на Сервере и права чтения учётной записи службы Rapid SCADA. |
| Legend remains visible / Легенда остаётся видимой | Use `legendPosition=none` or `showLegend=false`; in XML separate parameters with `&amp;`. / Используйте `legendPosition=none` или `showLegend=false`; в XML разделяйте параметры через `&amp;`. |
| Relative expression with `+` fails / Не работает относительное выражение с `+` | Encode the plus sign as `%2B`, for example `DAY%2B1D`. / Кодируйте плюс как `%2B`, например `DAY%2B1D`. |
| Automatic refresh does not start / Автообновление не запускается | `refreshInterval` only selects the cadence; add `autoRefresh=true`. / `refreshInterval` только задаёт интервал; добавьте `autoRefresh=true`. |
| No control is visible in a headless view / В представлении без панели нет управления | `showToolbar=false` intentionally hides Actions too. Use `showControlPanel=false` to keep Actions. / `showToolbar=false` намеренно скрывает и «Действия». Используйте `showControlPanel=false`, чтобы оставить меню. |
| Gaps appear in a line / На линии есть разрывы | Check channel quality. `Stat <= 0`, missing and non-finite values are intentionally not connected. / Проверьте качество канала. Точки с `Stat <= 0`, отсутствующие и нечисловые значения намеренно не соединяются. |
| Excel export is unavailable or rejected / Экспорт Excel недоступен или отклонён | Load the trend first, keep the page open, check token expiry, the 10,000,000-point limit and the licensed channel count. / Сначала загрузите тренд, не закрывайте страницу, проверьте срок токена, лимит 10 000 000 точек и лицензионное число каналов. |

## Build / Сборка

The web project targets .NET 8 and uses `ScottPlot 5.1.58` and `MiniExcel 1.45.0`. The release package is created by:

```bat
C:\Projects\GitHub\scada-web-v6-develop\Plugins\Mimics\BuildPublish_TrendJP.bat
```

The script builds `PlgTrendJP` and `PlgTrendJP.View`, creates `Plugins\Mimics\Publish`, copies SCADA Web and Administrator files, language dictionaries, browser assets and project registration helpers, and applies the supplied protection step.

Веб-проект предназначен для .NET 8 и использует `ScottPlot 5.1.58` и `MiniExcel 1.45.0`. Скрипт собирает `PlgTrendJP` и `PlgTrendJP.View`, создаёт `Plugins\Mimics\Publish`, копирует файлы SCADA Web и Администратора, языковые словари, браузерные ресурсы и помощники регистрации проекта, после чего выполняет предусмотренный этап защиты.

## License / Лицензия

`PlgTrendJP` is distributed as shareware/commercial software. A valid installation-specific license is required. Do not rename the plugin DLL, activation request or license file.

`PlgTrendJP` распространяется как условно-бесплатное/коммерческое программное обеспечение. Требуется действующая лицензия, привязанная к установке. Не переименовывайте DLL плагина, запрос активации и файл лицензии.
