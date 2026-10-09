# PlgTrendJP — Configuring a Standalone View

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/configuring-a-standalone-view.md)

The plugin registers the fileless view type `TrendJP`. A configured view opens `/TrendJP?viewID=<ID>`. The `Args` field contains ordinary query-string parameters without the leading `?`.

## Data and Time Arguments

| Parameter | Values; default | Purpose |
| --- | --- | --- |
| `cnlNums` | Channel expression; empty | Input channels. |
| `archiveCode` | Archive code; `Min` | Archive in single-source mode. Alias: `archive`. |
| `startTime` | Absolute or relative local time | Start of range. Alias: `from`. |
| `endTime` | Absolute or relative local time | End of range. Alias: `to`. |
| `period` | Positive number plus `s`, `m` or `h` | Creates a range when an endpoint is omitted. |
| `hours` | Positive hours | Legacy compatibility alias for an hourly period. |

Without `startTime`, `endTime`, `period` and `hours`, TrendJP opens the complete current local day from `00:00` to the next `00:00`.

## Display and Behavior Arguments

| Parameter | Values; default | Purpose |
| --- | --- | --- |
| `theme` | `default`, `dark`; `default` | Page theme. |
| `trendType` | See trend types; `line` | Chart presentation. |
| `showLegend` | Boolean; `true` | Legacy legend switch; `false` overrides the position. |
| `legendPosition` | `none`, `top`, `right`, `bottom`, `left`; `top` | Legend position. |
| `markerShape` | `circle`, `triangle`, `square`; `circle` | Plot and tooltip marker. |
| `lineWidth` | `1`, `1.5`, `2`, `3`, `4`; `2` | Line width. |
| `markerSize` | `2`, `3`, `4`, `5`, `6`, `8`; `3` | Point size. |
| `tooltip` | `nearest`, `all`; `nearest` | Tooltip mode. |
| `refreshInterval` | `auto`, `1`, `5`, `10`, `30`, `60`; `30` | Timer cadence; does not start it alone. |
| `autoRefresh` | Boolean; `false` | Starts periodic refresh after opening. |
| `showToolbar` | Boolean; `true` | `false` hides the complete toolbar including **Actions**. |
| `showControlPanel` | Boolean; `true` | Hides filters but keeps **Actions**. |
| `showTimeline` | Boolean; `true` | Shows the timeline and Previous/Next buttons. |
| `navigationStep` | Positive integer; `1` | Previous/Next step. |
| `navigationUnit` | `s`, `m`, `h`, `d`; `d` | Seconds, minutes, hours or days. |
| `navigationMode` | `shift`, `expand`; `shift` | Moves both boundaries or expands one boundary. |
| `axisGroup1` ... `axisGroup4` | Channel expressions; empty | Manual Multiple axes assignment. Ignored by other trend types. |
| `exportLayout` | `wide`, `long`; `wide` | Default Excel layout. |
| `splitByDay` | Boolean; `false` | Creates separate daily worksheets. |

Use `true` and `false` for Boolean arguments. The general parser also accepts `1` as true and `0`, `no` or `off` as false.

## Multiple Archive Arguments

| Parameter | Values | Purpose |
| --- | --- | --- |
| `multiArchive` | Boolean; `false` | Enables multiple sources. Alias: `multi`. |
| `sourceNArchive` | Archive code, `N=1..4` | Archive of source `N`. Alias: `archiveN`. |
| `sourceNCnlNums` | Channel expression, `N=1..4` | Channels of source `N`. Alias: `cnlNumsN`. |
| `sourceNEnabled` | Boolean, `N=1..4`; enabled | Enables or disables source `N`. Alias: `enabledN`. |

Source arguments automatically enable multiple-archive mode. One embedded `TrendWindow` uses only one archive and one channel list.

## Channel Expression Syntax

Channel expressions accept commas, semicolons and spaces as separators. Ascending and descending ranges are expanded, invalid and non-positive numbers are ignored, and duplicates are removed while preserving the first occurrence.

| Expression | Result |
| --- | --- |
| `100, 200-203, 310` | `100,200,201,202,203,310` |
| `10; 12 14-16` | `10,12,14,15,16` |
| `5-2, 3, 5` | `5,4,3,2` |

The actual maximum is the positive `CountTags` value in the license. The same channel number repeated in several archive sources counts once.

## Relative Time Expressions

`startTime` and `endTime` accept an absolute local value such as `2026-07-23T00:00:00` or a case-insensitive relative expression.

| Base | Meaning |
| --- | --- |
| `NOW` | Current local time |
| `SECOND`, `MINUTE`, `HOUR` | Beginning of the current second, minute or hour |
| `DAY` | Beginning of the current day |
| `WEEK` | Monday `00:00` of the current week |
| `MONTH` | First day of the current month |
| `YEAR` | January 1 of the current year |

Offsets use `S`, `M`, `H`, `D`, `W`, `MO` and `Y`. Multiple offsets are applied from left to right. In a URL, encode a plus sign as `%2B` because an unescaped `+` is decoded as a space.

| Expression | Meaning |
| --- | --- |
| `DAY` ... `DAY%2B1D` | Complete current day |
| `DAY-1D` ... `DAY` | Complete previous day |
| `NOW-8H` ... `NOW` | Last eight hours |
| `MONTH-1MO` ... `MONTH` | Previous calendar month |

## Configuration Priority and XML

Configuration priority when a view opens:

1. Direct URL parameters.
2. Corresponding `View.Args` parameters.
3. Last configuration saved in this browser for the current `viewID`.
4. Plugin defaults.

Relative expressions are recalculated whenever the configured view is opened. In XML, replace `&` between parameters with `&amp;`.

Example:

```xml
<Args>cnlNums=101,103-108&amp;archiveCode=Min&amp;startTime=DAY&amp;endTime=DAY%2B1D&amp;trendType=line&amp;legendPosition=right&amp;navigationStep=1&amp;navigationUnit=d&amp;navigationMode=shift</Args>
```

Do not copy a numeric `ViewTypeID` from another project. Select the registered `TrendJP` view type in the Administrator because every configuration database assigns its own numeric identifier.

## Ready-to-Use Examples

Replace the example channel numbers with channels available to the current user.

Last eight hours with line markers

```xml
<Args>cnlNums=101,103-108&amp;archiveCode=Min&amp;period=8h&amp;trendType=line-markers&amp;legendPosition=top</Args>
```

Two archive sources

```xml
<Args>multiArchive=true&amp;source1Archive=Cur&amp;source1CnlNums=101,103-105&amp;source2Archive=Min&amp;source2CnlNums=101,103-108&amp;period=8h&amp;legendPosition=right</Args>
```

Live 30-second trend without controls

```xml
<Args>cnlNums=101,103&amp;archiveCode=Cur&amp;period=30s&amp;trendType=line-markers&amp;showToolbar=false&amp;showTimeline=false&amp;autoRefresh=true&amp;refreshInterval=1</Args>
```
