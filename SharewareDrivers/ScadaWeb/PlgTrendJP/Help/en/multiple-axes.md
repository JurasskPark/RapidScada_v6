# PlgTrendJP — Multiple Axes

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/multiple-axes.md)

`multiple-axes` creates no more than four real Y-axis groups. It does not create one axis for every channel.

Automatic grouping uses all valid values in the complete selected **From–To** period and does not change when the user zooms the chart:

1. TrendJP finds the minimum, maximum and midpoint of every channel.
2. The complete numeric range of all channels is divided into four equal high-to-low bins.
3. A channel is assigned by its midpoint. A value exactly on an internal boundary belongs to the lower bin.
4. Empty groups are removed.
5. Every visible group receives an axis covering the actual values of its channels with five-percent padding.

Axes are labelled `Y1`–`Y4`, alternate between the left and right sides and use the same grouping in the main plot, timeline and tooltip. The grid follows only the first visible axis.

An administrator can pin channels to groups through `axisGroup1`, `axisGroup2`, `axisGroup3` and `axisGroup4`. Unlisted channels remain automatic:

```text
trendType=multiple-axes&axisGroup1=1,2&axisGroup2=3-5
```

In XML:

```xml
<Args>trendType=multiple-axes&amp;axisGroup1=1,2&amp;axisGroup2=3-5</Args>
```

If a channel is listed in several groups, the lowest group number wins and the page displays a warning. Unselected or nonexistent channels are ignored. A manually listed channel number from different archive sources receives the same group. Manual grouping of channels with very different ranges can make the smaller signal look almost flat; this is the expected result of the chosen grouping.

The `axisGroup1`–`axisGroup4` settings are saved in URLs and profiles but affect only `multiple-axes`.
