# DrvDbImportPlus — How Import Works

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/how-import-works.md)

Each import command contains:

- command number and command code;
- display name and description;
- SQL or Influx query text;
- processing mode: column-based or row-based;
- a list of configured driver tags.

Only enabled import commands and enabled tags are processed. The tag **Name** is used to find data in the query result. The tag **Code** is used to update the Rapid SCADA channel with the same code.

## Column-Based Mode

In column-based mode the driver reads the first row of the result table. Column names are compared with configured tag names, case-insensitively.

Example:

```sql
select
  temperature as BoilerTemp,
  pressure as BoilerPressure,
  state as PumpState,
  measured_at as TAGDATETIME
from process_values
where unit_id = 1;
```

If a `TAGTIME` or `TAGDATETIME` column exists, it is parsed as a common timestamp for the values and the runtime enqueues historical archive slices.

The column-based parser also supports a single-row `TAGNAME` plus `TAGVALUE` result.

## Row-Based Mode

In row-based mode the query result must contain:

- `TAGNAME` - tag name configured in the driver;
- `TAGVALUE` - value to write to the tag;
- `TAGDATETIME` - optional timestamp.

Example:

```sql
select
  tag_name as TAGNAME,
  tag_value as TAGVALUE,
  tag_time as TAGDATETIME
from current_tag_values
where device_id = 1;
```

Every row is matched by `TAGNAME`. Rows with unknown tag names are ignored.
