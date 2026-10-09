# DrvParserTextJP — File selection

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/file-selection.md)

When working with files, the parser has several basic settings:
- Path is a setting that specifies the directory where the files that need to be parsed are located.
- Use subfolders? - this setting, which forces the parser to iterate through all subfolders in search of files for parsing.
- Read from the last line? - this setting, which will remember how many lines have been read in the file. If the setting is active, the fact that the file was previously processed is ignored.
- Read just one last line? - this setting will always read only one last line from the file.
- Filter - this setting allows you to filter files that are in the directory, but they do not need to be processed. For example, if there are other files in the directory besides text files, they will be ignored. In this setting, you need to specify the file format that the parser should work with, because in addition to the standard file.txt there are many more formats, for example, ini, csv, etc., which were invented by the developer.
- The template file name is a setting that helps when files have the same format and are in the same directory, but the file structure is different and the file name indicates by which they can be distinguished.
For example, data_hour_2025_01_01.txt and data_current_2025_01_01.txt
If you specify an hour in the Template file name, the parser will look for the presence of an hour in the file name and will process the file only if it exists. If there is only one file template in the directory, then only the file extension is specified.
