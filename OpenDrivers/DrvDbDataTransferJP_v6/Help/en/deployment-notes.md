# DrvDbDataTransferJP — Deployment Notes

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/deployment-notes.md)

For Microsoft SQL Server the driver uses `Microsoft.Data.SqlClient`. Use the package matching the server platform:

- `DrvDbDataTransferJP_6.5.0.1_win-x64`
- `DrvDbDataTransferJP_6.5.0.1_win-x32`
- `DrvDbDataTransferJP_6.5.0.1_linux-x64`
- `DrvDbDataTransferJP_6.5.0.1_anycpu`

If the wrong package is deployed, `Microsoft.Data.SqlClient is not supported on this platform` can appear at runtime. On Windows x64, use the `win-x64` package.

Check the communication line log after deployment. It should show the expected driver version, for example:

```text
[Driver DrvDbDataTransferJP]
[Version 6.5.0.1]
```
