# MicrosoftSqlStorage — Troubleshooting

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/troubleshooting.md)

- On connection errors, check the instance database connection settings, database availability and SQL account access.
- On missing-file errors, verify deployment, application ID and the normalized path.
- On missing configuration-table columns, verify the deployed schema. Reading a table requires its primary-key column; absent optional columns are skipped.
- On `PlatformNotSupportedException` from SqlClient, use a package with an explicit runtime identifier and preserve its dependencies.
- Treat archive data separately: it belongs to ModArcMicrosoftSqlJP, not this storage.
