# DrvDbDataTransferJP — Transfer Mode

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/transfer-mode.md)

A command works in transfer mode when both fields are filled:

- `SelectQuery`
- `InsertQuery`
- `SelectQuery`
- `InsertQuery`

## Execution Flow

1. The driver resolves date/time patterns in `SelectQuery`.
2. The driver executes `SelectQuery` against `SourceDbConnSettings`.
3. The result is read into a `DataTable`.
4. If the result is empty, the command stops without target writes.
5. The driver executes `InsertQuery` against `TargetDbConnSettings`.
6. Target SQL parameters are filled from `DataTable` columns by name.
7. If tags are configured and rows were read successfully, the same `DataTable` is converted to Rapid SCADA tag values.
8. The driver logs transfer result only when rows were read or an error occurred.

## SELECT Contract

`SelectQuery` defines the data contract. Every target parameter in `InsertQuery` must have a matching column or alias in the `SELECT` result.

Good:

```sql
SELECT
    occur_time AS AU_DateReport,
    description AS AU_Message,
    20::bigint AS AU_ALID
FROM public.event_data
WHERE level = 'Alarm';
```

If a source column has spaces or punctuation, give it a simple alias:

```sql
SELECT
    "Parametr" AS Parametr
FROM public."{YYYY}{MM}{DD}.Data";
```

`SelectQuery` may contain extra columns. Extra columns are not written to the target unless `InsertQuery` contains matching parameters, but they can still be used for Rapid SCADA tags.

## INSERT / UPDATE / UPSERT Contract

`InsertQuery` is a target non-query command. The setting name is historical: the command can be `INSERT`, `UPDATE`, `MERGE`, PostgreSQL `INSERT ... ON CONFLICT`, MySQL `INSERT ... ON DUPLICATE KEY UPDATE`, Firebird `UPDATE OR INSERT`, or another provider-supported non-query command.

Supported parameter forms:

- `@name`
- `:name`

The recommended style in the UI is `@name`. Oracle commands are normalized to `:name` automatically.

Example:

```sql
INSERT INTO dbo.Report_Data_JSON
    (AU_Id, AU_Date, AU_DateReport, AU_ALID, AU_Message)
VALUES
    (NEWID(), GETDATE(), @AU_DateReport, @AU_ALID, @AU_Message);
```

For this target command the source query must return columns named:

- `AU_DateReport`
- `AU_ALID`
- `AU_Message`
