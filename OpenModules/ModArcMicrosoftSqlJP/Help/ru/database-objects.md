# ModArcMicrosoftSqlJP — Объекты БД

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/database-objects.md)

Данные runtime-архивов записываются в схему SQL Server:

```sql
mod_arc_microsoft_sql
```

Имена таблиц формируются из кода архива:

| Тип архива | Суффикс таблицы | Пример |
| --- | --- | --- |
| Текущий | `_current` | `[mod_arc_microsoft_sql].[curcopy_current]` |
| Исторический | `_historical` | `[mod_arc_microsoft_sql].[mincopy_historical]` |
| События | `_event` | `[mod_arc_microsoft_sql].[eventscopy_event]` |

Расширение развёртывания записывает таблицы конфигурации проекта в схему `project`.
