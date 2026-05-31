// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Data.Models;
using Scada.Server.Lang;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Scada.Server.Modules.ModArcMicrosoftSqlJP.Logic
{
    /// <summary>
    /// Represents a queue for writing data points to a database.
    /// <para>Представляет очередь для записи точек данных в базу данных.</para>
    /// </summary>
    internal class PointQueue : QueueBase<CnlDataPoint>
    {
        private readonly SqlCommand command;          // writes a data point
        private readonly SqlParameter cnlNumParam;    // the channel number parameter
        private readonly SqlParameter timestampParam; // the timestamp parameter
        private readonly SqlParameter valParam;       // the channel value parameter
        private readonly SqlParameter statParam;      // the channel status parameter


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public PointQueue(int maxQueueSize, int batchSize, string insertSql)
            : base(maxQueueSize, batchSize)
        {
            command = new SqlCommand(insertSql);
            cnlNumParam = command.Parameters.Add("cnlNum", SqlDbType.Int);
            timestampParam = command.Parameters.Add("timestamp", SqlDbType.DateTime2);
            valParam = command.Parameters.Add("val", SqlDbType.Float);
            statParam = command.Parameters.Add("stat", SqlDbType.Int);

            ReturnOnError = false;
        }


        /// <summary>
        /// Gets a value indicating whether to return points to the queue in case of error.
        /// </summary>
        public bool ReturnOnError { get; init; }


        /// <summary>
        /// Sets the command parameters according to the data point.
        /// </summary>
        private void SetCommandParams(CnlDataPoint point)
        {
            cnlNumParam.Value = point.CnlNum;
            timestampParam.Value = point.Timestamp;
            valParam.Value = point.Val;
            statParam.Value = point.Stat;
        }

        /// <summary>
        /// Check if the data point should to be returned to the queue.
        /// </summary>
        private bool CheckReturnPoint(Exception ex, CnlDataPoint point)
        {
            return ReturnOnError;
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
                    // retrieve a data point from the queue
                    if (!TryDequeue(out CnlDataPoint point))
                        break;

                    try
                    {
                        // write the data point
                        SetCommandParams(point);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // return the unwritten data point to the queue
                        if (CheckReturnPoint(ex, point))
                            Enqueue(point);

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
