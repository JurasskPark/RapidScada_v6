# ExtDepMicrosoftSqlJP — Features

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/features.md)

- **Project upload** — deploys the current Rapid SCADA project to Microsoft SQL Server
- **Project download** — reads project configuration from Microsoft SQL Server back to Administrator
- **Database preparation** — creates the `project` schema, tables, views, foreign keys, and application configuration records
- **Configurable cleanup** — supports dropping tables or truncating existing tables before deployment
- **Connection test** — validates Microsoft SQL Server connections from Administrator
- **Agent integration** — restarts services through Agent when it is available
- **Localization** — English and Russian language support
- **Runtime dependency folder** — deploys `Microsoft.Data.SqlClient` dependencies in the extension-specific subfolder
