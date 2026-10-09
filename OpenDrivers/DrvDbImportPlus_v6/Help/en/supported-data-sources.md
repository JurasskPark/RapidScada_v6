# DrvDbImportPlus — Supported Data Sources

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/supported-data-sources.md)

| Data source | Notes |
| --- | --- |
| `MSSQL` | Uses `Microsoft.Data.SqlClient` |
| `Oracle` | Uses `Oracle.ManagedDataAccess.Core` |
| `PostgreSQL` | Uses `Npgsql` |
| `MySQL` | Uses `MySql.Data` |
| `Firebird` | Uses `FirebirdSql.Data.FirebirdClient` |
| `InfluxDBv2` | HTTP API, password field is used as token, database field is used as bucket |
| `InfluxDBv3` | HTTP API, password field is used as token, database field is used as database |

ODBC and OLE DB data sources are present only as legacy code comments and are not enabled in the current driver build.
