# DrvDDEJP

![DrvDDEJP](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=DrvDDEJP&color=4bb60e)
![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=10.0&color=purple)
![Platform](https://jurasskpark.ru/service/budges/?label=platform&message=Windows&color=lightgrey)

[English](README.md) · [Русский](README.ru.md) · [English help](Help/en/index.md) · [Русская справка](Help/ru/index.md)

Reads values from DDE servers for Rapid SCADA on Windows.

## Highlights

- **DDE client mode** - reads values from applications that expose a DDE service
- **Multiple topics** - keeps separate DDE client connections per topic and reuses them between requests
- **Default topic** - tag-specific topic can be empty, in this case the project default topic is used
- **Per-tag item mapping** - each Rapid SCADA tag maps to an individual DDE item name
- **Tag ordering** - enabled tags are polled in configured order

## Screenshots

![DrvDDEJP — screenshot 1](../../Source/DrvDDEJP_001.png)

[License and usage conditions](Help/en/license.md) · [Support](https://forum.rapidscada.ru/?topic=drvddejp)
