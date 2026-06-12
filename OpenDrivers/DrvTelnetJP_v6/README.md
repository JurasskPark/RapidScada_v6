# DrvTelnetJP

![DrvTelnetJP](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v6/DrvTelnetJP_v6.3.0.0/total)
![DrvTelnetJP](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v6/DrvTelnetJP_v6.0.0.1/total)
![DrvTelnetJP](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v6/DrvTelnetJP_v6.0.0.0/total)

![Rapid SCADA](https://img.shields.io/badge/Rapid%20SCADA-6.x-blue.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![Platform](https://img.shields.io/badge/platform-Windows%20%7C%20Linux-lightgrey.svg)
[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](https://www.apache.org/licenses/LICENSE-2.0)

## Overview / Обзор

DrvTelnetJP is a Rapid SCADA 6 driver for checking TCP port availability. The driver tries to open a TCP connection to each configured host and port, then writes the result to Rapid SCADA input channels as an Off/On value.

The runtime driver is built for `.NET 8.0`. The configuration module uses Windows Forms and is built for `net8.0-windows`.

DrvTelnetJP - драйвер Rapid SCADA 6 для проверки доступности TCP-портов. Драйвер пытается открыть TCP-соединение с каждым настроенным узлом и портом, затем записывает результат в каналы Rapid SCADA в формате Выкл/Вкл.

Исполняемая часть драйвера собирается под `.NET 8.0`. Модуль настройки использует Windows Forms и собирается под `net8.0-windows`.

## Features / Возможности

- TCP port availability checks by IP address or DNS host name.
- Separate host, port, timeout, channel code and enable flag for each tag.
- Automatic channel prototype generation from the tag list.
- Default polling period of 1 second in the configuration view.
- Optional driver log output.
- Built-in Windows Forms configuration dialog for ScadaAdmin.

- Проверка доступности TCP-портов по IP-адресу или DNS-имени.
- Отдельный узел, порт, таймаут, код канала и признак включения для каждого тега.
- Автоматическое создание прототипов каналов по списку тегов.
- Период опроса по умолчанию 1 секунда в модуле настройки.
- Опциональный вывод журнала драйвера.
- Встроенное окно настройки Windows Forms для ScadaAdmin.

## How It Works / Как это работает

The driver does not require a persistent communication connection to an external device. During each polling session, `DevTelnetJPLogic` calls `NetworkInformation.RunTelnet()` and passes the configured tag list.

For every enabled tag, the driver resolves the host name to an IP address if the configured address is not already an IP address. Then it creates a TCP socket and starts an asynchronous connection attempt to the configured `IP:Port`. If the connection is established before the tag timeout expires, the tag is treated as open. If the timeout expires, the tag is treated as closed.

Драйверу не требуется постоянное соединение с внешним устройством. Во время каждого сеанса опроса `DevTelnetJPLogic` вызывает `NetworkInformation.RunTelnet()` и передает список настроенных тегов.

Для каждого включенного тега драйвер преобразует имя узла в IP-адрес, если в настройке указан не IP-адрес. Затем создается TCP-сокет и запускается асинхронная попытка подключения к настроенному `IP:Port`. Если соединение установлено до истечения таймаута тега, порт считается открытым. Если таймаут истек, порт считается закрытым.

## Tag Values / Значения тегов

| Check result / Результат проверки | Value / Значение | Status / Статус | Format / Формат |
| --- | ---: | ---: | --- |
| TCP port is open / TCP-порт открыт | `1` | `1` | Off/On |
| TCP port is closed or connection timed out / TCP-порт закрыт или истек таймаут подключения | `0` | `1` | Off/On |
| Host name cannot be resolved / Имя узла не удалось распознать | `0` | `0` | Off/On |

The driver writes values by channel code when `TagCode` is specified. If the code is empty, values are written by the tag index.

Драйвер записывает значения по коду канала, если указан `TagCode`. Если код пустой, значения записываются по индексу тега.

## Tag Settings / Настройки тегов

| Setting / Настройка | XML element / XML-элемент | Description / Описание |
| --- | --- | --- |
| Tag ID / Идентификатор тега | `ID` | Internal GUID of the tag. |
| Name / Имя | `Name` | Tag name used in the configuration and generated channel name. |
| Code / Код | `Code` | Rapid SCADA channel code used for writing values and generating channel prototypes. |
| Address / Адрес | `IPAddress` | IP address or DNS host name. |
| Port / Порт | `Port` | TCP port to check. |
| Timeout / Таймаут | `Timeout` | Connection timeout in milliseconds. |
| Enabled / Включен | `Enable` | Disabled tags are skipped during polling and generated as inactive channels. |

## Channel Prototypes / Прототипы каналов

The driver creates one input channel prototype for each configured tag. The prototype uses the tag code, tag name and `OffOn` format. The channel active state is copied from the tag `Enable` setting.

Драйвер создает один прототип входного канала для каждого настроенного тега. В прототип записываются код тега, имя тега и формат `OffOn`. Активность канала копируется из настройки тега `Enable`.

## Configuration File / Файл конфигурации

The configuration file name is generated from the driver code `DrvTelnetJP` and the device number. The XML root element is `DrvTelnetJPConfig`.

The configuration contains the log flag, mode value and the list of TCP checks. The current runtime polling path calls the same TCP check method for the tag list; the mode value is loaded and saved, and is used only when terminating the communication line to stop active Telnet tasks when mode is `1`.

Имя файла конфигурации формируется по коду драйвера `DrvTelnetJP` и номеру КП. Корневой XML-элемент - `DrvTelnetJPConfig`.

Конфигурация содержит признак журнала, значение режима и список TCP-проверок. Текущий путь выполнения опроса вызывает один и тот же метод TCP-проверки для списка тегов; значение режима загружается и сохраняется, а при завершении линии используется только для остановки активных Telnet-задач, если режим равен `1`.

```xml
<DrvTelnetJPConfig>
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
</DrvTelnetJPConfig>
```

## Usage / Использование

English:

1. Add `DrvTelnetJP` to a communication line in ScadaAdmin.
2. Open the properties of the required device.
3. Add tags for the TCP ports that must be monitored.
4. Specify the tag name, channel code, IP address or DNS name, TCP port, timeout and enable flag.
5. Save the driver configuration.
6. Generate channel prototypes for the device.
7. Upload the project and restart the communication line.
8. Check the generated input channels and the driver log if logging is enabled.

Русский:

1. Добавьте `DrvTelnetJP` в линию связи в ScadaAdmin.
2. Откройте свойства нужного КП.
3. Добавьте теги для TCP-портов, которые нужно контролировать.
4. Укажите имя тега, код канала, IP-адрес или DNS-имя, TCP-порт, таймаут и признак включения.
5. Сохраните конфигурацию драйвера.
6. Создайте прототипы каналов для КП.
7. Загрузите проект и перезапустите линию связи.
8. Проверьте созданные входные каналы и журнал драйвера, если журнал включен.

## Notes / Замечания

Telecontrol commands are enabled at the `DeviceLogic` level, but this driver does not implement a command processing method. The documented and implemented behavior is TCP port polling and writing input channel values.

A closed result means that a TCP connection was not established within the configured timeout. It can be caused by a closed service port, firewall rules, routing problems, DNS problems, or a service that accepts connections slowly.

Команды телеуправления включены на уровне `DeviceLogic`, но драйвер не реализует метод обработки команд. Описанное и реализованное поведение - опрос TCP-портов и запись значений входных каналов.

Результат закрытого порта означает, что TCP-соединение не было установлено за настроенный таймаут. Причиной может быть закрытый порт службы, правила межсетевого экрана, проблемы маршрутизации, проблемы DNS или служба, которая медленно принимает соединения.

## Project Structure / Структура проекта

| Path / Путь | Description / Описание |
| --- | --- |
| `DrvTelnetJP.Logic` | Runtime driver logic loaded by Rapid SCADA Communicator. |
| `DrvTelnetJP.View` | Windows Forms configuration module for ScadaAdmin. |
| `DrvTelnetJP.Shared` | Shared configuration model, tag model, TCP check logic, channel prototype generation and utilities. |
| `DrvTelnetJP.WinForm` | Standalone Windows Forms project used for local testing and debugging. |

## Build / Сборка

English:

1. Open `DrvTelnetJP.sln` in Visual Studio.
2. Restore references to Rapid SCADA assemblies if required.
3. Build the solution for Release configuration.
4. Copy the generated driver files to the corresponding Rapid SCADA driver directories.

Русский:

1. Откройте `DrvTelnetJP.sln` в Visual Studio.
2. При необходимости восстановите ссылки на сборки Rapid SCADA.
3. Соберите решение в конфигурации Release.
4. Скопируйте полученные файлы драйвера в соответствующие каталоги драйверов Rapid SCADA.

## Video / Видео

[![Video on YouTube](https://img.youtube.com/vi/TGCN7rIGmP8/0.jpg)](https://www.youtube.com/watch?v=TGCN7rIGmP8)

## Screenshots / Скриншоты

![DrvTelnetJP configuration](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/OpenDrivers/Source/DrvTelnetJP_001.png)

## License / Лицензия

Licensed under the [Apache License 2.0](https://www.apache.org/licenses/LICENSE-2.0).

Распространяется по лицензии [Apache License 2.0](https://www.apache.org/licenses/LICENSE-2.0).

## SAST Tools

[PVS-Studio](https://pvs-studio.com/en/pvs-studio/)
