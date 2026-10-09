# DrvPingJP — Файл конфигурации

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/configuration-file.md)

Файл проекта содержит режим ping, список тегов КП и настройки отладки. Если файл конфигурации отсутствует, драйвер создает файл проекта с настройками по умолчанию.

```xml
<Project>
  <Mode>0</Mode>
  <DeviceTags>
    <Tag>
      <ID>...</ID>
      <Name>Server</Name>
      <Code>SERVER_PING</Code>
      <IPAddress>192.168.1.10</IPAddress>
      <Timeout>1000</Timeout>
      <Enable>true</Enable>
    </Tag>
  </DeviceTags>
  <DebugerSettings />
</Project>
```
