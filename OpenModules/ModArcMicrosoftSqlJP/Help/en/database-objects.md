# ModArcMicrosoftSqlJP — Database Objects

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/database-objects.md)

Runtime archive data is stored in the SQL Server schema:

```sql
mod_arc_microsoft_sql
```

Table names are generated from the archive code:

| Archive kind | Table suffix | Example |
| --- | --- | --- |
| Current | `_current` | `[mod_arc_microsoft_sql].[curcopy_current]` |
| Historical | `_historical` | `[mod_arc_microsoft_sql].[mincopy_historical]` |
| Events | `_event` | `[mod_arc_microsoft_sql].[eventscopy_event]` |

The deployment extension writes project configuration tables to the `project` schema.
