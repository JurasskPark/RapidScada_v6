# DrvDbDataTransferJP — Updating Rapid SCADA Tags

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/updating-rapid-scada-tags.md)

Transfer mode can also update device tags. This is optional.

To update tags:

1. Add tags to the command.
2. Set each tag **Name** equal to a `SELECT` column name.
3. Set each tag **Code** equal to the Rapid SCADA channel code.
4. Use the proper tag format: `Float`, `Integer`, `DateTime`, `String` or `Boolean`.

Example:

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

Matching tags:

| Tag Name | Tag Code | Format |
| --- | --- | --- |
| `AU_ALID` | `AU_ALID_Code` | `Integer` |
| `AU_Process` | `AU_Process_Code` | `Boolean` or numeric |
| `AU_DateReport` | `AU_DateReport_Code` | `DateTime` |
| `AU_Message` | `AU_Message_Code` | `String` |
