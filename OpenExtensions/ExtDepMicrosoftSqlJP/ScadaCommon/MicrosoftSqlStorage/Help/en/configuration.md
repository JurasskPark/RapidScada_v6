# MicrosoftSqlStorage — Configuration

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/configuration.md)

The storage creates a Microsoft SQL Server connection from the instance's default database connection options, using `KnownDBMS.MSSQL`.

`WaitTimeout` is read from the storage configuration XML node as an integer number of seconds. For example:

```xml
<WaitTimeout>30</WaitTimeout>
```

Startup attempts to connect during this wait period, with a connection attempt period of 10 seconds. Failed attempts are written to the application log.

Paths are normalized to backslash separators. This is the database key convention and does not change the Linux package platform.
