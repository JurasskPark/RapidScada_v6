# DrvDbImportPlus — Обзор

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/overview.md)

![DrvDbImportPlus](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=DrvDbImportPlus&color=4bb60e)

![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=10.0&color=purple)
![Платформа](https://jurasskpark.ru/service/budges/?label=platform&message=Windows%20%2F%20Linux&color=lightgrey)

**DrvDbImportPlus** - драйвер связи Rapid SCADA для импорта текущих значений тегов из реляционных БД и InfluxDB, а также для отправки команд телеуправления Rapid SCADA в запросы БД.

Драйвер настраивается отдельно для каждого КП Rapid SCADA. У каждого КП есть собственный XML-файл конфигурации, например `DrvDbImportPlus_001.xml`. Во время сеанса опроса драйвер последовательно выполняет все включённые команды импорта, разбирает полученные таблицы в теги драйвера и обновляет соответствующие каналы Rapid SCADA.

Драйверу не требуется постоянное соединение с КП. Соединение с БД открывается на время выполнения запроса и затем закрывается. Период опроса по умолчанию, создаваемый модулем представления, составляет 5 секунд, но его можно изменить в настройках линии связи.
