// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Devices;
using Scada.Comm.Lang;
using Scada.Config;
using Scada.Data.Const;
using Scada.Lang;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Security.Principal;
using Scada.Data.Models;
using DrvDbImportPlus.Common.Configuration;
using Scada.Data.Entities;

namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    /// <summary>
    /// Creates channel prototypes for a device.
    /// <para>Создает прототипы каналов устройства.</para>
    /// </summary>
    internal static class CnlPrototypeFactory
    {
        /// <summary>
        /// The length of the array tag.
        /// </summary>
        private static int ArrayLength = 0;

        /// <summary>
        /// The data source
        /// </summary>
        public static DataSource dataSource;

        private static DataTable dtData = new DataTable("Data");
        private static DataTable dtSchema = new DataTable("Schema");

        /// <summary>
        /// Gets the grouped channel prototypes.
        /// </summary>
        public static List<CnlPrototypeGroup> GetCnlPrototypeGroups(DrvDbImportPlusConfig config)
        {
            List<CnlPrototypeGroup> groups = new List<CnlPrototypeGroup>();
            string nameTagGroup = Locale.IsRussian ? "Теги" : "Tags";
            CnlPrototypeGroup group = new CnlPrototypeGroup(nameTagGroup);

            if (config.DeviceTagsBasedRequestedTableColumns)
            {
                ArrayLength = config.AutoTagCount ? config.CalcTagCount() : config.TagCount;
            }
            else
            {
                ArrayLength = config.TagCount;
            }

            dataSource = DataSource.GetDataSourceType(config);

            if (dataSource != null)
            {
                string connStr = string.IsNullOrEmpty(config.DbConnSettings.ConnectionString) ?
                    dataSource.BuildConnectionString(config.DbConnSettings) :
                    config.DbConnSettings.ConnectionString;

                if (string.IsNullOrEmpty(connStr))
                {
                    dataSource = null;
                }
                else
                {
                    dataSource.Init(connStr, config);

                    try
                    {
                        dataSource.Connect();
                        DataTable dt = new DataTable();

                        //Tag based Columns
                        if (config.DeviceTagsBasedRequestedTableColumns == true)
                        {
                            using (DbDataReader reader = dataSource.SelectCommand.ExecuteReader(CommandBehavior.SingleRow))
                            {
                                if (reader.HasRows == true)
                                {
                                    int tagCnt = ArrayLength;
                                    int fieldCnt = reader.FieldCount;

                                    for (int i = 0, cnt = Math.Min(tagCnt, fieldCnt); i < cnt; i++)
                                    {
                                        group.AddCnlPrototype("DBTAG" + (i + 1).ToString() + "", reader.GetName(i)).SetFormat(FormatCode.String);
                                    }

                                    groups.Add(group);

                                    dataSource.Disconnect();
                                    return groups;
                                }
                            }
                        }
                        else //Tag base Columns
                        {
                            dtData = new DataTable("Data");

                            using (DbDataReader reader = dataSource.SelectCommand.ExecuteReader(CommandBehavior.Default))
                            {
                                dtSchema = reader.GetSchemaTable();
                                DataColumnCollection columns = dtData.Columns;

                                for (int cntRow = 0; cntRow < dtSchema.Rows.Count; cntRow++)
                                {
                                    DataRow schemarow = dtSchema.Rows[cntRow];

                                    string columnName = string.Empty;
                                    object columnSize = new object();
                                    Type dataType;
                                    string dataTypeName = string.Empty;

                                    try
                                    {
                                        columnName = reader.GetName(cntRow);
                                    }
                                    catch
                                    {
                                        columnName = schemarow["ColumnName"].ToString();
                                    }

                                    try
                                    {
                                        columnSize = schemarow["ColumnSize"];
                                    }
                                    catch { }

                                    try
                                    {
                                        dataType = reader.GetFieldType(cntRow);
                                    }
                                    catch
                                    {
                                        dataTypeName = schemarow["DataTypeName"].ToString();
                                        dataType = ElementType.GetElementType(dataTypeName);
                                    }

                                    if (!columns.Contains(columnName.ToString()))
                                    {
                                        dtData.Columns.Add(columnName.ToString(), (Type)dataType);
                                    }
                                }

                                while (reader.Read())
                                {
                                    object[] ColArray = new object[reader.FieldCount];
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        if (reader[i] != null)
                                        {
                                            ColArray[i] = reader[i];
                                        }
                                    }
                                    dtData.LoadDataRow(ColArray, true);
                                }
                            }

                            int tagCnt = ArrayLength;
                            int rowCnt = dtData.Rows.Count;

                            try
                            {

                                for (int i = 0; i < Math.Min(tagCnt, rowCnt); i++)
                                {
                                    group.AddCnlPrototype("DBTAG" + (i + 1).ToString() + "", dtData.Rows[i][0].ToString());
                                }
                                groups.Add(group);

                                dataSource.Disconnect();
                                return groups;
                            }
                            catch { }
                        }
                    }
                    catch { }
                }
            }
        
            groups.Add(group);
            return groups;      
        }
    }
}


