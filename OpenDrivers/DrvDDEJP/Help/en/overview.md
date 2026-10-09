# DrvDDEJP — Overview

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/overview.md)

![DrvDDEJP](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=DrvDDEJP&color=4bb60e)

![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=10.0&color=purple)
![Platform](https://jurasskpark.ru/service/budges/?label=platform&message=Windows&color=lightgrey)

**DrvDDEJP** is a Rapid SCADA communication driver for reading real-time values from Windows applications through the Dynamic Data Exchange (DDE) protocol.

DrvDDEJP works as a DDE client. For each configured tag the driver requests a DDE value by `Service|Topic!Item`, decodes the returned text according to the selected tag format, and writes the value to Rapid SCADA device data.

The driver is configured per Rapid SCADA device. Each device uses its own XML configuration file, for example `DrvDDEJP_001.xml`. If the file does not exist, the driver creates it with default settings.

DrvDDEJP does not implement Rapid SCADA telecontrol commands. It is a read-oriented driver: `CanSendCommands` is disabled in the runtime logic.
