# DrvDebug — Прототипы каналов

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/channel-prototypes.md)

ScadaAdmin создаёт один прототип канала `InputOutput` для каждого настроенного тега в порядке `Order`. Имя прототипа берётся из имени тега, код тега формируется из имени тега, а `DataLen` берётся из `DataLength`.

`Ascii` и `Unicode` используют строковые типы данных, `Int64` и `UInt64` используют Int64, остальные форматы используют Double.
