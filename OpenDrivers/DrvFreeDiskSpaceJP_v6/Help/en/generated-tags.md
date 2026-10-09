# DrvFreeDiskSpaceJP — Generated Tags

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/generated-tags.md)

For every enabled task named `<TaskName>`, the driver creates the following tag codes.

| Tag code | Data |
| --- | --- |
| `DriverName_<TaskName>` | Drive name, for example `C:\`. |
| `DriverType_<TaskName>` | Drive type from .NET `DriveInfo`. |
| `DriverVolumeLabel_<TaskName>` | Volume label. |
| `DriverTotalSize_<TaskName>` | Total drive size in bytes. |
| `DriverTotalSizeString_<TaskName>` | Total drive size as a formatted string. |
| `DriverCurrentSize_<TaskName>` | Used drive size in bytes. |
| `DriverCurrentSizeString_<TaskName>` | Used drive size as a formatted string. |
| `PercentFreeSpaceSetPoint_<TaskName>` | Configured free-space threshold. |
| `PercentFreeSpaceCurrent_<TaskName>` | Current free-space percentage. |
| `StatusAlarm_<TaskName>` | `0` when free space is above the threshold, `1` when cleanup condition is active. |
| `ActionTask_<TaskName>` | Configured action name. |
| `ActionDate_<TaskName>` | UTC time when the alarm/action condition was detected or updated. |
