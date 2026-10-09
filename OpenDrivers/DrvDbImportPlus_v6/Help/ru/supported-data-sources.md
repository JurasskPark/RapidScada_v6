# DrvDbImportPlus — Поддерживаемые источники данных

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/supported-data-sources.md)

| Источник данных | Примечание |
| --- | --- |
| `MSSQL` | Microsoft SQL Server через `Microsoft.Data.SqlClient` |
| `Oracle` | Oracle через `Oracle.ManagedDataAccess.Core` |
| `PostgreSQL` | PostgreSQL через `Npgsql` |
| `MySQL` | MySQL через `MySql.Data` |
| `Firebird` | Firebird через `FirebirdSql.Data.FirebirdClient` |
| `InfluxDBv2` | HTTP API, поле password используется как token, поле database используется как bucket |
| `InfluxDBv3` | HTTP API, поле password используется как token, поле database используется как database |

Источники ODBC и OLE DB оставлены только как legacy-комментарии в коде и в текущей сборке драйвера не включены.
