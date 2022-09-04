// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.ComponentModel;
using Scada.Forms;
using Scada.Lang;

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
                    "Версия 6.0.0.0 (04.09.2022)\n"
                    :
                    "The author: Mikhail Shiryaev\n" +
                    "Revision: Yuriy Pradius\n" +
                    "Import from a third-party database.\n\n" +
                    "Version 6.0.0.0 (04.09.2022)\n";
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