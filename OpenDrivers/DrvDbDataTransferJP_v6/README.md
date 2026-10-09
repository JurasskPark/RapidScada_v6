# DrvDbDataTransferJP

![DrvDbDataTransferJP](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=DrvDbDataTransferJP&color=4bb60e)
![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=10.0&color=purple)
![Platform](https://jurasskpark.ru/service/budges/?label=platform&message=Windows%20%2F%20Linux&color=lightgrey)

[English](README.md) · [Русский](README.ru.md) · [English help](Help/en/index.md) · [Русская справка](Help/ru/index.md)

Transfers database query results and updates Rapid SCADA tags.

## Highlights

- Source and target database connections are configured independently.
- Supported providers: Microsoft SQL Server, Oracle, PostgreSQL, MySQL, Firebird, InfluxDB 2.x and InfluxDB 3.x.
- `SelectQuery` is executed only against the source database.
- `InsertQuery` is executed only against the target database.
- Target parameters are filled from `SELECT` columns by name.

## Screenshots

![DrvDbDataTransferJP — screenshot 1](../../Source/DrvDbDataTransferJP_001.png)

![DrvDbDataTransferJP — screenshot 2](../../Source/DrvDbDataTransferJP_002.png)

[License and usage conditions](Help/en/license.md) · [Support](https://forum.rapidscada.org/?topic=drvdbdatatransferjp)
