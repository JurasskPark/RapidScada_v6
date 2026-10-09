# DrvDbDataTransferJP — Окна опроса

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/polling-windows.md)

```sql
WHERE occur_time >= localtimestamp - interval '5 hour 5 minute'
```

```sql
WHERE occur_time >= localtimestamp - interval '5 hour 5 minute 10 second'
  AND occur_time <  localtimestamp - interval '5 hour 5 minute'
```

Драйвер не хранит timestamp предыдущего успешного SQL-запроса. Если запрос использует скользящее время, само SQL-условие должно задавать ограниченное окно. Для защиты от дублей используйте уникальные ключи или UPSERT на стороне приемника.
