# ExtDepMicrosoftSqlJP — Обзор

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/overview.md)

![ExtDepMicrosoftSqlJP](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=ExtDepMicrosoftSqlJP&color=4bb60e)

![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=10.0&color=purple)
[![Лицензия](https://jurasskpark.ru/service/budges/?label=license&message=Apache%202.0&color=blue)](../../../License.txt)

**Развёртывание в Microsoft SQL Server JP** — расширение Администратора Rapid SCADA для развёртывания конфигурации проекта в Microsoft SQL Server.

ExtDepMicrosoftSqlJP загружает и выгружает базу конфигурации проекта Rapid SCADA, используя Microsoft SQL Server в качестве целевой СУБД.

Расширение предназначено для совместной работы с `MicrosoftSqlStorage` и может использоваться вместе с серверным модулем `ModArcMicrosoftSqlJP`. Расширение хранит конфигурацию проекта в схеме `project`, а архивный модуль записывает runtime-архивы в схему `mod_arc_microsoft_sql`.
