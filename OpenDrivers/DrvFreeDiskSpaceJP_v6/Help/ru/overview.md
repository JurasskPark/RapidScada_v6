# DrvFreeDiskSpaceJP — Обзор

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/overview.md)

![DrvFreeDiskSpaceJP](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=DrvFreeDiskSpaceJP&color=4bb60e)

![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=10.0&color=purple)
![Платформа](https://jurasskpark.ru/service/budges/?label=platform&message=Windows%20%7C%20Linux&color=lightgrey)
[![Лицензия](https://jurasskpark.ru/service/budges/?label=license&message=Apache%202.0&color=blue)](https://www.apache.org/licenses/LICENSE-2.0)

DrvFreeDiskSpaceJP - драйвер Rapid SCADA 6 для контроля свободного места на дисках и автоматической реакции, когда свободное место на выбранном носителе становится ниже заданного порога.

Драйвер создает одну или несколько задач. Каждая задача проверяет диск, передает теги состояния диска в Rapid SCADA и при необходимости удаляет старые каталоги либо сжимает и переносит их в другой каталог.
