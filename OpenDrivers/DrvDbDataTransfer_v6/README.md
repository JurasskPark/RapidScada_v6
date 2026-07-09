# DrvDbDataTransfer

![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=8.0&color=purple)
![Platform](https://jurasskpark.ru/service/budges/?label=platform&message=Windows%20%2F%20Linux&color=lightgrey)

**DrvDbDataTransfer** is a Rapid SCADA 6 communication driver for moving data between databases. It executes a `SELECT` query against a source database, then writes the returned rows to a target database using a parameterized `INSERT`, `UPDATE`, `MERGE` or UPSERT command.

The same `SELECT` result can also update Rapid SCADA tags. If a transfer command contains configured tags, the driver maps result columns to tag names after a successful transfer.

**DrvDbDataTransfer** - драйвер связи Rapid SCADA 6 для переноса данных между базами данных. Он выполняет `SELECT` в базе-источнике и записывает полученные строки в базу-приемник через параметризованный `INSERT`, `UPDATE`, `MERGE` или UPSERT.

Тот же результат `SELECT` может дополнительно обновлять теги Rapid SCADA. Если в команде переноса настроены теги, драйвер после успешного переноса сопоставляет колонки результата с именами тегов.

## Features / Возможности

- Source and target database connections are configured independently.
- Supported providers: Microsoft SQL Server, Oracle, PostgreSQL, MySQL, Firebird, InfluxDB 2.x and InfluxDB 3.x.
- `SelectQuery` is executed only against the source database.
- `InsertQuery` is executed only against the target database.
- Target parameters are filled from `SELECT` columns by name.
- Writes use `DbParameter`, not string concatenation.
- Target writes run in transactions.
- `BatchSize` can split target writes into smaller transactions.
- `StopOnError` can stop transfer on the first write error.
- Empty `SELECT` results skip target writes and are not logged as transfer results.
- `SelectQuery` supports date/time table-name patterns such as `{YYYY}{MM}{DD}`.
- The same source data can update Rapid SCADA tags by column name or by `TAGNAME`/`TAGVALUE` rows.
- Telecontrol command export is preserved: driver commands can execute SQL with `cmdVal`.
- English and Russian UI language files are included.

- Подключения источника и приемника настраиваются независимо.
- Поддерживаемые провайдеры: Microsoft SQL Server, Oracle, PostgreSQL, MySQL, Firebird, InfluxDB 2.x и InfluxDB 3.x.
- `SelectQuery` выполняется только в базе-источнике.
- `InsertQuery` выполняется только в базе-приемнике.
- Параметры целевой команды заполняются из колонок `SELECT` по имени.
- Запись выполняется через `DbParameter`, без склейки SQL-строк со значениями.
- Запись в приемник выполняется в транзакциях.
- `BatchSize` может разбивать запись в приемник на транзакции меньшего размера.
- `StopOnError` может остановить перенос при первой ошибке записи.
- Если `SELECT` вернул 0 строк, запись в приемник не выполняется и результат переноса не логируется.
- `SelectQuery` поддерживает шаблоны даты и времени для имен таблиц, например `{YYYY}{MM}{DD}`.
- Те же данные источника могут обновлять теги Rapid SCADA по именам колонок или по строкам `TAGNAME`/`TAGVALUE`.
- Сохранен экспорт команд телеуправления: команды драйвера могут выполнять SQL с параметром `cmdVal`.
- В комплекте есть английская и русская локализация интерфейса.

## Supported Databases / Поддерживаемые БД

| Data source | Provider | Notes |
|---|---|---|
| `MSSQL` | `Microsoft.Data.SqlClient` | Use a platform-specific package, especially on Windows x64. |
| `Oracle` | `Oracle.ManagedDataAccess.Core` | Oracle parameters are normalized to `:name`. |
| `PostgreSQL` | `Npgsql` | Supports quoted table names such as `public."20260708.Data"`. |
| `MySQL` | `MySql.Data` | SSL mode can be set through connection options. |
| `Firebird` | `FirebirdSql.Data.FirebirdClient` | Supports Firebird SQL commands supported by the provider. |
| `InfluxDBv2` | HTTP API | Password field is used as token, database field as bucket. |
| `InfluxDBv3` | HTTP API | Password field is used as token, database field as database. |

## Transfer Mode / Режим переноса

A command works in transfer mode when both fields are filled:

- `SelectQuery`
- `InsertQuery`

Команда работает в режиме переноса, если заполнены оба поля:

- `SelectQuery`
- `InsertQuery`

### Execution Flow / Порядок выполнения

1. The driver resolves date/time patterns in `SelectQuery`.
2. The driver executes `SelectQuery` against `SourceDbConnSettings`.
3. The result is read into a `DataTable`.
4. If the result is empty, the command stops without target writes.
5. The driver executes `InsertQuery` against `TargetDbConnSettings`.
6. Target SQL parameters are filled from `DataTable` columns by name.
7. If tags are configured and rows were read successfully, the same `DataTable` is converted to Rapid SCADA tag values.
8. The driver logs transfer result only when rows were read or an error occurred.

Русский:

1. Драйвер подставляет шаблоны даты и времени в `SelectQuery`.
2. Драйвер выполняет `SelectQuery` в `SourceDbConnSettings`.
3. Результат читается в `DataTable`.
4. Если результат пустой, запись в приемник не выполняется.
5. Драйвер выполняет `InsertQuery` в `TargetDbConnSettings`.
6. SQL-параметры приемника заполняются из колонок `DataTable` по имени.
7. Если в команде настроены теги и строки успешно прочитаны, та же `DataTable` преобразуется в значения тегов Rapid SCADA.
8. Результат переноса логируется только если строки были прочитаны или произошла ошибка.

### SELECT Contract

`SelectQuery` defines the data contract. Every target parameter in `InsertQuery` must have a matching column or alias in the `SELECT` result.

Good:

```sql
SELECT
    occur_time AS AU_DateReport,
    description AS AU_Message,
    20::bigint AS AU_ALID
FROM public.event_data
WHERE level = 'Alarm';
```

If a source column has spaces or punctuation, give it a simple alias:

```sql
SELECT
    "Parametr" AS Parametr
FROM public."{YYYY}{MM}{DD}.Data";
```

`SelectQuery` may contain extra columns. Extra columns are not written to the target unless `InsertQuery` contains matching parameters, but they can still be used for Rapid SCADA tags.

### INSERT / UPDATE / UPSERT Contract

`InsertQuery` is a target non-query command. The setting name is historical: the command can be `INSERT`, `UPDATE`, `MERGE`, PostgreSQL `INSERT ... ON CONFLICT`, MySQL `INSERT ... ON DUPLICATE KEY UPDATE`, Firebird `UPDATE OR INSERT`, or another provider-supported non-query command.

Supported parameter forms:

- `@name`
- `:name`

The recommended style in the UI is `@name`. Oracle commands are normalized to `:name` automatically.

Example:

```sql
INSERT INTO dbo.Report_Data_JSON
    (AU_Id, AU_Date, AU_DateReport, AU_ALID, AU_Message)
VALUES
    (NEWID(), GETDATE(), @AU_DateReport, @AU_ALID, @AU_Message);
```

For this target command the source query must return columns named:

- `AU_DateReport`
- `AU_ALID`
- `AU_Message`

## Updating Rapid SCADA Tags / Обновление тегов Rapid SCADA

Transfer mode can also update device tags. This is optional.

To update tags:

1. Add tags to the command.
2. Set each tag **Name** equal to a `SELECT` column name.
3. Set each tag **Code** equal to the Rapid SCADA channel code.
4. Use the proper tag format: `Float`, `Integer`, `DateTime`, `String` or `Boolean`.

Example:

```sql
SELECT
    20::bigint AS AU_ALID,
    1 AS AU_Process,
    to_timestamp(("time" - 621355968000000000)::double precision / 10000000) AS AU_DateReport,
    (jsonb_build_object('time', "time"))::text AS AU_Message
FROM public."{YYYY}{MM}{DD}.Data"
ORDER BY "time"
LIMIT 1;
```

Matching tags:

| Tag Name | Tag Code | Format |
|---|---|---|
| `AU_ALID` | `AU_ALID_Code` | `Integer` |
| `AU_Process` | `AU_Process_Code` | `Boolean` or numeric |
| `AU_DateReport` | `AU_DateReport_Code` | `DateTime` |
| `AU_Message` | `AU_Message_Code` | `String` |

Important rules:

- The tag **Name** must match the `SELECT` column name, not a JSON field inside a text column.
- If `AU_Message` contains JSON, a tag named `APM_CheEx_LifeTime` will not read the JSON property. Add `APM_CheEx_LifeTime` as a separate `SELECT` column if it must be a tag.
- String tags use Unicode channels. The configured string length must be large enough for the value.
- Numeric `bigint` values from PostgreSQL are written as normal numeric tag values.

Русский:

- **Name** тега должен совпадать с именем колонки `SELECT`, а не с полем внутри JSON-строки.
- Если `AU_Message` содержит JSON, тег `APM_CheEx_LifeTime` не прочитает свойство JSON. Добавьте `APM_CheEx_LifeTime` отдельной колонкой `SELECT`, если это должен быть тег.
- Строковые теги используют Unicode-каналы. Длина строки должна быть достаточной для значения.
- PostgreSQL `bigint` записывается как обычное числовое значение тега.

## Legacy Tag Import / Старый режим импорта тегов

If `InsertQuery` is empty, the command does not transfer data to a target database. It works as a tag import command.

Если `InsertQuery` пустой, команда не выполняет перенос в базу-приемник. Она работает как команда импорта тегов.

### Column-Based Mode / Режим по колонкам

In column-based mode the first result row contains tag values. Column aliases are matched with configured tag names.

```sql
SELECT
    temperature AS BoilerTemp,
    pressure AS BoilerPressure
FROM process_values
ORDER BY measured_at DESC
LIMIT 1;
```

### Row-Based Mode / Режим по строкам

In row-based mode each row contains one tag value. The result must contain:

- `TAGNAME`
- `TAGVALUE`
- optional `TAGTIME` or `TAGDATETIME`

```sql
SELECT
    tag_name AS TAGNAME,
    tag_value AS TAGVALUE,
    tag_time AS TAGDATETIME
FROM current_tag_values;
```

`TAGTIME` and `TAGDATETIME` are parsed as timestamps. Tags with timestamps are grouped and enqueued as historical slices by the driver runtime.

`TAGTIME` и `TAGDATETIME` разбираются как метки времени. Теги с метками времени группируются и передаются runtime-логике драйвера как исторические срезы.

## Date/Time Patterns / Шаблоны даты и времени

`SelectQuery` is processed by:

```csharp
DriverUtils.ResolveDateTimePatterns(string input, DateTime? dateTime = null)
```

Supported tokens:

| Token | Meaning | Example |
|---|---|---|
| `{YYYY}` | four-digit year | `2026` |
| `{YY}` | two-digit year | `26` |
| `{MM}` | month | `07` |
| `{DD}` | day | `09` |
| `{HH}` | hour | `21` |
| `{mm}` | minute | `05` |
| `{ss}` | second | `08` |

Example:

```sql
SELECT *
FROM public."{YYYY}{MM}{DD}.Data"
WHERE "time" >= 638940096000000000;
```

Unknown expressions in braces are left unchanged. Historical SQL-window processing is not part of the current driver.

Неизвестные выражения в фигурных скобках не изменяются. Историческая обработка SQL-окон не входит в текущий драйвер.

## Polling Windows / Окна опроса

The driver does not remember the previous successful SQL timestamp. If a query uses a moving time condition, the SQL itself must define a bounded window.

Bad for periodic polling:

```sql
WHERE occur_time >= localtimestamp - interval '5 hour 5 minute'
```

This returns the same trailing interval on every polling cycle.

Better:

```sql
WHERE occur_time >= localtimestamp - interval '5 hour 5 minute 10 second'
  AND occur_time <  localtimestamp - interval '5 hour 5 minute'
```

Use target-side unique keys or UPSERT when duplicate protection is required.

Драйвер не хранит timestamp предыдущего успешного SQL-запроса. Если запрос использует скользящее время, само SQL-условие должно задавать ограниченное окно. Для защиты от дублей используйте уникальные ключи или UPSERT на стороне приемника.

## Logging / Логирование

The driver writes transfer progress to the communication line log:

- command name;
- source and target database type;
- read row count;
- written row count;
- target write errors;
- optional query result and tag tables when debug logging is enabled.

If `SELECT` returns 0 rows in transfer mode, target write is skipped and the transfer result is not logged.

## Deployment Notes / Замечания по развертыванию

For Microsoft SQL Server the driver uses `Microsoft.Data.SqlClient`. Use the package matching the server platform:

- `DrvDbDataTransfer_6.5.0.1_win-x64`
- `DrvDbDataTransfer_6.5.0.1_win-x32`
- `DrvDbDataTransfer_6.5.0.1_linux-x64`
- `DrvDbDataTransfer_6.5.0.1_anycpu`

If the wrong package is deployed, `Microsoft.Data.SqlClient is not supported on this platform` can appear at runtime. On Windows x64, use the `win-x64` package.

Если установлен неподходящий пакет, во время работы может появиться ошибка `Microsoft.Data.SqlClient is not supported on this platform`. Для Windows x64 используйте пакет `win-x64`.

Check the communication line log after deployment. It should show the expected driver version, for example:

```text
[Driver DrvDbDataTransfer]
[Version 6.5.0.1]
```

## Build / Сборка

Requirements:

- .NET 8 SDK
- Rapid SCADA 6.5 libraries in `Libraries`
- Windows for the View and WinForms projects

Build solution:

```powershell
dotnet build .\DrvDbDataTransfer.sln -v minimal
```

Build release packages:

```cmd
StartСompiling.bat
```

Package-only mode:

```cmd
StartСompiling.bat --package-only
```

The build script creates platform folders such as:

```text
DrvDbDataTransfer_6.5.0.1_win-x64\Release\SCADA
DrvDbDataTransfer_6.5.0.1_win-x32\Release\SCADA
DrvDbDataTransfer_6.5.0.1_linux-x64\Release\SCADA
DrvDbDataTransfer_6.5.0.1_anycpu\Release\SCADA
```

## Project Structure / Структура проекта

```text
DrvDbDataTransfer_v6/
├── DrvDbDataTransfer.sln
├── StartСompiling.bat
├── README.md
├── DrvDbDataTransfer.Logic/       # runtime driver for ScadaComm
├── DrvDbDataTransfer.View/        # ScadaAdmin UI
├── DrvDbDataTransfer.Shared/      # shared settings, database, tag and utility code
├── DrvDbDataTransfer.Winform/     # standalone WinForms test/admin app
├── DrvDbDataTransfer.Converter/   # old XML converter
└── DrvDbDataTransfer.Tests/       # lightweight regression tests
```

## Configuration XML / XML-конфигурация

Main nodes:

```xml
<DrvDbDataTransferProject>
  <SourceDbConnSettings>...</SourceDbConnSettings>
  <TargetDbConnSettings>...</TargetDbConnSettings>
  <ImportCmds>
    <ImportCmd>
      <SelectQuery>...</SelectQuery>
      <InsertQuery>...</InsertQuery>
      <StopOnError>true</StopOnError>
      <BatchSize>0</BatchSize>
      <IsColumnBased>true</IsColumnBased>
      <DeviceTags>...</DeviceTags>
    </ImportCmd>
  </ImportCmds>
  <ExportCmds>...</ExportCmds>
</DrvDbDataTransferProject>
```

## Screenshots / Скриншоты

![DrvDbDataTransfer](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/Source/DrvDbDataTransfer_001.png)
![DrvDbDataTransfer](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/Source/DrvDbDataTransfer_002.png)


## License / Лицензия

This project is part of the Rapid SCADA ecosystem.

Данный проект является частью экосистемы Rapid SCADA.

## SAST Tools

[PVS-Studio](https://pvs-studio.ru/ru/pvs-studio/?utm_source=website&utm_medium=github&utm_campaign=open_source) - static analyzer for C, C++, C#, and Java code.
