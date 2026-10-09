# DrvDbDataTransferJP — Режим переноса

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/transfer-mode.md)

Команда работает в режиме переноса, если заполнены оба поля:

- `SelectQuery`;
- `InsertQuery`.

## Порядок выполнения

1. Драйвер подставляет шаблоны даты и времени в `SelectQuery`.
2. Выполняет `SelectQuery` в `SourceDbConnSettings`.
3. Читает результат в `DataTable`.
4. При пустом результате пропускает запись в приёмник.
5. Выполняет `InsertQuery` в `TargetDbConnSettings`.
6. Заполняет SQL-параметры приёмника из колонок `DataTable` по имени.
7. Если настроены теги и прочитаны строки, преобразует ту же `DataTable` в значения тегов Rapid SCADA.
8. Записывает результат переноса в журнал только при наличии строк или ошибки.

## Контракт SELECT

`SelectQuery` определяет контракт данных. Для каждого параметра целевого `InsertQuery` в результате `SELECT` должна быть колонка или псевдоним с таким же именем.

```sql
SELECT
    occur_time AS AU_DateReport,
    description AS AU_Message,
    20::bigint AS AU_ALID
FROM public.event_data
WHERE level = 'Alarm';
```

Если в имени исходной колонки есть пробелы или знаки пунктуации, задайте простой псевдоним:

```sql
SELECT
    "Parametr" AS Parametr
FROM public."{YYYY}{MM}{DD}.Data";
```

Результат может содержать дополнительные колонки. Они не записываются в целевую БД без соответствующих параметров `InsertQuery`, но могут использоваться для тегов Rapid SCADA.

## Контракт INSERT / UPDATE / UPSERT

`InsertQuery` — команда записи без результирующей выборки. Название параметра историческое: допустимы `INSERT`, `UPDATE`, `MERGE`, PostgreSQL `INSERT ... ON CONFLICT`, MySQL `INSERT ... ON DUPLICATE KEY UPDATE`, Firebird `UPDATE OR INSERT` и другие поддерживаемые провайдером команды.

Поддерживаются параметры `@name` и `:name`. В интерфейсе рекомендуется `@name`; для Oracle они автоматически приводятся к `:name`.

```sql
INSERT INTO dbo.Report_Data_JSON
    (AU_Id, AU_Date, AU_DateReport, AU_ALID, AU_Message)
VALUES
    (NEWID(), GETDATE(), @AU_DateReport, @AU_ALID, @AU_Message);
```

Для этой команды исходный запрос должен вернуть колонки `AU_DateReport`, `AU_ALID` и `AU_Message`.
