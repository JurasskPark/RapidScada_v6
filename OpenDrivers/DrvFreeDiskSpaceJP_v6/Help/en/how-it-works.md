# DrvFreeDiskSpaceJP — How It Works

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/how-it-works.md)

During each communication session the driver uses the loaded task list, processes enabled tasks sequentially, and writes tag values to Rapid SCADA. The default polling period returned by the driver view is 5 seconds.

For each task the driver reads `DriveInfo` for the configured `DiskName`, calculates the current free-space percentage as `AvailableFreeSpace / TotalSize * 100`, and compares it with `ProceentFreeSpace`. If the current percentage is greater than the configured threshold, `StatusAlarm` is set to `0`. If the current percentage is less than or equal to the threshold, `StatusAlarm` is set to `1` and the configured action is executed.

Automatic cleanup scans the configured `Path` recursively and processes only date-named archive folders whose names contain `MIN`, `HOUR`, or `DAY` plus a date in `yyyyMMdd` format. Folders named exactly `CUR`, `MIN`, `HOUR`, or `DAY` are skipped. Matching folders are sorted by date from oldest to newest, and the driver continues deleting or archiving folders until the free-space percentage rises above the threshold.
