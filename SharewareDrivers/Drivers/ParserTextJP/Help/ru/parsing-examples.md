# DrvParserTextJP — Примеры разбора

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/parsing-examples.md)

7. После создании тегов и настроек, при нажатии Проверить, содержимое текстового блока Содержание будет отображено в текстовом поле Результат.

Пример.

Содержание:

```text
Mikhail writes the world's best mnemonic editor.
Mikhail writes the world's best mnemonic editor and RapidScada.
Validate Int 0123 456 789 111 222 333 444 555 666 777.
Validate IntDot 0123 456 789 111 222 333 444 555.
ValaidateBool True False true false
TagDate 01.01.2020 2020-01-01 12-12-2020 01.01.2020 2020-01-01 12-12-2020
TagFloat checking the meaning of a sentence with a 123.546.
```

Теги:

```text
TagString1		[0][0][6] String
Michail 		[0][1][8] String
TagInt			[0][2][7] Integer
TagIntDot		[0][3][9] Integer
TagBoolTrue		[0][4][2] Boolean
TagBoolFalse		[0][4][3] Boolean
TagDate			[0][5][6] Datetime
TagFloat		[0][6][9] Float
```

Результат:

```text
TagString1=editor
Michail=RapidScada
TagInt=333
TagIntDot=555
TagBoolTrue=False
TagBoolFalse=True
TagDate=2020-12-12 00:00:00
TagFloat=123,546
```
