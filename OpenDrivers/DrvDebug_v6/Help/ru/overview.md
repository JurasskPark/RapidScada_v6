# DrvDebug — Обзор

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/overview.md)

![DrvDebug](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=DrvDebug&color=4bb60e)

![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=10.0&color=purple)
![Платформа](https://jurasskpark.ru/service/budges/?label=platform&message=Windows&color=lightgrey)
[![Лицензия](https://jurasskpark.ru/service/budges/?label=license&message=Apache%202.0&color=blue)](https://www.apache.org/licenses/LICENSE-2.0)

**DrvDebug** - драйвер связи Rapid SCADA для проверки байтовых протоколов, каналов связи, декодирования пакетов, отправки команд и симуляции значений тегов.

DrvDebug не привязан к одному промышленному протоколу. Он работает с сырыми массивами байт и подходит как диагностический драйвер при разработке, тестировании и поиске проблем обмена с внешними устройствами и сервисами.

Драйвер поддерживает режимы линии `Master`, `Slave` и `Mixed`. В режимах master и mixed он может отправлять настроенные payload-команды и читать ответы. В режимах slave и mixed он может принимать входящие пакеты, декодировать их в теги и при необходимости отправлять настроенный ответ.

DrvDebug также может генерировать значения без входящих данных. Симуляция полезна для тестовых проектов, проверки интерфейса, проверки прототипов каналов и резервных значений при ошибке декодирования.
