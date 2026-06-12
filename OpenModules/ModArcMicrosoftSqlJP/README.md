# ModArcMicrosoftSqlJP

![ModArcMicrosoftSqlJP](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v6/ModArcMicrosoftSqlJP_v6.2.2.0/total)

[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](LICENSE)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)

**Microsoft SQL Server Archive JP** — a Rapid SCADA server module that stores current data, historical data, and events in Microsoft SQL Server.  
**Архив Microsoft SQL Server JP** — серверный модуль Rapid SCADA для записи текущих данных, исторических данных и событий в Microsoft SQL Server.

## Overview / Обзор

ModArcMicrosoftSqlJP is based on the Rapid SCADA archive module architecture and provides archive storage in Microsoft SQL Server through `Microsoft.Data.SqlClient`.

Модуль ModArcMicrosoftSqlJP основан на архитектуре архивных модулей Rapid SCADA и обеспечивает хранение архивов в Microsoft SQL Server через `Microsoft.Data.SqlClient`.

The companion extension **ExtDepMicrosoftSqlJP** deploys the Rapid SCADA project configuration database to Microsoft SQL Server.

Сопутствующее расширение **ExtDepMicrosoftSqlJP** разворачивает базу конфигурации проекта Rapid SCADA в Microsoft SQL Server.

## Components / Компоненты

| Component | Description | Описание |
|---|---|---|
| **ModArcMicrosoftSqlJP.Logic** | Server module logic for writing and reading archives | Серверная логика записи и чтения архивов |
| **ModArcMicrosoftSqlJP.View** | Administrator UI for module and archive options | Интерфейс настройки модуля и архивов в Администраторе |
| **ModArcMicrosoftSqlJP.Shared** | Shared configuration classes and defaults | Общие классы конфигурации и настройки по умолчанию |
| **ExtDepMicrosoftSqlJP** | Admin extension for deploying project configuration to SQL Server | Расширение Администратора для развёртывания конфигурации проекта в SQL Server |
| **MicrosoftSqlStorage** | Active storage implementation for SQL Server configuration databases | Хранилище активной конфигурации в SQL Server |

## Features / Возможности

- **Archive kinds** — supports Current, Historical, and Events archives
- **Automatic table creation** — creates schema and archive tables on server startup
- **Batch writing** — writes data points and events in transactions with configurable batch size
- **Queue buffering** — uses write queues to reduce database load
- **Connection manager** — stores named SQL Server connections in `ModArcMicrosoftSqlJP.xml`
- **Admin deployment** — uploads and downloads project configuration through `ExtDepMicrosoftSqlJP`
- **Localization** — English and Russian language support
- **Runtime dependency folder** — deploys `Microsoft.Data.SqlClient` dependencies in module-specific subfolders

- **Типы архивов** — поддержка текущего, исторического архива и архива событий
- **Автоматическое создание таблиц** — создание схемы и таблиц при запуске сервера
- **Пакетная запись** — запись точек и событий транзакциями с настраиваемым размером пакета
- **Очереди записи** — буферизация данных для снижения нагрузки на базу
- **Менеджер соединений** — хранение именованных соединений SQL Server в `ModArcMicrosoftSqlJP.xml`
- **Развёртывание из Администратора** — загрузка и выгрузка базы конфигурации проекта через `ExtDepMicrosoftSqlJP`
- **Локализация** — поддержка английского и русского языков
- **Подпапка зависимостей** — развёртывание зависимостей `Microsoft.Data.SqlClient` в отдельных папках модулей

## Database Objects / Объекты БД

Runtime archive data is stored in the SQL Server schema:

Данные runtime-архивов записываются в схему SQL Server:

```sql
mod_arc_microsoft_sql
```

Table names are generated from the archive code:

Имена таблиц формируются из кода архива:

| Archive kind | Table suffix | Example |
|---|---|---|
| Current | `_current` | `[mod_arc_microsoft_sql].[curcopy_current]` |
| Historical | `_historical` | `[mod_arc_microsoft_sql].[mincopy_historical]` |
| Events | `_event` | `[mod_arc_microsoft_sql].[eventscopy_event]` |

The deployment extension writes project configuration tables to the `project` schema.

Расширение развёртывания записывает таблицы конфигурации проекта в схему `project`.

## Project Structure / Структура проекта

```text
ScadaServer/OpenModules/
├── OpenModules.sln
├── README.md
├── ModArcMicrosoftSqlJP.Logic/
│   ├── ModArcMicrosoftSqlJP.Logic.csproj
│   ├── ModArcMicrosoftSqlJPLogic.cs
│   ├── MicrosoftSqlCAL.cs
│   ├── MicrosoftSqlHAL.cs
│   ├── MicrosoftSqlEAL.cs
│   ├── QueryBuilder.cs
│   ├── PointQueue.cs
│   └── EventQueue.cs
├── ModArcMicrosoftSqlJP.Shared/
│   ├── ModArcMicrosoftSqlJP.Shared.projitems
│   ├── ModArcMicrosoftSqlJP.Shared.shproj
│   ├── ModuleUtils.cs
│   ├── ModulePhrases.cs
│   └── Config/
│       ├── ModArcMicrosoftSqlJP.xml
│       ├── ModuleConfig.cs
│       ├── MicrosoftSqlCAO.cs
│       ├── MicrosoftSqlHAO.cs
│       └── MicrosoftSqlEAO.cs
└── ModArcMicrosoftSqlJP.View/
    ├── ModArcMicrosoftSqlJP.View.csproj
    ├── ModArcMicrosoftSqlJPView.cs
    ├── MicrosoftSqlArchiveView.cs
    ├── Forms/
    ├── Controls/
    └── Lang/
        ├── ModArcMicrosoftSqlJP.en-GB.xml
        └── ModArcMicrosoftSqlJP.ru-RU.xml

ScadaAdmin/OpenExtensions/
├── OpenExtensions.sln
└── ExtDepMicrosoftSqlJP/
    ├── ExtDepMicrosoftSqlJP.csproj
    ├── ExtDepMicrosoftSqlJPLogic.cs
    ├── Downloader.cs
    ├── Uploader.cs
    ├── Config/
    │   └── ExtDepMicrosoftSqlJP.xml
    └── Lang/
        ├── ExtDepMicrosoftSqlJP.en-GB.xml
        └── ExtDepMicrosoftSqlJP.ru-RU.xml
```

## Build and Deploy / Сборка и развёртывание

### Prerequisites / Требования

- .NET 8.0 SDK
- Rapid SCADA 6 installed to `C:\Program Files\SCADA`
- Microsoft SQL Server
- Administrator rights for deployment

- .NET 8.0 SDK
- Rapid SCADA 6, установленная в `C:\Program Files\SCADA`
- Microsoft SQL Server
- Права администратора для развёртывания

### Manual Build / Ручная сборка

```bash
dotnet build ScadaServer/OpenModules/ModArcMicrosoftSqlJP.Logic/ModArcMicrosoftSqlJP.Logic.csproj -c Release
dotnet build ScadaServer/OpenModules/ModArcMicrosoftSqlJP.View/ModArcMicrosoftSqlJP.View.csproj -c Release
dotnet build ScadaAdmin/OpenExtensions/ExtDepMicrosoftSqlJP/ExtDepMicrosoftSqlJP.csproj -c Release
```

Or build the solution files:

Или соберите solution-файлы:

```bash
dotnet build ScadaServer/OpenModules/OpenModules.sln -c Release
dotnet build ScadaAdmin/OpenExtensions/OpenExtensions.sln -c Release
```

### Server Deployment / Развёртывание серверного модуля

Copy the server module files to Rapid SCADA:

Скопируйте файлы серверного модуля в Rapid SCADA:

```text
ModArcMicrosoftSqlJP.Logic/bin/Release/net8.0/ModArcMicrosoftSqlJP.Logic.dll
  -> C:\Program Files\SCADA\ScadaServer\Mod\ModArcMicrosoftSqlJP.Logic.dll

ModArcMicrosoftSqlJP.Logic/bin/Release/net8.0/ModArcMicrosoftSqlJP.Logic/
  -> C:\Program Files\SCADA\ScadaServer\Mod\ModArcMicrosoftSqlJP.Logic\

ModArcMicrosoftSqlJP.Shared/Config/ModArcMicrosoftSqlJP.xml
  -> C:\Program Files\SCADA\ScadaServer\Config\ModArcMicrosoftSqlJP.xml
```

Add the module to `ScadaServerConfig.xml`:

Добавьте модуль в `ScadaServerConfig.xml`:

```xml
<Module code="ModArcMicrosoftSqlJP" />
```

Configure archives to use the module:

Настройте архивы на использование модуля:

```xml
<Archive active="true" code="CurCopy" name="Current data copy" kind="Current" module="ModArcMicrosoftSqlJP">
  <Option name="UseDefaultConn" value="false" />
  <Option name="Connection" value="MicrosoftSqlConn" />
  <Option name="ReadOnly" value="false" />
  <Option name="MaxQueueSize" value="1000" />
  <Option name="BatchSize" value="1000" />
</Archive>
```

When `UseDefaultConn` is `false`, the module uses a named connection from `ModArcMicrosoftSqlJP.xml`. When it is `true`, the module uses the default connection from `ScadaInstanceConfig.xml`.

Если `UseDefaultConn` равен `false`, модуль использует именованное соединение из `ModArcMicrosoftSqlJP.xml`. Если значение `true`, модуль использует соединение по умолчанию из `ScadaInstanceConfig.xml`.

### Administrator Deployment / Развёртывание в Администраторе

Copy the view module and extension files:

Скопируйте файлы View-модуля и расширения:

```text
ModArcMicrosoftSqlJP.View/bin/Release/net8.0-windows/ModArcMicrosoftSqlJP.View.dll
  -> C:\Program Files\SCADA\ScadaAdmin\Lib\ModArcMicrosoftSqlJP.View.dll

ModArcMicrosoftSqlJP.View/bin/Release/net8.0-windows/ModArcMicrosoftSqlJP.View/
  -> C:\Program Files\SCADA\ScadaAdmin\Lib\ModArcMicrosoftSqlJP.View\

ModArcMicrosoftSqlJP.View/Lang/ModArcMicrosoftSqlJP.*.xml
  -> C:\Program Files\SCADA\ScadaAdmin\Lang\

ExtDepMicrosoftSqlJP/bin/Release/net8.0-windows/ExtDepMicrosoftSqlJP.dll
  -> C:\Program Files\SCADA\ScadaAdmin\Lib\ExtDepMicrosoftSqlJP.dll

ExtDepMicrosoftSqlJP/bin/Release/net8.0-windows/ExtDepMicrosoftSqlJP/
  -> C:\Program Files\SCADA\ScadaAdmin\Lib\ExtDepMicrosoftSqlJP\

ExtDepMicrosoftSqlJP/Config/ExtDepMicrosoftSqlJP.xml
  -> C:\Program Files\SCADA\ScadaAdmin\Config\ExtDepMicrosoftSqlJP.xml

ExtDepMicrosoftSqlJP/Lang/ExtDepMicrosoftSqlJP.*.xml
  -> C:\Program Files\SCADA\ScadaAdmin\Lang\
```

The extension code must be registered in the Admin configuration:

Код расширения должен быть зарегистрирован в конфигурации Администратора:

```xml
<Extension code="ExtDepMicrosoftSqlJP" />
```

## Connection Options / Параметры подключения

The default module configuration contains one connection:

Конфигурация модуля по умолчанию содержит одно соединение:

```xml
<ModArcMicrosoftSqlJP>
  <Connections>
    <Connection>
      <Name>MicrosoftSqlConn</Name>
      <DBMS>MSSQL</DBMS>
      <Server>localhost</Server>
      <Database>rapid_scada</Database>
      <Username>sa</Username>
      <Password />
      <ConnectionString />
    </Connection>
  </Connections>
</ModArcMicrosoftSqlJP>
```

If a custom connection string is needed, set `ConnectionString`. For example:

Если нужна пользовательская строка подключения, заполните `ConnectionString`. Например:

```text
Server=192.168.150.150;Database=RapidScada;User ID=sa;Password=***;Encrypt=False;Persist Security Info=True;Trust Server Certificate=True
```

## Archive Options / Параметры архивов

| Option | Description | Описание |
|---|---|---|
| `UseDefaultConn` | Use default instance connection from `ScadaInstanceConfig.xml` | Использовать соединение экземпляра по умолчанию из `ScadaInstanceConfig.xml` |
| `Connection` | Named connection from `ModArcMicrosoftSqlJP.xml` | Именованное соединение из `ModArcMicrosoftSqlJP.xml` |
| `ReadOnly` | Disable writes to the database | Отключить запись в базу данных |
| `MaxQueueSize` | Maximum number of queued items | Максимальное количество элементов в очереди |
| `BatchSize` | Number of items written in one transaction | Количество элементов, записываемых одной транзакцией |
| `PartitionSize` | Logical partition period option for historical and event archives | Период логической секции для исторического архива и событий |
| `UseMemoryCache` | Cache historical values in memory when reading | Кэшировать исторические значения в памяти при чтении |
| `CacheSizeRatio` | Cache size ratio to the number of archive channels | Коэффициент размера кэша относительно количества каналов архива |

## Verification / Проверка

After starting ScadaServer, the log should contain:

После запуска ScadaServer в журнале должны появиться строки:

```text
Модуль ModArcMicrosoftSqlJP ... загружен
Архив CurCopy инициализирован успешно
Архив MinCopy инициализирован успешно
```

Check the database:

Проверьте базу данных:

```sql
SELECT TABLE_SCHEMA, TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_SCHEMA IN ('mod_arc_microsoft_sql', 'project')
ORDER BY TABLE_SCHEMA, TABLE_NAME;

SELECT COUNT(*) FROM [mod_arc_microsoft_sql].[curcopy_current];
SELECT COUNT(*) FROM [mod_arc_microsoft_sql].[mincopy_historical];
SELECT COUNT(*) FROM [mod_arc_microsoft_sql].[eventscopy_event];
```

## Notes / Примечания

- The archive schema name is intentionally kept as `mod_arc_microsoft_sql` for compatibility with existing tables and data.
- `Microsoft.Data.SqlClient.dll` in the dependency subfolder must be the Windows runtime assembly. The build target copies the correct runtime file and `Microsoft.Data.SqlClient.SNI.dll`.
- `ExtDepMicrosoftSqlJP` creates and updates `project.*` tables. `ModArcMicrosoftSqlJP` writes runtime archive data to `mod_arc_microsoft_sql.*` tables.
- If no tables are created, check `UseDefaultConn`. With `UseDefaultConn=true`, the module does not use `ModArcMicrosoftSqlJP.xml`; it uses the default instance connection.

- Имя схемы архива намеренно оставлено `mod_arc_microsoft_sql` для совместимости с уже созданными таблицами и данными.
- В подпапке зависимостей должна лежать Windows runtime-сборка `Microsoft.Data.SqlClient.dll`. Цель сборки копирует правильный runtime-файл и `Microsoft.Data.SqlClient.SNI.dll`.
- `ExtDepMicrosoftSqlJP` создаёт и обновляет таблицы `project.*`. `ModArcMicrosoftSqlJP` пишет runtime-архивы в таблицы `mod_arc_microsoft_sql.*`.
- Если таблицы не создаются, проверьте `UseDefaultConn`. При `UseDefaultConn=true` модуль использует не `ModArcMicrosoftSqlJP.xml`, а default connection экземпляра.

## License / Лицензия

This project is part of the Rapid SCADA ecosystem.  
Данный проект является частью экосистемы Rapid SCADA.

## SAST Tools

[PVS-Studio](https://pvs-studio.ru/ru/pvs-studio/?utm_source=website&utm_medium=github&utm_campaign=open_source) - static analyzer for C, C++, C#, and Java code.
