# DrvTelnetJP — Configuration File

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/configuration-file.md)

The configuration file name is generated from the driver code `DrvTelnetJP` and the device number. The XML root element is `Project`.

The configuration contains the log flag, mode value and the list of TCP checks. The current runtime polling path calls the same TCP check method for the tag list; the mode value is loaded and saved, and is used only when terminating the communication line to stop active Telnet tasks when mode is `1`.

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
