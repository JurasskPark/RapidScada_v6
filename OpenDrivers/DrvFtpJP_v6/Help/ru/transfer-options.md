# DrvFtpJP — Параметры передачи

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/transfer-options.md)

Передача файлов использует параметры FluentFTP, сохраненные в каждом действии. Загрузка файла на сервер использует `RemoteExistsMode`; скачивание файла использует `LocalExistsMode`. Передача каталогов дополнительно использует `Mode`, который соответствует `FtpFolderSyncMode`.

Для передачи каталогов драйвер может создавать правила FluentFTP:

- `Formats` создает белый список расширений через `FtpFileExtensionRule`.
- `MaxSizeFile` создает правило размера через `FtpSizeRule` с условием `LessThan`.
