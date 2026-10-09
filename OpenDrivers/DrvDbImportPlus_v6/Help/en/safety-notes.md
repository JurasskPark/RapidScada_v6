# DrvDbImportPlus — Safety Notes

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/safety-notes.md)

- Import queries are executed every polling cycle, so keep them deterministic and fast.
- Column-based mode reads only the first returned row. Use row-based mode when one result must contain many tag rows.
- Without `TAGTIME` or `TAGDATETIME`, values are written as current tag data. With `TAGTIME` or `TAGDATETIME`, values are sent as historical archive slices.
- Command export queries are executed as non-query commands. Use restricted database accounts for write operations.
- Password and custom connection string are encrypted in the XML configuration, but database-side permissions still remain the main protection.
- Empty, `null` or `DBNull` values invalidate the target tag data.
- Detailed driver logging can write query results and tag tables to the communication line log. Avoid enabling it permanently for sensitive data.
