# DrvFtpJP — Действия сценариев

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/scenario-actions.md)

| Действие | Русское поведение |
| --- | --- |
| `None` | Ничего не делает. |
| `LocalCreateDirectory` | Создает локальный каталог из `LocalPath`. |
| `RemoteCreateDirectory` | Создает удаленный FTP-каталог из `RemotePath`. |
| `LocalRename` | Переименовывает локальный файл или каталог из `LocalPath` в `RemotePath`. |
| `RemoteRename` | Переименовывает удаленный FTP-объект из `LocalPath` в `RemotePath`. |
| `LocalDeleteFile` | Удаляет локальный файл, если он существует. |
| `LocalDeleteDirectory` | Рекурсивно удаляет локальный каталог, если он существует. |
| `RemoteDeleteFile` | Удаляет удаленный FTP-файл. |
| `RemoteDeleteDirectory` | Удаляет удаленный FTP-каталог. |
| `LocalUploadFile` | Загружает один локальный файл в удаленный каталог. |
| `LocalUploadDirectory` | Загружает локальный каталог в удаленный каталог и создает удаленный дочерний каталог с именем локального каталога. |
| `RemoteDownloadFile` | Скачивает один удаленный файл в локальный каталог. |
| `RemoteDownloadDirectory` | Скачивает удаленный каталог в локальный каталог и создает локальный дочерний каталог с именем удаленного каталога. |
