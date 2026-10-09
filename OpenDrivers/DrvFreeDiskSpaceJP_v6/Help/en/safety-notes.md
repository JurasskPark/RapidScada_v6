# DrvFreeDiskSpaceJP — Safety Notes

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/safety-notes.md)

- `Delete` and `CompressMove` can permanently remove directories. Test every task with non-critical data before enabling it on production paths.
- The driver catches many file-operation exceptions without stopping the communication line. Check the driver log when cleanup does not produce the expected result.
- `CompressMove` requires a valid `PathTo` directory and enough permissions to create archives, move files and delete original folders.
- The driver acts with the permissions of the Rapid SCADA communication service process.
- Use unique task names because tag codes are built from the task name.
