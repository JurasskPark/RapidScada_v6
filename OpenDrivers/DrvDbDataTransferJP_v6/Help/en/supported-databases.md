# DrvDbDataTransferJP — Supported Databases

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/supported-databases.md)

| Data source | Provider | Notes |
| --- | --- | --- |
| `MSSQL` | `Microsoft.Data.SqlClient` | Use a platform-specific package, especially on Windows x64. |
| `Oracle` | `Oracle.ManagedDataAccess.Core` | Oracle parameters are normalized to `:name`. |
| `PostgreSQL` | `Npgsql` | Supports quoted table names such as `public."20260708.Data"`. |
| `MySQL` | `MySql.Data` | SSL mode can be set through connection options. |
| `Firebird` | `FirebirdSql.Data.FirebirdClient` | Supports Firebird SQL commands supported by the provider. |
| `InfluxDBv2` | HTTP API | Password field is used as token, database field as bucket. |
| `InfluxDBv3` | HTTP API | Password field is used as token, database field as database. |
