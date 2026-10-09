# DrvDDEJP — Адресация DDE

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/dde-addressing.md)

```text
ServiceName|Topic!ItemName
```

```text
Excel|Sheet1!R1C1
```

DDE-запрос строится из трёх полей:

`ServiceName` задаётся один раз для КП. `Topic` можно указать для каждого тега; если он пустой, используется `DefaultTopic`. `ItemName` обязателен для каждого тега.

Пример:
