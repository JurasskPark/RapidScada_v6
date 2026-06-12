# DrvPingJP

![DrvPingJP](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=DrvPingJP&color=4bb60e)

![Rapid SCADA](https://img.shields.io/badge/Rapid%20SCADA-6.x-blue.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![Platform](https://img.shields.io/badge/platform-Windows%20%7C%20Linux-lightgrey.svg)
[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](https://www.apache.org/licenses/LICENSE-2.0)

## Overview / Обзор

DrvPingJP is a Rapid SCADA 6 driver for monitoring host availability by ICMP ping. The driver polls configured IP addresses or DNS host names and writes the result to Rapid SCADA input channels as an Off/On value.

The runtime driver is built for `.NET 8.0`. The configuration module uses Windows Forms and is built for `net8.0-windows`.

DrvPingJP - драйвер Rapid SCADA 6 для контроля доступности узлов по ICMP ping. Драйвер опрашивает заданные IP-адреса или DNS-имена и записывает результат в каналы Rapid SCADA в формате Выкл/Вкл.

Исполняемая часть драйвера собирается под `.NET 8.0`. Модуль настройки использует Windows Forms и собирается под `net8.0-windows`.

## Features / Возможности

- Monitoring hosts by IP address or DNS name.
- Synchronous and asynchronous ping modes.
- Individual enable flag and channel code for each tag.
- Automatic channel prototype generation from the configured tag list.
- Built-in configuration form for ScadaAdmin.
- Optional driver log output controlled by debug settings.

- Контроль узлов по IP-адресу или DNS-имени.
- Синхронный и асинхронный режимы ping.
- Индивидуальный признак включения и код канала для каждого тега.
- Автоматическое создание прототипов каналов по списку настроенных тегов.
- Встроенная форма настройки для ScadaAdmin.
- Опциональный вывод журнала драйвера через настройки отладки.

## How It Works / Как это работает

The driver does not require a communication connection to an external device. On startup, it loads the device configuration file from the Rapid SCADA configuration directory using the device number and the driver code.

During a polling session, `DevPingJPLogic` calls `DriverClient.Ping()`. The client selects the ping mode from the project settings and passes the resulting tag values back to the device logic. The device logic writes values to Rapid SCADA tags by channel code when it is specified, otherwise by tag index.

Драйверу не требуется соединение с внешним устройством. При запуске он загружает файл конфигурации КП из каталога конфигурации Rapid SCADA по номеру КП и коду драйвера.

Во время сеанса опроса `DevPingJPLogic` вызывает `DriverClient.Ping()`. Клиент выбирает режим ping из настроек проекта и возвращает полученные значения тегов в логику КП. Логика КП записывает значения в теги Rapid SCADA по коду канала, если он указан, иначе по индексу тега.

## Ping Modes / Режимы ping

| Mode / Режим | Name / Название | Description / Описание |
| --- | --- | --- |
| `0` | Synchronous / Синхронный | Runs ping processing for enabled tags and waits until all created tasks are completed. |
| `1` | Asynchronous / Асинхронный | Starts asynchronous ping operations for enabled tags and waits for all operations with `Task.WhenAll`. |

## Tag Values / Значения тегов

| Ping result / Результат ping | Value / Значение | Status / Статус | Format / Формат |
| --- | ---: | ---: | --- |
| Host is available / Узел доступен | `1` | `1` | Off/On |
| Host is unavailable or host name cannot be resolved / Узел недоступен или имя не распознано | `0` | `0` | Off/On |

The driver resolves host names before pinging when the configured address is not an IP address. If the DNS name cannot be resolved to an IP address, the tag receives value `0` and status `0`.

Драйвер выполняет разрешение имени узла перед ping, если в настройке указан не IP-адрес. Если DNS-имя не удается преобразовать в IP-адрес, тег получает значение `0` и статус `0`.

## Tag Settings / Настройки тегов

| Setting / Настройка | XML element / XML-элемент | Description / Описание |
| --- | --- | --- |
| Tag ID / Идентификатор тега | `ID` | Internal GUID of the tag. |
| Name / Имя | `Name` | Tag name shown in the configuration and used for the channel name. |
| Code / Код | `Code` | Rapid SCADA channel code used for writing values and generating channel prototypes. |
| Address / Адрес | `IPAddress` | IP address or DNS host name to ping. |
| Timeout / Таймаут | `Timeout` | Tag timeout setting saved in the project file. |
| Enabled / Включен | `Enable` | Disabled tags are skipped during polling and generated as inactive channels. |

## Channel Prototypes / Прототипы каналов

The configuration module creates one input channel prototype for each configured tag. The generated prototype uses the tag name, tag code, tag number and the `OffOn` format. The channel active state is taken from the tag `Enable` setting.

Модуль настройки создает один прототип входного канала для каждого настроенного тега. В прототип записываются имя тега, код тега, номер тега и формат `OffOn`. Активность канала берется из настройки тега `Enable`.

## Configuration File / Файл конфигурации

The project file contains the ping mode, the list of device tags and debug settings. If the configuration file is missing, the driver creates a default project file.

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

## Usage / Использование

English:

1. Add `DrvPingJP` to a communication line in ScadaAdmin.
2. Open the properties of the required device.
3. Add tags for the hosts that must be monitored.
4. Specify the tag name, channel code, IP address or DNS name, timeout and enable flag.
5. Select the ping mode: synchronous or asynchronous.
6. Generate channel prototypes for the device.
7. Upload the project and restart the communication line.
8. Check the created input channels and driver log if debugging is enabled.

Русский:

1. Добавьте `DrvPingJP` в линию связи в ScadaAdmin.
2. Откройте свойства нужного КП.
3. Добавьте теги для узлов, которые нужно контролировать.
4. Укажите имя тега, код канала, IP-адрес или DNS-имя, таймаут и признак включения.
5. Выберите режим ping: синхронный или асинхронный.
6. Создайте прототипы каналов для КП.
7. Загрузите проект и перезапустите линию связи.
8. Проверьте созданные входные каналы и журнал драйвера, если включена отладка.

## Notes / Замечания

Telecontrol commands are disabled in the driver. The driver only writes input channel values produced by ping checks.

ICMP availability depends on the operating system, firewall rules, routing and permissions. A host can be reachable by TCP or UDP while ICMP echo requests are blocked.

Команды телеуправления в драйвере отключены. Драйвер только записывает значения входных каналов, полученные по результатам ping.

Доступность ICMP зависит от операционной системы, правил межсетевого экрана, маршрутизации и прав. Узел может быть доступен по TCP или UDP, но блокировать ICMP echo-запросы.

## Project Structure / Структура проекта

| Path / Путь | Description / Описание |
| --- | --- |
| `DrvPingJP.Logic` | Runtime driver logic loaded by Rapid SCADA Communicator. |
| `DrvPingJP.View` | Windows Forms configuration module for ScadaAdmin. |
| `DrvPingJP.Shared` | Shared project model, tag model, ping logic, channel prototype generation and utilities. |
| `DrvPingJP.WinForm` | Standalone Windows Forms project used for local testing and debugging. |

## Build / Сборка

English:

1. Open `DrvPingJP.sln` in Visual Studio.
2. Restore references to Rapid SCADA assemblies if required.
3. Build the solution for Release configuration.
4. Copy the generated driver files to the corresponding Rapid SCADA driver directories.

Русский:

1. Откройте `DrvPingJP.sln` в Visual Studio.
2. При необходимости восстановите ссылки на сборки Rapid SCADA.
3. Соберите решение в конфигурации Release.
4. Скопируйте полученные файлы драйвера в соответствующие каталоги драйверов Rapid SCADA.

## Video / Видео

[![Video on YouTube](https://img.youtube.com/vi/b6Mxzdv-Q4k/0.jpg)](https://www.youtube.com/watch?v=b6Mxzdv-Q4k)

## Screenshots / Скриншоты

![DrvPingJP configuration](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/OpenDrivers/Source/DrvPingJP_001.png)

![DrvPingJP host search](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/OpenDrivers/Source/DrvPingJP_002.png)

## License / Лицензия

Licensed under the [Apache License 2.0](https://www.apache.org/licenses/LICENSE-2.0).

Распространяется по лицензии [Apache License 2.0](https://www.apache.org/licenses/LICENSE-2.0).

## SAST Tools

[PVS-Studio](https://pvs-studio.com/en/pvs-studio/)
