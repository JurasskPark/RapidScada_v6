	Драйвера для Rapid SCADA.
	Drivers  for Rapid SCADA.

	
### DrvParserTextJP
##Инструкция

При работе с файлами у парсера есть несколько основных настроек:
- Путь - эта настройка, в которой указывается каталог, где находятся файлы, которые необходимо парсить.
- Использовать подпапки? - эта настройка, которая заставляет парсер перебирать все подпапки в поиске файлов для парсинга.
- Читать с последней строки? - эта настройка, которая будет запоминать, сколько было прочитано строк в файле. В случае, если настройка активна, то игнорируется факт, что файл был ранее обработан.
- Фильтр - эта настройка, позволяет отфильтровать файлы, которые находятся в каталоге, но их не нужно обрабатывать. Например, если в каталоге помимо текстовых файлов, находятся другие файлы, то они будут игнорироваться. В данной настроке нужно указывать формат файлов, с которыми парсер должен работать, т.к. помимо стандартного файла .txt существуют еще много форматов, например, ini, csv и д.р., которые придумал разработчик.
- Имя файла шаблона - это настройка, которая помогает, когда у файлов одинаковый формат и они находятся в одном каталоге, но структура файлов разная и в имени файла признак, по которым их можно различать.
Например, data_hour_2025_01_01.txt и data_current_2025_01_01.txt
Если в Имени файла шаблона указать hour, то парсер будет искать наличие в имени файла hour и только при наличии его, будет обрабатывать файла. Если в каталоге только один шаблон файлов, то указывается только расширение файла.

Разделение файла
Нужно понимать, что для драйвера нужно три разделителя для парсинга:
- блок
- строка
- параметр


Блок - это обязательный параметр, по которому приложение определяет как разбивать на одинаковую структуру данные.
Признаки у блока от разработчика могут разные, но даже если его нет, блок разделитель нужно указать, например, {BLOCK} .  

	Пример, блоков.  
	Оглавление 1  
	Текст: 01  
	Текст: 02  
	-------  
	  
	Оглавление 2  
	Текст: 01  
	Текст: 02  
	-------  
  


Где, разделителем блока можно было взять как ------- , так и Оглавление , потому что это оба признаки, по которому дальше шла одинаковая структура файла.\

Строка - это параметр, который разбивает текст по строкам. В 95% случаев это символы переноса \r и \n, но так как данные символы не отображаются для пользователя, то для наглядного отображения, что разделитель указан, используется формат:
	{LF} - \n 
	{CR} - \r 

Параметр - это свойство, у которого разделителем в основном является пробел или символ табуляции, а для csv файлов символ , или ;. Для наглядного отображения, что разделитель указан, используется формат:
	{SPACE} -  (пробел)
	{TAB} - \t (табуляция)

При разделении блоков, строк и параметров иногда остаются пустые переменные. С пустыми переменными возможно три действия:
- ничего не делать
- удалять
- обрезать

Для удобного составление проекта для парснга удобно делать следующий порядок действий.
1. Скопировать содержимое файла и вставить в текстовое поле Содержимое на второй вкладке Проверить.
2. Проанализировать файл на содержимое и определить какими разделителями будут разделены блоки, строки и параметры.
3. Внести в Настройки необходимые разделители и сохранить проект.
4. Нажать кнопку Карта и посмотреть как приложение разделило файл содержимое.

Пример.
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
Где первое значение в квадратных скобках - это номер блока, второе значение в квадратных скобках - это номер строки, третье значение в квадратных скобках - это номер параметра. 

5. Если вариант параметров на карте вас устраивает, то перейти к созданию тегов, в обратном случае, вернуться к созданию разделителей.
6. Создание тегов.
У тега существуют несколько свойств:
ID - автоматически генерируемый индивидуальный неповторяемый ключ.
Включен - должен ли тег обрабатываться драйвером или нет.
Название - название тега, который будет в Rapid Scada.
Код тега - код, по которому Rapid Scada привязывает переменную.
Адрес блока - номер блока, по которому будет производится поиск переменной.
Адрес строки - номер строки, по которому будет производится поиск переменной.
Адрес параметра - номер параметра, по которому будет производится поиск переменной.
Тип данных - формат, что из себя представляет переменная.
Количество знаков после запятой - сколько символов после запятой у переменной должно быть после обработки.
Максимальное количество знаков в слове - сколько символов выделятся для хранения слова в Rapid Scada.

Тип данных Table открывает несколько дополнительных настроек:
- Теги на основе списка запрашиваемых столбцов таблицы - ставится активным, когда тег - это содержимое столбца таблицы.
- Теги на основе списка запрашиваемых строк таблицы - ставится активным, когда тег находится в одной записи таблицы, но название тега и значения тега находятся в разных столбцах.
- Названия столбцов - указываются через запятую количество параметров, которым будут даны названия столбцов. Названия параметров используются при определения названия столбцов таблицы.
- Название столбца с тегом - указывается название столбца, который должен остаться в таблице после применении фильтра. Формат столбца - строковый.
- Название столбца с значением - указывается название столбца, который должен остаться в таблице после применении фильтра. Формат столбца указывается в Тип значения.
- Тип значения - формат столбца с значением. 
- Количество знаков после запяток - сколько символов после запятой у переменной должно быть после обработки значения.
- Максимальное количество знаков в слове - сколько символов выделятся для хранения значения в Rapid Scada.
- Название столбца с временем - необязательный параметр. Если заполнено название, то формат столбца указывается - Datetime. Используется для передачи исторических значений.
 
 Для типа тега Table адреса указываются по следующей логике:
 - Адрес блока - номер блока, где находится параметр-таблица.
 - Адрес строки - номер строки блока, откуда начинается таблицы. заголовок таблицы не учитываются при создании таблицы. Номер окончания строки в адресе не указывается, т.к. это неизвестное и периодически изменяемое значение.
 Пример. 0.1- , где 0 - это номер блока, 1 - с какой строки начинается таблица.
 Пример. 0.1-4, где 0 - это номер блока, 1 - с какой строки начинается таблица, 4 - на какой строке закончится чтение таблицы.
 - Адрес параметра - количество параметров (столбцов) будет в таблице. 
Пример. 0.0-5, где 0 - это номер блока, 0 - с какого параметра таблица начинается, 5 - на каком параметре заканчивается таблица.
Пример. 0.2-5, где 0 - это номер блока, 2 - означает, что при создании таблицы два первых параметра будут пропущены, 5 - на каком параметре заканчивается таблица.
Пропуск параметров удобно, когда таблица большая, а из большой таблицы необходимо добавить всего 2-3 столбца, чтобы не тратить ресурсы на лишнее заполнение столбцов значений, который потом всё равно будут удалены при фильтрации.

7. После создании тегов и настроек, при нажатии Проверить, содержимое текстового блока Содержание будет отображено в текстовом поле Результат.
Пример.
Содержание:
	Mikhail writes the world's best mnemonic editor.
	Mikhail writes the world's best mnemonic editor and RapidScada.
	Validate Int 0123 456 789 111 222 333 444 555 666 777.
	Validate IntDot 0123 456 789 111 222 333 444 555.
	ValaidateBool True False true false 
	TagDate 01.01.2020 2020-01-01 12-12-2020 01.01.2020 2020-01-01 12-12-2020
	TagFloat checking the meaning of a sentence with a 123.546.

Теги:
	TagString1		[0][0][6] String
	Michail 		[0][1][8] String
	TagInt			[0][2][7] Integer
	TagIntDot		[0][3][9] Integer
	TagBoolTrue		[0][4][2] Boolean	
	TagBoolFalse		[0][4][3] Boolean
	TagDate			[0][5][6] Datetime
	TagFloat		[0][6][9] Float

Результат:
	TagString1=editor
	Michail=RapidScada
	TagInt=333
	TagIntDot=555
	TagBoolTrue=False
	TagBoolFalse=True
	TagDate=2020-12-12 00:00:00
	TagFloat=123,546

8. После проверки тегов, проверяется правильность работы задачи парсинга. Необходимо в меню выбрать Запустить.
Результат работы будет в текстовом поле Результат.
Пример.
	[2025-03-09 16:35:47.41083] Парсинг файла C:\Debug\1\format.txt.
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

Где в первом пунке даты была указана начало даты парсинг и путь до файла, который парсер файл обрабатывал.
Во втором пункте было указано содержимое файла и какие значения теги получили на выходе.

Информация об обрабатываем файле также была записана в файл с названием задачи и расширением .db
C:\Debug\2\test.csv|25.02.2025 10:48:08|280|5|False|0
В первом значении указывается путь до файла.
В втором значении указывается дата изменения файла.
В третьем значении указывается размер файла.
В четвертом значении указывается количество строк, сколько было прочитано.
В пятом значении указывается признак был ли файл изменен.
В шестом значении указывается результат обработки файла, где
0 - файл не обработан
1 - файл обработан успешно
2 - файл не обработан из-за ошибки

9. После отладки парсера логирование можно отключить через Настройки - Записывать результат выполнения (отладка).

##Instructions

When working with files, the parser has several basic settings:
- Path is a setting that specifies the directory where the files that need to be parsed are located.
- Use subfolders? - this setting, which forces the parser to iterate through all subfolders in search of files for parsing.
- Read from the last line? - this setting, which will remember how many lines have been read in the file. If the setting is active, the fact that the file was previously processed is ignored.
- Filter - this setting allows you to filter files that are in the directory, but they do not need to be processed. For example, if there are other files in the directory besides text files, they will be ignored. In this setting, you need to specify the file format that the parser should work with, because in addition to the standard file.txt there are many more formats, for example, ini, csv, etc., which were invented by the developer.
- The template file name is a setting that helps when files have the same format and are in the same directory, but the file structure is different and the file name indicates by which they can be distinguished.
For example, data_hour_2025_01_01.txt and data_current_2025_01_01.txt
If you specify an hour in the Template file name, the parser will look for the presence of an hour in the file name and will process the file only if it exists. If there is only one file template in the directory, then only the file extension is specified.

Splitting the file
You need to understand that the driver needs three delimiters for parsing.:
- block
- line
- parameter

A block is a required parameter by which an application determines how to split data into the same structure.
The signs of the block from the developer may vary, but even if it does not exist, the separator block must be specified, for example, {BLOCK}

Example, blocks.
Table of contents 1
Text: 01
Text: 02
-------

Table of contents 2
Text: 01
Text: 02
-------

Where, the block separator could be both ------- and the Table of Contents , because these are both signs, according to which the same file structure followed.

A string is a parameter that splits text into lines. In 95% of cases, these are hyphenation characters \r and \n, but since these characters are not displayed to the user, the format is used to visually indicate that the separator is specified:
{LF} - \n
{CR} - \r

A parameter is a property where the separator is mainly a space or a tab character, and for csv files, the character , or ;. To visually indicate that the separator is specified, the format is used:
{SPACE} - (space)
{TAB} - \t (tab)

When separating blocks, rows, and parameters, sometimes empty variables remain. There are three possible actions with empty variables:
- do nothing
- delete
- crop

For convenient drafting for parsing, it is convenient to do the following procedure.
1. Copy the contents of the file and paste the Contents into the text field on the second Check tab.
2. Analyze the file for its contents and determine which delimiters will separate the blocks, lines, and parameters.
3. Make the necessary separators in the Settings and save the project.
4. Click the Map button and see how the application has divided the file contents.

Example.
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

Where the first value in square brackets is the block number, the second value in square brackets is the line number, and the third value in square brackets is the parameter number. 

5. If the option of the parameters on the map suits you, then go to the creation of tags, otherwise, go back to the creation of separators.
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

7. After creating tags and settings, when you click Check, the contents of the Content text block will be displayed in the Result text field.
Example.
Content:
Mikhail writes the world's best mnemonic editor.
Mikhail writes the world's best mnemonic editor and RapidScada.
Validate Int 0123 456 789 111 222 333 444 555 666 777.
Validate IntDot 0123 456 789 111 222 333 444 555.
ValaidateBool True False true false 
TagDate 01.01.2020 2020-01-01 12-12-2020 01.01.2020 2020-01-01 12-12-2020
TagFloat checking the meaning of a sentence with a 123.546.

Tags:
TagString1		[0][0][6] String
Michail 		[0][1][8] String
TagInt			[0][2][7] Integer
TagIntDot		[0][3][9] Integer
TagBoolTrue		[0][4][2] Boolean	
TagBoolFalse	[0][4][3] Boolean
TagDate			[0][5][6] Datetime
TagFloat		[0][6][9] Float

Result:
TagString1=editor
Michail=RapidScada
TagInt=333
TagIntDot=555
TagBoolTrue=False
TagBoolFalse=True
TagDate=2020-12-12 00:00:00
TagFloat=123,546

8. After checking the tags, the correctness of the parsing task is checked. Select Run from the menu.
The result of the work will be in the Result text field.
Example.
[2025-03-09 16:35:47.41083] File parsing C:\Debug\1\format.txt .
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

Where the first date point indicated the beginning of the parsing date and the path to the file that the file parser was processing.
The second paragraph indicated the contents of the file and what values the tags received at the output.

Information about the file being processed was also recorded in a file with the task name and extension.db
C:\Debug\2\test.csv|25.02.2025 10:48:08|280|5|False|0
The first value specifies the path to the file.
The second value indicates the date when the file was modified.
The third value indicates the file size.
The fourth value indicates the number of lines that have been read.
The fifth value indicates whether the file has been modified.
The sixth value indicates the result of file processing, where
0 means the file has not been processed
and 1 means the file has been processed successfully.
2 - the file was not processed due to an error.

9. After debugging the parser, logging can be disabled through the Settings - Record the result of execution (debugging).

![DrvParserTextJP](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v6/DrvParserTextJP_v6.4.0.0/total)

Video YouTube
https://youtu.be/t9LyHzDUzmo?si=giMWBqSsCHaqGY7n

Video Rutube 
https://rutube.ru/video/78239cf1a34e714c75cd883a314240bf/

Screenshots

![DrvParserTextJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/SharewareDrivers/Drivers/ParserTextJP/DrvParserTextJP_001.png) ![DrvParserTextJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/SharewareDrivers/Drivers/ParserTextJP/DrvParserTextJP_002.png)
![DrvParserTextJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/SharewareDrivers/Drivers/ParserTextJP/DrvParserTextJP_003.png) ![DrvParserTextJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/SharewareDrivers/Drivers/ParserTextJP/DrvParserTextJP_004.png)
![DrvParserTextJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/SharewareDrivers/Drivers/ParserTextJP/DrvParserTextJP_005.png) ![DrvParserTextJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/SharewareDrivers/Drivers/ParserTextJP/DrvParserTextJP_006.png) 
![DrvParserTextJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/SharewareDrivers/Drivers/ParserTextJP/DrvParserTextJP_007.png) ![DrvParserTextJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/SharewareDrivers/Drivers/ParserTextJP/DrvParserTextJP_008.png)
![DrvParserTextJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/SharewareDrivers/Drivers/ParserTextJP/DrvParserTextJP_009.png) 
