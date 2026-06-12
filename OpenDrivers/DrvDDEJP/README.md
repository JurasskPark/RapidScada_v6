# DrvDDEJP

![DrvDDEJP](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v6/DrvDDEJP_v6.0.0.1/total)

![Rapid SCADA](https://img.shields.io/badge/Rapid%20SCADA-6.x-blue.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![Platform](https://img.shields.io/badge/platform-Windows-lightgrey.svg)
[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](https://www.apache.org/licenses/LICENSE-2.0)


**DrvDDEJP** is a Rapid SCADA communication driver for reading real-time values from Windows applications through the Dynamic Data Exchange (DDE) protocol.

**DrvDDEJP** - драйвер связи Rapid SCADA для чтения текущих значений из Windows-приложений через протокол Dynamic Data Exchange (DDE).

## Overview / Обзор

DrvDDEJP works as a DDE client. For each configured tag the driver requests a DDE value by `Service|Topic!Item`, decodes the returned text according to the selected tag format, and writes the value to Rapid SCADA device data.

The driver is configured per Rapid SCADA device. Each device uses its own XML configuration file, for example `DrvDDEJP_001.xml`. If the file does not exist, the driver creates it with default settings.

DrvDDEJP does not implement Rapid SCADA telecontrol commands. It is a read-oriented driver: `CanSendCommands` is disabled in the runtime logic.

DrvDDEJP работает как DDE-клиент. Для каждого настроенного тега драйвер запрашивает DDE-значение по адресу `Service|Topic!Item`, декодирует полученный текст согласно выбранному формату тега и записывает значение в данные КП Rapid SCADA.

Драйвер настраивается отдельно для каждого КП Rapid SCADA. У каждого КП используется собственный XML-файл конфигурации, например `DrvDDEJP_001.xml`. Если файл отсутствует, драйвер создаёт его с настройками по умолчанию.

DrvDDEJP не реализует команды телеуправления Rapid SCADA. Это драйвер чтения: в runtime-логике отключено `CanSendCommands`.

## Features / Возможности

- **DDE client mode** - reads values from applications that expose a DDE service
- **Multiple topics** - keeps separate DDE client connections per topic and reuses them between requests
- **Default topic** - tag-specific topic can be empty, in this case the project default topic is used
- **Per-tag item mapping** - each Rapid SCADA tag maps to an individual DDE item name
- **Tag ordering** - enabled tags are polled in configured order
- **Data formats** - supports `Bool`, signed and unsigned integer formats, `Float`, `Double`, `Ascii`, `Unicode` and `HexString`
- **String tags** - ASCII, Unicode and HexString formats are registered as string device tags
- **Automatic reconnect** - failed topics are disconnected and reconnected on the next request
- **Error throttling** - repeated topic errors are logged no more often than `ReconnectDelay`
- **Configurable request timeout** - DDE request timeout is configured in milliseconds
- **Channel prototype generation** - ScadaAdmin view creates Rapid SCADA channel prototypes from configured tags
- **Localization** - English and Russian language files are included
- **Sample DDE server** - the repository includes a `SampleServer` utility for DDE testing

- **Режим DDE-клиента** - чтение значений из приложений, которые предоставляют DDE-сервис
- **Несколько topic** - отдельные DDE-подключения для разных topic с повторным использованием между запросами
- **Topic по умолчанию** - если topic тега пустой, используется default topic проекта
- **Привязка item на уровне тега** - каждый тег Rapid SCADA сопоставляется с отдельным именем DDE item
- **Порядок тегов** - включённые теги опрашиваются в настроенном порядке
- **Форматы данных** - поддерживаются `Bool`, знаковые и беззнаковые integer-форматы, `Float`, `Double`, `Ascii`, `Unicode` и `HexString`
- **Строковые теги** - форматы ASCII, Unicode и HexString регистрируются как строковые теги устройства
- **Автоматическое переподключение** - при ошибке topic отключается и переподключается при следующем запросе
- **Ограничение частоты ошибок** - повторные ошибки topic пишутся в лог не чаще, чем задано в `ReconnectDelay`
- **Настраиваемый timeout запроса** - timeout DDE-запроса задаётся в миллисекундах
- **Генерация прототипов каналов** - View-модуль ScadaAdmin создаёт прототипы каналов Rapid SCADA из настроенных тегов
- **Локализация** - включены английские и русские языковые файлы
- **Тестовый DDE-сервер** - в репозитории есть утилита `SampleServer` для проверки DDE

## Configuration / Конфигурация

The driver stores device configuration in XML files named by device number:

- `DrvDDEJP.xml` for device number `0`;
- `DrvDDEJP_001.xml`, `DrvDDEJP_002.xml`, and so on for normal device numbers.

Main project settings:

| Setting | Default | Description | Описание |
|---|---:|---|---|
| `ServiceName` | `ServiceName` | DDE service name, for example an application service name | Имя DDE-сервиса, например имя сервиса приложения |
| `DefaultTopic` | `DefaultTopic` | Topic used when a tag does not define its own topic | Topic, который используется, если у тега не задан свой topic |
| `RequestTimeout` | `5000` | DDE request timeout in milliseconds, minimum `100` | Timeout DDE-запроса в миллисекундах, минимум `100` |
| `ReconnectDelay` | `2000` | Minimum interval between repeated error log messages for the same topic | Минимальный интервал между повторными сообщениями об ошибке одного topic |
| `WriteLogDriver` | `true` | Enables driver messages in the ScadaComm log | Включает сообщения драйвера в логе ScadaComm |
| `MessageTypeLogDriver` | `Action` | Rapid SCADA log message type used by the driver | Тип сообщений лога Rapid SCADA, используемый драйвером |

Драйвер хранит конфигурацию КП в XML-файлах, имя которых зависит от номера КП:

- `DrvDDEJP.xml` для номера КП `0`;
- `DrvDDEJP_001.xml`, `DrvDDEJP_002.xml` и далее для обычных номеров КП.

Основные параметры проекта приведены в таблице выше.

## DDE Addressing / Адресация DDE

A DDE request is built from three fields:

```text
ServiceName|Topic!ItemName
```

`ServiceName` is configured once for the device. `Topic` can be set for each tag; if it is empty, `DefaultTopic` is used. `ItemName` is required for every tag.

Example:

```text
Excel|Sheet1!R1C1
```

DDE-запрос строится из трёх полей:

```text
ServiceName|Topic!ItemName
```

`ServiceName` задаётся один раз для КП. `Topic` можно указать для каждого тега; если он пустой, используется `DefaultTopic`. `ItemName` обязателен для каждого тега.

Пример:

```text
Excel|Sheet1!R1C1
```

## Tag Configuration / Настройка тегов

Each tag contains:

- `Enabled` - whether the tag is polled;
- `Name` - tag name and the base for generated Rapid SCADA tag code;
- `Channel` - tag channel value stored in the configuration and used as fallback when a name is empty;
- `Topic` - optional per-tag DDE topic;
- `ItemName` - DDE item name to request;
- `DataFormat` - how the returned DDE text should be converted;
- `DataLength` - buffer/data length, important for string channels.

Tag codes are generated from the tag name by trimming it and replacing spaces with underscores. If the name is empty, the fallback code is built from channel and tag ID.

Каждый тег содержит:

- `Enabled` - опрашивается ли тег;
- `Name` - имя тега и основа для генерируемого кода тега Rapid SCADA;
- `Channel` - значение канала, сохраняемое в конфигурации и используемое как fallback при пустом имени;
- `Topic` - необязательный индивидуальный DDE topic;
- `ItemName` - имя DDE item для запроса;
- `DataFormat` - способ преобразования текста, возвращённого DDE;
- `DataLength` - длина данных, важна для строковых каналов.

Коды тегов формируются из имени тега: пробелы заменяются подчёркиваниями. Если имя пустое, fallback-код строится из канала и ID тега.

## Data Formats / Форматы данных

| Format | Runtime behavior | Поведение |
|---|---|---|
| `Bool` | Accepts `1`, `TRUE`, `ON`, `YES`, `0`, `FALSE`, `OFF`, `NO` | Принимает `1`, `TRUE`, `ON`, `YES`, `0`, `FALSE`, `OFF`, `NO` |
| `Int16`, `UInt16`, `Int32`, `UInt32`, `Int64`, `UInt64` | Parses the returned text as a number and writes it to Rapid SCADA | Разбирает возвращённый текст как число и записывает в Rapid SCADA |
| `Float`, `Double` | Parses decimal values using invariant, current and Russian-culture fallbacks | Разбирает дробные числа через invariant, текущую и русскую локаль |
| `Ascii` | Writes the returned text as ASCII string data | Записывает возвращённый текст как ASCII-строку |
| `Unicode` | Writes the returned text as Unicode string data | Записывает возвращённый текст как Unicode-строку |
| `HexString` | Writes the returned text as ASCII string data in the current runtime | В текущей runtime-логике записывает возвращённый текст как ASCII-строку |

Incoming values are trimmed and trailing `NUL`, CR and LF characters are removed before decoding.

Перед декодированием входные значения очищаются от пробелов по краям и завершающих символов `NUL`, CR и LF.

## Channel Prototypes / Прототипы каналов

ScadaAdmin generates one channel prototype for each configured tag ordered by `Order`. The prototype name is the tag name, and the tag code is generated from the tag name.

String formats use string Rapid SCADA data types: `Ascii` maps to ASCII, `Unicode` maps to Unicode. `Int64` and `UInt64` map to Int64, while other numeric and boolean formats map to Double. The prototype channel type is created as `InputOutput`, but runtime command sending is disabled.

ScadaAdmin создаёт один прототип канала для каждого настроенного тега в порядке `Order`. Имя прототипа берётся из имени тега, код тега формируется из имени тега.

Строковые форматы используют строковые типы данных Rapid SCADA: `Ascii` сопоставляется с ASCII, `Unicode` - с Unicode. `Int64` и `UInt64` сопоставляются с Int64, остальные числовые и boolean-форматы - с Double. Тип канала создаётся как `InputOutput`, но отправка команд в runtime отключена.

## Usage / Использование

English:

1. Add `DrvDDEJP` to a communication line in ScadaAdmin.
2. Open the device properties.
3. Set the DDE `ServiceName`, `DefaultTopic`, `RequestTimeout` and `ReconnectDelay`.
4. Add tags and configure `Topic`, `ItemName`, `DataFormat` and `DataLength`.
5. Generate channel prototypes for the configured device.
6. Make sure the target DDE server application is running in the Windows environment where ScadaComm can access it.
7. Upload the Rapid SCADA project and restart the communication line.

Русский:

1. Добавьте `DrvDDEJP` в линию связи в ScadaAdmin.
2. Откройте свойства КП.
3. Задайте DDE-параметры `ServiceName`, `DefaultTopic`, `RequestTimeout` и `ReconnectDelay`.
4. Добавьте теги и настройте `Topic`, `ItemName`, `DataFormat` и `DataLength`.
5. Создайте прототипы каналов для настроенного КП.
6. Убедитесь, что целевое DDE-приложение запущено в Windows-среде, где ScadaComm может получить к нему доступ.
7. Загрузите проект Rapid SCADA и перезапустите линию связи.

## Safety Notes / Замечания по безопасности

- DDE is a Windows technology. The driver view is built for `net8.0-windows`, and practical DDE operation requires Windows.
- DDE communication depends on the target application, its service name, topic syntax and Windows session/access context.
- If a topic fails, the driver disconnects the DDE client for that topic and suppresses repeated error messages until `ReconnectDelay` expires.
- Empty or invalid returned values are logged and are not written to device data.
- `RequestTimeout` should be long enough for the DDE server but short enough not to block the polling cycle for too long.
- Detailed logging writes DDE requests, responses and decoded values to the ScadaComm log. Avoid permanent detailed logging for sensitive data.
- Rapid SCADA telecontrol command sending is not implemented by the driver runtime.

- DDE - технология Windows. View-модуль собирается под `net8.0-windows`, а практическая работа DDE требует Windows.
- DDE-обмен зависит от целевого приложения, имени сервиса, синтаксиса topic и контекста Windows-сессии/доступа.
- При ошибке topic драйвер отключает DDE-клиент этого topic и подавляет повторные сообщения до истечения `ReconnectDelay`.
- Пустые или некорректные возвращённые значения логируются и не записываются в данные КП.
- `RequestTimeout` должен быть достаточным для DDE-сервера, но не слишком большим, чтобы не блокировать цикл опроса надолго.
- Подробное логирование записывает DDE-запросы, ответы и декодированные значения в лог ScadaComm. Не включайте его постоянно для чувствительных данных.
- Отправка команд телеуправления Rapid SCADA в runtime драйвера не реализована.

## Project Structure / Структура проекта

```text
DrvDDEJP/
├── DrvDDEJP.sln                    # Solution file
├── StartСompiling.bat              # Build and local deployment helper
├── README.md                       # This file
│
├── DdeNet/                         # Embedded DDE client/server library sources
├── DrvDDEJP.DDE/                   # DDE client wrapper used by the driver
├── DrvDDEJP.Logic/                 # Runtime driver for ScadaComm
├── DrvDDEJP.Shared/                # Shared configuration, tags, utilities and language helpers
├── DrvDDEJP.View/                  # ScadaAdmin configuration UI
│   ├── Forms/                      # Project and tag forms
│   └── Lang/                       # English and Russian language XML files
├── DrvDDEJP.WinForm/               # Standalone configuration/test UI host
├── Hex.Shared/                     # Shared conversion helpers
├── Libraries/                      # Rapid SCADA referenced libraries
└── SampleServer/                   # Sample DDE server utility
```

## Build / Сборка

### Prerequisites / Требования

- .NET 8.0 SDK
- Rapid SCADA 6.x libraries in `Libraries`
- Windows for DDE operation and the ScadaAdmin view

### Commands / Команды

```powershell
dotnet build .\DrvDDEJP.sln -c Release
```

The `StartСompiling.bat` script restores the solution, builds `DrvDDEJP.DDE`, publishes Logic and View to `Release\anycpu`, stops local Rapid SCADA services, copies files to `C:\Program Files\SCADA`, restarts services and opens ScadaAdmin. It must be run as Administrator.

Скрипт `StartСompiling.bat` восстанавливает решение, собирает `DrvDDEJP.DDE`, публикует Logic и View в `Release\anycpu`, останавливает локальные службы Rapid SCADA, копирует файлы в `C:\Program Files\SCADA`, перезапускает службы и открывает ScadaAdmin. Его нужно запускать от имени администратора.

## Screenshots / Скриншоты

![DrvDDEJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/Source/DrvDDEJP_001.png)

## License / Лицензия

This project is part of the Rapid SCADA ecosystem and uses the Apache 2.0 license badge.

Данный проект является частью экосистемы Rapid SCADA и использует badge лицензии Apache 2.0.

## SAST Tools

[PVS-Studio](https://pvs-studio.ru/ru/pvs-studio/?utm_source=website&utm_medium=github&utm_campaign=open_source) - static analyzer for C, C++, C#, and Java code.
