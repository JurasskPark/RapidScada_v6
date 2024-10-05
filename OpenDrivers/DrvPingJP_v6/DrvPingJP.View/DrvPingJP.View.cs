using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Forms;
using Scada.Lang;

namespace Scada.Comm.Drivers.DrvPingJP.View
{
    /// <summary>
    /// Implements the driver user interface.
    /// <para>Реализует пользовательский интерфейс драйвера.</para>
    /// </summary>
    public class DrvPingJPView : DriverView
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DrvPingJPView()
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
                    "Ping JP" :
                    "Ping JP";
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
                    "Автор: Юрий Прадиус\n" +
                    "Проверка доступности сетевых устройств.\n\n" +
                    "Версия 6.3.0.0 (05.10.2024)\n" +
                    "[v] Переход с Net Core 6.0 на 8.0. \n" +
                    "Версия 6.1.0.2 (15.03.2024)\n" +
                    "[v] Изменены иконки на форме. \n" +
                    "Версия 6.1.0.1 (16.01.2024)\n" +
                    "[v] Исправлена ошибка записи информации в пустой список. \n" +
                    "Версия 6.1.0.0 (26.12.2023)\n" +
                    "[v] Исправлены пересмотрен подход в коде проекта.\n" +
					"[v] Исправлены мелкие ошибки и недочеты.\n" +
                    "[+] Добавлен асинхронный режим пинга.\n" +
                    "Версия 6.0.0.2 (21.02.2023)\n" +
                    "[v] Исправлены ошибка в логе, когда устройство не отвечает - информация об этом не записывалась.\n" +
                    "[+] Добавлен функционал по добавленю каналов через Мастер.\n" +
                    "Версия 6.0.0.1 (21.02.2023)\n" +
                    "[v] Библиотека переименована из DrvPing в DrvPingJP из-за конфликта имён.\n" +
                    "[v] Исправлены ошибки в файле перевода.\n" +
                    "[v] Исправлено отображение значения тега из Float в Вкл\\Выкл.\n" +
                    "[+] Режим включения\\отключения логов.\n" +
                    "[+] Информация о результате опроса.\n" +  
                    "[-] Убраны лишние ссылки на библиотеки Windows и теперь библиотека работает на Linux.\n" +
                    "Версия 6.0.0.0 (17.02.2023)\n" +
                    "[v] Релиз драйвера.\n"
                    :
                    "Author: Yuri Pradius\n" +
                    "Checking the availability of network devices.\n\n" +
                    "Version 6.3.0.0 (09/10/2024)\n" +
                    "[v] Switching from Net Core 6.0 to 8.0. \n" +
                    "Version 6.1.0.2 (03/15/2024)\n" +
                    "[v] Changed icons on the form.\n" +
                    "Version 6.1.0.1 (01/16/2023)\n" +
                    "[v] Fixed a bug in writing information to an empty list.\n" +
                    "Version 6.1.0.0 (12/26/2023)\n" +
                    "[v] Fixed revised approach in the project code.\n" +
					"[v] Minor bugs and shortcomings have been fixed.\n" +
                    "[+] Added asynchronous ping mode.\n" +
                    "Version 6.0.0.2 (02/21/2023)\n" +
                    "[v] Fixed an error in the log when the device does not respond - information about this was not recorded.\n" +
                    "[+] Added functionality for adding channels through the Wizard.\n" +
                    "Version 6.0.0.1 (02/21/2023)\n" +
                    "[v] The library was renamed from DrvPing to DrvPingJP due to a name conflict.\n" +
                    "[v] Fixed errors in the translation file.\n" +
                    "[v] Fixed displaying the tag value from Float to On\\Off.\n" +
                    "[+] The mode of enabling\\disabling logs.\n" +
                    "[+] Information about the survey result.\n" +
                    "[-] Removed unnecessary links to Windows libraries and now the library runs on Linux.\n" +
                    "Version 6.0.0.0 (02/17/2023)\n" +
                    "[v] Driver release.\n";
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
            return new DevPingJPView(this, lineConfig, deviceConfig);
        }
    }
}