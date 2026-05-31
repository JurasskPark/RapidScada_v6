// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Data.Models;
using Scada.Server.Lang;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Scada.Server.Modules.ModArcMicrosoftSqlJP.Logic
{
    /// <summary>
    /// Represents a queue for writing events to a database.
    /// <para>Представляет очередь для записи событий в базу данных.</para>
    /// </summary>
    internal class EventQueue : QueueBase<Event>
    {
        private readonly SqlCommand command; // writes an event
        private readonly SqlParameter eventIdParam;
        private readonly SqlParameter timestampParam;
        private readonly SqlParameter hiddenParam;
        private readonly SqlParameter cnlNumParam;
        private readonly SqlParameter objNumParam;
        private readonly SqlParameter deviceNumParam;
        private readonly SqlParameter prevCnlValParam;
        private readonly SqlParameter prevCnlStatParam;
        private readonly SqlParameter cnlValParam;
        private readonly SqlParameter cnlStatParam;
        private readonly SqlParameter severityParam;
        private readonly SqlParameter ackRequiredParam;
        private readonly SqlParameter ackParam;
        private readonly SqlParameter ackTimestampParam;
        private readonly SqlParameter ackUserIDParam;
        private readonly SqlParameter textFormatParam;
        private readonly SqlParameter eventTextParam;
        private readonly SqlParameter eventDataParam;


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public EventQueue(int maxQueueSize, int batchSize, string insertSql)
            : base(maxQueueSize, batchSize)
        {
            command = new SqlCommand(insertSql);
            eventIdParam = command.Parameters.Add("eventID", SqlDbType.BigInt);
            timestampParam = command.Parameters.Add("timestamp", SqlDbType.DateTime2);
            hiddenParam = command.Parameters.Add("hidden", SqlDbType.Bit);
            cnlNumParam = command.Parameters.Add("cnlNum", SqlDbType.Int);
            objNumParam = command.Parameters.Add("objNum", SqlDbType.Int);
            deviceNumParam = command.Parameters.Add("deviceNum", SqlDbType.Int);
            prevCnlValParam = command.Parameters.Add("prevCnlVal", SqlDbType.Float);
            prevCnlStatParam = command.Parameters.Add("prevCnlStat", SqlDbType.Int);
            cnlValParam = command.Parameters.Add("cnlVal", SqlDbType.Float);
            cnlStatParam = command.Parameters.Add("cnlStat", SqlDbType.Int);
            severityParam = command.Parameters.Add("severity", SqlDbType.Int);
            ackRequiredParam = command.Parameters.Add("ackRequired", SqlDbType.Bit);
            ackParam = command.Parameters.Add("ack", SqlDbType.Bit);
            ackTimestampParam = command.Parameters.Add("ackTimestamp", SqlDbType.DateTime2);
            ackUserIDParam = command.Parameters.Add("ackUserID", SqlDbType.Int);
            textFormatParam = command.Parameters.Add("textFormat", SqlDbType.Int);
            eventTextParam = command.Parameters.Add("eventText", SqlDbType.NVarChar, -1);
            eventDataParam = command.Parameters.Add("eventData", SqlDbType.VarBinary, -1);
        }


        /// <summary>
        /// Sets the command parameters according to the event.
        /// </summary>
        private void SetCommandParams(Event ev)
        {
            eventIdParam.Value = ev.EventID;
            timestampParam.Value = ev.Timestamp;
            hiddenParam.Value = ev.Hidden;
            cnlNumParam.Value = ev.CnlNum;
            objNumParam.Value = ev.ObjNum;
            deviceNumParam.Value = ev.DeviceNum;
            prevCnlValParam.Value = ev.PrevCnlVal;
            prevCnlStatParam.Value = ev.PrevCnlStat;
            cnlValParam.Value = ev.CnlVal;
            cnlStatParam.Value = ev.CnlStat;
            severityParam.Value = ev.Severity;
            ackRequiredParam.Value = ev.AckRequired;
            ackParam.Value = ev.Ack;
            ackTimestampParam.Value = ev.AckTimestamp;
            ackUserIDParam.Value = ev.AckUserID;
            textFormatParam.Value = (int)ev.TextFormat;
            eventTextParam.Value = string.IsNullOrEmpty(ev.Text) ? DBNull.Value : ev.Text;
            eventDataParam.Value = ev.Data == null || ev.Data.Length == 0 ? DBNull.Value : ev.Data;
        }

        /// <summary>
        /// Retrieves items from the queue and inserts or updates them in the database.
        /// </summary>
        public override bool ProcessItems()
        {
            if (Connection == null)
                throw new InvalidOperationException("Connection must not be null.");

            if (Count == 0)
                return false;

            SqlTransaction trans = null;

            try
            {
                Connection.Open();
                trans = Connection.BeginTransaction();
                command.Connection = Connection;
                command.Transaction = trans;

                for (int i = 0; i < BatchSize; i++)
                {
                    // retrieve an event from the queue
                    if (!TryDequeue(out Event ev))
                        break;

                    try
                    {
                        // write the event
                        if (ev != null)
                        {
                            SetCommandParams(ev);
                            command.ExecuteNonQuery();
                        }
                    }
                    catch
                    {
                        // return the unwritten event to the queue
                        Enqueue(ev);
                        throw;
                    }
                }

                if (Count == 0)
                    ArcLog?.WriteInfo(ServerPhrases.QueueBecameEmpty);

                trans.Commit();
                LastCommitTime = DateTime.UtcNow;
                Stats.HasError = false;
                return true;
            }
            catch (Exception ex)
            {
                SilentCommitOrRollback(trans);
                Stats.HasError = true;
                AppLog?.WriteError(ex, ServerPhrases.ArchiveMessage, ArchiveCode, ServerPhrases.WriteDbError);
                ArcLog?.WriteError(ex, ServerPhrases.WriteDbError);
                Thread.Sleep(ScadaUtils.ErrorDelay);
                return false;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}
