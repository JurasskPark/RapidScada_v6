// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Lang;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    /// <summary>
    /// Contains localized phrases used by the driver UI.
    /// <para>Содержит локализованные фразы интерфейса драйвера.</para>
    /// </summary>
    public static class DriverPhrases
    {
        #region Property
        /// <summary>Gets the product name.</summary>
        public static string ProductName { get; private set; } = "TextParserJP";

        /// <summary>Gets the About dialog title.</summary>
        public static string TitleAbout { get; private set; } = "About";

        /// <summary>Gets the project dialog title.</summary>
        public static string TitleProject { get; private set; } = "Project";

        /// <summary>Gets the project file filter.</summary>
        public static string FilterProject { get; private set; } = "Project (*.xml)|*.xml|All files (*.*)|*.*";

        /// <summary>Gets the license dialog title.</summary>
        public static string TitleLicense { get; private set; } = "License";

        /// <summary>Gets the license file filter.</summary>
        public static string FilterLicense { get; private set; } = "License (*.bin)|*.bin|All files (*.*)|*.*";

        /// <summary>Gets the CSV load dialog title.</summary>
        public static string TitleLoadCSV { get; private set; } = "Data load...";

        /// <summary>Gets the CSV save dialog title.</summary>
        public static string TitleSaveCSV { get; private set; } = "Data save...";

        /// <summary>Gets the CSV file filter.</summary>
        public static string FilterCSV { get; private set; } = "CSV (*.csv)|*.csv|All files (*.*)|*.*";

        /// <summary>Gets the record ID variable phrase.</summary>
        public static string DictonaryVariableId { get; private set; } = "Record Id";

        /// <summary>Gets the file modification date variable phrase.</summary>
        public static string DictonaryVariableDate { get; private set; } = "File modification datetime";

        /// <summary>Gets the device name variable phrase.</summary>
        public static string DictonaryVariableName { get; private set; } = "Device name";

        /// <summary>Gets the file name variable phrase.</summary>
        public static string DictonaryVariableFileName { get; private set; } = "File name";

        /// <summary>Gets the full path variable phrase.</summary>
        public static string DictonaryVariableFullPath { get; private set; } = "Path to file";

        /// <summary>Gets the file content variable phrase.</summary>
        public static string DictonaryVariableContent { get; private set; } = "File contents";

        /// <summary>Gets the file owner variable phrase.</summary>
        public static string DictonaryVariableOwner { get; private set; } = "File owner";

        /// <summary>Gets the file reading indicator variable phrase.</summary>
        public static string DictonaryVariableIsNeedToRead { get; private set; } = "File reading indicator";

        /// <summary>Gets the file processing status variable phrase.</summary>
        public static string DictonaryVariableStatus { get; private set; } = "File processing status";

        /// <summary>Gets the received status phrase.</summary>
        public static string DictonaryVariableStatusReceived { get; private set; } = "Received";

        /// <summary>Gets the modified status phrase.</summary>
        public static string DictonaryVariableStatusModified { get; private set; } = "Modified";

        /// <summary>Gets the processed status phrase.</summary>
        public static string DictonaryVariableStatusProcessed { get; private set; } = "Processed";

        /// <summary>Gets the errored status phrase.</summary>
        public static string DictonaryVariableStatusErrored { get; private set; } = "Errored";

        /// <summary>Gets the file size variable phrase.</summary>
        public static string DictonaryVariableSizeFile { get; private set; } = "File size";

        /// <summary>Gets the number of lines variable phrase.</summary>
        public static string DictonaryVariableNumberLines { get; private set; } = "Number of lines in the file";

        /// <summary>Gets the all items phrase.</summary>
        public static string All { get; private set; } = "All";

        /// <summary>Gets the delete row question.</summary>
        public static string DeleteRow { get; private set; } = "Are you sure you want to delete this entry?";

        /// <summary>Gets the message phrase.</summary>
        public static string Message { get; private set; } = "Message";

        /// <summary>Gets the named file not found message.</summary>
        public static string NamedFileNotFound { get; private set; } = "File {0} not found.";

        /// <summary>Gets the invalid parameter value message.</summary>
        public static string InvalidParamVal { get; private set; } = "Invalid parameter value &quot;{0}&quot;.";

        /// <summary>Gets the file does not exist message.</summary>
        public static string FileDoesNotExist { get; private set; } = "The file does not exist!";

        /// <summary>Gets the read-only open question.</summary>
        public static string OpenReadonly { get; private set; } = "The file is locked. Do you want to open it in read-only mode?";

        /// <summary>Gets the open failed message.</summary>
        public static string OpenFailed { get; private set; } = "File is locked by another process and cannot be opened!";

        /// <summary>Gets the read-only marker.</summary>
        public static string ReadOnly { get; private set; } = "[read-only]";

        /// <summary>Gets the save changes question.</summary>
        public static string SaveChangesQuestion { get; private set; } = "Do you want to save changes?";

        /// <summary>Gets the not hexadecimal message.</summary>
        public static string NotHexadecimal { get; private set; } = "String is not hexadecimal.";
        #endregion Property

        #region Basic
        /// <summary>
        /// Initializes localized driver phrases.
        /// <para>Инициализирует локализованные фразы драйвера.</para>
        /// </summary>
        public static void Init()
        {
            LocaleDict dict = Locale.GetDictionary("Scada.Comm.Drivers.DrvFtpJP.View.Application");
            ProductName = dict[nameof(ProductName)];
            TitleAbout = dict[nameof(TitleAbout)];
            TitleProject = dict[nameof(TitleProject)];
            FilterProject = dict[nameof(FilterProject)];
            TitleLicense = dict[nameof(TitleLicense)];
            FilterLicense = dict[nameof(FilterLicense)];
            TitleLoadCSV = dict[nameof(TitleLoadCSV)];
            TitleSaveCSV = dict[nameof(TitleSaveCSV)];
            FilterCSV = dict[nameof(FilterCSV)];

            DictonaryVariableId = dict[nameof(DictonaryVariableId)];
            DictonaryVariableDate = dict[nameof(DictonaryVariableDate)];
            DictonaryVariableName = dict[nameof(DictonaryVariableName)];
            DictonaryVariableFileName = dict[nameof(DictonaryVariableFileName)];
            DictonaryVariableFullPath = dict[nameof(DictonaryVariableFullPath)];
            DictonaryVariableContent = dict[nameof(DictonaryVariableContent)];
            DictonaryVariableOwner = dict[nameof(DictonaryVariableOwner)];
            DictonaryVariableIsNeedToRead = dict[nameof(DictonaryVariableIsNeedToRead)];
            DictonaryVariableStatus = dict[nameof(DictonaryVariableStatus)];
            DictonaryVariableStatusReceived = dict[nameof(DictonaryVariableStatusReceived)];
            DictonaryVariableStatusModified = dict[nameof(DictonaryVariableStatusModified)];
            DictonaryVariableStatusProcessed = dict[nameof(DictonaryVariableStatusProcessed)];
            DictonaryVariableStatusErrored = dict[nameof(DictonaryVariableStatusErrored)];
            DictonaryVariableSizeFile = dict[nameof(DictonaryVariableSizeFile)];
            DictonaryVariableNumberLines = dict[nameof(DictonaryVariableNumberLines)];

            dict = Locale.GetDictionary("Scada.Comm.Drivers.DrvFtpJP.View.Combobox");
            All = dict[nameof(All)];

            dict = Locale.GetDictionary("Scada.Comm.Drivers.DrvFtpJP.View.DialogBox");
            DeleteRow = dict[nameof(DeleteRow)];

            dict = Locale.GetDictionary("Scada.Comm.Drivers.DrvFtpJP.View.Files");
            NamedFileNotFound = dict[nameof(NamedFileNotFound)];
            NotHexadecimal = dict[nameof(NotHexadecimal)];

            dict = Locale.GetDictionary("Scada.Comm.Drivers.DrvFtpJP.View.Format");
            FileDoesNotExist = dict[nameof(FileDoesNotExist)];
        }
        #endregion Basic
    }
}