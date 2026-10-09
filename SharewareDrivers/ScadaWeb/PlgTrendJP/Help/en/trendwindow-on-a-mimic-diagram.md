# PlgTrendJP — TrendWindow on a Mimic Diagram

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/trendwindow-on-a-mimic-diagram.md)

`TrendWindow` is a compact interactive trend placed directly on a mimic diagram. The editor displays demonstration data so that appearance can be configured without archive access. At runtime it loads real data and supports tooltips, wheel zoom, drag panning, Reset Zoom and the interactive legend.

The component intentionally has no title or permanent navigation button. If `openOnClick=true`, an ordinary click opens the full TrendJP page in a new tab. Zooming, dragging, Reset Zoom and legend clicks do not open the page. In edit mode a click only selects the component.

## TrendWindow Properties

| Property | Default | Purpose |
| --- | --- | --- |
| `channelNumbers` / **Channels** | `1` | Channel list or ranges. |
| `archiveCode` / **Archive** | `Min` | Archive used by the component. |
| `periodValue` / **Period** | `1` | Positive rolling-period value. |
| `periodUnit` / **Period unit** | `h` | Seconds (`s`), minutes (`m`) or hours (`h`). |
| `preset` / **Preset** | `default` | Light `default` or `dark` theme. |
| `transparentBackground` / **Transparent background** | `true` | Shows the mimic underlay through the plot background. |
| `trendType` / **Trend type** | `line` | One of the supported trend types. |
| `showLegend` / **Show legend** | `true` | Enables or disables the legend. |
| `legendPosition` / **Legend position** | `top` | `none`, `top`, `right`, `bottom` or `left`. |
| `markerShape` / **Point marker** | `circle` | `circle`, `triangle` or `square`; also used by the tooltip. |
| `lineWidth` / **Line width** | `2` | Line width in CSS pixels. |
| `markerSize` / **Point size** | `3` | Point size in CSS pixels. |
| `autoRefresh` / **Auto refresh** | `true` | Enables periodic requests at runtime. |
| `refreshSeconds` / **Refresh, sec** | `30` | Refresh delay; `0` stops periodic refresh. |
| `openOnClick` / **Open trend page on click** | `true` | Opens the full page with matching settings. |

The legacy `periodHours` property is still accepted when an old mimic is opened. New components use `periodValue` and `periodUnit`.
