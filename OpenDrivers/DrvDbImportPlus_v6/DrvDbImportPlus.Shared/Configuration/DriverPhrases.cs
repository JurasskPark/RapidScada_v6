// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Lang;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    public static class DriverPhrases
    {
        public static string ConfigDirRequired { get; private set; }

        public static void Init()
        {
            LocaleDict dictionary = Locale.GetDictionary("Scada.Comm.Drivers.DrvDbImportPlus.View.Forms.FrmConfig");
            ConfigDirRequired = dictionary["KpDbImportPlusDictionaries"];
        }
    }
}
