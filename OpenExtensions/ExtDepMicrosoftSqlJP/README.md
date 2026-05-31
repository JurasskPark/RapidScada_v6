# ExtDepMicrosoftSqlJP

[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](../../../License.txt)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)

**Microsoft SQL Server Deployment JP** — a Rapid SCADA Administrator extension for deploying project configuration to Microsoft SQL Server.  
**Развёртывание в Microsoft SQL Server JP** — расширение Администратора Rapid SCADA для развёртывания конфигурации проекта в Microsoft SQL Server.

## Overview / Обзор

ExtDepMicrosoftSqlJP uploads and downloads a Rapid SCADA project configuration database using Microsoft SQL Server as the target DBMS.

ExtDepMicrosoftSqlJP загружает и выгружает базу конфигурации проекта Rapid SCADA, используя Microsoft SQL Server в качестве целевой СУБД.

The extension is intended to work together with `MicrosoftSqlStorage` and can be used alongside the `ModArcMicrosoftSqlJP` server module. The extension stores project configuration in the `project` schema, while the archive module writes runtime archive data to the `mod_arc_microsoft_sql` schema.

Расширение предназначено для совместной работы с `MicrosoftSqlStorage` и может использоваться вместе с серверным модулем `ModArcMicrosoftSqlJP`. Расширение хранит конфигурацию проекта в схеме `project`, а архивный модуль записывает runtime-архивы в схему `mod_arc_microsoft_sql`.

## Features / Возможности

- **Project upload** — deploys the current Rapid SCADA project to Microsoft SQL Server
- **Project download** — reads project configuration from Microsoft SQL Server back to Administrator
- **Database preparation** — creates the `project` schema, tables, views, foreign keys, and application configuration records
- **Configurable cleanup** — supports dropping tables or truncating existing tables before deployment
- **Connection test** — validates Microsoft SQL Server connections from Administrator
- **Agent integration** — restarts services through Agent when it is available
- **Localization** — English and Russian language support
- **Runtime dependency folder** — deploys `Microsoft.Data.SqlClient` dependencies in the extension-specific subfolder

- **Загрузка проекта** — развёртывание текущего проекта Rapid SCADA в Microsoft SQL Server
- **Выгрузка проекта** — чтение конфигурации проекта из Microsoft SQL Server обратно в Администратор
- **Подготовка базы данных** — создание схемы `project`, таблиц, представлений, внешних ключей и записей конфигурации приложений
- **Настраиваемая очистка** — удаление таблиц или очистка существующих таблиц перед развёртыванием
- **Проверка соединения** — проверка подключений Microsoft SQL Server из Администратора
- **Интеграция с Агентом** — перезапуск служб через Агент, если он доступен
- **Локализация** — поддержка английского и русского языков
- **Подпапка зависимостей** — развёртывание зависимостей `Microsoft.Data.SqlClient` в отдельной папке расширения

## Configuration / Конфигурация

The extension configuration file is:

Файл конфигурации расширения:

```text
ExtDepMicrosoftSqlJP.xml
```

Default configuration:

Конфигурация по умолчанию:

```xml
<?xml version="1.0" encoding="utf-8"?>
<ExtDepMicrosoftSqlJP>
  <ClearBaseMethod>DropTables</ClearBaseMethod>
</ExtDepMicrosoftSqlJP>
```

Available cleanup methods:

Доступные методы очистки:

| Value | Description | Описание |
|---|---|---|
| `DropTables` | Drops and recreates project configuration tables | Удаляет и создаёт заново таблицы конфигурации проекта |
| `TruncateTables` | Clears existing project configuration tables | Очищает существующие таблицы конфигурации проекта |

Use `DropTables` for the first deployment or a full rebuild of the configuration database. Use `TruncateTables` when the schema already exists and only table data must be refreshed.

Используйте `DropTables` для первого развёртывания или полной пересборки базы конфигурации. Используйте `TruncateTables`, когда схема уже существует и нужно обновить только данные таблиц.

## Database Objects / Объекты БД

ExtDepMicrosoftSqlJP creates and fills project configuration objects in the SQL Server schema:

ExtDepMicrosoftSqlJP создаёт и заполняет объекты конфигурации проекта в схеме SQL Server:

```sql
project
```

Typical objects include configuration tables, views, foreign keys, and application configuration records. Examples of tables:

Типовые объекты включают таблицы конфигурации, представления, внешние ключи и записи конфигурации приложений. Примеры таблиц:

```text
project.app
project.archive
project.cnl
project.comm_line
project.device
project.format
project.obj
project.quantity
project.role
project.unit
```

Runtime archives are not written by this extension. Runtime archive data is written by `ModArcMicrosoftSqlJP`.

Это расширение не записывает runtime-архивы. Runtime-данные архивов записывает `ModArcMicrosoftSqlJP`.

## Project Structure / Структура проекта

```text
ExtDepMicrosoftSqlJP/
├── ExtDepMicrosoftSqlJP.csproj
├── ExtDepMicrosoftSqlJPLogic.cs
├── Downloader.cs
├── Uploader.cs
├── ExtensionPhrases.cs
├── Config/
│   ├── ExtDepMicrosoftSqlJP.xml
│   ├── ExtensionConfig.cs
│   └── ClearBaseMethod.cs
└── Lang/
    ├── ExtDepMicrosoftSqlJP.en-GB.xml
    └── ExtDepMicrosoftSqlJP.ru-RU.xml
```

## Build / Сборка

Build the extension project:

Сборка проекта расширения:

```powershell
dotnet build C:\Projects\scada-v6-develop\scada-v6-develop\ScadaAdmin\OpenExtensions\ExtDepMicrosoftSqlJP\ExtDepMicrosoftSqlJP.csproj -c Release
```

Or build all open Administrator extensions:

Или сборка всех открытых расширений Администратора:

```powershell
dotnet build C:\Projects\scada-v6-develop\scada-v6-develop\ScadaAdmin\OpenExtensions\OpenExtensions.sln -c Release
```

## Deployment / Развёртывание

Copy the extension files to the Administrator application directory:

Скопируйте файлы расширения в каталог приложения Администратора:

| Source | Target |
|---|---|
| `bin\Release\net8.0-windows\ExtDepMicrosoftSqlJP.dll` | `C:\Program Files\SCADA\ScadaAdmin\Lib\ExtDepMicrosoftSqlJP.dll` |
| `bin\Release\net8.0-windows\ExtDepMicrosoftSqlJP\*` | `C:\Program Files\SCADA\ScadaAdmin\Lib\ExtDepMicrosoftSqlJP\` |
| `Config\ExtDepMicrosoftSqlJP.xml` | `C:\Program Files\SCADA\ScadaAdmin\Config\ExtDepMicrosoftSqlJP.xml` |
| `Lang\ExtDepMicrosoftSqlJP.en-GB.xml` | `C:\Program Files\SCADA\ScadaAdmin\Lang\ExtDepMicrosoftSqlJP.en-GB.xml` |
| `Lang\ExtDepMicrosoftSqlJP.ru-RU.xml` | `C:\Program Files\SCADA\ScadaAdmin\Lang\ExtDepMicrosoftSqlJP.ru-RU.xml` |

Register the extension in the Administrator configuration:

Зарегистрируйте расширение в конфигурации Администратора:

```xml
<Extension code="ExtDepMicrosoftSqlJP" />
```

## Requirements / Требования

- Rapid SCADA 6 Administrator
- .NET 8
- Microsoft SQL Server
- SQL Server user permissions to create schemas, tables, views, foreign keys, and write project data
- `Microsoft.Data.SqlClient` runtime files copied to the `ExtDepMicrosoftSqlJP` dependency subfolder

- Администратор Rapid SCADA 6
- .NET 8
- Microsoft SQL Server
- Права пользователя SQL Server на создание схем, таблиц, представлений, внешних ключей и запись данных проекта
- Runtime-файлы `Microsoft.Data.SqlClient`, скопированные в подпапку зависимостей `ExtDepMicrosoftSqlJP`

## Troubleshooting / Диагностика

- If the extension is not shown in Administrator, check `ScadaAdminConfig.xml`, `ExtDepMicrosoftSqlJP.dll`, and language files.
- If the connection test fails, check that the selected DBMS is Microsoft SQL Server and verify the server, database, user, password, and connection string options.
- If SQL Server uses encrypted connections with a self-signed certificate, add `Trust Server Certificate=True` to the connection string options.
- If deployment fails while clearing the database, use `DropTables` for the first deployment.
- If `Microsoft.Data.SqlClient` reports a platform error, verify that the Windows `Microsoft.Data.SqlClient.dll` and `Microsoft.Data.SqlClient.SNI.dll` files are present in the `ExtDepMicrosoftSqlJP` subfolder.

- Если расширение не отображается в Администраторе, проверьте `ScadaAdminConfig.xml`, `ExtDepMicrosoftSqlJP.dll` и языковые файлы.
- Если проверка подключения не проходит, убедитесь, что выбрана СУБД Microsoft SQL Server, и проверьте сервер, базу данных, пользователя, пароль и параметры строки подключения.
- Если SQL Server использует шифрованное подключение с самоподписанным сертификатом, добавьте `Trust Server Certificate=True` в параметры строки подключения.
- Если развёртывание падает при очистке базы, используйте `DropTables` для первого развёртывания.
- Если `Microsoft.Data.SqlClient` сообщает об ошибке платформы, проверьте наличие Windows-файлов `Microsoft.Data.SqlClient.dll` и `Microsoft.Data.SqlClient.SNI.dll` в подпапке `ExtDepMicrosoftSqlJP`.

## License / Лицензия

Licensed under the Apache License, Version 2.0.

Лицензируется на условиях Apache License, Version 2.0.
