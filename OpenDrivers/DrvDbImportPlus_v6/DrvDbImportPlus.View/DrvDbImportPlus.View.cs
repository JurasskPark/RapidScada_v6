// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Forms;
using Scada.Lang;
using System.Windows.Forms;

namespace Scada.Comm.Drivers.DrvDbImportPlus.View
{
    /// <summary>
    /// Implements the driver user interface.
    /// <para>Реализует пользовательский интерфейс драйвера.</para>
    /// </summary>
    public class DrvDbImportPlusView : DriverView
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DrvDbImportPlusView()
        {
            CanCreateDevice = true;
        }


        /// <summary>
        /// Gets the driver name.
        /// </summary>
        public override string Name
        {
            get
            {
                return Locale.IsRussian ?
                    "Импорт из БД Плюс" :
                    "DB Import Plus";
            }
        }

        /// <summary>
        /// Gets the driver description.
        /// </summary>
        public override string Descr
        {
            get
            {
                return Locale.IsRussian ?
                    "Автор:  Михаил Ширяев\n" +
                    "Доработка: Юрий Прадиус\n" +
                    "Импорт из сторонней базы данных.\n\n" +

                    "Версия 6.3.0.2 (12.01.2025)\n" +
                    "[v] Изменён интерфейс. Добавлены иконки.\n" +
                    "[v] Строковые теги теперь отображаются единым значением.\n" +
                    "[v] Исправлена ошибка в отображении команд на форме при сохранении конфигурации.\n" +
                    "Версия 6.3.0.1 (23.11.2024)\n" +
                    "[+] Добавлена возможность отправлять команды со строковыми значениями.\n" +
                    "[v] Исправлена ошибка в отображении команд на форме при сохранении конфигурации.\n" +
                    "[v] Переход на библиотеку Microsoft.Data.SqlClient. Библиотека Sql.Data.SqlClient больше не поддерживается.\n" +
                    "[v] Компиляция библиотеки сделана под несколько платформ, чтобы не было проблемы 'Ваша платформа не поддерживается'.\n" +
                    "[v] Обновлена Справка.\n" +
                    "Версия 6.3.0.0 (29.09.2024)\n" +
                    "[+] Переход на библиотеки RapidScada 6.3.0.0 и переход на Net Core 8.0.\n" +
                    "[+] Добавлена возможность у строковых данных указывать длину, по которой будут автоматически генерироваться теги.\n" +
                    "[v] Обновление библиотек на более свежие версии Net Core 8.0.\n" +
                    "[v] Подправление интерфейс под ноутбуки и некоторые недочеты.\n" +
                    "[-] Удаление источников данных ODBC и OLEDB по причине неактуальности и невозможности использования.\n" +
                    "Версия 6.0.0.6 (14.12.2023)\n" +
                    "[+] Добавлен функционал по отображению и округлению значения на количество знаков после запятой.\n" +
                    "Версия 6.0.0.5 (21.09.2023)\n" +
                    "[v] Исправлен режим подключения к MySQL.\n" +
                    "[v] Исправлены мелкие недочеты в коде и незначительные ошибки.\n" +
                    "Версия 6.0.0.4 (23.02.2023)\n" +
                    "[+] Добавлен функционал по добавлению каналов через Мастер.\n" +
                    "[v] Исправлены ошибки в файле перевода.\n" +
                    "Версия 6.0.0.3 (10.02.2023)\n" +
                    "Удалено:\n" +
                    "[-] Режим передачи исторических данных. Данный режим показал опасную возможность нагрузить сервер большим количеством срезов, которые могу привести к отказу сервера.\n" +
                    "Исправлено:\n" +
                    "[v] Изменён способ отображения, добавления, удаления и редактирования тегов.\n" +
                    "Версия 6.0.0.2 (13.11.2022)\n" +
                    "Исправлено:\n" +
                    "[v] У элемента Количество тегов изменено максимальное значение с 100 до 65535.\n" +
                    "Версия 6.0.0.1 (05.11.2022)\n" +
                    "Исправлено:\n" +
                    "[v] В строке подключения теперь не отображается информация о настройках подключения, если это не драйвер OLEDB или ODBC.\n" +
                    "[v] Включен режим блокирования полей в зависимости от типа использования подключения.\n" +
                    "Добавлено:\n" +
                    "[+] Контекстное меню в редакторы SQL-запросов.\n" +
                    "[+] Добавлена возможность добавлять заранее статические теги в настройки, чтобы в случае отсутствия данных не сдвигались номера тегов.\n" +
                    "Версия 6.0.0.0 (05.09.2022)\n" +
                    "Добавлено:\n" +
                    "[+] Подключение через ODBC.\n" +
                    "[+] Подключение к базе данных Firebird.\n" +
                    "[+] Проверка подключения к базе данных.\n" +
                    "[+] Проверка SQL-запроса.\n" +
                    "[+] Второй способ получения данных, когда тег является значением в строке.\n" +
                    "[v] Строка подключения в конфигурационном файле теперь шифруется.\n"
                    :
                    "The author: Mikhail Shiryaev\n" +
                    "Revision: Yuriy Pradius\n" +
                    "Import from a third-party database.\n\n" +

                    "Version 6.3.0.2 (01/12/2025)\n" +
                    "[v] Changed the interface. Added icons.\n" +
                    "[v] String tags are now displayed with a single value.\n" +
                    "Version 6.3.0.1 (11/23/2024)\n" +
                    "[+] Added ability to send commands with string values.\n" +
                    "[v] Fixed an error in the display of commands on the form when saving the configuration.\n" +
                    "[v] Switching to the Microsoft.Data.SqlClient library. The Sql.Data.SqlClient library is no longer supported.\n" +
                    "[v] The compilation of the library is made for several platforms so that there is no problem 'Your platform is not supported'.\n" +
                    "[v] Updated Help.\n" +
                    "Version 6.3.0.0 (09/29/2024)\n" +
                    "[+] Transition to RapidScada 6.3.0.0 libraries and transition to Net Core 8.0.\n" +
                    "[+] Added the ability to specify the length of string data that will automatically generate tags.\n" +
                    "[v] Updating the libraries to newer versions of Net Core 8.0.\n" +
                    "[v] Improvements to the interface for laptops and some shortcomings.\n" +
                    "[-] Removal of ODBC and OLEDB data sources due to irrelevance and unusability.\n" +
                    "Version 6.0.0.6 (12/14/2023)\n" +
                    "[+] Added functionality for displaying and rounding values by the number of decimal places.\n" +
                    "Version 6.0.0.5 (09/21/2023)\n" +
                    "[v] Fixed MySQL connection mode.\n" +
                    "[v] Minor bugs in the code and minor errors have been fixed.\n" +
                    "Version 6.0.0.4 (02/23/2023)\n" +
                    "[+] Added functionality for adding channels through the Wizard.\n" +
                    "[v] Fixed errors in the translation file.\n" +
                    "Version 6.0.0.3 (02/10/2023)\n" +
                    "Removed:\n" +
                    "[-] Historical data transmission mode. This mode has shown a dangerous opportunity to load the server with a large number of slices, which can lead to server failure.\n" +
                    "Fixed:\n" +
                    "[v] Changed the way tags are displayed, added, deleted, and edited.\n" +
                    "Version 6.0.0.2 (11/13/2022)\n" +
                    "Fixed:\n" +
                    "[v] The maximum value of the Tags Count element has been changed from 100 to 65535.\n" +
                    "Version 6.0.0.1 (11/05/2022)\n" +
                    "Fixed:\n" +
                    "[v] The connection string now does not display information about the connection settings, unless it is an OLEDB or ODBC driver.\n" +
                    "[v] Field blocking mode is enabled depending on the type of connection usage.\n" +
                    "Added:\n" +
                    "[+] Context menu to SQL Query editors.\n" +
                    "[+] Added the ability to add static tags in advance to the settings so that in the absence of data, the tag numbers do not shift.\n" +
                    "Version 5.1.1.1 (09/05/2022)\n" +
                    "Added:\n" +
                    "[+] Connection via ODBC.\n" +
                    "[+] Connecting to the Firebird database.\n" +
                    "[+] Checking the connection to the database.\n" +
                    "[+] Checking the SQL query.\n" +
                    "[+] The second way to get data is when the tag is a value in a string.\n" +
                    "[v] The connection string in the configuration file is now encrypted.\n";
            }
        }


        /// <summary>
        /// Loads language dictionaries.
        /// </summary>
        public override void LoadDictionaries()
        {
            if (!Locale.LoadDictionaries(AppDirs.LangDir, DriverUtils.DriverCode, out string errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
            }
                
            DriverPhrases.Init();
        }

        /// <summary>
        /// Creates a new device user interface.
        /// </summary>
        public override DeviceView CreateDeviceView(LineConfig lineConfig, DeviceConfig deviceConfig)
        {
            return new DevDbImportPlusView(this, lineConfig, deviceConfig);
        }
    }
}