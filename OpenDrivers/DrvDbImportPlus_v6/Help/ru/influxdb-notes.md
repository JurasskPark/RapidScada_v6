# DrvDbImportPlus — Замечания по InfluxDB

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/influxdb-notes.md)

Поддержка InfluxDB реализована через HTTP API `/query`. Если пользовательская строка подключения не задана, драйвер формирует её из полей интерфейса:

- `Server` и `Port` формируют `Url`, порт по умолчанию - `8086`;
- `Password` используется как bearer token;
- `Database` используется как `Bucket` для InfluxDB 2.x;
- `Database` используется как `Database` для InfluxDB 3.x;
- `OptionalOptions` добавляются в сформированную строку подключения.
