# DrvDbDataTransferJP — Обновление тегов Rapid SCADA

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/updating-rapid-scada-tags.md)

В режиме переноса можно дополнительно обновлять теги КП.

1. Добавьте теги в команду.
2. Задайте **Name** каждого тега равным имени колонки `SELECT`.
3. Задайте **Code** равным коду канала Rapid SCADA.
4. Выберите формат `Float`, `Integer`, `DateTime`, `String` или `Boolean`.

## Пример и соответствие тегов

```sql
SELECT
    20::bigint AS AU_ALID,
    1 AS AU_Process,
    to_timestamp(("time" - 621355968000000000)::double precision / 10000000) AS AU_DateReport,
    (jsonb_build_object('time', "time"))::text AS AU_Message
FROM public."{YYYY}{MM}{DD}.Data"
ORDER BY "time"
LIMIT 1;
```

| Имя тега | Код тега | Формат |
| --- | --- | --- |
| `AU_ALID` | `AU_ALID_Code` | `Integer` |
| `AU_Process` | `AU_Process_Code` | `Boolean` или числовой |
| `AU_DateReport` | `AU_DateReport_Code` | `DateTime` |
| `AU_Message` | `AU_Message_Code` | `String` |
