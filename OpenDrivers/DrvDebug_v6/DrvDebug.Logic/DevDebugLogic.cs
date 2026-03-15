// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using DebugerLog;
using ProjectDriver;
using Scada.Comm.Channels;
using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Lang;
using Scada.Data.Const;
using Scada.Data.Entities;
using Scada.Data.Models;
using Scada.Lang;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using static System.Collections.Specialized.BitVector32;

namespace Scada.Comm.Drivers.DrvDebug.Logic
{
    /// <summary>
    /// Implements the device logic.
    /// <para>Реализует логику устройства.</para>
    /// </summary>
    internal class DevDebugLogic : DeviceLogic
    {
 
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DevDebugLogic(ICommContext commContext, ILineContext lineContext, DeviceConfig deviceConfig)
            : base(commContext, lineContext, deviceConfig)
        {
            CanSendCommands = true;
            ConnectionRequired = false;

            this.isDll = true;
            this.writeLogDriver = true;

            this.appDirs = commContext.AppDirs;
           
            this.deviceNum = deviceConfig.DeviceNum;
            this.commLineNum = lineContext.CommLineNum;
            this.numAddress = deviceConfig.NumAddress;
            this.strAddress = deviceConfig.StrAddress;

            this.driverCode = DriverUtils.DriverCode;

            string shortFileName = Project.GetFileName(deviceNum);
            this.pathProject = CommContext.AppDirs.ConfigDir;
            this.projectFileName = Path.Combine(CommContext.AppDirs.ConfigDir, shortFileName);

            string shortFileLogName = DriverUtils.GetFileLogName(commLineNum);
            this.pathLog = commContext.AppDirs.LogDir;
            this.logFileName = Path.Combine(commContext.AppDirs.LogDir, shortFileLogName);

            this.writeLogLine = lineContext.LineConfig.LineOptions.DetailedLog;

            // Load the project.
            this.project = new Project();
            if (!this.project.Load(projectFileName, out string errMsg))
            {
                LogDriver(errMsg);
            }

            // Initialize buffers.
            this.buffer = new byte[12288];
            this.bufferReceiver = new byte[0];

            // Initialize stop condition settings.
            this.checkAddress = project.StopConditionCheckAddress;
            this.checkLength = project.StopConditionCheckLength;
            this.checkFormat = project.StopConditionCheckFormat;
            this.markerValue = project.StopConditionCheckValue;
            this.checkMode = project.StopConditionLengthMode;
            this.checkLengthIncludesItself = project.StopConditionLengthIncludesItself;
            this.stopCondition = new ClientStopCondition(checkAddress, checkLength, checkFormat, checkMode, markerValue, checkLengthIncludesItself);

            // Initialize logging settings.
            this.messageType = project.MessageTypeLogDriver;
            this.writeLogDriver = project.WriteLogDriver;

            // Initialize channel settings.
            this.typeChannel = project.TypeChannel;
            this.commands = project.Commands?.Where(c => c != null && c.Enabled).OrderBy(c => c.Order).ToList() ?? new List<ProjectCommand>();
            this.tags = project.Tags?.Where(t => t != null && t.Enabled).OrderBy(t => t.Order).ToList() ?? new List<ProjectTag>();
            this.simulationStartTime = DateTime.UtcNow;
        }

        #region Variables

        private ClientStopCondition stopCondition;                                      // The client stop condition.

        public readonly bool isDll;                                                     // Indicates whether the driver runs as a DLL.
        private readonly AppDirs appDirs;                                               // The application directories.
        
        private readonly string driverCode;                                             // The driver code.
        private readonly int deviceNum;                                                 // The device number.
        private readonly int commLineNum;                                               // The communication line number.
        private readonly int numAddress;                                                // The device numeric address.
        private readonly string strAddress;                                             // The device string address.

        private readonly Project project;                                               // The device configuration.
        private readonly string pathProject;                                            // The configuration path.
        private readonly string pathLog;                                                // The log path.
        private readonly Scada.Log.LogMessageType messageType;                          // The log message type.
        private readonly string projectFileName;                                        // The project file name.
        private readonly string logFileName;                                            // The log file name.

        private readonly bool writeLogLine;                                             // Writes the communication line log.
        private bool writeLogDriver;                                                    // Writes the driver log.

        private readonly byte[] buffer;                                                 // The driver buffer.
        private byte[] bufferReceiver;                                                  // The input buffer.
        private readonly List<ProjectCommand> commands;                                 // The configured commands.
        private readonly List<ProjectTag> tags;                                         // The configured tags.
        private readonly DateTime simulationStartTime;                                  // The simulation start time.
        private int offset = 0;                                                         // The data offset.
        private ProtocolFormat format = ProtocolFormat.Hex;                             // The protocol format.

        private readonly int checkAddress;                                              // The stop condition check address.
        private readonly int checkLength;                                               // The stop condition check length.
        private readonly TypeCode checkFormat;                                          // The stop condition check format.
        private readonly object markerValue;                                            // The stop condition marker value.
        private readonly bool checkMode;                                                // The stop condition mode.
        private readonly bool checkLengthIncludesItself;                                // Indicates whether the length includes itself.

        private Connection? currentConnection;                                          // The current connection.
        private ChannelBehavior typeChannel;                                            // The channel behavior.

        private bool indicatorReceivingSendingData = false;                             // Indicates send or receive activity.

        public const string defaultTimestampFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";   // The timestamp format.
        #endregion Variables

        /// <summary>
        /// Checks that all devices on the communication line support the channel behavior.
        /// </summary>
        public override bool CheckBehaviorSupport(ChannelBehavior behavior)
        {
            switch (behavior)
            {
                case ChannelBehavior.Mixed:
                    return true;
                case ChannelBehavior.Master:
                    return true;
                case ChannelBehavior.Slave:
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Performs actions after adding the device to a communication line.
        /// </summary>
        public override void OnCommLineStart()
        {
            base.OnCommLineStart();

            LogDriver(Locale.IsRussian ?
                       "Запуск драйвера" :
                       "Running the driver");

            LogDriver("[Driver " + driverCode + "]");
            LogDriver("[Version " + DriverUtils.Version + "]");
            LogDriver("[" + DriverDictonary.StartDriver + "]");
            LogDriver("[" + DriverDictonary.Delay + "][" + DriverUtils.NullToString(PollingOptions.Delay) + "]");
            LogDriver("[" + DriverDictonary.Timeout + "][" + DriverUtils.NullToString(PollingOptions.Timeout) + "]");
            LogDriver("[" + DriverDictonary.Period + "][" + DriverUtils.NullToString(PollingOptions.Period) + "]");
        }

        /// <summary>
        /// Performs actions after stopping the device on the communication line.
        /// </summary>
        public override void OnCommLineTerminate()
        {
            base.OnCommLineTerminate();

            LogDriver(Locale.IsRussian ?
                       "Останов драйвера" :
                       "Stopping the driver");
        }

        /// <summary>
        /// Initializes the device tags.
        /// </summary>
        public override void InitDeviceTags()
        {
            DeviceTags.FlattenGroups = true;
            TagGroup tagGroup = new TagGroup("DrvDebug");

            int index = 0;
            foreach (ProjectTag projectTag in tags)
            {
                DeviceTag deviceTag = tagGroup.AddTag(GetTagCode(projectTag), projectTag.Name);
                deviceTag.Index = index++;
                deviceTag.DataType = GetTagDataType(projectTag);
                deviceTag.DataLen = GetTagDataLen(projectTag);
                deviceTag.Format = GetTagFormat(projectTag);
                deviceTag.Aux = projectTag;
            }

            DeviceTags.AddGroup(tagGroup);
            DeviceData.Init();
        }

        /// <summary>
        /// Performs a communication session.
        /// </summary>
        public override void Session()
        {
            base.Session();

            #region Log
            DebugerReturn.OnDebug = new DebugerReturn.DebugData(LogDriver);
            #endregion Log

            try
            {
                if (!indicatorReceivingSendingData)
                {
                    if(typeChannel != ChannelBehavior.Slave)
                    {
                        LogDriver("[START SESSION]");
                    }


                    string text = string.Empty;
                    string logText = string.Empty;

                    if (typeChannel == ChannelBehavior.Master)
                    {
                        if (commands.Count > 0)
                        {
                            LogDriver("Master Session");
                            ExecuteConfiguredCommands(Connection);
                        }
                    }

                    if (typeChannel == ChannelBehavior.Mixed)
                    {
                        if (commands.Count > 0)
                        {
                            LogDriver("Mixed Session");
                            ExecuteConfiguredCommands(Connection);
                        }
                    }

                    ApplySimulationOnlyTags();

                    if (typeChannel != ChannelBehavior.Slave)
                    {
                        LogDriver("[END SESSION]");
                    }

                    LastRequestOK = true;

                    FinishRequest();
                    SleepPollingDelay();
                }
            }
            catch (Exception ex)
            {
                LastRequestOK = false;
                Log.WriteError(ex.Message);
            }

            FinishSession();
        }

        /// <summary>
        /// Receives an unread incoming request in slave mode.
        /// </summary>
        public override void ReceiveIncomingRequest(Connection connection, IncomingRequestArgs requestArgs)
        {
            #region Log
            DebugerReturn.OnDebug = new DebugerReturn.DebugData(LogDriver);
            #endregion Log

            LogDriver("[START CONNECTION]");

            #region Indicator
            indicatorReceivingSendingData = true;
            #endregion Indicator

            if (string.IsNullOrEmpty(connection.RemoteAddress))
            {
                LogDriver(Locale.IsRussian ?
                    "Прием входящего запроса" :
                    "Receive incoming request");
            }
            else
            {
                LogDriver(Locale.IsRussian ?
                    $"Прием входящего запроса от {connection.RemoteAddress}" :
                    $"Receive incoming request from {connection.RemoteAddress}");
            }

            #region Receive data from the client
            string text = string.Empty;
            string logText = string.Empty;

            // Start the timeout watcher.
            var stopwatch = Stopwatch.StartNew();
            var frequency = Stopwatch.Frequency;
            var targetTicks = ((DeviceLogic)this).PollingOptions.Timeout * Stopwatch.Frequency / 1000;

            long iterations = 0;

            bool stopReceived = false;

            int count = 0;
            byte[] bufferReceiver = new byte[count];


            if (stopCondition == null)
            {
                stopCondition = new ClientStopCondition(checkAddress, checkLength, checkFormat, checkMode, markerValue, checkLengthIncludesItself);
            }

            while (stopwatch.ElapsedTicks < targetTicks && !stopReceived)
            {
                count = connection.Read(this.buffer, 0, this.buffer.Length, ((DeviceLogic)this).PollingOptions.Timeout, stopCondition, out stopReceived, ProtocolFormat.Hex, out logText);

                iterations++;

                if (count > 0)
                {
                    bufferReceiver = new byte[count];
                    Array.Copy((Array)buffer, bufferReceiver, count);
                }
            }

            stopwatch.Stop();

            LogDriver(Locale.IsRussian ?
                       $"Выполнено {iterations} итераций за {stopwatch.ElapsedMilliseconds} мс" :
                       $"Executed {iterations} iterations in {stopwatch.ElapsedMilliseconds} ms");

            if (count > 0)
            {
                LogDriver(Locale.IsRussian ?
                       $"Прочитано {count} байт" :
                       $"Read {count} bytes");

                if (stopReceived)
                {
                    stopCondition.Reset();
                }
            }

            if (bufferReceiver != null && bufferReceiver.Length > 0)
            {
                ApplyReceivedData(bufferReceiver);

                if (typeChannel == ChannelBehavior.Slave)
                {
                    LogDriver("Slave Session");

                    string readFallback = Connection.BuildReadLogText(bufferReceiver, offset, bufferReceiver.Length, format);
                    LogTransport(string.Empty, readFallback);

                    byte[] responsePayload = GetDefaultResponsePayload();
                    if (responsePayload.Length > 0)
                    {
                        LogDriver($"Command: {GetDefaultResponseCommandName()}");
                        string writeFallback = Connection.BuildWriteLogText(responsePayload, offset, responsePayload.Length, format);
                        Connection.Write(responsePayload, 0, responsePayload.Length, ProtocolFormat.Hex, out logText);
                        LogTransport(logText, writeFallback);
                    }
                    else
                    {
                        LogDriver(Locale.IsRussian ? "Ответ не настроен" : "Response is not configured");
                    }
                }

                if (typeChannel == ChannelBehavior.Mixed)
                {
                    LogDriver("Mixed Session");

                    string readFallback = Connection.BuildReadLogText(bufferReceiver, offset, bufferReceiver.Length, format);
                    LogTransport(string.Empty, readFallback);

                    byte[] responsePayload = GetDefaultResponsePayload();
                    if (responsePayload.Length > 0)
                    {
                        LogDriver($"Command: {GetDefaultResponseCommandName()}");
                        string writeFallback = Connection.BuildWriteLogText(responsePayload, offset, responsePayload.Length, format);
                        Connection.Write(responsePayload, 0, responsePayload.Length, ProtocolFormat.Hex, out logText);
                        LogTransport(logText, writeFallback);
                    }
                    else
                    {
                        LogDriver(Locale.IsRussian ? "Ответ не настроен" : "Response is not configured");
                    }
                }
            }
            else
            {
                LogDriver(Locale.IsRussian ? "Приём (0): " : "Receive (0): ", false);
            }

            #endregion Receive data from the client

            #region Device Logic

            DeviceLogic deviceLogic = this;

            // Decode the device address from the received command.
            int address = -1;

            // Apply device-specific address decoding logic.
            // ...
            // 

            address = numAddress;
            
            if (connection.Connected == true)
            {
                currentConnection = connection;

                LineContext.GetDeviceByAddress(address, out deviceLogic);
                requestArgs.TargetDevices.Add(deviceLogic);
            }

            #endregion Device Logic

            #region Indicator
            indicatorReceivingSendingData = false;
            #endregion Indicator

            LogDriver("[END CONNECTION]");

            FinishRequest();
        }

        /// <summary>
        /// Sends the telecontrol command.
        /// </summary>
        public override void SendCommand(TeleCommand cmd)
        {
            base.SendCommand(cmd);

            LogDriver("[START COMMAND]");

            #region Init Log
            string logText = string.Empty;
            #endregion Init Log

            try
            {
                LogDriver(string.Format(Locale.IsRussian ?
                    "Получена команда. " :
                    "Command received. "));
                LogDriver(string.Format(Locale.IsRussian ?
                    "Дата: " + cmd.CreationTime.ToString() :
                    "Date: " + cmd.CreationTime.ToString()));
                LogDriver(string.Format(Locale.IsRussian ?
                   "Пользователь ID: " + cmd.UserID.ToString() :
                   "User ID: " + cmd.UserID.ToString()));
                LogDriver(string.Format(Locale.IsRussian ?
                   "Номер устройства: " + cmd.DeviceNum.ToString() :
                   "Device number: " + cmd.DeviceNum.ToString()));
                LogDriver(string.Format(Locale.IsRussian ?
                    "Номер команды (@cmdNum): " + cmd.CmdNum :
                    "Command number (@cmdNum): " + cmd.CmdNum));
                LogDriver(string.Format(Locale.IsRussian ?
                    "Код команды (@cmdCode): " + cmd.CmdCode :
                    "Command code (@cmdCode): " + cmd.CmdCode));
                LogDriver(string.Format(Locale.IsRussian ?
                    "Значение команды (@cmdVal): " + cmd.CmdVal :
                    "Command value (@cmdVal): " + cmd.CmdVal));
                LogDriver(string.Format(Locale.IsRussian ?
                    "Значение команды (@cmdData): " + HexTools.String.ToStringWithDots(cmd.CmdData) :
                    "Command value (@cmdData): " + HexTools.String.ToStringWithDots(cmd.CmdData)));
            }
            catch { }

            if (cmd.CmdCode == "SendStr")
            {
                try
                {
                    string text = cmd.GetCmdDataString();
                    Connection.WriteLine(text, out logText);
                    LastRequestOK = true;

                    if (logText != string.Empty)
                    {
                        LogDriver(logText);
                    }
                }
                catch
                {
                    LastRequestOK = false;
                }

                FinishRequest();
            }
            else if (cmd.CmdCode == "SendBin")
            {
                try
                {
                    byte[] buffer = cmd.CmdData ?? Array.Empty<byte>();
                    Connection.Write(buffer, 0, buffer.Length, ProtocolFormat.Hex, out logText);
                    LastRequestOK = true;


                    if (logText != string.Empty)
                    {
                        LogDriver(logText);
                    }
                }
                catch
                {
                    LastRequestOK = false;
                }

                FinishRequest();
            }
            else
            {
                LastRequestOK = false;
                Log.WriteLine(CommPhrases.InvalidCommand);
            }

            FinishCommand();

            LogDriver("[END COMMAND]");
        }

        /// <summary>
        /// Executes the configured command sequence.
        /// </summary>
        private void ExecuteConfiguredCommands(Connection connection)
        {
            foreach (ProjectCommand command in commands)
            {
                byte[] payload = BuildCommandPayload(command);
                if (payload.Length == 0)
                {
                    continue;
                }

                ExecuteSinglePayload(connection, command.Name, payload, command.DelayMs);
            }
        }

        /// <summary>
        /// Executes a single command payload.
        /// </summary>
        private void ExecuteSinglePayload(Connection connection, string commandName, byte[] payload, int delayMs)
        {
            string logText;
            LogDriver($"Command: {commandName}");
            string writeFallback = Connection.BuildWriteLogText(payload, offset, payload.Length, format);
            connection.Write(payload, 0, payload.Length, ProtocolFormat.Hex, out logText);
            LogTransport(logText, writeFallback);

            byte[] responseBytes = ReadResponse(connection, out logText);

            if (responseBytes.Length > 0)
            {
                string readFallback = Connection.BuildReadLogText(responseBytes, offset, responseBytes.Length, format);
                LogTransport(logText, readFallback);
                ApplyReceivedData(responseBytes);
            }

            if (delayMs > 0)
            {
                Thread.Sleep(delayMs);
            }
        }

        /// <summary>
        /// Reads a response from the connection.
        /// </summary>
        private byte[] ReadResponse(Connection connection, out string logText)
        {
            logText = string.Empty;

            if (stopCondition == null)
            {
                stopCondition = new ClientStopCondition(checkAddress, checkLength, checkFormat, checkMode, markerValue, checkLengthIncludesItself);
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            long targetTicks = PollingOptions.Timeout * Stopwatch.Frequency / 1000;
            bool stopReceived = false;
            byte[] responseBytes = Array.Empty<byte>();

            while (stopwatch.ElapsedTicks < targetTicks && !stopReceived)
            {
                int readCount = connection.Read(buffer, 0, buffer.Length, PollingOptions.Timeout, stopCondition, out stopReceived, ProtocolFormat.Hex, out logText);
                if (readCount > 0)
                {
                    responseBytes = new byte[readCount];
                    Array.Copy(buffer, responseBytes, readCount);
                }
            }

            if (stopReceived)
            {
                stopCondition.Reset();
            }

            return responseBytes;
        }

        /// <summary>
        /// Applies received data to configured tags.
        /// </summary>
        private void ApplyReceivedData(byte[] responseBytes)
        {
            if (responseBytes == null || responseBytes.Length == 0)
            {
                return;
            }

            bufferReceiver = responseBytes;

            foreach (ProjectTag tag in tags)
            {
                if (tag.Mode == TagMode.Simulate)
                {
                    continue;
                }

                bool decoded = SetDecodedTagData(tag, responseBytes);
                if (!decoded && tag.Mode == TagMode.DecodeAndSimulate && tag.SimulateOnDecodeError)
                {
                    SetSimulatedTagData(tag, Math.Max(0, (DateTime.UtcNow - simulationStartTime).TotalSeconds));
                }
            }
        }

        /// <summary>
        /// Applies simulation values to simulation-only tags.
        /// </summary>
        private void ApplySimulationOnlyTags()
        {
            double seconds = Math.Max(0, (DateTime.UtcNow - simulationStartTime).TotalSeconds);
            foreach (ProjectTag tag in tags)
            {
                if (tag.Mode == TagMode.Decode)
                {
                    continue;
                }

                if (tag.Mode == TagMode.DecodeAndSimulate && bufferReceiver != null && bufferReceiver.Length > tag.ArrayIndex)
                {
                    continue;
                }

                SetSimulatedTagData(tag, seconds);
            }
        }

        /// <summary>
        /// Sets decoded data for the specified tag.
        /// </summary>
        private bool SetDecodedTagData(ProjectTag tag, byte[] responseBytes)
        {
            string tagCode = GetTagCode(tag);
            int status = CnlStatusID.Defined;
            string textValue = ProjectTagCodec.DecodeToText(tag, responseBytes);

            if (string.IsNullOrWhiteSpace(textValue))
            {
                return false;
            }

            switch (tag.DataFormat)
            {
                case TagDataFormat.Ascii:
                case TagDataFormat.HexString:
                    DeviceData.SetAscii(tagCode, textValue, status);
                    return true;

                case TagDataFormat.Unicode:
                    DeviceData.SetUnicode(tagCode, textValue, status);
                    return true;

                default:
                    if (double.TryParse(textValue, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double numericValue))
                    {
                        DeviceData.Set(tagCode, numericValue, status);
                        return true;
                    }
                    return false;
            }
        }

        /// <summary>
        /// Sets a simulated value for the specified tag.
        /// </summary>
        private void SetSimulatedTagData(ProjectTag tag, double elapsedSeconds)
        {
            ProjectTagSimulationOptions options = tag.Simulation ?? new ProjectTagSimulationOptions();
            string tagCode = GetTagCode(tag);
            int status = CnlStatusID.Defined;
            double tick = Math.Max(0, Math.Floor(elapsedSeconds / Math.Max(0.001, options.UpdateIntervalMs / 1000.0)));

            switch (tag.SimulationKind)
            {
                case SimulationKind.Ramp:
                {
                    double value = options.StartValue + options.Step * tick;
                    if (options.Cycle && value > options.Max)
                    {
                        double span = Math.Max(options.Step, options.Max - options.Min + options.Step);
                        value = options.Min + ((value - options.Min) % span);
                    }
                    DeviceData.Set(tagCode, value, status);
                    break;
                }
                case SimulationKind.Sawtooth:
                {
                    double value = options.StartValue + options.Step * tick;
                    if (value > options.Max)
                    {
                        value = options.ResetValue;
                    }
                    DeviceData.Set(tagCode, value, status);
                    break;
                }
                case SimulationKind.Sine:
                {
                    double period = Math.Max(0.001, options.PeriodSeconds);
                    double angle = 2 * Math.PI * elapsedSeconds / period + options.PhaseDegrees * Math.PI / 180.0;
                    DeviceData.Set(tagCode, options.Bias + options.Amplitude * Math.Sin(angle), status);
                    break;
                }
                case SimulationKind.Square:
                {
                    double period = Math.Max(0.001, options.PeriodSeconds);
                    double duty = Math.Clamp(options.DutyCyclePercent, 0, 100) / 100.0;
                    double phase = (elapsedSeconds % period) / period;
                    DeviceData.Set(tagCode, phase < duty ? options.HighValue : options.LowValue, status);
                    break;
                }
                case SimulationKind.StringList:
                {
                    string[] values = SplitStringValues(options);
                    if (values.Length > 0)
                    {
                        int index = (int)tick;
                        string value = options.Cycle ? values[index % values.Length] : values[Math.Min(index, values.Length - 1)];
                        SetStringValue(tag, tagCode, value, status);
                    }
                    break;
                }
                case SimulationKind.StringGenerate:
                {
                    string value = (options.StringTemplate ?? "TAG_{N}")
                        .Replace("{N}", ((int)tick).ToString())
                        .Replace("{TIME}", DateTime.Now.ToString("HH:mm:ss"));
                    SetStringValue(tag, tagCode, value, status);
                    break;
                }
            }
        }

        /// <summary>
        /// Sets a string value for the specified tag.
        /// </summary>
        private void SetStringValue(ProjectTag tag, string tagCode, string value, int status)
        {
            if (tag.DataFormat == TagDataFormat.Unicode)
            {
                DeviceData.SetUnicode(tagCode, value, status);
            }
            else
            {
                DeviceData.SetAscii(tagCode, value, status);
            }
        }

        /// <summary>
        /// Splits configured string values.
        /// </summary>
        private static string[] SplitStringValues(ProjectTagSimulationOptions options)
        {
            string delimiter = string.IsNullOrEmpty(options.StringDelimiter) ? ";" : options.StringDelimiter;
            return (options.StringValues ?? string.Empty)
                .Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .ToArray();
        }

        /// <summary>
        /// Builds the payload for the specified command.
        /// </summary>
        private static byte[] BuildCommandPayload(ProjectCommand command)
        {
            if (command == null || string.IsNullOrWhiteSpace(command.Payload))
            {
                return Array.Empty<byte>();
            }

            return command.DataKind switch
            {
                CommandDataKind.Ascii => Encoding.ASCII.GetBytes(command.Payload),
                CommandDataKind.Unicode => Encoding.Unicode.GetBytes(command.Payload),
                _ => HexTools.String.ToByteArray(command.Payload)
            };
        }

        /// <summary>
        /// Gets the default response payload for slave and mixed modes.
        /// </summary>
        private byte[] GetDefaultResponsePayload()
        {
            if (commands == null || commands.Count == 0)
            {
                return Array.Empty<byte>();
            }

            ProjectCommand command = commands[0];
            return BuildCommandPayload(command);
        }

        /// <summary>
        /// Gets the default response command name.
        /// </summary>
        private string GetDefaultResponseCommandName()
        {
            if (commands == null || commands.Count == 0)
            {
                return Locale.IsRussian ? "Не задана" : "Not set";
            }

            return string.IsNullOrWhiteSpace(commands[0].Name)
                ? (Locale.IsRussian ? "Без имени" : "Unnamed")
                : commands[0].Name;
        }

        /// <summary>
        /// Writes transport log text without duplicating fallback entries.
        /// </summary>
        private void LogTransport(string transportText, string fallbackText)
        {
            if (!string.IsNullOrWhiteSpace(transportText))
            {
                LogDriver(transportText, false);
                return;
            }

            if (!string.IsNullOrWhiteSpace(fallbackText))
            {
                LogDriver(fallbackText, false);
            }
        }

        /// <summary>
        /// Gets the tag code for the specified tag.
        /// </summary>
        private static string GetTagCode(ProjectTag tag)
        {
            return string.IsNullOrWhiteSpace(tag.Name)
                ? $"tag_{tag.Channel}_{tag.Id:N}"
                : tag.Name.Trim().Replace(" ", "_");
        }

        /// <summary>
        /// Gets the data type for the specified tag.
        /// </summary>
        private static TagDataType GetTagDataType(ProjectTag tag)
        {
            return tag.DataFormat switch
            {
                TagDataFormat.Ascii => TagDataType.ASCII,
                TagDataFormat.Unicode => TagDataType.Unicode,
                TagDataFormat.Int64 => TagDataType.Int64,
                TagDataFormat.UInt64 => TagDataType.Int64,
                _ => TagDataType.Double
            };
        }

        /// <summary>
        /// Gets the format for the specified tag.
        /// </summary>
        private static TagFormat GetTagFormat(ProjectTag tag)
        {
            return tag.DataFormat switch
            {
                TagDataFormat.Ascii => new TagFormat(TagFormatType.String),
                TagDataFormat.Unicode => new TagFormat(TagFormatType.String),
                _ => new TagFormat(TagFormatType.Number)
            };
        }

        /// <summary>
        /// Gets the data length for the specified tag.
        /// </summary>
        private static int GetTagDataLen(ProjectTag tag)
        {
            if (tag == null)
            {
                return 1;
            }

            int bytes = Math.Max(1, tag.DataLength);
            return tag.DataFormat switch
            {
                TagDataFormat.Ascii => Math.Max(1, (int)Math.Ceiling(bytes / 8.0)),
                TagDataFormat.Unicode => Math.Max(1, (int)Math.Ceiling(bytes / 8.0)),
                _ => 1
            };
        }

        #region Log
        /// <summary>
        /// Writes a driver log message.
        /// </summary>
        /// <param name="text">The message to write.</param>
        private void LogDriver(string text)
        {
            LogDriver(text, true);
        }

        /// <summary>
        /// Writes a driver log message.
        /// </summary>
        /// <param name="text">The message to write.</param>
        /// <param name="writeTimestamp">Indicates whether to prepend timestamp in file log mode.</param>
        private void LogDriver(string text, bool writeTimestamp = true)
        {
            if (text == string.Empty || text == "" || text == null)
            {
                return;
            }

            if (!writeLogDriver)
            {
                return;
            }

            // For protocol dump lines we must write raw text without timestamp
            // regardless of the communication line detailed log setting.
            if (!writeTimestamp)
            {
                StreamWriter rawWriter = File.AppendText(logFileName);
                rawWriter.WriteLine(text);
                rawWriter.Close();
                return;
            }

            if (writeLogLine)
            {
                switch (messageType)
                {
                    case Scada.Log.LogMessageType.Action:
                        Log.WriteMessage(text, Scada.Log.LogMessageType.Action);
                        break;
                    case Scada.Log.LogMessageType.Info:
                        Log.WriteMessage(text, Scada.Log.LogMessageType.Info);
                        break;
                    case Scada.Log.LogMessageType.Warning:
                        Log.WriteMessage(text, Scada.Log.LogMessageType.Warning);
                        break;
                    case Scada.Log.LogMessageType.Error:
                        Log.WriteMessage(text, Scada.Log.LogMessageType.Error);
                        break;
                }
            }
            else
            {
                StringBuilder sb = new StringBuilder(DateTime.Now.ToString(defaultTimestampFormat));
                StreamWriter streamWriter = File.AppendText(logFileName);
                streamWriter.WriteLine(sb.Append(" ").Append(text).ToString());
                streamWriter.Close();
            }
        }
        #endregion Log
    }
}

