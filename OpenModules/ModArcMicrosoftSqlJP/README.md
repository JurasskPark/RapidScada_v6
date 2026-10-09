# ModArcMicrosoftSqlJP

![ModArcMicrosoftSqlJP](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=ModArcMicrosoftSqlJP&color=4bb60e)
![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=10.0&color=purple)

[English](README.md) · [Русский](README.ru.md) · [English help](Help/en/index.md) · [Русская справка](Help/ru/index.md)

Stores Rapid SCADA archives in Microsoft SQL Server.

## Highlights

- **Archive kinds** — supports Current, Historical, and Events archives
- **Automatic table creation** — creates schema and archive tables on server startup
- **Batch writing** — writes data points and events in transactions with configurable batch size
- **Queue buffering** — uses write queues to reduce database load
- **Connection manager** — stores named SQL Server connections in `ModArcMicrosoftSqlJP.xml`

[License and usage conditions](Help/en/license.md) · [Support](https://forum.rapidscada.ru/?topic=modarcmicrosoftsqljp)
