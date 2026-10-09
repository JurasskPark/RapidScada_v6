# ExtDepMicrosoftSqlJP — Развёртывание

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/deployment.md)

Скопируйте файлы расширения в каталог приложения Администратора:

| Источник | Назначение |
| --- | --- |
| `bin\Release\net10.0-windows\ExtDepMicrosoftSqlJP.dll` | `C:\Program Files\SCADA\ScadaAdmin\Lib\ExtDepMicrosoftSqlJP.dll` |
| `bin\Release\net10.0-windows\ExtDepMicrosoftSqlJP\*` | `C:\Program Files\SCADA\ScadaAdmin\Lib\ExtDepMicrosoftSqlJP\` |
| `Config\ExtDepMicrosoftSqlJP.xml` | `C:\Program Files\SCADA\ScadaAdmin\Config\ExtDepMicrosoftSqlJP.xml` |
| `Lang\ExtDepMicrosoftSqlJP.en-GB.xml` | `C:\Program Files\SCADA\ScadaAdmin\Lang\ExtDepMicrosoftSqlJP.en-GB.xml` |
| `Lang\ExtDepMicrosoftSqlJP.ru-RU.xml` | `C:\Program Files\SCADA\ScadaAdmin\Lang\ExtDepMicrosoftSqlJP.ru-RU.xml` |

Зарегистрируйте расширение в конфигурации Администратора:

```xml
<Extension code="ExtDepMicrosoftSqlJP" />
```
