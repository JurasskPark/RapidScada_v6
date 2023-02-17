// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.ComponentModel;
using Scada.Forms;
using Scada.Lang;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Scada.Comm.Drivers.DrvPing.View
{
    /// <summary>
    /// Implements the driver user interface.
    /// <para>Реализует пользовательский интерфейс драйвера.</para>
    /// </summary>
    public class DrvPingView : DriverView
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DrvPingView()
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
                    "Ping" :
                    "Ping";
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
                    "Версия 6.0.0.0 (17.02.2023)\n" +
                    "[v] Релиз драйвера.\n"
                    :
                    "Author: Yuri Pradius\n" +
                    "Checking the availability of network devices.\n\n" +
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
            return new DevPingView(this, lineConfig, deviceConfig);
        }
    }
}