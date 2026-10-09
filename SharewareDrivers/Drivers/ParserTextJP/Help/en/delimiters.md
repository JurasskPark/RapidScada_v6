# DrvParserTextJP — Delimiters

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/delimiters.md)

Splitting the file
You need to understand that the driver needs three delimiters for parsing.:
- block
- line
- parameter

A block is a required parameter by which an application determines how to split data into the same structure.
The signs of the block from the developer may vary, but even if it does not exist, the separator block must be specified, for example, {BLOCK}

Example, blocks.

```text
Table of contents 1
Text: 01
Text: 02
-------

Table of contents 2
Text: 01
Text: 02
-------
```

Where, the block separator could be both ------- and the Table of Contents , because these are both signs, according to which the same file structure followed.

A string is a parameter that splits text into lines. In 95% of cases, these are hyphenation characters \r and \n, but since these characters are not displayed to the user, the format is used to visually indicate that the separator is specified:
	{LF} - \n
	{CR} - \r

A parameter is a property where the separator is mainly a space or a tab character, and for csv files, the character , or ;. To visually indicate that the separator is specified, the format is used:
	{SPACE} - (space)
{	TAB} - \t (tab)

When separating blocks, rows, and parameters, sometimes empty variables remain. There are three possible actions with empty variables:
- do nothing
- delete
- crop
