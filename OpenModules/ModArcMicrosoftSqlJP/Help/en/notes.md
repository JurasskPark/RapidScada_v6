# ModArcMicrosoftSqlJP — Notes

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/notes.md)

- The archive schema name is intentionally kept as `mod_arc_microsoft_sql` for compatibility with existing tables and data.
- `Microsoft.Data.SqlClient.dll` in the dependency subfolder must be the Windows runtime assembly. The build target copies the correct runtime file and `Microsoft.Data.SqlClient.SNI.dll`.
- `ExtDepMicrosoftSqlJP` creates and updates `project.*` tables. `ModArcMicrosoftSqlJP` writes runtime archive data to `mod_arc_microsoft_sql.*` tables.
- If no tables are created, check `UseDefaultConn`. With `UseDefaultConn=true`, the module does not use `ModArcMicrosoftSqlJP.xml`; it uses the default instance connection.
