# DrvDbDataTransferJP — Date/Time Patterns

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/date-time-patterns.md)

`SelectQuery` is processed by:

```csharp
DriverUtils.ResolveDateTimePatterns(string input, DateTime? dateTime = null)
```

Supported tokens:

| Token | Meaning | Example |
| --- | --- | --- |
| `{YYYY}` | four-digit year | `2026` |
| `{YY}` | two-digit year | `26` |
| `{MM}` | month | `07` |
| `{DD}` | day | `09` |
| `{HH}` | hour | `21` |
| `{mm}` | minute | `05` |
| `{ss}` | second | `08` |

Example:

```sql
SELECT *
FROM public."{YYYY}{MM}{DD}.Data"
WHERE "time" >= 638940096000000000;
```

Unknown expressions in braces are left unchanged. Historical SQL-window processing is not part of the current driver.
