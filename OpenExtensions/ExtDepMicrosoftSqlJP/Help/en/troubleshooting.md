# ExtDepMicrosoftSqlJP — Troubleshooting

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/troubleshooting.md)

- If the extension is not shown in Administrator, check `ScadaAdminConfig.xml`, `ExtDepMicrosoftSqlJP.dll`, and language files.
- If the connection test fails, check that the selected DBMS is Microsoft SQL Server and verify the server, database, user, password, and connection string options.
- If SQL Server uses encrypted connections with a self-signed certificate, add `Trust Server Certificate=True` to the connection string options.
- If deployment fails while clearing the database, use `DropTables` for the first deployment.
- If `Microsoft.Data.SqlClient` reports a platform error, verify that the Windows `Microsoft.Data.SqlClient.dll` and `Microsoft.Data.SqlClient.SNI.dll` files are present in the `ExtDepMicrosoftSqlJP` subfolder.
