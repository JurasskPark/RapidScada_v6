# ModArcMicrosoftSqlJP — Connection Options

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/connection-options.md)

The default module configuration contains one connection:

```xml
<ModArcMicrosoftSqlJP>
  <Connections>
    <Connection>
      <Name>MicrosoftSqlConn</Name>
      <DBMS>MSSQL</DBMS>
      <Server>localhost</Server>
      <Database>rapid_scada</Database>
      <Username>sa</Username>
      <Password />
      <ConnectionString />
    </Connection>
  </Connections>
</ModArcMicrosoftSqlJP>
```

If a custom connection string is needed, set `ConnectionString`. For example:

```text
Server=192.168.150.150;Database=RapidScada;User ID=sa;Password=***;Encrypt=False;Persist Security Info=True;Trust Server Certificate=True
```
