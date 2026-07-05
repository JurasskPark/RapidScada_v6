# DrvDbDataTransfer

![DrvDbDataTransfer](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=DrvDbDataTransfer&color=4bb60e)

![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=8.0&color=purple)
![Platform](https://jurasskpark.ru/service/budges/?label=platform&message=Windows%20%2F%20Linux&color=lightgrey)

**DrvDbDataTransfer** is a Rapid SCADA communication driver for transferring data between different database engines. It can read rows from a source database using a `SELECT` query and write them to a target database using a parameterized `INSERT`, `UPDATE` or `UPSERT` query. The legacy Rapid SCADA tag import and telecontrol command export scenarios are preserved for compatibility.

**DrvDbDataTransfer** - драйвер связи Rapid SCADA для перекладки данных между разными типами баз данных. Драйвер читает строки из исходной БД через `SELECT` и записывает их в целевую БД через параметризованный `INSERT`, `UPDATE` или `UPSERT`. Legacy-сценарии импорта тегов Rapid SCADA и экспорта команд телеуправления сохранены для совместимости.

## Overview / Обзор

The driver is configured per Rapid SCADA device. Each device has its own XML configuration file, for example `DrvDbDataTransfer_001.xml`. A configuration contains two independent database connections: source and target. During a polling session the driver executes enabled transfer commands, reads source rows into a table and writes them to the target database inside a transaction.

The driver does not require a persistent device connection. Database connections are opened for query execution and then closed. The default polling period created by the view module is 5 seconds, but it can be changed in the communication line settings.

Драйвер настраивается отдельно для каждого КП Rapid SCADA. У каждого КП есть собственный XML-файл конфигурации, например `DrvDbDataTransfer_001.xml`. Конфигурация содержит два независимых подключения к БД: источник и приемник. Во время сеанса опроса драйвер выполняет включённые команды переноса, читает строки источника в таблицу и записывает их в целевую БД внутри транзакции.

Драйверу не требуется постоянное соединение с КП. Соединение с БД открывается на время выполнения запроса и затем закрывается. Период опроса по умолчанию, создаваемый модулем представления, составляет 5 секунд, но его можно изменить в настройках линии связи.

## Features / Возможности

- **Database-to-database transfer** - reads rows from a source DB and writes them to a target DB using parameterized commands
- **Independent source and target connections** - each side has its own provider type, generated connection string, custom connection string and connection test
- **Multiple transfer queries** - executes several enabled transfer commands sequentially in one polling session
- **Multiple database engines** - supports Microsoft SQL Server, Oracle, PostgreSQL, MySQL, Firebird, InfluxDB 2.x and InfluxDB 3.x
- **Generated or custom connection string** - builds a connection string from UI fields or uses a custom encrypted connection string
- **Encrypted secrets** - stores the database password and custom connection string encrypted in the driver XML configuration
- **Parameterized target writes** - maps source columns to target SQL parameters without string-building SQL values
- **Type conversion layer** - preserves `bigint`/`int8` as `long`, numeric values as `decimal`, GUIDs, byte arrays, booleans and timestamps
- **Transactional writes** - target writes are committed on success and rolled back when stop-on-error is enabled
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

- **Перекладка данных БД-БД** - читает строки из исходной БД и записывает их в целевую БД параметризованными командами
- **Независимые подключения источника и приемника** - для каждой стороны задается свой тип провайдера, сгенерированная строка подключения, пользовательская строка и тест соединения
- **Несколько команд переноса** - выполняет несколько включённых команд переноса последовательно за один сеанс опроса
- **Несколько типов БД** - поддерживает Microsoft SQL Server, Oracle, PostgreSQL, MySQL, Firebird, InfluxDB 2.x и InfluxDB 3.x
- **Автоматическая или пользовательская строка подключения** - формирует строку подключения из полей интерфейса или использует пользовательскую зашифрованную строку
- **Шифрование секретов** - пароль БД и пользовательская строка подключения хранятся в XML-конфигурации драйвера в зашифрованном виде
- **Параметризованная запись в приемник** - сопоставляет колонки источника с SQL-параметрами приемника без генерации SQL-строк со значениями
- **Слой конвертации типов** - сохраняет `bigint`/`int8` как `long`, числовые значения как `decimal`, GUID, byte array, boolean и timestamp
- **Транзакционная запись** - записи в приемник фиксируются при успехе и откатываются при ошибке, если включен режим остановки при ошибке
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

## How DB Transfer Works / Как работает перенос БД-БД

Each transfer command contains:

- command number and command code;
- display name and description;
- `SelectQuery` executed against the source database;
- `InsertQuery` executed against the target database;
- `StopOnError`, enabled by default;
- optional `BatchSize` reserved for future bulk/batch strategies.

The target command must use parameters whose names match the `SELECT` column names. The UI uses the `@name` parameter style. The driver normalizes parameter prefixes for providers that require a different style, such as Oracle.

Data flow:

1. The driver opens the source database connection.
2. It executes `SelectQuery` only against the source database.
3. It reads the result into an internal `DataTable`.
4. It opens the target database connection and starts a target transaction.
5. It extracts parameters from `InsertQuery`.
6. For each source row, it finds a source column with the same name as each target parameter.
7. It writes values through `DbParameter` and executes `InsertQuery` once per row.
8. It commits the target transaction on success or rolls it back when `StopOnError` is enabled and an error occurs.

Values are not concatenated into SQL text. The driver passes `long`, `decimal`, `DateTime`, `Guid`, `bool`, `byte[]`, `DBNull` and other CLR values through provider parameters.

### SELECT Query Contract

`SelectQuery` is a normal read query for the source database. Its result columns are the data contract for the target write query.

Rules:

- Return one result row for each target write operation.
- Use explicit aliases for every value that must be written to the target database.
- Alias names should be parameter-friendly: Latin letters, digits and underscore, starting with a letter or underscore.
- If a source column has spaces, punctuation or provider-specific quoting, give it a simple alias.
- Extra `SELECT` columns are allowed, but they are ignored unless `InsertQuery` contains a parameter with the same name.
- `SelectQuery` is executed as text. The DB-to-DB transfer flow does not automatically create parameters for `SelectQuery`.

Good source query:

```sql
select
  src_id as id,
  src_tag as tag_name,
  src_value as tag_value,
  src_time as measured_at
from source_values;
```

Avoid unstable aliases:

```sql
-- Bad for transfer parameters: the aliases contain spaces and punctuation.
select
  src_id as "Source ID",
  src_value as "Value, kg"
from source_values;
```

Use simple aliases instead:

```sql
select
  src_id as source_id,
  src_value as value_kg
from source_values;
```

### INSERT, UPDATE and UPSERT Query Contract

`InsertQuery` is executed only against the target database. Despite the setting name, it can contain `INSERT`, `UPDATE`, `MERGE`, PostgreSQL `INSERT ... ON CONFLICT`, MySQL/MariaDB `INSERT ... ON DUPLICATE KEY UPDATE`, Firebird `UPDATE OR INSERT`, or another non-query command supported by the selected provider.

The driver understands what to send by reading parameter placeholders from `InsertQuery`:

- Supported placeholder forms are `@name` and `:name`.
- The recommended UI style is `@name`.
- Oracle commands are converted from `@name` to `:name` automatically.
- Parameter names are matched to `SELECT` columns without the `@` or `:` prefix.
- Matching is case-insensitive.
- `@@name` tokens are ignored, so SQL Server system variables such as `@@ROWCOUNT` are not treated as transfer parameters.
- A parameter name must start with a Latin letter or underscore and can contain Latin letters, digits and underscore.

For each source row, the driver effectively does this:

```text
for each parameter in InsertQuery:
    columnName = parameter name without @ or :
    value = current SELECT row[columnName]
    command.Parameters[parameter] = value

execute InsertQuery
```

If `InsertQuery` contains `@tag_value`, the source `SELECT` must return a column or alias named `tag_value`. If it does not, the command fails with an error like `Parameter @tag_value does not match a SELECT column`.

The target table column names do not need to match the source column names. Only target SQL parameters must match the `SELECT` aliases:

```sql
-- source SELECT
select
  sensor_id as id,
  sensor_value as value,
  event_time as measured_at
from sensor_history;

-- target INSERT
insert into archive_values(target_id, archived_value, archived_at)
values (@id, @value, @measured_at);
```

In this example:

- target column `target_id` receives source column `id`;
- target column `archived_value` receives source column `value`;
- target column `archived_at` receives source column `measured_at`.

The mapping comes from `values (@id, @value, @measured_at)`, not from equal table or column names.

### Common Target Query Examples

Plain insert:

```sql
insert into target_values(id, tag_name, tag_value, measured_at)
values (@id, @tag_name, @tag_value, @measured_at);
```

Update existing target rows:

```sql
update target_values
set
  tag_name = @tag_name,
  tag_value = @tag_value,
  measured_at = @measured_at
where id = @id;
```

PostgreSQL upsert:

```sql
insert into target_values(id, tag_name, tag_value, measured_at)
values (@id, @tag_name, @tag_value, @measured_at)
on conflict (id) do update
set
  tag_name = excluded.tag_name,
  tag_value = excluded.tag_value,
  measured_at = excluded.measured_at;
```

SQL Server `MERGE`:

```sql
merge dbo.target_values as target
using (
  select
    @id as id,
    @tag_name as tag_name,
    @tag_value as tag_value,
    @measured_at as measured_at
) as source
on target.id = source.id
when matched then
  update set
    tag_name = source.tag_name,
    tag_value = source.tag_value,
    measured_at = source.measured_at
when not matched then
  insert (id, tag_name, tag_value, measured_at)
  values (source.id, source.tag_name, source.tag_value, source.measured_at);
```

MySQL or MariaDB upsert:

```sql
insert into target_values(id, tag_name, tag_value, measured_at)
values (@id, @tag_name, @tag_value, @measured_at)
on duplicate key update
  tag_name = values(tag_name),
  tag_value = values(tag_value),
  measured_at = values(measured_at);
```

Firebird update or insert:

```sql
update or insert into target_values(id, tag_name, tag_value, measured_at)
values (@id, @tag_name, @tag_value, @measured_at)
matching (id);
```

Oracle users can still write `@name` in the UI. The driver converts the command text and parameters to Oracle-style `:name` before execution:

```sql
merge into target_values target
using (
  select
    @id as id,
    @tag_name as tag_name,
    @tag_value as tag_value,
    @measured_at as measured_at
  from dual
) source
on (target.id = source.id)
when matched then
  update set
    target.tag_name = source.tag_name,
    target.tag_value = source.tag_value,
    target.measured_at = source.measured_at
when not matched then
  insert (id, tag_name, tag_value, measured_at)
  values (source.id, source.tag_name, source.tag_value, source.measured_at);
```

### Transfer Settings and Keywords

| Name | Where used | Meaning |
|---|---|---|
| `Source database` | Connection settings | Database where `SelectQuery` is executed |
| `Target database` | Connection settings | Database where `InsertQuery` is executed |
| `SelectQuery` | Transfer command | SQL query that returns source rows and column aliases |
| `InsertQuery` | Transfer command | Target non-query command executed once per source row |
| `@column_name` | `InsertQuery` | Target parameter that receives the value of the `SELECT` column named `column_name` |
| `:column_name` | `InsertQuery` | Alternative parameter prefix; used by Oracle after normalization |
| `StopOnError` | Transfer command | If enabled, the first row error rolls back the target transaction |
| `BatchSize` | Transfer command | Reserved for future batch and bulk strategies; the base implementation writes row by row |
| `TAGNAME`, `TAGVALUE`, `TAGDATETIME` | Legacy tag import only | These names are not required for DB-to-DB transfer |
| `cmdVal` | Telecontrol command export only | This parameter is not used by DB-to-DB transfer |

Каждая команда переноса содержит:

- номер и код команды;
- отображаемое имя и описание;
- `SelectQuery`, выполняемый в исходной БД;
- `InsertQuery`, выполняемый в целевой БД;
- `StopOnError`, включен по умолчанию;
- необязательный `BatchSize`, зарезервированный для будущих batch/bulk-стратегий.

Целевая команда должна использовать параметры, имена которых совпадают с именами колонок `SELECT`. В интерфейсе используется стиль параметров `@name`. Драйвер нормализует префиксы параметров для провайдеров, которым нужен другой стиль, например Oracle.

Поток данных:

1. Драйвер открывает подключение к исходной БД.
2. Выполняет `SelectQuery` только в исходной БД.
3. Читает результат во внутренний `DataTable`.
4. Открывает подключение к целевой БД и начинает транзакцию на целевой БД.
5. Извлекает параметры из `InsertQuery`.
6. Для каждой строки источника ищет колонку с таким же именем, как у каждого параметра приемника.
7. Передает значения через `DbParameter` и выполняет `InsertQuery` один раз на каждую строку.
8. При успехе делает commit, а при ошибке и включенном `StopOnError` делает rollback.

Значения не склеиваются в SQL-текст. Драйвер передает `long`, `decimal`, `DateTime`, `Guid`, `bool`, `byte[]`, `DBNull` и другие CLR-типы через параметры провайдера.

### Контракт SELECT-запроса

`SelectQuery` - обычный запрос чтения в исходной БД. Колонки его результата являются контрактом данных для запроса записи в приемник.

Правила:

- Возвращайте одну строку результата на одну операцию записи в приемник.
- Задавайте явные алиасы для каждого значения, которое нужно записать в целевую БД.
- Алиасы должны быть удобны для параметров: латинские буквы, цифры и подчёркивание, первый символ - буква или подчёркивание.
- Если исходная колонка содержит пробелы, знаки пунктуации или требует provider-specific quoting, задайте ей простой алиас.
- Лишние колонки в `SELECT` допустимы, но они игнорируются, если в `InsertQuery` нет параметра с таким же именем.
- `SelectQuery` выполняется как текст. В DB-to-DB режиме драйвер не создаёт автоматические параметры для `SelectQuery`.

Хороший запрос источника:

```sql
select
  src_id as id,
  src_tag as tag_name,
  src_value as tag_value,
  src_time as measured_at
from source_values;
```

Нестабильные алиасы лучше не использовать:

```sql
-- Плохо для параметров переноса: в алиасах есть пробелы и знаки пунктуации.
select
  src_id as "Source ID",
  src_value as "Value, kg"
from source_values;
```

Используйте простые алиасы:

```sql
select
  src_id as source_id,
  src_value as value_kg
from source_values;
```

### Контракт INSERT, UPDATE и UPSERT-запроса

`InsertQuery` выполняется только в целевой БД. Несмотря на имя настройки, в этом поле можно писать `INSERT`, `UPDATE`, `MERGE`, PostgreSQL `INSERT ... ON CONFLICT`, MySQL/MariaDB `INSERT ... ON DUPLICATE KEY UPDATE`, Firebird `UPDATE OR INSERT` или другую non-query команду, которую поддерживает выбранный провайдер.

Драйвер понимает, что нужно отправлять, по параметрам в `InsertQuery`:

- Поддерживаются placeholder-формы `@name` и `:name`.
- Рекомендуемый стиль в интерфейсе - `@name`.
- Для Oracle команды автоматически преобразуются из `@name` в `:name`.
- Имена параметров сопоставляются с колонками `SELECT` без префикса `@` или `:`.
- Сопоставление выполняется без учёта регистра.
- Токены `@@name` игнорируются, поэтому системные переменные SQL Server, например `@@ROWCOUNT`, не считаются параметрами переноса.
- Имя параметра должно начинаться с латинской буквы или подчёркивания и может содержать латинские буквы, цифры и подчёркивание.

Для каждой строки источника драйвер фактически делает следующее:

```text
for each parameter in InsertQuery:
    columnName = parameter name without @ or :
    value = current SELECT row[columnName]
    command.Parameters[parameter] = value

execute InsertQuery
```

Если в `InsertQuery` есть `@tag_value`, то исходный `SELECT` должен вернуть колонку или алиас `tag_value`. Если такой колонки нет, команда завершится ошибкой вида `Parameter @tag_value does not match a SELECT column`.

Имена колонок целевой таблицы не обязаны совпадать с именами колонок источника. Совпадать должны только параметры целевого SQL и алиасы `SELECT`:

```sql
-- source SELECT
select
  sensor_id as id,
  sensor_value as value,
  event_time as measured_at
from sensor_history;

-- target INSERT
insert into archive_values(target_id, archived_value, archived_at)
values (@id, @value, @measured_at);
```

В этом примере:

- целевая колонка `target_id` получает исходную колонку `id`;
- целевая колонка `archived_value` получает исходную колонку `value`;
- целевая колонка `archived_at` получает исходную колонку `measured_at`.

Связь задается параметрами `values (@id, @value, @measured_at)`, а не одинаковыми именами таблиц или колонок.

### Частые примеры запросов приемника

Обычная вставка:

```sql
insert into target_values(id, tag_name, tag_value, measured_at)
values (@id, @tag_name, @tag_value, @measured_at);
```

Обновление существующих строк:

```sql
update target_values
set
  tag_name = @tag_name,
  tag_value = @tag_value,
  measured_at = @measured_at
where id = @id;
```

PostgreSQL upsert:

```sql
insert into target_values(id, tag_name, tag_value, measured_at)
values (@id, @tag_name, @tag_value, @measured_at)
on conflict (id) do update
set
  tag_name = excluded.tag_name,
  tag_value = excluded.tag_value,
  measured_at = excluded.measured_at;
```

SQL Server `MERGE`:

```sql
merge dbo.target_values as target
using (
  select
    @id as id,
    @tag_name as tag_name,
    @tag_value as tag_value,
    @measured_at as measured_at
) as source
on target.id = source.id
when matched then
  update set
    tag_name = source.tag_name,
    tag_value = source.tag_value,
    measured_at = source.measured_at
when not matched then
  insert (id, tag_name, tag_value, measured_at)
  values (source.id, source.tag_name, source.tag_value, source.measured_at);
```

MySQL или MariaDB upsert:

```sql
insert into target_values(id, tag_name, tag_value, measured_at)
values (@id, @tag_name, @tag_value, @measured_at)
on duplicate key update
  tag_name = values(tag_name),
  tag_value = values(tag_value),
  measured_at = values(measured_at);
```

Firebird update or insert:

```sql
update or insert into target_values(id, tag_name, tag_value, measured_at)
values (@id, @tag_name, @tag_value, @measured_at)
matching (id);
```

Для Oracle в интерфейсе можно писать `@name`. Драйвер перед выполнением преобразует текст команды и параметры в Oracle-стиль `:name`:

```sql
merge into target_values target
using (
  select
    @id as id,
    @tag_name as tag_name,
    @tag_value as tag_value,
    @measured_at as measured_at
  from dual
) source
on (target.id = source.id)
when matched then
  update set
    target.tag_name = source.tag_name,
    target.tag_value = source.tag_value,
    target.measured_at = source.measured_at
when not matched then
  insert (id, tag_name, tag_value, measured_at)
  values (source.id, source.tag_name, source.tag_value, source.measured_at);
```

### Настройки и ключевые слова переноса

| Имя | Где используется | Значение |
|---|---|---|
| `Source database` | Настройки подключения | БД, в которой выполняется `SelectQuery` |
| `Target database` | Настройки подключения | БД, в которой выполняется `InsertQuery` |
| `SelectQuery` | Команда переноса | SQL-запрос, который возвращает строки и алиасы колонок источника |
| `InsertQuery` | Команда переноса | Non-query команда приемника, выполняется один раз на каждую строку источника |
| `@column_name` | `InsertQuery` | Параметр приемника, который получает значение колонки `SELECT` с именем `column_name` |
| `:column_name` | `InsertQuery` | Альтернативный префикс параметра; используется Oracle после нормализации |
| `StopOnError` | Команда переноса | Если включено, первая ошибка строки откатывает транзакцию целевой БД |
| `BatchSize` | Команда переноса | Зарезервировано для будущих batch и bulk-стратегий; базовая реализация пишет построчно |
| `TAGNAME`, `TAGVALUE`, `TAGDATETIME` | Только legacy-импорт тегов | Эти имена не требуются для DB-to-DB переноса |
| `cmdVal` | Только экспорт команд телеуправления | Этот параметр не используется DB-to-DB переносом |

## Legacy Tag Import / Legacy-импорт тегов

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

1. Add `DrvDbDataTransfer` to a communication line in ScadaAdmin.
2. Open device properties for the required device.
3. Configure the source database connection and test it.
4. Configure the target database connection and test it.
5. Add one or more transfer commands.
6. Define `SELECT` for the source side and parameterized `INSERT`, `UPDATE` or `UPSERT` for the target side.
7. Test the command from the SQL editor.
8. Configure legacy driver tags only if the command is also used for Rapid SCADA tag import.
9. Upload the project and restart the communication line.

Русский:

1. Добавьте `DrvDbDataTransfer` в линию связи в ScadaAdmin.
2. Откройте свойства нужного КП.
3. Настройте подключение к исходной БД и проверьте его.
4. Настройте подключение к целевой БД и проверьте его.
5. Добавьте одну или несколько команд переноса.
6. Задайте `SELECT` для источника и параметризованный `INSERT`, `UPDATE` или `UPSERT` для приемника.
7. Проверьте команду в SQL-редакторе.
8. Настраивайте legacy-теги драйвера только если команда также используется для импорта тегов Rapid SCADA.
9. Создайте прототипы каналов для КП при использовании legacy-тегов.
10. Загрузите проект и перезапустите линию связи.

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
DrvDbDataTransfer_v6/
├── DrvDbDataTransfer.sln                  # Solution file
├── StartСompilingFull.bat               # Build and deployment helper
├── README.md                            # This file
│
├── DrvDbDataTransfer.Logic/               # Runtime driver for ScadaComm
│   ├── DrvDbDataTransfer.Logic.csproj
│   └── DevDbDataTransferLogic.cs          # Device runtime logic
│
├── DrvDbDataTransfer.View/                # ScadaAdmin driver UI
│   ├── DrvDbDataTransfer.View.csproj
│   ├── DevDbDataTransferView.cs           # Device view entry point
│   ├── Forms/                           # Project, import, export and tag forms
│   ├── FastColorTextBox/                # SQL editor control
│   └── Lang/                            # English and Russian language XML files
│
├── DrvDbDataTransfer.Shared/              # Shared logic used by Logic, View and WinForms
│   ├── Client/                          # Polling client
│   ├── Data/                            # Database and InfluxDB data sources
│   ├── Database/                        # Query execution helpers
│   ├── Settings/                        # XML configuration classes
│   └── Tags/                            # Tag and channel prototype generation
│
└── DrvDbDataTransfer.Winform/             # Standalone WinForms test/admin build
```

## Build / Сборка

### Prerequisites / Требования

- .NET 8.0 SDK
- Rapid SCADA 6.5 libraries in `Libraries`
- Windows for the ScadaAdmin view and standalone WinForms UI
- Database provider packages used by the selected data source

### Commands / Команды

```powershell
dotnet build .\DrvDbDataTransfer.sln -c Release
```

The `StartСompilingFull.bat` script publishes runtime packages for Windows x64, Windows x86 and Linux x64 logic, and can copy files to a local Rapid SCADA installation.

Скрипт `StartСompilingFull.bat` публикует пакеты для Windows x64, Windows x86 и runtime-логики Linux x64, а также может копировать файлы в локальную установку Rapid SCADA.

## Screenshots / Скриншоты

![DrvDbDataTransfer](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/Source/DrvDbDataTransfer_001.png)
![DrvDbDataTransfer](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/Source/DrvDbDataTransfer_002.png)
![DrvDbDataTransfer](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/Source/DrvDbDataTransfer_003.png)
![DrvDbDataTransfer](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/Source/DrvDbDataTransfer_004.png)
![DrvDbDataTransfer](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/Source/DrvDbDataTransfer_005.png)
![DrvDbDataTransfer](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/Source/DrvDbDataTransfer_006.png)
![DrvDbDataTransfer](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/Source/DrvDbDataTransfer_007.png)

## License / Лицензия

This project is part of the Rapid SCADA ecosystem.

Данный проект является частью экосистемы Rapid SCADA.

## SAST Tools

[PVS-Studio](https://pvs-studio.ru/ru/pvs-studio/?utm_source=website&utm_medium=github&utm_campaign=open_source) - static analyzer for C, C++, C#, and Java code.
