# DrvDbImportPlus — Features

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/features.md)

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
