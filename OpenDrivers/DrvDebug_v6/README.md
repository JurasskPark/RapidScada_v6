# DrvDebug

![DrvDebug](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=DrvDebug&color=4bb60e)

![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=8.0&color=purple)
![Platform](https://jurasskpark.ru/service/budges/?label=platform&message=Windows&color=lightgrey)
[![License](https://jurasskpark.ru/service/budges/?label=license&message=Apache%202.0&color=blue)](https://www.apache.org/licenses/LICENSE-2.0)


**DrvDebug** is a Rapid SCADA communication driver for testing byte-array protocols, transport channels, packet decoding, command sending and simulated tag values.

**DrvDebug** - драйвер связи Rapid SCADA для проверки байтовых протоколов, каналов связи, декодирования пакетов, отправки команд и симуляции значений тегов.

## Overview / Обзор

DrvDebug is not tied to one field protocol. It works with raw byte arrays and can be used as a diagnostic driver when developing, testing or troubleshooting communication with external devices and services.

The driver supports `Master`, `Slave` and `Mixed` channel behavior. In master and mixed modes it can send configured command payloads and read responses. In slave and mixed modes it can receive incoming packets, decode them into tags and optionally send a configured response payload.

DrvDebug can also generate values without incoming data. Simulation can be used for test projects, UI checks, channel prototype checks and fallback values when decoding fails.

DrvDebug не привязан к одному промышленному протоколу. Он работает с сырыми массивами байт и подходит как диагностический драйвер при разработке, тестировании и поиске проблем обмена с внешними устройствами и сервисами.

Драйвер поддерживает режимы линии `Master`, `Slave` и `Mixed`. В режимах master и mixed он может отправлять настроенные payload-команды и читать ответы. В режимах slave и mixed он может принимать входящие пакеты, декодировать их в теги и при необходимости отправлять настроенный ответ.

DrvDebug также может генерировать значения без входящих данных. Симуляция полезна для тестовых проектов, проверки интерфейса, проверки прототипов каналов и резервных значений при ошибке декодирования.

## Features / Возможности

- **Protocol-neutral byte arrays** - sends and receives raw bytes instead of implementing a fixed protocol
- **Master, Slave and Mixed modes** - supports all Rapid SCADA channel behavior modes
- **Configurable command list** - sends enabled commands in configured order
- **Command payload formats** - sends `Ascii` and `Unicode` as text encodings; other configured command data kinds are sent as HEX bytes by the current runtime
- **Incoming packet stop condition** - detects packet completion by marker or by length field
- **Tag byte decoding** - reads tag data from configured byte offsets and lengths
- **Byte order support** - supports little endian, big endian and mixed byte orders `1032` and `2301`
- **Value scaling** - applies coefficient, offset and precision to decoded numeric values
- **Simulation engine** - supports ramp, sawtooth, sine, square, string list and string template generation
- **Fallback simulation** - `DecodeAndSimulate` tags can simulate a value if decoding fails
- **Telecontrol commands** - supports `SendStr` and `SendBin` command codes for direct output through the current connection
- **Channel prototypes** - generates Rapid SCADA channel prototypes from configured tags
- **Transport logging** - can write sent and received byte dumps to the communication line log
- **Standalone editor host** - includes a WinForms host for opening the configuration UI outside ScadaAdmin

- **Протокольно-независимые массивы байт** - отправка и приём сырых байт без жёсткой привязки к одному протоколу
- **Режимы Master, Slave и Mixed** - поддержка всех режимов поведения канала Rapid SCADA
- **Настраиваемый список команд** - отправка включённых команд в заданном порядке
- **Форматы payload команд** - `Ascii` и `Unicode` отправляются как текстовые кодировки; остальные типы данных команд в текущем runtime отправляются как HEX-байты
- **Условие завершения входящего пакета** - определение конца пакета по маркеру или полю длины
- **Декодирование байтов в теги** - чтение данных тега из заданного смещения и длины массива
- **Порядок байт** - поддержка little endian, big endian и смешанных порядков `1032` и `2301`
- **Масштабирование значений** - применение коэффициента, смещения и точности к числовым значениям
- **Механизм симуляции** - линейный рост, пила, синусоида, прямоугольные импульсы, список строк и генерация строки по шаблону
- **Резервная симуляция** - теги `DecodeAndSimulate` могут генерировать значение, если декодирование не удалось
- **Команды телеуправления** - поддержка кодов `SendStr` и `SendBin` для прямой отправки через текущее соединение
- **Прототипы каналов** - генерация прототипов каналов Rapid SCADA из настроенных тегов
- **Транспортный лог** - запись дампов отправленных и принятых байт в лог линии связи
- **Отдельный WinForms host** - возможность открыть UI настройки вне ScadaAdmin

## How It Works / Как это работает

The runtime loads the per-device XML configuration from the ScadaComm configuration directory. The file name is based on the device number, for example `DrvDebug_001.xml`. If the file is missing, the project model saves a default configuration.

During a master or mixed polling session, the driver sends each enabled configured command, waits for a response until the stop condition or polling timeout, logs transport data and decodes the response into tags.

During slave or mixed incoming request processing, the driver reads incoming bytes until the stop condition is reached or the timeout expires. It then decodes the received buffer into tags and, in slave/mixed mode, sends the first enabled configured command as the default response payload.

Runtime загружает XML-конфигурацию КП из каталога конфигурации ScadaComm. Имя файла зависит от номера КП, например `DrvDebug_001.xml`. Если файл отсутствует, модель проекта сохраняет конфигурацию по умолчанию.

В master или mixed сеансе опроса драйвер отправляет каждую включённую команду, ждёт ответ до выполнения условия остановки или истечения timeout, пишет транспортный лог и декодирует ответ в теги.

При обработке входящего запроса в slave или mixed режиме драйвер читает байты до условия остановки или timeout. Затем он декодирует полученный буфер в теги и в slave/mixed режиме отправляет первую включённую настроенную команду как ответ по умолчанию.

## Driver Modes / Режимы драйвера

| Mode | Runtime behavior | Поведение |
|---|---|---|
| `Master` | Sends configured commands during polling sessions and reads responses | Отправляет настроенные команды во время сеансов опроса и читает ответы |
| `Slave` | Waits for incoming requests, decodes them and sends the first configured command as response | Ожидает входящие запросы, декодирует их и отправляет первую настроенную команду как ответ |
| `Mixed` | Combines master polling and incoming request processing | Совмещает master-опрос и обработку входящих запросов |

The driver advertises support for all three channel behaviors to Rapid SCADA.

Драйвер сообщает Rapid SCADA о поддержке всех трёх режимов поведения канала.

## Stop Condition / Условие остановки

The stop condition defines when a packet is considered complete. It is used while reading responses in master mode and incoming requests in slave/mixed mode.

Supported modes:

- marker mode: read until the configured marker value is found at the configured check address;
- length mode: read the length field and use it to determine the expected full packet size.

Main settings:

| Setting | Description | Описание |
|---|---|---|
| `StopConditionCheckAddress` | Byte offset used for marker or length check | Смещение байта для проверки маркера или длины |
| `StopConditionCheckLength` | Number of bytes to check | Количество байт для проверки |
| `StopConditionCheckFormat` | Value type: `Byte`, `UInt16`, `UInt32`, `UInt64` | Тип значения: `Byte`, `UInt16`, `UInt32`, `UInt64` |
| `StopConditionCheckValueText` | Marker value, decimal or `0x` hex | Значение маркера, decimal или `0x` hex |
| `StopConditionLengthMode` | Enables length-field mode | Включает режим поля длины |
| `StopConditionLengthIncludesItself` | Indicates that the length field already includes the full packet length | Указывает, что поле длины уже содержит полный размер пакета |

Условие остановки определяет, когда пакет считается принятым полностью. Оно используется при чтении ответов в master режиме и входящих запросов в slave/mixed режиме.

Поддерживаются два режима:

- режим маркера: чтение до найденного значения маркера по заданному адресу;
- режим длины: чтение поля длины и расчёт полного ожидаемого размера пакета.

Основные параметры приведены в таблице выше.

## Commands / Команды

Configured commands are stored in the project and ordered by `Order`. Only enabled commands are executed.

Command fields:

| Field | Description | Описание |
|---|---|---|
| `Enabled` | Whether the command is active | Включена ли команда |
| `Name` | Command name used in logs | Имя команды в логах |
| `DataKind` | Payload format in configuration. Runtime handles `Ascii` and `Unicode` explicitly; other values are converted as HEX bytes | Формат payload в конфигурации. Runtime явно обрабатывает `Ascii` и `Unicode`; остальные значения преобразуются как HEX-байты |
| `Payload` | Command data | Данные команды |
| `DelayMs` | Pause after command execution | Пауза после выполнения команды |
| `Note` | Comment | Примечание |

Rapid SCADA telecontrol commands are also supported:

- `SendStr` writes command data as a text line through the current connection;
- `SendBin` writes binary command data through the current connection.

Настроенные команды хранятся в проекте и сортируются по `Order`. Выполняются только включённые команды.

Поля команд приведены в таблице выше.

Также поддерживаются команды телеуправления Rapid SCADA:

- `SendStr` отправляет данные команды как текстовую строку через текущее соединение;
- `SendBin` отправляет бинарные данные команды через текущее соединение.

## Tag Decoding / Декодирование тегов

Each enabled tag reads its data from the received byte array by `ArrayIndex` and `DataLength`. The selected `DataFormat` determines how the byte segment is converted to text and then written to Rapid SCADA.

Numeric formats apply byte order, coefficient, offset and precision. String formats are written as ASCII or Unicode data. `HexString` converts the selected byte segment to a spaced hex string.

Tag codes are generated from the tag name by trimming it and replacing spaces with underscores. If the name is empty, the fallback code is built from channel and tag ID.

Каждый включённый тег читает данные из принятого массива байт по `ArrayIndex` и `DataLength`. Выбранный `DataFormat` определяет, как сегмент байт преобразуется в текст и затем записывается в Rapid SCADA.

Числовые форматы применяют порядок байт, коэффициент, смещение и точность. Строковые форматы записываются как ASCII или Unicode. `HexString` преобразует выбранный сегмент байт в hex-строку с пробелами.

Коды тегов формируются из имени тега: пробелы заменяются подчёркиваниями. Если имя пустое, fallback-код строится из канала и ID тега.

## Data Formats / Форматы данных

| Format | Runtime behavior | Поведение |
|---|---|---|
| `Bool` | `0` is false, any non-zero first byte is true | `0` - false, любой ненулевой первый байт - true |
| `Int16`, `UInt16` | Decodes 2-byte integer with selected byte order | Декодирует 2-байтовое число с выбранным порядком байт |
| `Int32`, `UInt32` | Decodes 4-byte integer with selected byte order | Декодирует 4-байтовое число с выбранным порядком байт |
| `Int64`, `UInt64` | Decodes 8-byte integer with selected byte order | Декодирует 8-байтовое число с выбранным порядком байт |
| `Float` | Decodes 4-byte float with selected byte order | Декодирует 4-байтовый float с выбранным порядком байт |
| `Double` | Decodes 8-byte double with selected byte order | Декодирует 8-байтовый double с выбранным порядком байт |
| `Ascii` | ASCII string, trailing `NUL` removed | ASCII-строка, завершающие `NUL` удаляются |
| `Unicode` | UTF-16 Unicode string, byte order can be adjusted | UTF-16 Unicode-строка с учётом порядка байт |
| `HexString` | Byte segment converted to hex text | Сегмент байт преобразуется в hex-текст |

## Simulation / Симуляция

Tags can work in `Decode`, `Simulate` or `DecodeAndSimulate` mode. Simulation-only tags are updated during normal polling sessions. `DecodeAndSimulate` tags use simulation as fallback if decoding fails and `SimulateOnDecodeError` is enabled.

Simulation kinds:

- `Ramp` - linear growth with step and optional cycle;
- `Sawtooth` - growth with reset value;
- `Sine` - sine wave by amplitude, bias, period and phase;
- `Square` - low/high value by period and duty cycle;
- `StringList` - cyclic or fixed enumeration of configured strings;
- `StringGenerate` - template generation with `{N}` and `{TIME}` placeholders.

Теги могут работать в режимах `Decode`, `Simulate` или `DecodeAndSimulate`. Теги только с симуляцией обновляются во время обычных сеансов опроса. Теги `DecodeAndSimulate` используют симуляцию как fallback, если декодирование не удалось и включён `SimulateOnDecodeError`.

Типы симуляции:

- `Ramp` - линейный рост с шагом и опциональным циклом;
- `Sawtooth` - рост со значением сброса;
- `Sine` - синусоида по амплитуде, смещению, периоду и фазе;
- `Square` - нижний/верхний уровень по периоду и скважности;
- `StringList` - циклическое или фиксированное перечисление строк;
- `StringGenerate` - генерация по шаблону с `{N}` и `{TIME}`.

## Channel Prototypes / Прототипы каналов

ScadaAdmin generates one `InputOutput` channel prototype for each configured tag ordered by `Order`. The prototype name is the tag name, the tag code is generated from the tag name, and `DataLen` is taken from `DataLength`.

`Ascii` and `Unicode` use string data types, `Int64` and `UInt64` use Int64, and other formats use Double.

ScadaAdmin создаёт один прототип канала `InputOutput` для каждого настроенного тега в порядке `Order`. Имя прототипа берётся из имени тега, код тега формируется из имени тега, а `DataLen` берётся из `DataLength`.

`Ascii` и `Unicode` используют строковые типы данных, `Int64` и `UInt64` используют Int64, остальные форматы используют Double.

## Usage / Использование

English:

1. Add `DrvDebug` to a communication line in ScadaAdmin.
2. Select the line channel behavior: `Master`, `Slave` or `Mixed`.
3. Open device properties and configure the driver mode and stop condition.
4. Add commands if the driver must send requests or default responses.
5. Configure tags: mode, byte index, length, data format, byte order, scaling and simulation.
6. Use test bytes and preview fields in the UI to verify decoding.
7. Generate channel prototypes for the device.
8. Upload the Rapid SCADA project and restart the communication line.

Русский:

1. Добавьте `DrvDebug` в линию связи в ScadaAdmin.
2. Выберите режим поведения канала: `Master`, `Slave` или `Mixed`.
3. Откройте свойства КП и настройте режим драйвера и условие остановки.
4. Добавьте команды, если драйвер должен отправлять запросы или ответы по умолчанию.
5. Настройте теги: режим, индекс байта, длину, формат данных, порядок байт, масштабирование и симуляцию.
6. Проверьте декодирование через тестовые байты и preview-поля интерфейса.
7. Создайте прототипы каналов для КП.
8. Загрузите проект Rapid SCADA и перезапустите линию связи.

## Safety Notes / Замечания по безопасности

- DrvDebug is a diagnostic driver. Use it carefully on production communication lines because it can send arbitrary byte payloads.
- In slave and mixed modes, the first enabled configured command is used as the default response payload.
- Stop condition settings must match the protocol under test. Incorrect length or marker settings can delay reads until timeout.
- `SendStr` and `SendBin` telecontrol commands write directly to the current connection.
- Detailed transport logging can write raw protocol data to log files. Avoid permanent detailed logging for sensitive payloads.
- Simulation can hide real decode failures when `DecodeAndSimulate` and `SimulateOnDecodeError` are enabled.

- DrvDebug - диагностический драйвер. На production-линиях используйте его осторожно, потому что он может отправлять произвольные байтовые payload.
- В slave и mixed режимах первая включённая настроенная команда используется как ответ по умолчанию.
- Условие остановки должно соответствовать тестируемому протоколу. Неверная длина или маркер могут задерживать чтение до timeout.
- Команды телеуправления `SendStr` и `SendBin` пишут напрямую в текущее соединение.
- Подробное транспортное логирование может записывать сырые протокольные данные в файлы логов. Не включайте его постоянно для чувствительных payload.
- Симуляция может скрывать реальные ошибки декодирования, если включены `DecodeAndSimulate` и `SimulateOnDecodeError`.

## Project Structure / Структура проекта

```text
DrvDebug_v6/
├── DrvDebug.sln                    # Solution file
├── StartСompiling.bat              # Build and local deployment helper
├── README.md                       # This file
│
├── DemoProjects/                   # Demo Rapid SCADA projects
├── DrvDebug.Logic/                 # Runtime driver for ScadaComm
├── DrvDebug.Shared/                # Shared project model, codec, simulation and utilities
├── DrvDebug.View/                  # ScadaAdmin configuration UI
├── DrvDebug.WinForm/               # Standalone configuration/test UI host
├── Hex.Shared/                     # Shared byte conversion helpers
└── Libraries/                      # Rapid SCADA referenced libraries
```

## Build / Сборка

### Prerequisites / Требования

- .NET 8.0 SDK
- Rapid SCADA 6.x libraries in `Libraries`
- Windows for the ScadaAdmin view and WinForms host

### Commands / Команды

```powershell
dotnet build .\DrvDebug.sln -c Release
```

The `StartСompiling.bat` script publishes Windows x86, Windows x64 and Any CPU packages, stops local Rapid SCADA services, copies the Any CPU package to `C:\Program Files\SCADA`, restarts services and opens ScadaAdmin.

Скрипт `StartСompiling.bat` публикует пакеты Windows x86, Windows x64 и Any CPU, останавливает локальные службы Rapid SCADA, копирует Any CPU пакет в `C:\Program Files\SCADA`, перезапускает службы и открывает ScadaAdmin.

## Screenshots / Скриншоты

![DrvDebug](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/OpenDrivers/Source/DrvDebug_001.png)
![DrvDebug](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/OpenDrivers/Source/DrvDebug_002.png)
![DrvDebug](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/OpenDrivers/Source/DrvDebug_003.png)
![DrvDebug](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/OpenDrivers/Source/DrvDebug_004.png)
![DrvDebug](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/OpenDrivers/Source/DrvDebug_005.png)

## License / Лицензия

This project is part of the Rapid SCADA ecosystem and uses the Apache 2.0 license badge.

Данный проект является частью экосистемы Rapid SCADA и использует badge лицензии Apache 2.0.

## SAST Tools

[PVS-Studio](https://pvs-studio.ru/ru/pvs-studio/?utm_source=website&utm_medium=github&utm_campaign=open_source) - static analyzer for C, C++, C#, and Java code.
