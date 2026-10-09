# DrvDebug

![DrvDebug](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=DrvDebug&color=4bb60e)
![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=10.0&color=purple)
![Platform](https://jurasskpark.ru/service/budges/?label=platform&message=Windows&color=lightgrey)

[English](README.md) · [Русский](README.ru.md) · [English help](Help/en/index.md) · [Русская справка](Help/ru/index.md)

Проверяет сценарии связи, разбирает ответы и моделирует значения устройств.

## Основные возможности

- **Протокольно-независимые массивы байт** - отправка и приём сырых байт без жёсткой привязки к одному протоколу
- **Режимы Master, Slave и Mixed** - поддержка всех режимов поведения канала Rapid SCADA
- **Настраиваемый список команд** - отправка включённых команд в заданном порядке
- **Форматы payload команд** - `Ascii` и `Unicode` отправляются как текстовые кодировки; остальные типы данных команд в текущем runtime отправляются как HEX-байты
- **Условие завершения входящего пакета** - определение конца пакета по маркеру или полю длины

## Скриншоты

![DrvDebug — скриншот 1](../Source/DrvDebug_001.png)

![DrvDebug — скриншот 2](../Source/DrvDebug_002.png)

[Лицензия и условия использования](Help/ru/license.md) · [Поддержка](https://forum.rapidscada.ru/?topic=drvdebug)
