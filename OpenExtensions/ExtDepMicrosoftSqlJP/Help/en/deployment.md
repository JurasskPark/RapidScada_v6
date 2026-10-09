# ExtDepMicrosoftSqlJP — Deployment

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/deployment.md)

Copy the extension files to the Administrator application directory:

| Source | Target |
| --- | --- |
| `bin\Release\net10.0-windows\ExtDepMicrosoftSqlJP.dll` | `C:\Program Files\SCADA\ScadaAdmin\Lib\ExtDepMicrosoftSqlJP.dll` |
| `bin\Release\net10.0-windows\ExtDepMicrosoftSqlJP\*` | `C:\Program Files\SCADA\ScadaAdmin\Lib\ExtDepMicrosoftSqlJP\` |
| `Config\ExtDepMicrosoftSqlJP.xml` | `C:\Program Files\SCADA\ScadaAdmin\Config\ExtDepMicrosoftSqlJP.xml` |
| `Lang\ExtDepMicrosoftSqlJP.en-GB.xml` | `C:\Program Files\SCADA\ScadaAdmin\Lang\ExtDepMicrosoftSqlJP.en-GB.xml` |
| `Lang\ExtDepMicrosoftSqlJP.ru-RU.xml` | `C:\Program Files\SCADA\ScadaAdmin\Lang\ExtDepMicrosoftSqlJP.ru-RU.xml` |

Register the extension in the Administrator configuration:

```xml
<Extension code="ExtDepMicrosoftSqlJP" />
```
