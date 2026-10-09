# DrvParserTextInDatabaseJP — Checking the data map

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/checking-the-data-map.md)

For convenient drafting for parsing, it is convenient to do the following procedure.
1. Copy the contents of the file and paste the Contents into the text field on the second Check tab.
2. Analyze the file for its contents and determine which delimiters will separate the blocks, lines, and parameters.
3. Make the necessary separators in the Settings and save the project.
4. Click the Map button and see how the application has divided the file contents.

Example.

```text
[0][0][0]=[Mikhail]
[0][0][1]=[writes]
[0][0][2]=[the]
[0][0][3]=[world's]
[0][0][4]=[best]
[0][0][5]=[mnemonic]
[0][0][6]=[editor.]
[0][1][0]=[Mikhail]
[0][1][1]=[writes]
[0][1][2]=[the]
[0][1][3]=[world's]
[0][1][4]=[best]
[0][1][5]=[mnemonic]
[0][1][6]=[editor]
[0][1][7]=[and]
[0][1][8]=[RapidScada.]
```

Where the first value in square brackets is the block number, the second value in square brackets is the line number, and the third value in square brackets is the parameter number.

5. If the option of the parameters on the map suits you, then go to the creation of tags, otherwise, go back to the creation of separators.
