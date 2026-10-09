# ModArcMicrosoftSqlJP — Проверка

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/verification.md)

После запуска ScadaServer в журнале должны появиться строки:

```text
Модуль ModArcMicrosoftSqlJP ... загружен
Архив CurCopy инициализирован успешно
Архив MinCopy инициализирован успешно
```

Проверьте базу данных:

```sql
SELECT TABLE_SCHEMA, TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_SCHEMA IN ('mod_arc_microsoft_sql', 'project')
ORDER BY TABLE_SCHEMA, TABLE_NAME;

SELECT COUNT(*) FROM [mod_arc_microsoft_sql].[curcopy_current];
SELECT COUNT(*) FROM [mod_arc_microsoft_sql].[mincopy_historical];
SELECT COUNT(*) FROM [mod_arc_microsoft_sql].[eventscopy_event];
```
