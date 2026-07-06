using Scada.Comm.Drivers.DrvDbDataTransfer;
using System.Data;
using System.Xml;

internal static class Program
{
    private static int Main()
    {
        List<Action> tests = new List<Action>
        {
            TypeNamesMapToStableClrTypes,
            ValuesConvertWithoutStringSerialization,
            LegacyXmlLoadsIntoSourceAndTargetSettings,
            NewXmlRoundTripsSourceTargetAndTransferQueries,
            HistoryCommentsParseAndWindowsStayInsideDays,
            HistoryWindowQueryRendersDatePatternsAndWindowBounds,
            HistoryWindowQueryRendersBareWindowPlaceholders
        };

        int failed = 0;
        foreach (Action test in tests)
        {
            try
            {
                test();
                Console.WriteLine($"PASS {test.Method.Name}");
            }
            catch (Exception ex)
            {
                failed++;
                Console.WriteLine($"FAIL {test.Method.Name}: {ex.Message}");
            }
        }

        return failed == 0 ? 0 : 1;
    }

    private static void TypeNamesMapToStableClrTypes()
    {
        AssertEqual(typeof(long), DbValueConverter.GetClrType("bigint"));
        AssertEqual(typeof(long), DbValueConverter.GetClrType("int8"));
        AssertEqual(typeof(int), DbValueConverter.GetClrType("integer"));
        AssertEqual(typeof(int), DbValueConverter.GetClrType("int4"));
        AssertEqual(typeof(short), DbValueConverter.GetClrType("smallint"));
        AssertEqual(typeof(short), DbValueConverter.GetClrType("int2"));
        AssertEqual(typeof(decimal), DbValueConverter.GetClrType("numeric"));
        AssertEqual(typeof(double), DbValueConverter.GetClrType("double precision"));
        AssertEqual(typeof(float), DbValueConverter.GetClrType("float4"));
        AssertEqual(typeof(bool), DbValueConverter.GetClrType("boolean"));
        AssertEqual(typeof(DateTimeOffset), DbValueConverter.GetClrType("timestamp with time zone"));
        AssertEqual(typeof(Guid), DbValueConverter.GetClrType("uuid"));
        AssertEqual(typeof(string), DbValueConverter.GetClrType("jsonb"));
        AssertEqual(typeof(byte[]), DbValueConverter.GetClrType("bytea"));
    }

    private static void ValuesConvertWithoutStringSerialization()
    {
        DataTable table = new DataTable();
        table.Columns.Add("id", typeof(long));
        table.Columns.Add("amount", typeof(decimal));
        table.Columns.Add("active", typeof(bool));
        table.Columns.Add("uid", typeof(Guid));
        table.Columns.Add("payload", typeof(byte[]));
        table.Columns.Add("empty", typeof(string));

        Guid uid = Guid.NewGuid();
        byte[] bytes = new byte[] { 1, 2, 3 };
        DataRow row = table.NewRow();
        row["id"] = 9223372036854770000L;
        row["amount"] = 12345.6789m;
        row["active"] = true;
        row["uid"] = uid;
        row["payload"] = bytes;
        row["empty"] = DBNull.Value;
        table.Rows.Add(row);

        AssertEqual(9223372036854770000L, DbValueConverter.ConvertForParameter(row["id"], table.Columns["id"]));
        AssertEqual(12345.6789m, DbValueConverter.ConvertForParameter(row["amount"], table.Columns["amount"]));
        AssertEqual(true, DbValueConverter.ConvertForParameter(row["active"], table.Columns["active"]));
        AssertEqual(uid, DbValueConverter.ConvertForParameter(row["uid"], table.Columns["uid"]));
        AssertSame(bytes, DbValueConverter.ConvertForParameter(row["payload"], table.Columns["payload"]));
        AssertSame(DBNull.Value, DbValueConverter.ConvertForParameter(row["empty"], table.Columns["empty"]));
    }

    private static void LegacyXmlLoadsIntoSourceAndTargetSettings()
    {
        string fileName = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.xml");
        File.WriteAllText(fileName, """
<?xml version="1.0" encoding="utf-8"?>
<DrvDbDataTransferProject>
  <DbConnSettings>
    <DataSourceType>PostgreSQL</DataSourceType>
    <Server>source-host</Server>
    <Database>postgres</Database>
    <Port>5432</Port>
    <User>postgres</User>
    <Password />
    <OptionalOptions>Timeout=10;</OptionalOptions>
    <ConnectionString />
    <Timeout>10</Timeout>
  </DbConnSettings>
  <ImportCmds>
    <ImportCmd>
      <Id>f3913750-1bf2-486b-872b-e74d4f74526f</Id>
      <Enabled>true</Enabled>
      <CmdNum>1</CmdNum>
      <CmdCode>DBIMPORT001</CmdCode>
      <Name>Import</Name>
      <Description />
      <Query>SELECT 1 AS id</Query>
      <IsColumnBased>true</IsColumnBased>
      <DeviceTags />
    </ImportCmd>
  </ImportCmds>
  <ExportCmds />
  <DebugerSettings>
    <LogWrite>true</LogWrite>
    <LogDays>7</LogDays>
  </DebugerSettings>
</DrvDbDataTransferProject>
""");

        try
        {
            DrvDbDataTransferProject project = new DrvDbDataTransferProject();
            Assert(project.Load(fileName, out string errMsg), errMsg);
            AssertEqual(DataSourceType.PostgreSQL, project.SourceDbConnSettings.DataSourceType);
            AssertEqual(DataSourceType.PostgreSQL, project.TargetDbConnSettings.DataSourceType);
            AssertEqual("SELECT 1 AS id", project.ImportCmds[0].SelectQuery);
            AssertEqual("", project.ImportCmds[0].InsertQuery);
        }
        finally
        {
            File.Delete(fileName);
        }
    }

    private static void NewXmlRoundTripsSourceTargetAndTransferQueries()
    {
        string fileName = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.xml");

        try
        {
            DrvDbDataTransferProject project = new DrvDbDataTransferProject();
            project.SourceDbConnSettings.DataSourceType = DataSourceType.PostgreSQL;
            project.SourceDbConnSettings.Server = "source-host";
            project.TargetDbConnSettings.DataSourceType = DataSourceType.MSSQL;
            project.TargetDbConnSettings.Server = "target-host";
            project.ImportCmds.Add(new ImportCmd
            {
                CmdNum = 1,
                CmdCode = "TRANSFER",
                Name = "Transfer",
                SelectQuery = "SELECT id FROM src",
                InsertQuery = "INSERT INTO dst(id) VALUES (@id)",
                StopOnError = true,
                BatchSize = 0,
                HistoryEnabled = true,
                HistoryWindowMinutes = 30,
                HistoryBatchSize = 100,
                HistoryStopOnError = false
            });

            Assert(project.Save(fileName, out string errMsg), errMsg);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            Assert(xmlDoc.SelectSingleNode("/DrvDbDataTransferProject/SourceDbConnSettings") != null,
                "SourceDbConnSettings was not saved.");
            Assert(xmlDoc.SelectSingleNode("/DrvDbDataTransferProject/TargetDbConnSettings") != null,
                "TargetDbConnSettings was not saved.");

            DrvDbDataTransferProject loaded = new DrvDbDataTransferProject();
            Assert(loaded.Load(fileName, out errMsg), errMsg);
            AssertEqual(DataSourceType.PostgreSQL, loaded.SourceDbConnSettings.DataSourceType);
            AssertEqual("source-host", loaded.SourceDbConnSettings.Server);
            AssertEqual(DataSourceType.MSSQL, loaded.TargetDbConnSettings.DataSourceType);
            AssertEqual("target-host", loaded.TargetDbConnSettings.Server);
            AssertEqual("SELECT id FROM src", loaded.ImportCmds[0].SelectQuery);
            AssertEqual("INSERT INTO dst(id) VALUES (@id)", loaded.ImportCmds[0].InsertQuery);
            AssertEqual(true, loaded.ImportCmds[0].HistoryEnabled);
            AssertEqual(30, loaded.ImportCmds[0].HistoryWindowMinutes);
            AssertEqual(100, loaded.ImportCmds[0].HistoryBatchSize);
            AssertEqual(false, loaded.ImportCmds[0].HistoryStopOnError);
        }
        finally
        {
            File.Delete(fileName);
        }
    }

    private static void HistoryCommentsParseAndWindowsStayInsideDays()
    {
        string sql = """
-- HISTORY_START=2025-11-19 23:30:00
-- HISTORY_END=2025-11-20 01:30:00
SELECT * FROM "{YYYY}{MM}{DD}"."Data"
WHERE "Time" >= {WINDOW_START}
  AND "Time" < {WINDOW_END}
""";

        Assert(HistoryQueryHelper.TryParsePeriod(sql, out DateTime startTime, out DateTime endTime, out string errMsg), errMsg);
        List<HistoryQueryWindow> windows = HistoryQueryHelper.BuildWindows(startTime, endTime, 60);

        AssertEqual(3, windows.Count);
        AssertEqual(new DateTime(2025, 11, 19, 23, 30, 0), windows[0].StartTime);
        AssertEqual(new DateTime(2025, 11, 20, 0, 0, 0), windows[0].EndTime);
        AssertEqual(new DateTime(2025, 11, 20, 0, 0, 0), windows[1].StartTime);
        AssertEqual(new DateTime(2025, 11, 20, 1, 0, 0), windows[1].EndTime);
        AssertEqual(new DateTime(2025, 11, 20, 1, 0, 0), windows[2].StartTime);
        AssertEqual(new DateTime(2025, 11, 20, 1, 30, 0), windows[2].EndTime);
    }

    private static void HistoryWindowQueryRendersDatePatternsAndWindowBounds()
    {
        string sql = """
-- HISTORY_START=2025-11-19 00:00:00
-- HISTORY_END=2025-11-19 01:00:00
SELECT * FROM "{YYYY}{MM}{DD}"."Data"
WHERE "Time" >= {WINDOW_START}
  AND "Time" < {WINDOW_END}
""";

        HistoryQueryWindow window = new HistoryQueryWindow(
            new DateTime(2025, 11, 19, 0, 0, 0),
            new DateTime(2025, 11, 19, 1, 0, 0));

        string rendered = HistoryQueryHelper.RenderWindowQuery(sql, window);
        Assert(rendered.Contains(@"""20251119"".""Data"""), rendered);
        Assert(rendered.Contains("'2025-11-19 00:00:00.000000'"), rendered);
        Assert(rendered.Contains("'2025-11-19 01:00:00.000000'"), rendered);
    }

    private static void HistoryWindowQueryRendersBareWindowPlaceholders()
    {
        string sql = """
-- HISTORY_START=2025-11-19 00:00:00
-- HISTORY_END=2025-11-19 01:00:00
SELECT * FROM public."{YYYY}{MM}{DD}.Data"
WHERE "Time" >= WINDOW_START
  AND "Time" < WINDOW_END
""";

        HistoryQueryWindow window = new HistoryQueryWindow(
            new DateTime(2025, 11, 19, 0, 0, 0),
            new DateTime(2025, 11, 19, 1, 0, 0));

        Assert(HistoryQueryHelper.HasPeriodComments(sql), "History period comments not detected.");
        string rendered = HistoryQueryHelper.RenderWindowQuery(sql, window);
        Assert(rendered.Contains(@"public.""20251119.Data"""), rendered);
        Assert(!rendered.Contains("WINDOW_START"), rendered);
        Assert(!rendered.Contains("WINDOW_END"), rendered);
    }

    private static void Assert(bool condition, string message)
    {
        if (!condition)
        {
            throw new InvalidOperationException(message);
        }
    }

    private static void AssertEqual<T>(T expected, T actual)
    {
        if (!EqualityComparer<T>.Default.Equals(expected, actual))
        {
            throw new InvalidOperationException($"Expected {expected}, actual {actual}.");
        }
    }

    private static void AssertSame(object expected, object actual)
    {
        if (!ReferenceEquals(expected, actual))
        {
            throw new InvalidOperationException("Expected the same object reference.");
        }
    }
}
