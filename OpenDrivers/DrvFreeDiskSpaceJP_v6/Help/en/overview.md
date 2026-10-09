# DrvFreeDiskSpaceJP — Overview

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/overview.md)

![DrvFreeDiskSpaceJP](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=DrvFreeDiskSpaceJP&color=4bb60e)

![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=10.0&color=purple)
![Platform](https://jurasskpark.ru/service/budges/?label=platform&message=Windows%20%7C%20Linux&color=lightgrey)
[![License](https://jurasskpark.ru/service/budges/?label=license&message=Apache%202.0&color=blue)](https://www.apache.org/licenses/LICENSE-2.0)

DrvFreeDiskSpaceJP is a Rapid SCADA 6 driver for monitoring free disk space and automatically reacting when the free space on a selected drive drops below a configured threshold.

The driver creates one or more tasks. Each task checks a drive, publishes disk status tags to Rapid SCADA, and can optionally delete old folders or compress and move them to another directory.
