// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Lang;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    /// <summary>
    /// Supported data source types.
    /// <para>Поддерживаемые типы источников данных.</para>
    /// </summary>
    internal enum DataSourceType
    {
        Undefined,
        MSSQL,
        Oracle,
        PostgreSQL,
        MySQL,
        OLEDB,
        ODBC,
        Firebird
    }
}