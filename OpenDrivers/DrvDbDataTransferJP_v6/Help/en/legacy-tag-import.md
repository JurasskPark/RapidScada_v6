# DrvDbDataTransferJP — Legacy Tag Import

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/legacy-tag-import.md)

If `InsertQuery` is empty, the command does not transfer data to a target database. It works as a tag import command.

## Column-Based Mode

In column-based mode the first result row contains tag values. Column aliases are matched with configured tag names.

```sql
SELECT
    temperature AS BoilerTemp,
    pressure AS BoilerPressure
FROM process_values
ORDER BY measured_at DESC
LIMIT 1;
```

## Row-Based Mode

In row-based mode each row contains one tag value. The result must contain:

- `TAGNAME`
- `TAGVALUE`
- optional `TAGTIME` or `TAGDATETIME`

```sql
SELECT
    tag_name AS TAGNAME,
    tag_value AS TAGVALUE,
    tag_time AS TAGDATETIME
FROM current_tag_values;
```

`TAGTIME` and `TAGDATETIME` are parsed as timestamps. Tags with timestamps are grouped and enqueued as historical slices by the driver runtime.
