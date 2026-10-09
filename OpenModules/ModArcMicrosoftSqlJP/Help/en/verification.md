# ModArcMicrosoftSqlJP — Verification

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/verification.md)

After starting ScadaServer, the log should contain:

```text
Модуль ModArcMicrosoftSqlJP ... загружен
Архив CurCopy инициализирован успешно
Архив MinCopy инициализирован успешно
```

Check the database:

```sql
SELECT TABLE_SCHEMA, TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_SCHEMA IN ('mod_arc_microsoft_sql', 'project')
ORDER BY TABLE_SCHEMA, TABLE_NAME;

SELECT COUNT(*) FROM [mod_arc_microsoft_sql].[curcopy_current];
SELECT COUNT(*) FROM [mod_arc_microsoft_sql].[mincopy_historical];
SELECT COUNT(*) FROM [mod_arc_microsoft_sql].[eventscopy_event];
```
