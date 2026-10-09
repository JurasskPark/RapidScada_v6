# DrvDbDataTransferJP

![DrvDbDataTransferJP](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=DrvDbDataTransferJP&color=4bb60e)
![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=10.0&color=purple)
![Platform](https://jurasskpark.ru/service/budges/?label=platform&message=Windows%20%2F%20Linux&color=lightgrey)

[English](README.md) · [Русский](README.ru.md) · [English help](Help/en/index.md) · [Русская справка](Help/ru/index.md)

Переносит результаты запросов между базами данных и обновляет теги Rapid SCADA.

## Основные возможности

- Подключения источника и приемника настраиваются независимо.
- Поддерживаемые провайдеры: Microsoft SQL Server, Oracle, PostgreSQL, MySQL, Firebird, InfluxDB 2.x и InfluxDB 3.x.
- `SelectQuery` выполняется только в базе-источнике.
- `InsertQuery` выполняется только в базе-приемнике.
- Параметры целевой команды заполняются из колонок `SELECT` по имени.

## Скриншоты

![DrvDbDataTransferJP — скриншот 1](../../Source/DrvDbDataTransferJP_001.png)

![DrvDbDataTransferJP — скриншот 2](../../Source/DrvDbDataTransferJP_002.png)

[Лицензия и условия использования](Help/ru/license.md) · [Поддержка](https://forum.rapidscada.ru/?topic=drvdbdatatransferjp)
