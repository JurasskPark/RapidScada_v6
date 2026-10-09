# DrvDbImportPlus — Overview

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/overview.md)

![DrvDbImportPlus](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=DrvDbImportPlus&color=4bb60e)

![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=10.0&color=purple)
![Platform](https://jurasskpark.ru/service/budges/?label=platform&message=Windows%20%2F%20Linux&color=lightgrey)

**DrvDbImportPlus** is a Rapid SCADA communication driver for importing current tag values from relational databases and InfluxDB, and for sending Rapid SCADA telecontrol commands back to a database query.

The driver is configured per Rapid SCADA device. Each device has its own XML configuration file, for example `DrvDbImportPlus_001.xml`. During a polling session the driver executes all enabled import commands, parses returned tables into driver tags, and updates matching Rapid SCADA channels.

The driver does not require a persistent device connection. Database connections are opened for query execution and then closed. The default polling period created by the view module is 5 seconds, but it can be changed in the communication line settings.
