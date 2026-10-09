# PlgTrendJP — Working with the Chart

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/working-with-the-chart.md)

## Zoom and Pan

- Rotate the mouse wheel over the plot to zoom the time axis around the cursor.
- Drag the plot horizontally to move through the loaded data.
- Drag the highlighted timeline window to move it.
- Drag a timeline window edge to change the visible interval.
- Click **Reset Zoom** to display the complete loaded range.

Zooming and panning use data that is already loaded. They do not change the **From** and **To** calendar fields and do not request the archive again.

## Previous and Next

The arrow buttons beside the timeline change the calendar fields first and refresh the trend only after the new range is valid. Configure their behavior in **Actions → Display settings**.

| Setting | Values | Meaning |
| --- | --- | --- |
| **Step** | Positive integer | Number of selected time units per click. |
| **Unit** | Seconds, minutes, hours, days | Unit used by the step. |
| **Shift** | Default | Previous or Next moves both boundaries and preserves the interval width. |
| **Expand** | Optional | Previous moves only **From** backward; Next moves only **To** forward. Repeated clicks expand the range. |

Example with a one-day step:

- Shift: `23.07 00:00 – 24.07 00:00` → `22.07 00:00 – 23.07 00:00`.
- Expand with Previous: `23.07 00:00 – 24.07 00:00` → `22.07 00:00 – 24.07 00:00`.

Day steps use local calendar dates. Second, minute and hour steps preserve the selected precision.

## Tooltip

In the default mode, the tooltip shows the nearest channel value. Enable **All channels** to display all available channel values for the cursor time. Every row keeps the individual channel color. The marker shape follows the selected `circle`, `square` or `triangle` setting in both tooltip modes.

## Interactive Legend

The legend of ordinary Cartesian charts can temporarily hide channels without changing the configured channel list:

- click a legend item to hide or show that series;
- `Shift+click` or double-click to show only that series;
- repeat isolation of the only visible series to restore all series.

A hidden item remains dimmed and struck through in the legend. Hidden series are excluded from the plot, tooltip and timeline. Axis ranges stay stable, and temporary visibility is not saved in the URL, last configuration or profile.
