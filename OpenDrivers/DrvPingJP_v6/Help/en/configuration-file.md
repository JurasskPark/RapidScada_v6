# DrvPingJP — Configuration File

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/configuration-file.md)

The project file contains the ping mode, the list of device tags and debug settings. If the configuration file is missing, the driver creates a default project file.

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
