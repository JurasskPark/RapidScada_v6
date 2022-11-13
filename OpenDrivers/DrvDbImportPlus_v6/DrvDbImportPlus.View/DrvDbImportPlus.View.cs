// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.ComponentModel;
using Scada.Forms;
using Scada.Lang;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
                    "Version 6.0.0.2 (13.11.2022)\n" +
                    "Fixed:\n" +
                    "[v] The maximum value of the Tags Count element has been changed from 100 to 65535.\n" +
                    "Version 6.0.0.1 (05.11.2022)\n" +
                    "Fixed:\n" +
                    "[v] The connection string now does not display information about the connection settings, unless it is an OLEDB or ODBC driver.\n" +
                    "[v] Field blocking mode is enabled depending on the type of connection usage.\n" +
                    "Added:\n" +
                    "[+] Context menu to SQL Query editors.\n" +
                    "[+] Added the ability to add static tags in advance to the settings so that in the absence of data, the tag numbers do not shift.\n" +
                    "Version 5.1.1.1 (05.09.2022)\n" +
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