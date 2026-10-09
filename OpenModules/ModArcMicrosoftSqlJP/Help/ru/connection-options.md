# ModArcMicrosoftSqlJP — Параметры подключения

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/connection-options.md)

Конфигурация модуля по умолчанию содержит одно соединение:

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

Если нужна пользовательская строка подключения, заполните `ConnectionString`. Например:

```text
Server=192.168.150.150;Database=RapidScada;User ID=sa;Password=***;Encrypt=False;Persist Security Info=True;Trust Server Certificate=True
```
