// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.ComponentModel;
using Scada.Forms;
using Scada.Lang;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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