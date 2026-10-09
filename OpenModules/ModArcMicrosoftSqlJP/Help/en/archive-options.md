# ModArcMicrosoftSqlJP — Archive Options

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/archive-options.md)

| Option | Description |
| --- | --- |
| `UseDefaultConn` | Use default instance connection from `ScadaInstanceConfig.xml` |
| `Connection` | Named connection from `ModArcMicrosoftSqlJP.xml` |
| `ReadOnly` | Disable writes to the database |
| `MaxQueueSize` | Maximum number of queued items |
| `BatchSize` | Number of items written in one transaction |
| `PartitionSize` | Logical partition period option for historical and event archives |
| `UseMemoryCache` | Cache historical values in memory when reading |
| `CacheSizeRatio` | Cache size ratio to the number of archive channels |
