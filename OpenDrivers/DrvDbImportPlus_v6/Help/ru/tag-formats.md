# DrvDbImportPlus — Форматы тегов

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/tag-formats.md)

| Формат драйвера | Поведение |
| --- | --- |
| `Float` | Преобразует в double и применяет числовой формат `N{NumberDecimalPlaces}` |
| `Integer` | Записывает целые значения с целочисленным форматом |
| `DateTime` | Записывает DateTime-значения как DateTime Rapid SCADA |
| `String` | Записывает Unicode-строки |
| `Boolean` | Записывает значения в формате Off/On |

Если импортированное значение равно `null` или `DBNull`, данные соответствующего тега Rapid SCADA помечаются недостоверными.
