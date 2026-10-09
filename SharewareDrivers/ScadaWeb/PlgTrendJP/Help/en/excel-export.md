# PlgTrendJP — Excel Export

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/excel-export.md)

1. Load the required trend first.
2. Open **Actions → Export Excel**.
3. Select **Wide** or **Long** layout.
4. Enable **Split by day** if separate daily worksheets are required.
5. Start the export and keep the page open until the download is ready.

| Option | Result |
| --- | --- |
| **Wide** | One shared time column plus value and quality columns for every channel. |
| **Long** | Consecutive date/time, tag, value and quality rows. |
| **Split by day** | One `yyyy-MM-dd` worksheet for every local calendar day. |
| **Summary** | Archive, channel, tag, point count, minimum, maximum and average of good-quality values. |

The first row of every worksheet is frozen. Microsoft Excel is not required on the SCADA Web server. One export is limited to 10,000,000 points and to the licensed number of unique channels.
