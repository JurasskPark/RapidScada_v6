# DrvDbDataTransferJP — XML-конфигурация

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/configuration-xml.md)

Основные узлы XML-конфигурации:

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
