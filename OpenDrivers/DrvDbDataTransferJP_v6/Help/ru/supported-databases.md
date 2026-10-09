# DrvDbDataTransferJP — Поддерживаемые БД

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/supported-databases.md)

| Источник данных | Провайдер | Примечания |
| --- | --- | --- |
| `MSSQL` | `Microsoft.Data.SqlClient` | Используйте пакет для конкретной платформы, особенно в Windows x64. |
| `Oracle` | `Oracle.ManagedDataAccess.Core` | Параметры Oracle приводятся к виду `:name`. |
| `PostgreSQL` | `Npgsql` | Поддерживаются имена таблиц в кавычках, например `public."20260708.Data"`. |
| `MySQL` | `MySql.Data` | Режим SSL задаётся в параметрах подключения. |
| `Firebird` | `FirebirdSql.Data.FirebirdClient` | Поддерживаются SQL-команды Firebird, доступные через провайдер. |
| `InfluxDBv2` | HTTP API | Поле пароля используется как токен, поле базы данных — как корзина. |
| `InfluxDBv3` | HTTP API | Поле пароля используется как токен, поле базы данных — как имя базы. |
