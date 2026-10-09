# DrvDbDataTransferJP — Замечания по развертыванию

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/deployment-notes.md)

Для Microsoft SQL Server драйвер использует `Microsoft.Data.SqlClient`. Устанавливайте пакет для платформы сервера:

- `DrvDbDataTransferJP_6.5.0.1_win-x64`
- `DrvDbDataTransferJP_6.5.0.1_win-x32`
- `DrvDbDataTransferJP_6.5.0.1_linux-x64`
- `DrvDbDataTransferJP_6.5.0.1_anycpu`

Если установлен неподходящий пакет, во время работы может появиться ошибка `Microsoft.Data.SqlClient is not supported on this platform`. Для Windows x64 используйте пакет `win-x64`.

После установки проверьте журнал линии связи. В нём должна быть ожидаемая версия драйвера, например:

```text
[Driver DrvDbDataTransferJP]
[Version 6.5.0.1]
```
