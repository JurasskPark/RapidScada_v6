# DrvDbDataTransferJP — Polling Windows

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/polling-windows.md)

The driver does not remember the previous successful SQL timestamp. If a query uses a moving time condition, the SQL itself must define a bounded window.

Bad for periodic polling:

```sql
WHERE occur_time >= localtimestamp - interval '5 hour 5 minute'
```

This returns the same trailing interval on every polling cycle.

Better:

```sql
WHERE occur_time >= localtimestamp - interval '5 hour 5 minute 10 second'
  AND occur_time <  localtimestamp - interval '5 hour 5 minute'
```

Use target-side unique keys or UPSERT when duplicate protection is required.
