# DrvDebug

![DrvDebug](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=DrvDebug&color=4bb60e)
![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=10.0&color=purple)
![Platform](https://jurasskpark.ru/service/budges/?label=platform&message=Windows&color=lightgrey)

[English](README.md) · [Русский](README.ru.md) · [English help](Help/en/index.md) · [Русская справка](Help/ru/index.md)

Tests communication scenarios, decodes responses and simulates device values.

## Highlights

- **Protocol-neutral byte arrays** - sends and receives raw bytes instead of implementing a fixed protocol
- **Master, Slave and Mixed modes** - supports all Rapid SCADA channel behavior modes
- **Configurable command list** - sends enabled commands in configured order
- **Command payload formats** - sends `Ascii` and `Unicode` as text encodings; other configured command data kinds are sent as HEX bytes by the current runtime
- **Incoming packet stop condition** - detects packet completion by marker or by length field

## Screenshots

![DrvDebug — screenshot 1](../Source/DrvDebug_001.png)

![DrvDebug — screenshot 2](../Source/DrvDebug_002.png)

[License and usage conditions](Help/en/license.md) · [Support](https://forum.rapidscada.ru/?topic=drvdebug)
