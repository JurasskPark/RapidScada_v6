# DrvDbImportPlus

![DrvDbImportPlus](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=DrvDbImportPlus&color=4bb60e)

![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=8.0&color=purple)
![Platform](https://jurasskpark.ru/service/budges/?label=platform&message=Windows%20%2F%20Linux&color=lightgrey)

**DrvDbImportPlus** is a Rapid SCADA communication driver for importing current tag values from relational databases and InfluxDB, and for sending Rapid SCADA telecontrol commands back to a database query.

**DrvDbImportPlus** - драйвер связи Rapid SCADA для импорта текущих значений тегов из реляционных БД и InfluxDB, а также для отправки команд телеуправления Rapid SCADA в запросы БД.

## Overview / Обзор

The driver is configured per Rapid SCADA device. Each device has its own XML configuration file, for example `DrvDbImportPlus_001.xml`. During a polling session the driver executes all enabled import commands, parses returned tables into driver tags, and updates matching Rapid SCADA channels.

The driver does not require a persistent device connection. Database connections are opened for query execution and then closed. The default polling period created by the view module is 5 seconds, but it can be changed in the communication line settings.

Драйвер настраивается отдельно для каждого КП Rapid SCADA. У каждого КП есть собственный XML-файл конфигурации, например `DrvDbImportPlus_001.xml`. Во время сеанса опроса драйвер последовательно выполняет все включённые команды импорта, разбирает полученные таблицы в теги драйвера и обновляет соответствующие каналы Rapid SCADA.

Драйверу не требуется постоянное соединение с КП. Соединение с БД открывается на время выполнения запроса и затем закрывается. Период опроса по умолчанию, создаваемый модулем представления, составляет 5 секунд, но его можно изменить в настройках линии связи.

## Features / Возможности

- **Multiple import queries** - executes several enabled import commands sequentially in one polling session
- **Multiple database engines** - supports Microsoft SQL Server, Oracle, PostgreSQL, MySQL, Firebird, InfluxDB 2.x and InfluxDB 3.x
- **Generated or custom connection string** - builds a connection string from UI fields or uses a custom encrypted connection string
- **Encrypted secrets** - stores the database password and custom connection string encrypted in the driver XML configuration
- **Column-based import** - maps result columns to configured tag names
- **Row-based import** - maps rows with `TAGNAME`, `TAGVALUE` and optional `TAGDATETIME` columns to configured tags
- **Command export** - runs configured SQL commands from Rapid SCADA telecontrol commands and passes the command value as `cmdVal`
- **Command import trigger** - import command definitions can also be found by command number or command code when a telecontrol command is received
- **Channel prototypes** - generates Rapid SCADA channel prototypes for numeric, string and command tags
- **String tag support** - creates Unicode channels for string import tags and command tags
- **Value formatting** - supports float, integer, DateTime, string and boolean tag formats
- **Decimal rounding** - numeric values are rounded by the configured number of decimal places
- **SQL editor** - configuration forms include SQL syntax highlighting, context menu actions and common keyboard shortcuts
- **Connection and query testing** - the UI can test database connection and execute SQL queries before saving configuration
- **Localization** - English and Russian language files are included

- **Несколько запросов импорта** - выполняет несколько включённых команд импорта последовательно за один сеанс опроса
- **Несколько типов БД** - поддерживает Microsoft SQL Server, Oracle, PostgreSQL, MySQL, Firebird, InfluxDB 2.x и InfluxDB 3.x
- **Автоматическая или пользовательская строка подключения** - формирует строку подключения из полей интерфейса или использует пользовательскую зашифрованную строку
- **Шифрование секретов** - пароль БД и пользовательская строка подключения хранятся в XML-конфигурации драйвера в зашифрованном виде
- **Импорт по колонкам** - сопоставляет колонки результата с настроенными именами тегов
- **Импорт по строкам** - сопоставляет строки с колонками `TAGNAME`, `TAGVALUE` и необязательной `TAGDATETIME` с настроенными тегами
- **Экспорт команд** - выполняет настроенные SQL-команды по командам телеуправления Rapid SCADA и передаёт значение команды как `cmdVal`
- **Запуск по команде импорта** - при получении команды телеуправления определения import-команд также могут находиться по номеру или коду команды
- **Прототипы каналов** - создаёт прототипы каналов Rapid SCADA для числовых, строковых и командных тегов
- **Поддержка строковых тегов** - создаёт Unicode-каналы для строковых тегов импорта и командных тегов
- **Форматирование значений** - поддерживает форматы float, integer, DateTime, string и boolean
- **Округление** - числовые значения округляются по настроенному количеству знаков после запятой
- **SQL-редактор** - формы настройки содержат подсветку SQL, контекстное меню и стандартные горячие клавиши
- **Проверка подключения и запросов** - интерфейс позволяет проверить соединение с БД и выполнить SQL-запрос до сохранения настройки
- **Локализация** - включены английские и русские языковые файлы

## Supported Data Sources / Поддерживаемые источники данных

| Data source | Notes | Примечание |
|---|---|---|
| `MSSQL` | Uses `Microsoft.Data.SqlClient` | Microsoft SQL Server через `Microsoft.Data.SqlClient` |
| `Oracle` | Uses `Oracle.ManagedDataAccess.Core` | Oracle через `Oracle.ManagedDataAccess.Core` |
| `PostgreSQL` | Uses `Npgsql` | PostgreSQL через `Npgsql` |
| `MySQL` | Uses `MySql.Data` | MySQL через `MySql.Data` |
| `Firebird` | Uses `FirebirdSql.Data.FirebirdClient` | Firebird через `FirebirdSql.Data.FirebirdClient` |
| `InfluxDBv2` | HTTP API, password field is used as token, database field is used as bucket | HTTP API, поле password используется как token, поле database используется как bucket |
| `InfluxDBv3` | HTTP API, password field is used as token, database field is used as database | HTTP API, поле password используется как token, поле database используется как database |

ODBC and OLE DB data sources are present only as legacy code comments and are not enabled in the current driver build.

Источники ODBC и OLE DB оставлены только как legacy-комментарии в коде и в текущей сборке драйвера не включены.

## How Import Works / Как работает импорт

Each import command contains:

- command number and command code;
- display name and description;
- SQL or Influx query text;
- processing mode: column-based or row-based;
- a list of configured driver tags.

Only enabled import commands and enabled tags are processed. The tag **Name** is used to find data in the query result. The tag **Code** is used to update the Rapid SCADA channel with the same code.

Каждая команда импорта содержит:

- номер и код команды;
- отображаемое имя и описание;
- текст SQL- или Influx-запроса;
- режим обработки: по колонкам или по строкам;
- список настроенных тегов драйвера.

Обрабатываются только включённые команды импорта и включённые теги. Поле **Name** используется для поиска данных в результате запроса. Поле **Code** используется для обновления канала Rapid SCADA с таким же кодом.

### Column-Based Mode / Режим по колонкам

In column-based mode the driver reads the first row of the result table. Column names are compared with configured tag names, case-insensitively.

Example:

```sql
select
  temperature as BoilerTemp,
  pressure as BoilerPressure,
  state as PumpState,
  measured_at as TAGDATETIME
from process_values
where unit_id = 1;
```

If a `TAGDATETIME` column exists, it is parsed as a common timestamp for the values. The current runtime updates current tag data; it does not backfill historical archive slices from `TAGDATETIME`.

The column-based parser also supports a single-row `TAGNAME` plus `TAGVALUE` result.

В режиме по колонкам драйвер читает первую строку результирующей таблицы. Имена колонок сравниваются с настроенными именами тегов без учёта регистра.

Если присутствует колонка `TAGDATETIME`, она разбирается как общий timestamp для значений. Текущая runtime-логика обновляет текущие данные тегов; исторические срезы архива из `TAGDATETIME` не дозаписываются.

Парсер режима по колонкам также поддерживает однострочный результат с колонками `TAGNAME` и `TAGVALUE`.

### Row-Based Mode / Режим по строкам

In row-based mode the query result must contain:

- `TAGNAME` - tag name configured in the driver;
- `TAGVALUE` - value to write to the tag;
- `TAGDATETIME` - optional timestamp.

Example:

```sql
select
  tag_name as TAGNAME,
  tag_value as TAGVALUE,
  tag_time as TAGDATETIME
from current_tag_values
where device_id = 1;
```

Every row is matched by `TAGNAME`. Rows with unknown tag names are ignored.

В режиме по строкам результат запроса должен содержать:

- `TAGNAME` - имя тега, настроенное в драйвере;
- `TAGVALUE` - значение для записи в тег;
- `TAGDATETIME` - необязательное время значения.

Каждая строка сопоставляется по `TAGNAME`. Строки с неизвестными именами тегов игнорируются.

## Tag Formats / Форматы тегов

| Driver format | Runtime behavior | Поведение |
|---|---|---|
| `Float` | Converts to double and applies numeric format `N{NumberDecimalPlaces}` | Преобразует в double и применяет числовой формат `N{NumberDecimalPlaces}` |
| `Integer` | Writes integer values with integer format | Записывает целые значения с целочисленным форматом |
| `DateTime` | Writes DateTime values as Rapid SCADA DateTime | Записывает DateTime-значения как DateTime Rapid SCADA |
| `String` | Writes Unicode string values | Записывает Unicode-строки |
| `Boolean` | Writes values with Off/On format | Записывает значения в формате Off/On |

If the imported value is `null` or `DBNull`, the corresponding Rapid SCADA tag data is invalidated.

Если импортированное значение равно `null` или `DBNull`, данные соответствующего тега Rapid SCADA помечаются недостоверными.

## Channel Prototypes / Прототипы каналов

The driver view generates channel prototypes from enabled import and export definitions:

| Group | Source | Description |
|---|---|---|
| `Tags` / `Теги` | Non-string import tags | Numeric, integer, DateTime and boolean import channels |
| `String tags` / `Строковые теги` | Import tags with `String` format | Unicode channels. Data length is calculated as `ceil(NumberDecimalPlaces / 4)` |
| `Command tags` / `Командные теги` | Enabled export commands | Unicode command channels. Data length is calculated as `ceil(Length / 4)` |

Duplicate tag codes and duplicate command codes are skipped while generating prototypes.

Представление драйвера создаёт прототипы каналов из включённых команд импорта и экспорта:

| Группа | Источник | Описание |
|---|---|---|
| `Tags` / `Теги` | Нестроковые теги импорта | Числовые, целочисленные, DateTime и boolean-каналы импорта |
| `String tags` / `Строковые теги` | Теги импорта с форматом `String` | Unicode-каналы. Длина данных вычисляется как `ceil(NumberDecimalPlaces / 4)` |
| `Command tags` / `Командные теги` | Включённые команды экспорта | Unicode-каналы команд. Длина данных вычисляется как `ceil(Length / 4)` |

Повторяющиеся коды тегов и коды команд пропускаются при генерации прототипов.

## Telecontrol Commands / Команды телеуправления

The driver can receive Rapid SCADA telecontrol commands. A command is matched by command code or command number against enabled export commands and, as a fallback, import command definitions. When a matching database command is found, the driver:

English:

1. Initializes the configured data source.
2. Adds or updates the database command parameter `cmdVal`.
3. Uses `CmdVal` for numeric commands, or converts `CmdData` to string for binary/string commands.
4. Executes the configured SQL command with `ExecuteNonQuery`.
5. Updates the matching command tag in Rapid SCADA if a tag with the same command code exists.

Example:

```sql
insert into operator_commands(command_code, command_value, created_at)
values ('PUMP_SETPOINT', @cmdVal, current_timestamp);
```

Use the parameter syntax supported by the selected database provider. The driver creates the logical parameter name `cmdVal`; the provider decides the final placeholder style.

Драйвер принимает команды телеуправления Rapid SCADA. Команда сопоставляется по коду или номеру команды с включёнными командами экспорта, а затем, как fallback, с определениями команд импорта. Когда команда БД найдена, драйвер:

Русский:

1. Инициализирует настроенный источник данных.
2. Добавляет или обновляет параметр команды БД `cmdVal`.
3. Использует `CmdVal` для числовых команд или преобразует `CmdData` в строку для бинарных/строковых команд.
4. Выполняет настроенную SQL-команду через `ExecuteNonQuery`.
5. Обновляет соответствующий командный тег Rapid SCADA, если тег с таким же кодом команды существует.

Используйте синтаксис параметров, поддерживаемый выбранным провайдером БД. Драйвер создаёт логическое имя параметра `cmdVal`; финальный стиль placeholder зависит от провайдера.

## InfluxDB Notes / Замечания по InfluxDB

InfluxDB support is implemented through the HTTP `/query` API. The driver builds a connection string from UI fields if a custom connection string is not specified:

- `Server` and `Port` become `Url`, default port is `8086`;
- `Password` is used as the bearer token;
- `Database` is used as `Bucket` for InfluxDB 2.x;
- `Database` is used as `Database` for InfluxDB 3.x;
- `OptionalOptions` are appended to the generated connection string.

Поддержка InfluxDB реализована через HTTP API `/query`. Если пользовательская строка подключения не задана, драйвер формирует её из полей интерфейса:

- `Server` и `Port` формируют `Url`, порт по умолчанию - `8086`;
- `Password` используется как bearer token;
- `Database` используется как `Bucket` для InfluxDB 2.x;
- `Database` используется как `Database` для InfluxDB 3.x;
- `OptionalOptions` добавляются в сформированную строку подключения.

## Usage / Использование

English:

1. Add `DrvDbImportPlus` to a communication line in ScadaAdmin.
2. Open device properties for the required device.
3. Select the database type and configure connection settings or a custom connection string.
4. Use the connection test to verify access to the database.
5. Add one or more import commands and define their queries.
6. Test each query in the SQL editor and configure driver tags.
7. Add export commands if Rapid SCADA telecontrol commands must write to the database.
8. Generate channel prototypes for the device.
9. Upload the project and restart the communication line.

Русский:

1. Добавьте `DrvDbImportPlus` в линию связи в ScadaAdmin.
2. Откройте свойства нужного КП.
3. Выберите тип БД и настройте параметры подключения или пользовательскую строку подключения.
4. Проверьте доступ к БД через тест соединения.
5. Добавьте одну или несколько команд импорта и задайте их запросы.
6. Проверьте каждый запрос в SQL-редакторе и настройте теги драйвера.
7. Добавьте команды экспорта, если команды телеуправления Rapid SCADA должны записывать данные в БД.
8. Создайте прототипы каналов для КП.
9. Загрузите проект и перезапустите линию связи.

## Safety Notes / Замечания по безопасности

- Import queries are executed every polling cycle, so keep them deterministic and fast.
- Column-based mode reads only the first returned row. Use row-based mode when one result must contain many tag rows.
- `TAGDATETIME` is parsed, but the current runtime writes current tag values rather than historical archive slices.
- Command export queries are executed as non-query commands. Use restricted database accounts for write operations.
- Password and custom connection string are encrypted in the XML configuration, but database-side permissions still remain the main protection.
- Empty, `null` or `DBNull` values invalidate the target tag data.
- Detailed driver logging can write query results and tag tables to the communication line log. Avoid enabling it permanently for sensitive data.

- Запросы импорта выполняются каждый цикл опроса, поэтому они должны быть детерминированными и быстрыми.
- Режим по колонкам читает только первую возвращённую строку. Используйте режим по строкам, если один результат должен содержать много строк тегов.
- `TAGDATETIME` разбирается, но текущая runtime-логика записывает текущие значения тегов, а не исторические срезы архива.
- Команды экспорта выполняются как non-query команды. Для операций записи используйте ограниченные учётные записи БД.
- Пароль и пользовательская строка подключения шифруются в XML-конфигурации, но основная защита всё равно задаётся правами на стороне БД.
- Пустые значения, `null` и `DBNull` помечают данные целевого тега недостоверными.
- Подробное логирование драйвера может записывать результаты запросов и таблицы тегов в лог линии связи. Не включайте его постоянно для чувствительных данных.

## Project Structure / Структура проекта

```text
DrvDbImportPlus_v6/
├── DrvDbImportPlus.sln                  # Solution file
├── StartСompilingFull.bat               # Build and deployment helper
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

## Build / Сборка

### Prerequisites / Требования

- .NET 8.0 SDK
- Rapid SCADA 6.5 libraries in `Libraries`
- Windows for the ScadaAdmin view and standalone WinForms UI
- Database provider packages used by the selected data source

### Commands / Команды

```powershell
dotnet build .\DrvDbImportPlus.sln -c Release
```

The `StartСompilingFull.bat` script publishes runtime packages for Windows x64, Windows x86 and Linux x64 logic, and can copy files to a local Rapid SCADA installation.

Скрипт `StartСompilingFull.bat` публикует пакеты для Windows x64, Windows x86 и runtime-логики Linux x64, а также может копировать файлы в локальную установку Rapid SCADA.

## Videos / Видео

[Video on YouTube](https://www.youtube.com/watch?v=cZMksoblUgo)

[Video on YouTube](https://www.youtube.com/watch?v=TFZfBMuRMVU)

[![Video on YouTube](https://img.youtube.com/vi/cZMksoblUgo/0.jpg)](https://www.youtube.com/watch?v=cZMksoblUgo)
[![Video on YouTube](https://img.youtube.com/vi/TFZfBMuRMVU/0.jpg)](https://www.youtube.com/watch?v=TFZfBMuRMVU)

## Screenshots / Скриншоты

![DrvDbImportPlus](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/Source/DrvDbImportPlus_001.png)
![DrvDbImportPlus](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/Source/DrvDbImportPlus_002.png)
![DrvDbImportPlus](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/Source/DrvDbImportPlus_003.png)
![DrvDbImportPlus](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/Source/DrvDbImportPlus_004.png)
![DrvDbImportPlus](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/Source/DrvDbImportPlus_005.png)
![DrvDbImportPlus](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/Source/DrvDbImportPlus_006.png)
![DrvDbImportPlus](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/Source/DrvDbImportPlus_007.png)

## License / Лицензия

This project is part of the Rapid SCADA ecosystem.

Данный проект является частью экосистемы Rapid SCADA.

## SAST Tools

[PVS-Studio](https://pvs-studio.ru/ru/pvs-studio/?utm_source=website&utm_medium=github&utm_campaign=open_source) - static analyzer for C, C++, C#, and Java code.
