// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Drivers.DrvDbDataTransferJP;
using Scada.Lang;
using System.Xml;

namespace Scada.Comm.Drivers.DrvDbDataTransferJP.Converter
{
    /// <summary>
    /// Converts old DrvDbDataTransferJP configuration files to the current format.
    /// <para>Конвертирует старые файлы конфигурации DrvDbDataTransferJP в текущий формат.</para>
    /// </summary>
    internal sealed class ProjectConverter
    {
        /// <summary>
        /// Converts the specified files or directories.
        /// </summary>
        public ConversionSummary Convert(ConverterOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (options.SourcePaths.Count == 0)
            {
                throw new ArgumentException("Source path is not specified.", nameof(options));
            }

            string outputPath = string.IsNullOrWhiteSpace(options.OutputPath)
                ? GetDefaultOutputPath(options.SourcePaths)
                : Path.GetFullPath(options.OutputPath);

            Directory.CreateDirectory(outputPath);

            ConversionSummary summary = new ConversionSummary();
            foreach (string sourcePath in options.SourcePaths)
            {
                ConvertSourcePath(Path.GetFullPath(sourcePath), outputPath, options, summary);
            }

            return summary;
        }

        private static void ConvertSourcePath(
            string sourcePath,
            string outputPath,
            ConverterOptions options,
            ConversionSummary summary)
        {
            if (Directory.Exists(sourcePath))
            {
                foreach (string fileName in Directory.GetFiles(sourcePath, ConversionConstants.FileSearchPattern))
                {
                    ConvertFileSafe(fileName, outputPath, options, summary);
                }
            }
            else if (File.Exists(sourcePath))
            {
                ConvertFileSafe(sourcePath, outputPath, options, summary);
            }
            else
            {
                summary.FailedCount++;
                summary.Errors.Add($"{sourcePath}: Source path does not exist.");
            }
        }

        private static void ConvertFileSafe(
            string sourceFileName,
            string outputPath,
            ConverterOptions options,
            ConversionSummary summary)
        {
            summary.TotalCount++;

            try
            {
                ConvertFile(sourceFileName, outputPath, options);
                summary.ConvertedCount++;
            }
            catch (Exception ex)
            {
                summary.FailedCount++;
                summary.Errors.Add($"{Path.GetFileName(sourceFileName)}: {ex.Message}");
            }
        }

        private static void ConvertFile(string sourceFileName, string outputPath, ConverterOptions options)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(sourceFileName);

            XmlElement rootElem = xmlDoc.DocumentElement ??
                throw new InvalidOperationException("Root XML element is missing.");

            if (rootElem.Name == ConversionConstants.NewRootName)
            {
                SaveExistingNewConfig(xmlDoc, sourceFileName, outputPath, options);
                return;
            }

            if (rootElem.Name != ConversionConstants.OldRootName)
            {
                throw new InvalidOperationException($"Unsupported root element \"{rootElem.Name}\".");
            }

            DrvDbDataTransferJPProject project = ConvertProject(rootElem);
            string targetFileName = Path.Combine(outputPath, Path.GetFileName(sourceFileName));

            if (File.Exists(targetFileName) && !options.Overwrite && !IsOldConfig(targetFileName))
            {
                throw new IOException($"Target file already exists and is not an old config: {targetFileName}");
            }

            if (!project.Save(targetFileName, out string errMsg))
            {
                throw new InvalidOperationException(errMsg);
            }

            DrvDbDataTransferJPProject savedProject = new DrvDbDataTransferJPProject();
            if (!savedProject.Load(targetFileName, out errMsg))
            {
                throw new InvalidOperationException(errMsg);
            }
        }

        private static void SaveExistingNewConfig(
            XmlDocument xmlDoc,
            string sourceFileName,
            string outputPath,
            ConverterOptions options)
        {
            if (!options.Overwrite)
            {
                return;
            }

            string targetFileName = Path.Combine(outputPath, Path.GetFileName(sourceFileName));
            xmlDoc.Save(targetFileName);
        }

        private static DrvDbDataTransferJPProject ConvertProject(XmlElement rootElem)
        {
            DrvDbDataTransferJPProject project = new DrvDbDataTransferJPProject();
            LoadDbConnSettings(rootElem, project.SourceDbConnSettings);
            LoadDbConnSettings(rootElem, project.TargetDbConnSettings);

            ImportCmd importCmd = new ImportCmd
            {
                Enabled = true,
                CmdNum = 1,
                CmdCode = "DBIMPORT001",
                Name = "Import",
                Description = "Converted from old DrvDbDataTransferJP configuration.",
                SelectQuery = XmlUtils.GetChildText(rootElem, "SelectQuery"),
                IsColumnBased = XmlUtils.GetChildBool(rootElem, "DeviceTagsBasedRequestedTableColumns", true)
            };

            XmlNode deviceTagsNode = rootElem.SelectSingleNode("DeviceTags");
            if (deviceTagsNode != null)
            {
                foreach (XmlNode tagNode in deviceTagsNode.SelectNodes("Tag"))
                {
                    DriverTag tag = new DriverTag();
                    tag.LoadFromXml(tagNode);
                    importCmd.DeviceTags.Add(tag);
                }
            }

            if (!string.IsNullOrWhiteSpace(importCmd.SelectQuery) || importCmd.DeviceTags.Count > 0)
            {
                project.ImportCmds.Add(importCmd);
            }

            XmlNode exportCmdsNode = rootElem.SelectSingleNode("ExportCmds");
            if (exportCmdsNode != null)
            {
                foreach (XmlNode exportCmdNode in exportCmdsNode.SelectNodes("ExportCmd"))
                {
                    project.ExportCmds.Add(ConvertExportCmd(exportCmdNode));
                }
            }

            project.ExportCmds.Sort();
            project.DebugerSettings.LogWrite = XmlUtils.GetChildBool(rootElem, "WriteLogDriver", true);
            project.DebugerSettings.LogDays = 7;

            return project;
        }

        private static void LoadDbConnSettings(XmlElement rootElem, DbConnSettings dbConnSettings)
        {
            dbConnSettings.DataSourceType = XmlUtils.GetChildEnum(
                rootElem,
                "DataSourceType",
                DataSourceType.Undefined);

            XmlNode dbConnNode = rootElem.SelectSingleNode("DbConnSettings");
            if (dbConnNode == null)
            {
                return;
            }

            dbConnSettings.Server = XmlUtils.GetChildText(dbConnNode, "Server");
            dbConnSettings.Database = XmlUtils.GetChildText(dbConnNode, "Database");
            dbConnSettings.Port = XmlUtils.GetChildText(dbConnNode, "Port");
            dbConnSettings.User = XmlUtils.GetChildText(dbConnNode, "User");
            dbConnSettings.Password = DecryptOldValue(XmlUtils.GetChildText(dbConnNode, "Password"));
            dbConnSettings.OptionalOptions = XmlUtils.GetChildText(dbConnNode, "OptionalOptions");
            dbConnSettings.ConnectionString = DecryptOldValue(XmlUtils.GetChildText(dbConnNode, "ConnectionString"));
            dbConnSettings.Timeout = XmlUtils.GetChildText(dbConnNode, "Timeout");

            if (string.IsNullOrWhiteSpace(dbConnSettings.Timeout))
            {
                dbConnSettings.Timeout = "10";
            }
        }

        private static ExportCmd ConvertExportCmd(XmlNode exportCmdNode)
        {
            ExportCmd exportCmd = new ExportCmd
            {
                Id = XmlUtils.GetChildGuid(
                    exportCmdNode,
                    "Id",
                    XmlUtils.GetChildGuid(exportCmdNode, "ID", Guid.NewGuid())),
                Enabled = XmlUtils.GetChildBool(exportCmdNode, "Enabled", true),
                CmdNum = XmlUtils.GetChildInt(exportCmdNode, "CmdNum", 0),
                CmdCode = XmlUtils.GetChildText(exportCmdNode, "CmdCode"),
                Name = XmlUtils.GetChildText(exportCmdNode, "Name"),
                Description = XmlUtils.GetChildText(exportCmdNode, "Description"),
                Query = XmlUtils.GetChildText(exportCmdNode, "Query"),
                Length = XmlUtils.GetChildInt(exportCmdNode, "Length", 0)
            };

            if (exportCmd.Length <= 0)
            {
                exportCmd.Length = XmlUtils.GetChildInt(exportCmdNode, "Lenght", 80);
            }

            if (exportCmd.Length <= 0)
            {
                exportCmd.Length = 80;
            }

            return exportCmd;
        }

        private static bool IsOldConfig(string fileName)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                return xmlDoc.DocumentElement?.Name == ConversionConstants.OldRootName;
            }
            catch
            {
                return false;
            }
        }

        private static string DecryptOldValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }

            try
            {
                return ScadaUtils.Decrypt(value);
            }
            catch
            {
                return value;
            }
        }

        private static string GetDefaultOutputPath(List<string> sourcePaths)
        {
            return ConverterOptions.GetDefaultOutputPath(sourcePaths);
        }
    }
}
