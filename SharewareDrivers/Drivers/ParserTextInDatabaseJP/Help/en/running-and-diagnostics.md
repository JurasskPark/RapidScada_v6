# DrvParserTextInDatabaseJP — Running and diagnostics

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/running-and-diagnostics.md)

8. After checking the tags, the correctness of the parsing task is checked. Select Run from the menu.
The result of the work will be in the Result text field.

Example.

```text
[2025-03-09 16:35:47.41083] File parsing C:\Debug\1\format.txt.
[2025-03-09 16:35:47.41324]
Mikhail writes the world's best mnemonic editor.
Mikhail writes the world's best mnemonic editor and RapidScada.
Validate Int 0123 456 789 111 222 333 444 555 666 777.
Validate IntDot 0123 456 789 111 222 333 444 555.
ValaidateBool True False true false
TagDate 01.01.2020 2020-01-01 12-12-2020 01.01.2020 2020-01-01 12-12-2020
TagFloat checking the meaning of a sentence with a 123.546.

TagString1=editor
Michail=RapidScada
TagInt=333
TagIntDot=555
TagBoolTrue=False
TagBoolFalse=True
TagDate=2020-12-12 00:00:00
TagFloat=123,546
```

Where the first date point indicated the beginning of the parsing date and the path to the file that the file parser was processing.
The second paragraph indicated the contents of the file and what values the tags received at the output.

Information about the file being processed was also recorded in a file with the task name and extension.db
```text
C:\Debug\2\test.csv|25.02.2025 10:48:08|280|5|False|0
```
The first value specifies the path to the file.
The second value indicates the date when the file was modified.
The third value indicates the file size.
The fourth value indicates the number of lines that have been read.
The fifth value indicates whether the file has been modified.
The sixth value indicates the result of file processing, where

0 - means the file has not been processed<br>
1 - means the file has been processed successfully.<br>
2 - the file was not processed due to an error.<br>

9. After debugging the parser, logging can be disabled through the Settings - Record the result of execution (debugging).
