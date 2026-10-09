# ModArcMicrosoftSqlJP — Features

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/features.md)

- **Archive kinds** — supports Current, Historical, and Events archives
- **Automatic table creation** — creates schema and archive tables on server startup
- **Batch writing** — writes data points and events in transactions with configurable batch size
- **Queue buffering** — uses write queues to reduce database load
- **Connection manager** — stores named SQL Server connections in `ModArcMicrosoftSqlJP.xml`
- **Admin deployment** — uploads and downloads project configuration through `ExtDepMicrosoftSqlJP`
- **Localization** — English and Russian language support
- **Runtime dependency folder** — deploys `Microsoft.Data.SqlClient` dependencies in module-specific subfolders
