# DrvDbDataTransferJP — Logging

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/logging.md)

The driver writes transfer progress to the communication line log:

- command name;
- source and target database type;
- read row count;
- written row count;
- target write errors;
- optional query result and tag tables when debug logging is enabled.

If `SELECT` returns 0 rows in transfer mode, target write is skipped and the transfer result is not logged.
