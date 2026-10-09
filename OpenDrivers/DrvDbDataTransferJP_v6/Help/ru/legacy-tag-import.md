# DrvDbDataTransferJP — Старый режим импорта тегов

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/legacy-tag-import.md)

Если `InsertQuery` пустой, команда не выполняет перенос в базу-приемник. Она работает как команда импорта тегов.

## Режим по колонкам

```sql
SELECT
    temperature AS BoilerTemp,
    pressure AS BoilerPressure
FROM process_values
ORDER BY measured_at DESC
LIMIT 1;
```

## Режим по строкам

- `TAGNAME`
- `TAGVALUE`

```sql
SELECT
    tag_name AS TAGNAME,
    tag_value AS TAGVALUE,
    tag_time AS TAGDATETIME
FROM current_tag_values;
```

`TAGTIME` и `TAGDATETIME` разбираются как метки времени. Теги с метками времени группируются и передаются runtime-логике драйвера как исторические срезы.

В режиме по колонкам значения тегов находятся в первой строке результата. Псевдонимы колонок сопоставляются с настроенными именами тегов.

В режиме по строкам каждая строка содержит одно значение. Нужны колонки `TAGNAME`, `TAGVALUE` и, при необходимости, `TAGTIME` или `TAGDATETIME`.
