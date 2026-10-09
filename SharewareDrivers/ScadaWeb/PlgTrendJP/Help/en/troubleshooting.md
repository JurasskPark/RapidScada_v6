# PlgTrendJP — Troubleshooting

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/troubleshooting.md)

| Symptom | Cause and action |
| --- | --- |
| TrendJP is absent from Webstation | Check plugin registration, deployed files and restart SCADA Web. |
| Standard chart action opens another plugin | Set `<ChartFeature>PlgTrendJP</ChartFeature>`. |
| License error | Check the file name, license directory, `AppName=PlgTrendJP`, positive `CountTags` and restart the host. |
| Channel limit exceeded | Reduce the number of unique channel numbers or use a suitable license. |
| Archive list is empty | Check the project archive configuration. |
| Selected channel has no data | Check that the channel belongs to the selected archive and that its quality is good. |
| Gaps appear in a line | Missing, invalid and bad-quality points are intentionally not connected. |
| One Multiple axes line looks flat | Check manual axis groups. Strongly different ranges assigned to one group share one scale. |
| Previous/Next uses an unexpected interval | Open **Display settings** and check step, unit and shift/expand mode. |
| A hidden series does not return | Click its dimmed legend item or isolate the only visible series again. |
| A profile is missing on another workstation | Profiles are browser-local. Configure shared defaults in `View.Args`. |
| Relative expression containing `+` fails | Encode plus as `%2B`, for example `DAY%2B1D`. |
| Automatic refresh does not start | Click **Start** or configure `autoRefresh=true`; `refreshInterval` only selects the cadence. |
| No controls are visible | `showToolbar=false` hides **Actions** too. Use `showControlPanel=false` to keep **Actions**. |
| Excel export is rejected | Load the trend first, keep the page open and check the point and licensed channel limits. |
