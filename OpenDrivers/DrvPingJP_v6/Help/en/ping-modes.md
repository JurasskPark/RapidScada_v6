# DrvPingJP — Ping Modes

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/ping-modes.md)

| Mode | Name | Description |
| --- | --- | --- |
| `0` | Synchronous | Runs ping processing for enabled tags and waits until all created tasks are completed. |
| `1` | Asynchronous | Starts asynchronous ping operations for enabled tags and waits for all operations with `Task.WhenAll`. |
