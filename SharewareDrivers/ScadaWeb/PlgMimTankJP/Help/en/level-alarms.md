# PlgMimTankJP — Level Alarms

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/level-alarms.md)

Default thresholds are `LL = 5%`, `L = 15%`, `H = 85%` and `HH = 95%`. Threshold order is always normalized:

```text
0 ≤ LL ≤ L ≤ H ≤ HH ≤ useful height
```

When `alarmScale` changes between `Percent` and `Meters`, the alternate set is recalculated automatically. The property editor shows the selected unit set.

Without an external channel, `LL` and `L` are active when the useful total is at or below the threshold; `H` and `HH` are active when it is at or above the threshold.

Each alarm can use an optional external discrete channel:

| External channel data | Alarm state |
| --- | --- |
| Good quality, value `0` | Inactive |
| Good quality, nonzero value | Active |
| Missing or bad quality | Unknown, with no calculated fallback |

An external channel is authoritative whenever its number is positive. In runtime only active alarm markers are visible; inactive and unknown alarms do not leave gray placeholders. Edit mode displays alarm markers as a layout preview.
