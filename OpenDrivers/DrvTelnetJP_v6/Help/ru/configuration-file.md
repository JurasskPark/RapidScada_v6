# DrvTelnetJP — Файл конфигурации

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/configuration-file.md)

Имя файла конфигурации формируется по коду драйвера `DrvTelnetJP` и номеру КП. Корневой XML-элемент - `Project`.

Конфигурация содержит признак журнала, значение режима и список TCP-проверок. Текущий путь выполнения опроса вызывает один и тот же метод TCP-проверки для списка тегов; значение режима загружается и сохраняется, а при завершении линии используется только для остановки активных Telnet-задач, если режим равен `1`.

```xml
<Project>
  <Log>false</Log>
  <Mode>0</Mode>
  <DeviceTags>
    <Tag>
      <ID>...</ID>
      <Name>Web server</Name>
      <Code>WEB_80</Code>
      <IPAddress>192.168.1.10</IPAddress>
      <Port>80</Port>
      <Timeout>1000</Timeout>
      <Enable>true</Enable>
    </Tag>
  </DeviceTags>
</Project>
```
