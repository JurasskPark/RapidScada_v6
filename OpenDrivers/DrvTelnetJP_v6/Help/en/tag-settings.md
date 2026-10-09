# DrvTelnetJP — Tag Settings

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/tag-settings.md)

| Setting | XML element | Description |
| --- | --- | --- |
| Tag ID | `ID` | Internal GUID of the tag. |
| Name | `Name` | Tag name used in the configuration and generated channel name. |
| Code | `Code` | Rapid SCADA channel code used for writing values and generating channel prototypes. |
| Address | `IPAddress` | IP address or DNS host name. |
| Port | `Port` | TCP port to check. |
| Timeout | `Timeout` | Connection timeout in milliseconds. |
| Enabled | `Enable` | Disabled tags are skipped during polling and generated as inactive channels. |
