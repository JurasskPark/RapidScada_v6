# DrvDbDataTransferJP — Шаблоны даты и времени

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/date-time-patterns.md)

`SelectQuery` обрабатывается методом `DriverUtils.ResolveDateTimePatterns(string input, DateTime? dateTime = null)`.

Поддерживаемые шаблоны и примеры:

```csharp
DriverUtils.ResolveDateTimePatterns(string input, DateTime? dateTime = null)
```

| Шаблон | Значение | Пример |
| --- | --- | --- |
| `{YYYY}` | четырёхзначный год | `2026` |
| `{YY}` | двухзначный год | `26` |
| `{MM}` | месяц | `07` |
| `{DD}` | день | `09` |
| `{HH}` | час | `21` |
| `{mm}` | минута | `05` |
| `{ss}` | секунда | `08` |

```sql
SELECT *
FROM public."{YYYY}{MM}{DD}.Data"
WHERE "time" >= 638940096000000000;
```

Неизвестные выражения в фигурных скобках не изменяются. Историческая обработка SQL-окон не входит в текущий драйвер.
