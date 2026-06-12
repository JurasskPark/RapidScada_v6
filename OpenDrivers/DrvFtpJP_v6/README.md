# DrvFtpJP

![DrvFtpJP](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v6/DrvFtpJP_v6.4.0.5/total)

![Rapid SCADA](https://img.shields.io/badge/Rapid%20SCADA-6.x-blue.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![Platform](https://img.shields.io/badge/platform-Windows%20%7C%20Linux-lightgrey.svg)
[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](https://www.apache.org/licenses/LICENSE-2.0)

## Overview / Обзор

DrvFtpJP is a Rapid SCADA 6 driver for executing file operations on an FTP server. It is built around scenarios: a scenario contains ordered actions, and the communication session executes enabled scenarios and enabled actions sequentially.

The driver uses the FluentFTP library 52.0.0. It can upload and download files or directories, create, rename and delete local or remote objects, and log transfer progress to the Rapid SCADA communication log.

DrvFtpJP - драйвер Rapid SCADA 6 для выполнения файловых операций на FTP-сервере. Работа построена вокруг сценариев: сценарий содержит упорядоченные действия, а сеанс связи последовательно выполняет включенные сценарии и включенные действия.

Драйвер использует библиотеку FluentFTP 52.0.0. Он может загружать и скачивать файлы или каталоги, создавать, переименовывать и удалять локальные или удаленные объекты, а также записывать прогресс передачи в журнал службы связи Rapid SCADA.

## Features / Возможности

English:

- FTP connection settings are edited in ScadaAdmin: name, host, user name, password, port and connection type.
- The configuration tool includes a file manager for browsing local and remote directories.
- Multiple scenarios can be configured for one device.
- Each scenario can contain multiple ordered actions.
- Directory transfers support synchronization mode, overwrite/skip behavior, verification options, file extension filters and maximum file size filters.
- Transfer progress is logged with local path, remote path, speed, transferred size and upload percentage.
- The driver does not generate Rapid SCADA channels in the current implementation; it performs actions and writes logs.

Русский:

- Параметры FTP-подключения настраиваются в ScadaAdmin: имя, хост, пользователь, пароль, порт и тип подключения.
- В утилите настройки есть файловый менеджер для просмотра локальных и удаленных каталогов.
- Для одного КП можно настроить несколько сценариев.
- Каждый сценарий может содержать несколько упорядоченных действий.
- Передача каталогов поддерживает режим синхронизации, поведение при существующих файлах, параметры проверки, фильтры по расширениям и фильтр максимального размера файла.
- Прогресс передачи записывается в журнал с локальным путем, удаленным путем, скоростью, переданным объемом и процентом загрузки.
- В текущей реализации драйвер не создает каналы Rapid SCADA; он выполняет действия и пишет журнал.

## How It Works / Как это работает

English:

On communication line start the driver loads `DrvFtpJP_<device number>.xml` from the Rapid SCADA configuration directory and initializes a `DriverClient` with FTP connection settings and scenarios.

During each polling session the driver creates a FluentFTP client, connects to the configured FTP server, executes all enabled scenarios, disconnects and disposes the client. The default polling period returned by the View part is 5 seconds.

Scenarios are executed in the order stored in the configuration. Inside each enabled scenario, actions are also executed in order. Disabled scenarios and disabled actions are skipped.

Русский:

При запуске линии связи драйвер загружает `DrvFtpJP_<номер КП>.xml` из каталога конфигурации Rapid SCADA и инициализирует `DriverClient` с параметрами FTP-подключения и сценариями.

Во время каждого сеанса опроса драйвер создает клиент FluentFTP, подключается к заданному FTP-серверу, выполняет все включенные сценарии, отключается и освобождает клиент. Период опроса по умолчанию, который возвращает View-часть, равен 5 секундам.

Сценарии выполняются в порядке, сохраненном в конфигурации. Внутри каждого включенного сценария действия также выполняются по порядку. Отключенные сценарии и отключенные действия пропускаются.

## FTP Settings / Настройки FTP

| Setting | English | Русский |
|---|---|---|
| `Name` | Connection profile name displayed in the configuration UI. | Имя профиля подключения, отображаемое в интерфейсе настройки. |
| `Host` | FTP server host. Default value in code is `127.0.0.1`. | Хост FTP-сервера. Значение по умолчанию в коде: `127.0.0.1`. |
| `Username` | FTP user name. | Имя пользователя FTP. |
| `Password` | FTP password stored in the XML configuration using `ScadaUtils.Encrypt`. | Пароль FTP, сохраняемый в XML-конфигурации через `ScadaUtils.Encrypt`. |
| `Port` | FTP port. The UI uses port `21` when the default port option is enabled. | Порт FTP. Интерфейс использует порт `21`, если включен порт по умолчанию. |
| `FtpDataType` | Data connection type selected in the UI and applied to `client.Config.DataConnectionType`: passive, passive without route info, or active. | Тип соединения данных, выбранный в интерфейсе и применяемый к `client.Config.DataConnectionType`: пассивный, пассивный без учета маршрутизации или активный. |
| `EncryptionMode` | FluentFTP encryption mode used by runtime connection code. | Режим шифрования FluentFTP, используемый кодом подключения во время работы. |
| `Encryption` | UI checkbox value for TLS. When enabled, the settings form writes `EncryptionMode = Explicit`; when disabled, it writes `EncryptionMode = None`. | Значение флажка TLS в интерфейсе. Если включен, форма настройки записывает `EncryptionMode = Explicit`; если выключен, записывает `EncryptionMode = None`. |
| `SshKey` | SSH key text can be stored by the UI, but FTP runtime connection code does not use it directly. | Текст SSH-ключа может сохраняться интерфейсом, но runtime-код FTP-подключения напрямую его не использует. |

## Scenario Actions / Действия сценариев

| Action | English behavior | Русское поведение |
|---|---|---|
| `None` | Does nothing. | Ничего не делает. |
| `LocalCreateDirectory` | Creates a local directory from `LocalPath`. | Создает локальный каталог из `LocalPath`. |
| `RemoteCreateDirectory` | Creates a remote FTP directory from `RemotePath`. | Создает удаленный FTP-каталог из `RemotePath`. |
| `LocalRename` | Renames a local file or directory from `LocalPath` to `RemotePath`. | Переименовывает локальный файл или каталог из `LocalPath` в `RemotePath`. |
| `RemoteRename` | Renames a remote FTP object from `LocalPath` to `RemotePath`. | Переименовывает удаленный FTP-объект из `LocalPath` в `RemotePath`. |
| `LocalDeleteFile` | Deletes a local file if it exists. | Удаляет локальный файл, если он существует. |
| `LocalDeleteDirectory` | Deletes a local directory recursively if it exists. | Рекурсивно удаляет локальный каталог, если он существует. |
| `RemoteDeleteFile` | Deletes a remote FTP file. | Удаляет удаленный FTP-файл. |
| `RemoteDeleteDirectory` | Deletes a remote FTP directory. | Удаляет удаленный FTP-каталог. |
| `LocalUploadFile` | Uploads one local file to a remote directory. | Загружает один локальный файл в удаленный каталог. |
| `LocalUploadDirectory` | Uploads a local directory to a remote directory and creates a remote child directory with the local directory name. | Загружает локальный каталог в удаленный каталог и создает удаленный дочерний каталог с именем локального каталога. |
| `RemoteDownloadFile` | Downloads one remote file to a local directory. | Скачивает один удаленный файл в локальный каталог. |
| `RemoteDownloadDirectory` | Downloads a remote directory to a local directory and creates a local child directory with the remote directory name. | Скачивает удаленный каталог в локальный каталог и создает локальный дочерний каталог с именем удаленного каталога. |

## Transfer Options / Параметры передачи

English:

File transfers use FluentFTP options stored in each action. File upload uses `RemoteExistsMode`; file download uses `LocalExistsMode`. Directory upload and download also use `Mode`, which maps to `FtpFolderSyncMode`.

For directory transfers the driver can build FluentFTP rules:

- `Formats` creates an extension whitelist using `FtpFileExtensionRule`.
- `MaxSizeFile` creates a size rule using `FtpSizeRule` with `LessThan`.

Русский:

Передача файлов использует параметры FluentFTP, сохраненные в каждом действии. Загрузка файла на сервер использует `RemoteExistsMode`; скачивание файла использует `LocalExistsMode`. Передача каталогов дополнительно использует `Mode`, который соответствует `FtpFolderSyncMode`.

Для передачи каталогов драйвер может создавать правила FluentFTP:

- `Formats` создает белый список расширений через `FtpFileExtensionRule`.
- `MaxSizeFile` создает правило размера через `FtpSizeRule` с условием `LessThan`.

## Configuration / Настройка

English:

1. Add `DrvFtpJP` to a communication line in ScadaAdmin.
2. Open the device properties.
3. Configure FTP connection settings and run the connection test.
4. Add one or more scenarios.
5. Add actions to each scenario and put them in the required order.
6. For upload and download actions, configure local path, remote path, existing-file behavior and transfer filters if needed.
7. Save the configuration, upload the project and restart the communication line.

Русский:

1. Добавьте `DrvFtpJP` в линию связи в ScadaAdmin.
2. Откройте свойства КП.
3. Настройте FTP-подключение и выполните проверку соединения.
4. Добавьте один или несколько сценариев.
5. Добавьте действия в каждый сценарий и расположите их в нужном порядке.
6. Для действий загрузки и скачивания задайте локальный путь, удаленный путь, поведение при существующих файлах и фильтры передачи при необходимости.
7. Сохраните конфигурацию, загрузите проект и перезапустите линию связи.

## Configuration File / Файл конфигурации

English:

The driver stores settings in the Rapid SCADA configuration directory. The file name is generated from the driver code and device number, for example `DrvFtpJP_001.xml`.

The main XML blocks are:

- `FtpClientSettings` - FTP connection profile.
- `Scenarios` - ordered scenario list.
- `Actions` - ordered action list inside a scenario.
- `DeviceTags` - saved tag metadata, although runtime channel prototypes are not generated in the current code.
- `DebugerSettings` - log path, log writing flag and log retention days.
- `LanguageIsRussian` - language flag used by the configuration tool.

Русский:

Драйвер хранит настройки в каталоге конфигурации Rapid SCADA. Имя файла формируется по коду драйвера и номеру КП, например `DrvFtpJP_001.xml`.

Основные XML-блоки:

- `FtpClientSettings` - профиль FTP-подключения.
- `Scenarios` - упорядоченный список сценариев.
- `Actions` - упорядоченный список действий внутри сценария.
- `DeviceTags` - сохраненные метаданные тегов, хотя в текущем коде runtime-прототипы каналов не создаются.
- `DebugerSettings` - путь журнала, признак записи журнала и срок хранения.
- `LanguageIsRussian` - признак языка, используемый утилитой настройки.

## Safety Notes / Замечания по безопасности

English:

- The driver can delete local files, local directories, remote FTP files and remote FTP directories. Test scenarios on non-critical paths before enabling them in production.
- Passwords are encrypted before saving by `ScadaUtils.Encrypt` and decrypted during loading by `ScadaUtils.Decrypt`.
- The runtime connection sets `ValidateAnyCertificate` to `false`; FTPS certificate validation depends on the certificate being accepted by the environment and FluentFTP.
- `CanSendCommands` is disabled. The driver does not process Rapid SCADA telecontrol commands.
- If a connection attempt fails, the session is marked unsuccessful and data is invalidated, but there are no generated channels in the current implementation.

Русский:

- Драйвер может удалять локальные файлы, локальные каталоги, удаленные FTP-файлы и удаленные FTP-каталоги. Проверяйте сценарии на некритичных путях перед включением в рабочей системе.
- Пароли шифруются перед сохранением через `ScadaUtils.Encrypt` и расшифровываются при загрузке через `ScadaUtils.Decrypt`.
- Runtime-подключение устанавливает `ValidateAnyCertificate` в `false`; проверка сертификата FTPS зависит от того, принят ли сертификат окружением и FluentFTP.
- `CanSendCommands` отключен. Драйвер не обрабатывает команды телеуправления Rapid SCADA.
- Если подключение завершилось ошибкой, сеанс помечается как неуспешный и данные инвалидируются, но в текущей реализации генерируемых каналов нет.

## Project Structure / Структура проекта

| Path | English | Русский |
|---|---|---|
| `DrvFtpJP.Logic` | Runtime driver logic loaded by `ScadaComm`. | Исполняемая логика драйвера, загружаемая `ScadaComm`. |
| `DrvFtpJP.View` | Windows Forms configuration UI for ScadaAdmin. | Windows Forms-интерфейс настройки для ScadaAdmin. |
| `DrvFtpJP.Shared` | Shared settings, scenario model, FTP client wrapper and helper code. | Общие настройки, модель сценариев, обертка FTP-клиента и служебный код. |
| `DrvFtpJP.Winform` | Standalone WinForms host used for development and testing. | Отдельная WinForms-обертка для разработки и проверки. |
| `DrvFtpJP.Console` | Console project used for development experiments. | Консольный проект для экспериментов при разработке. |
| `FluentFTP` | Bundled FluentFTP source project. | Встроенный исходный проект FluentFTP. |
| `StartСompilingDrvFtpJP.bat` | Publishes the driver to the release layout and copies files to a local SCADA installation. | Публикует драйвер в релизную структуру и копирует файлы в локальную установку SCADA. |

## Build / Сборка

English:

The solution targets .NET 8. The Logic project uses `net8.0`, and the View and WinForms projects use `net8.0-windows`. The build script publishes an AnyCPU release layout for `ScadaComm\Drv`, `ScadaAdmin\Lib` and the standalone application.

Русский:

Решение рассчитано на .NET 8. Проект Logic использует `net8.0`, а проекты View и WinForms используют `net8.0-windows`. Скрипт сборки публикует AnyCPU-релиз для `ScadaComm\Drv`, `ScadaAdmin\Lib` и отдельного приложения.

## Screenshots / Скриншоты

![DrvFtpJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/OpenDrivers/Source/DrvFtpJP_001.png)

![DrvFtpJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/OpenDrivers/Source/DrvFtpJP_002.png)

![DrvFtpJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/OpenDrivers/Source/DrvFtpJP_003.png)

![DrvFtpJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/OpenDrivers/Source/DrvFtpJP_004.png)

## License / Лицензия

This project is part of the Rapid SCADA ecosystem and is distributed under the Apache License 2.0.

Данный проект является частью экосистемы Rapid SCADA и распространяется под лицензией Apache License 2.0.

The bundled FluentFTP library is distributed under the MIT License.

Встроенная библиотека FluentFTP распространяется под лицензией MIT.

## SAST Tools

[PVS-Studio](https://pvs-studio.ru/ru/pvs-studio/?utm_source=website&utm_medium=github&utm_campaign=open_source) - static analyzer for C, C++, C#, and Java code.
