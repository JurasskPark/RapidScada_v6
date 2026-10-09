# DrvDbDataTransferJP — Features

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/features.md)

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
