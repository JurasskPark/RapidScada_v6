# DrvDDEJP — Обзор

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/overview.md)

![DrvDDEJP](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=DrvDDEJP&color=4bb60e)

![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=10.0&color=purple)
![Платформа](https://jurasskpark.ru/service/budges/?label=platform&message=Windows&color=lightgrey)

**DrvDDEJP** - драйвер связи Rapid SCADA для чтения текущих значений из Windows-приложений через протокол Dynamic Data Exchange (DDE).

DrvDDEJP работает как DDE-клиент. Для каждого настроенного тега драйвер запрашивает DDE-значение по адресу `Service|Topic!Item`, декодирует полученный текст согласно выбранному формату тега и записывает значение в данные КП Rapid SCADA.

Драйвер настраивается отдельно для каждого КП Rapid SCADA. У каждого КП используется собственный XML-файл конфигурации, например `DrvDDEJP_001.xml`. Если файл отсутствует, драйвер создаёт его с настройками по умолчанию.

DrvDDEJP не реализует команды телеуправления Rapid SCADA. Это драйвер чтения: в runtime-логике отключено `CanSendCommands`.
