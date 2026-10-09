# DrvParserTextJP — Tag settings

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/tag-settings.md)

6. Creating tags.
The tag has several properties:
ID is an automatically generated individual non-repeatable key.
Enabled - whether the tag should be processed by the driver or not.
The name is the name of the tag that will be in Rapid Scada.
The tag code is the code by which Rapid Scada binds the variable.
The block address is the block number that the variable will be searched for.
Line address is the line number that the variable will be searched for.
Parameter address is the parameter number that will be used to search for the variable.
The data type is the format that a variable represents.
Number of decimal places - how many decimal places a variable should have after processing.
The maximum number of characters in a word is how many characters will be allocated to store the word in Rapid Scada.

The Table data type opens several additional settings.:
- Tags based on the list of requested table columns - is set active when the tag is the contents of a table column.
- Tags based on the list of requested table rows - is set active when the tag is in the same table entry, but the tag name and tag values are in different columns.
- Column names - specify the number of parameters separated by commas, which will be given column names. The names of the parameters are used to determine the names of the columns in the table.
- The name of the column with the tag - indicates the name of the column that should remain in the table after applying the filter. The column format is string format.
- Name of the column with the value - indicates the name of the column that should remain in the table after applying the filter. The column format is specified in the Value type.
- Value type - the format of the column with the value.
- Number of decimal places - how many decimal places the variable should have after processing the value.
- The maximum number of characters in a word is how many characters will be allocated to store the value in Rapid Scada.
- The name of the time column is an optional parameter. If the name is filled in, then the column format is specified as Datetime. It is used to convey historical values.

 For the Table tag type, addresses are specified according to the following logic:
 - Block address - the block number where the parameter table is located.
 - Row address - the row number of the block from where the table starts. the table header is not taken into account when creating the table. The line ending number is not specified in the address, because it is an unknown and periodically changed value.
 Example. 0.1- , where 0 is the block number, 1 is which row the table starts from.
 Example. 0.1-4, where 0 is the block number, 1 is which row the table starts from, and 4 is which row the table ends on.
 - Parameter address - the number of parameters (columns) will be in the table.
Example. 0.0-5, where 0 is the block number, 0 is the parameter the table starts with, 5 is the parameter the table ends with.
Example. 0.2-5, where 0 is the block number, 2 means that when creating the table, the first two parameters will be skipped, and 5 means which parameter the table ends with.
Skipping parameters is convenient when the table is large, and only 2-3 columns need to be added from a large table so as not to waste resources on unnecessarily filling in columns of values, which will then be deleted anyway during filtering.
