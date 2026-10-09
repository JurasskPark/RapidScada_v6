# ExtDepMicrosoftSqlJP — Database Objects

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/database-objects.md)

ExtDepMicrosoftSqlJP creates and fills project configuration objects in the SQL Server schema:

```sql
project
```

Typical objects include configuration tables, views, foreign keys, and application configuration records. Examples of tables:

```text
project.app
project.archive
project.cnl
project.comm_line
project.device
project.format
project.obj
project.quantity
project.role
project.unit
```

Runtime archives are not written by this extension. Runtime archive data is written by `ModArcMicrosoftSqlJP`.
