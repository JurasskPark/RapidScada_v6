# DrvDbDataTransferJP — Configuration XML

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/configuration-xml.md)

Main nodes:

```xml
<DrvDbDataTransferJPProject>
  <SourceDbConnSettings>...</SourceDbConnSettings>
  <TargetDbConnSettings>...</TargetDbConnSettings>
  <ImportCmds>
    <ImportCmd>
      <SelectQuery>...</SelectQuery>
      <InsertQuery>...</InsertQuery>
      <StopOnError>true</StopOnError>
      <BatchSize>0</BatchSize>
      <IsColumnBased>true</IsColumnBased>
      <DeviceTags>...</DeviceTags>
    </ImportCmd>
  </ImportCmds>
  <ExportCmds>...</ExportCmds>
</DrvDbDataTransferJPProject>
```
