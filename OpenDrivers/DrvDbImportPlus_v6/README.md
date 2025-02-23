	Драйвера для Rapid SCADA.
	Drivers  for Rapid SCADA.


	
### DrvDbImportPlus
![DrvDbImportPlus](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v6/DrvDbImportPlus_v6.3.0.2/total)
![DrvDbImportPlus](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v6/DrvDbImportPlus_v6.3.0.1/total)
![DrvDbImportPlus](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v6/DrvDbImportPlus_v6.3.0.0/total)
![DrvDbImportPlus](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v6/DrvDbImportPlus_v6.0.0.6/total)
![DrvDbImportPlus](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v6/DrvDbImportPlus_v6.0.0.5/total)
![DrvDbImportPlus](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v6/DrvDbImportPlus_v6.0.0.4/total)
![DrvDbImportPlus](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v6/DrvDbImportPlus_v6.0.0.3/total)
![DrvDbImportPlus](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v6/DrvDbImportPlus_v6.0.0.2/total)
![DrvDbImportPlus](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v6/DrvDbImportPlus_v6.0.0.1/total)
![DrvDbImportPlus](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v6/DrvDbImportPlus_v6.0.0.0/total)

Библиотека DrvDbImportPlus (6.3.0.2)
- Изменён интерфейс. Добавлены иконки.
- Строковые теги теперь отображаются единым значением.

Библиотека DrvDbImportPlus (6.3.0.1)
- Добавлена возможность отправлять команды со строковыми значениями.
- Исправлена ошибка в отображении команд на форме при сохранении конфигурации.
- Переход на библиотеку Microsoft.Data.SqlClient. Библиотека Sql.Data.SqlClient больше не поддерживается.
- Компиляция библиотеки сделана под несколько платформ, чтобы не было проблемы 'Ваша платформа не поддерживается'.
- Обновлена Справка.

Библиотека DrvDbImportPlus (6.3.0.0)
- Переход на библиотеки RapidScada 6.3.0.0 и переход на Net Core 8.0.
- Добавлена возможность у строковых данных указывать длину, по которой будут автоматически генерировать теги.
- Обновление библиотек на более свежие версии Net Core 8.0.
- Подправление интерфейс под ноутбуки и некоторые недочеты.
- Удаление источников данных ODBC и OLEDB по причине неактуальности и невозможности использования.

Библиотека DrvDbImportPlus (6.0.0.6)
- Добавлен функционал по отображению и округлению значения на количество знаков после запятой.

Библиотека DrvDbImportPlus (6.0.0.5)
- Исправлен режим подключения к MySQL.
- Исправлены мелкие недочеты в коде и незначительные ошибки.
                    
Библиотека DrvDbImportPlus (6.0.0.4)
- Добавлен функционал по добавлению каналов через Мастер.
- Исправлены ошибки в файле перевода.
	
Библиотека DrvDbImportPlus (6.0.0.3)
- Убран режим сохранения историчесих данных. Данный режим показал опасную возможность нагрузить сервер большим количеством срезов, которые могу привести к отказу сервера.
- Изменён способ отображения, добавления, удаления и редактирования тегов.

Библиотека DrvDbImportPlus (6.0.0.2)
- У элемента Количество тегов изменено максимальное значение с 100 до 65535.

Библиотека DrvDbImportPlus (6.0.0.1)
- В строке подключения теперь не отображается информация о настройках подключения, если это не драйвер OLEDB или ODBC.
- Включен режим блокирования полей в зависимости от типа использования подключения.
- Добавлено контекстное меню в редакторы SQL-запросов, чтобы выполнять простейшие действия (копирование, вставка, вырезать) и возможность отменить изменения.
- Добавлена возможность добавлять заранее статические теги в настройки, чтобы в случае отсутствия данных или записей не сдвигались номера тегов, которые приводили к коллизии данные из-за смещения.
- Добавлена проверка значения тега, если он отсутствует в полученной таблице, то значение и статус сменяется на 0.

Библиотека DrvDbImportPlus (6.0.0.0)
- В конфигурационном файле теперь шифриуется не только пароль, но и строка подключения.
- Добавлен сбор данных через драйвера ODBC.
- Добавлен сбор данных из СУБД Firebird.
- Проверка подключения из формы драйвера.
- Проверка SQL-запроса из формы драйвера.
- Подсветка синтаксиса SQL.
- Реализован новый режим сбора данных, когда в первом столбце - название тега, во втором - значение тега, в третьем - время значения. (Работает историческая запись данных через срез.)

---------------------------------------------------------------------------

DrvDbImportPlus library (6.3.0.2)
- Interface changed. Icons have been added.
- String tags are now displayed as a single value.

DrvDbImportPlus library (6.3.0.1)
- Added the ability to send commands with string values.
- Fixed an error in displaying commands on the form when saving the configuration.
- Switching to the Microsoft.Data.SqlClient library. The Sql.Data.SqlClient library is no longer supported.
- The library has been compiled for several platforms so that there is no problem of 'Your platform is not supported'.
- The Help has been updated.

DrvDbImportPlus Library (6.3.0.0)
- Switching to the Rapid Scada 6.3.0.0 libraries and switching to Net Core 8.0.
- Added the ability for string data to specify the length by which tags will be automatically generated.
- Updating libraries to more recent versions of Net Core 8.0.
- Congratulations on the interface for laptops and some bugs.
- Removal of ODBC and OLEDB data sources due to their irrelevance and inability to use.

DrvDbImportPlus library (6.0.0.6)
- Added functionality for displaying and rounding values by the number of decimal places.

DrvDbImportPlus library (6.0.0.5)
- Fixed MySQL connection mode.
- Fixed minor bugs in the code and minor errors.
                    
DrvDbImportPlus library (6.0.0.4)
- Added functionality for adding channels through the Wizard.
- Fixed errors in the translation file.
	
DrvDbImportPlus library (6.0.0.3)
- The mode of saving historical data has been removed. This mode showed a dangerous opportunity to load the server with a large number of slices, which can lead to server failure.
- Changed the way tags are displayed, added, deleted, and edited.

DrvDbImportPlus library (6.0.0.2)
- The maximum value of the Number of tags element has been changed from 100 to 65535.

DrvDbImportPlus library (6.0.0.1)
- The connection string now does not display information about connection settings, unless it is an OLEDB or ODBC driver.
- The field blocking mode is enabled depending on the type of connection usage.
- Added a context menu to SQL query editors to perform the simplest actions (copy, paste, cut) and the ability to undo changes.
- Added the ability to add static tags in advance to the settings so that if there is no data or records, the tag numbers do not shift, which led to a data collision due to the offset.
- Added a check of the tag value, if it is not in the resulting table, then the value and status is changed to 0.

DrvDbImportPlus library (6.0.0.0)
- The configuration file now encrypts not only the password, but also the connection string.
- Added data collection via ODBC drivers.
- Added data collection from the Firebird DBMS.
- Checking the connection from the driver form.
- Checking the SQL query from the driver form.
- SQL syntax highlighting.
- A new data collection mode has been implemented, when the first column contains the tag name, the second column contains the tag value, and the third column contains the time of the value. (Historical data recording via slice works.)

Video on YouTube 
https://www.youtube.com/watch?v=cZMksoblUgo
https://www.youtube.com/watch?v=TFZfBMuRMVU

[![Video on YouTube](https://img.youtube.com/vi/cZMksoblUgo/0.jpg)](https://www.youtube.com/watch?v=cZMksoblUgo)
[![Video on YouTube](https://img.youtube.com/vi/TFZfBMuRMVU/0.jpg)](https://www.youtube.com/watch?v=TFZfBMuRMVU)
Screenshots

![DrvDbImportPlus](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/OpenDrivers/Source/DrvDbImportPlus_001.png) ![DrvDbImportPlus](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/OpenDrivers/Source/DrvDbImportPlus_002.png)
![DrvDbImportPlus](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/OpenDrivers/Source/DrvDbImportPlus_003.png) ![DrvDbImportPlus](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/OpenDrivers/Source/DrvDbImportPlus_004.png)
![DrvDbImportPlus](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/OpenDrivers/Source/DrvDbImportPlus_005.png) ![DrvDbImportPlus](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/OpenDrivers/Source/DrvDbImportPlus_006.png)
![DrvDbImportPlus](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/OpenDrivers/Source/DrvDbImportPlus_007.png)


## SAST Tools

[PVS-Studio](https://pvs-studio.ru/ru/pvs-studio/?utm_source=website&utm_medium=github&utm_campaign=open_source) - static analyzer for C, C++, C#, and Java code.
