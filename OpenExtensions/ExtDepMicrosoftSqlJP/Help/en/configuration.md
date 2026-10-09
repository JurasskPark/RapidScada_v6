# ExtDepMicrosoftSqlJP — Configuration

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/configuration.md)

The extension configuration file is:

```text
ExtDepMicrosoftSqlJP.xml
```

Default configuration:

```xml
<?xml version="1.0" encoding="utf-8"?>
<ExtDepMicrosoftSqlJP>
  <ClearBaseMethod>DropTables</ClearBaseMethod>
</ExtDepMicrosoftSqlJP>
```

Available cleanup methods:

| Value | Description |
| --- | --- |
| `DropTables` | Drops and recreates project configuration tables |
| `TruncateTables` | Clears existing project configuration tables |

Use `DropTables` for the first deployment or a full rebuild of the configuration database. Use `TruncateTables` when the schema already exists and only table data must be refreshed.
