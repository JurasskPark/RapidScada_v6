# DrvDbDataTransferJP — Overview

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/overview.md)

![DrvDbDataTransferJP](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=DrvDbDataTransferJP&color=4bb60e)

![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=10.0&color=purple)
![Platform](https://jurasskpark.ru/service/budges/?label=platform&message=Windows%20%2F%20Linux&color=lightgrey)

**DrvDbDataTransferJP** is a Rapid SCADA 6 communication driver for moving data between databases. It executes a `SELECT` query against a source database, then writes the returned rows to a target database using a parameterized `INSERT`, `UPDATE`, `MERGE` or UPSERT command.

The same `SELECT` result can also update Rapid SCADA tags. If a transfer command contains configured tags, the driver maps result columns to tag names after a successful transfer.
