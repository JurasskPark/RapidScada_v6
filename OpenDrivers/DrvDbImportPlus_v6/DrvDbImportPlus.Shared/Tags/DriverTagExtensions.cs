using DataTablePrettyPrinter;
using Scada.Comm.Drivers.DrvDbImportPlus;
using System.Data;
using System.Xml.Linq;

public static class DriverTagExtensions
{
    /// <summary>
    /// Convert your text into a formatted table.
    /// </summary>
    public static string ConvertTagsToTable(this List<DriverTag> tags, string title = "Tags")
    {
        if (tags == null || tags.Count == 0)
        {
            return "No tags available";
        }

        DataTable table = new DataTable(title ?? "Tags");

        table.Columns.Add("TAGNAME", typeof(string));
        table.Columns.Add("TAGVALUE", typeof(string));

        for (int i = 0; i < tags.Count; i++)
        {
            if (tags[i].Date != DateTime.MinValue)
            {
                if (!table.Columns.Contains("TAGDATETIME"))
                {
                    table.Columns.Add("TAGDATETIME", typeof(string));
                }
            }
        }
        foreach (DataColumn column in table.Columns)
        {
            switch (column.ColumnName)
            {
                case "TAGNAME":
                    column.SetColumnNameAlignment(TextAlignment.Center);
                    column.SetDataAlignment(TextAlignment.Left);
                    column.SetDataTextFormat((col, row) => row[col].ToString());
                    break;

                case "TAGVALUE":
                    column.SetColumnNameAlignment(TextAlignment.Center);
                    column.SetDataAlignment(TextAlignment.Right);
                    column.SetDataTextFormat((col, row) => row[col].ToString());
                    break;

                case "TAGDATETIME":
                    column.SetColumnNameAlignment(TextAlignment.Center);
                    column.SetDataAlignment(TextAlignment.Center);
                    column.SetDataTextFormat((col, row) => row[col].ToString());
                    break;
            }
        }

        table.SetShowTableName(true);
        table.SetShowColumnHeader(true);
        table.SetTitleTextAlignment(TextAlignment.Center);

        foreach (var tag in tags)
        {
            DataRow row = table.NewRow();

            row["TAGNAME"] = tag.Name ?? "";
            row["TAGVALUE"] = FormatTagValue(tag);

            if (tag.Date != DateTime.MinValue)
            {
                row["TAGDATETIME"] = tag.Date != DateTime.MinValue
                    ? tag.Date.ToString("dd.MM.yyyy HH:mm:ss")
                    : "";
            }

            table.Rows.Add(row);
        }

        return table.ToPrettyPrintedString();
    }

    public static string FormatTagValue(DriverTag tag)
    {
        if (tag.Val == null)
        {
            return "null";
        }

        switch (tag.Format)
        {
            case DriverTag.FormatTag.Float:
                if (tag.Val is float f)
                {
                    return tag.NumberDecimalPlaces > 0
                        ? f.ToString($"F{tag.NumberDecimalPlaces}")
                        : f.ToString();
                }
                if (tag.Val is double d)
                {
                    return tag.NumberDecimalPlaces > 0
                        ? d.ToString($"F{tag.NumberDecimalPlaces}")
                        : d.ToString();
                }
                if (tag.Val is decimal dec)
                {
                    return tag.NumberDecimalPlaces > 0
                        ? dec.ToString($"F{tag.NumberDecimalPlaces}")
                        : dec.ToString();
                }
                break;

            case DriverTag.FormatTag.DateTime:
                if (tag.Val is DateTime dt)
                {
                    return dt.ToString("dd.MM.yyyy HH:mm:ss");
                }
                break;

            case DriverTag.FormatTag.String:
                return tag.Val.ToString();

            case DriverTag.FormatTag.Integer:
                if (tag.Val is int i)
                {
                    return i.ToString();
                }
                if (tag.Val is long l)
                {
                    return l.ToString();
                }
                break;

            case DriverTag.FormatTag.Boolean:
                if (tag.Val is bool b)
                {
                    return b.ToString();
                }
                break;
        }

        return DriverUtils.NullToString(tag.Val);
    }
}