# PlgTrendJP — Automatic Refresh

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/automatic-refresh.md)

Select a timer interval and click **Start**. Click **Stop** to disable periodic requests. The timer is not started merely by selecting an interval unless `autoRefresh=true` is configured.

`Auto` uses a short interval for current data and gradually increases it to at most five seconds while values remain unchanged. Historical archives use intervals appropriate to their resolution. Explicit `1`, `5`, `10`, `30` and `60` second modes use the selected cadence.

A manually zoomed time window is preserved during refresh. A window attached to the newest edge follows incoming current data.
