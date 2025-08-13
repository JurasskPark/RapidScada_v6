// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Lang;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    public static class DriverPhrases
    {
        // Client
        public static string ProductName { get; private set; } = "TextParserJP";
        public static string TitleAbout { get; private set; } = "About";

        public static string TitleProject { get; private set; } = "Project";
        public static string FilterProject { get; private set; } = "Project (*.xml)|*.xml|All files (*.*)|*.*";

        public static string TitleLicense { get; private set; } = "License";
        public static string FilterLicense { get; private set; } = "License (*.bin)|*.bin|All files (*.*)|*.*";


        public static string TitleLoadCSV { get; private set; } = "Data load...";
        public static string TitleSaveCSV { get; private set; } = "Data save...";
        public static string FilterCSV { get; private set; } = "CSV (*.csv)|*.csv|All files (*.*)|*.*";


        public static string DictonaryVariableId { get; private set; } = "Record Id";
        public static string DictonaryVariableDate { get; private set; } = "File modification datetime";
        public static string DictonaryVariableName { get; private set; } = "Device name";
        public static string DictonaryVariableFileName { get; private set; } = "File name";
        public static string DictonaryVariableFullPath { get; private set; } = "Path to file";
        public static string DictonaryVariableContent { get; private set; } = "File contents";
        public static string DictonaryVariableOwner { get; private set; } = "File owner";
        public static string DictonaryVariableIsNeedToRead { get; private set; } = "File reading indicator";
        public static string DictonaryVariableStatus { get; private set; } = "File processing status";
        public static string DictonaryVariableStatusReceived { get; private set; } = "Received";
        public static string DictonaryVariableStatusModified { get; private set; } = "Modified";
        public static string DictonaryVariableStatusProcessed { get; private set; } = "Processed";
        public static string DictonaryVariableStatusErrored { get; private set; } = "Errored";    
        public static string DictonaryVariableSizeFile { get; private set; } = "File size";
        public static string DictonaryVariableNumberLines { get; private set; } = "Number of lines in the file";

        // Combo Box
        public static string All { get; private set; } = "All";

        // Dialog Box
        public static string DeleteRow { get; private set; } = "Are you sure you want to delete this entry?";

        // TreeView
        public static string Message { get; private set; } = "Message";


        // Files 
        public static string NamedFileNotFound { get; private set; } = "File {0} not found.";
        public static string InvalidParamVal { get; private set; } = "Invalid parameter value &quot;{0}&quot;.";
        public static string FileDoesNotExist { get; private set; } = "The file does not exist!";
        public static string OpenReadonly { get; private set; } = "The file is locked. Do you want to open it in read-only mode?";
        public static string OpenFailed { get; private set; } = "File is locked by another process and cannot be opened!";
        public static string ReadOnly { get; private set; } = "[read-only]";
        public static string SaveChangesQuestion { get; private set; } = "Do you want to save changes?";

        // Hexdecimial
        public static string NotHexadecimal { get; private set; } = "String is not hexadecimal.";



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
    }
}
