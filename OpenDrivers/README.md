	Драйвера для Rapid SCADA.
	Drivers  for Rapid SCADA.
1. Библиотека DrvDbImport (6.0.0.0) была доработана и добавлен новый функционал:
- В конфигурационном файле теперь шифриуется не только пароль, но и строка подключения.
- Добавлен сбор данных через драйвера ODBC.
- Добавлен сбор данных из СУБД Firebird.
- Проверка подключения из формы драйвера.
- Проверка SQL-запроса из формы драйвера.
- Подсветка синтаксиса SQL.
- Реализован новый режим сбора данных, когда в первом столбце - название тега, во втором - значение тега, в третьем - время значения. (Работает историческая запись данных через срез.)

Библиотека DrvDbImport (6.0.0.1) была исправлена и доработана:
- В строке подключения теперь не отображается информация о настройках подключения, если это не драйвер OLEDB или ODBC.
- Включен режим блокирования полей в зависимости от типа использования подключения.
- Добавлено контекстное меню в редакторы SQL-запросов, чтобы выполнять простейшие действия (копирование, вставка, вырезать) и возможность отменить изменения.
- Добавлена возможность добавлять заранее статические теги в настройки, чтобы в случае отсутствия данных или записей не сдвигались номера тегов, которые приводили к коллизии данные из-за смещения.
- Добавлена проверка значения тега, если он отсутствует в полученной таблице, то значение и статус сменяется на 0.

The KpDbImport library has been improved and new functionality has been added:
- The configuration file now encrypts not only the password, but also the connection string.
- Added data collection via ODBC drivers.
- Added data collection from the Firebird DBMS.
- Checking the connection from the driver form.
- Checking the SQL query from the driver form.
- SQL syntax highlighting.
- A new data collection mode has been implemented, when the first column contains the tag name, the second column contains the tag value, and the third column contains the time of the value. (Historical data recording via slice works.)

The DrvDbImport library (6.0.0.1) has been fixed and improved:
- The connection string now does not display information about the connection settings, unless it is an OLEDB or ODBC driver.
- The field blocking mode is enabled depending on the type of connection usage.
- Added a context menu to SQL query editors to perform the simplest actions (copy, paste, cut) and the ability to undo changes.
- Added the ability to add static tags in advance to the settings so that if there is no data or records, the tag numbers do not shift, which led to a data collision due to the offset.
- Added a check of the tag value, if it is not in the resulting table, then the value and status is changed to 0.

Video on YouTube 
https://www.youtube.com/watch?v=YysMbwIxhwE

[![Video on YouTube](https://img.youtube.com/vi/YysMbwIxhwE/0.jpg)](https://www.youtube.com/watch?v=YysMbwIxhwE)


Screenshots

![DrvDbImportPlus](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/OpenDrivers/Source/DrvDbImportPlus_001.png) ![DrvDbImportPlus](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/OpenDrivers/Source/DrvDbImportPlus_002.png)
![DrvDbImportPlus](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/OpenDrivers/Source/DrvDbImportPlus_003.png) ![DrvDbImportPlus](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/OpenDrivers/Source/DrvDbImportPlus_004.png)
![DrvDbImportPlus](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/OpenDrivers/Source/DrvDbImportPlus_005.png) ![DrvDbImportPlus](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/OpenDrivers/Source/DrvDbImportPlus_006.png)