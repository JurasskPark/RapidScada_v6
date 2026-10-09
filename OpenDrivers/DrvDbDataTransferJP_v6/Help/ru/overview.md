# DrvDbDataTransferJP — Обзор

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/overview.md)

![DrvDbDataTransferJP](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=DrvDbDataTransferJP&color=4bb60e)

![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=10.0&color=purple)
![Платформа](https://jurasskpark.ru/service/budges/?label=platform&message=Windows%20%2F%20Linux&color=lightgrey)

**DrvDbDataTransferJP** - драйвер связи Rapid SCADA 6 для переноса данных между базами данных. Он выполняет `SELECT` в базе-источнике и записывает полученные строки в базу-приемник через параметризованный `INSERT`, `UPDATE`, `MERGE` или UPSERT.

Тот же результат `SELECT` может дополнительно обновлять теги Rapid SCADA. Если в команде переноса настроены теги, драйвер после успешного переноса сопоставляет колонки результата с именами тегов.
