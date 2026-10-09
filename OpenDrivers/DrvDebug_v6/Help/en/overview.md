# DrvDebug — Overview

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/overview.md)

![DrvDebug](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=DrvDebug&color=4bb60e)

![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=10.0&color=purple)
![Platform](https://jurasskpark.ru/service/budges/?label=platform&message=Windows&color=lightgrey)
[![License](https://jurasskpark.ru/service/budges/?label=license&message=Apache%202.0&color=blue)](https://www.apache.org/licenses/LICENSE-2.0)

**DrvDebug** is a Rapid SCADA communication driver for testing byte-array protocols, transport channels, packet decoding, command sending and simulated tag values.

DrvDebug is not tied to one field protocol. It works with raw byte arrays and can be used as a diagnostic driver when developing, testing or troubleshooting communication with external devices and services.

The driver supports `Master`, `Slave` and `Mixed` channel behavior. In master and mixed modes it can send configured command payloads and read responses. In slave and mixed modes it can receive incoming packets, decode them into tags and optionally send a configured response payload.

DrvDebug can also generate values without incoming data. Simulation can be used for test projects, UI checks, channel prototype checks and fallback values when decoding fails.
