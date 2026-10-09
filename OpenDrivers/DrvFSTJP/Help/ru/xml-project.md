# DrvFSTJP — XML-проект

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/xml-project.md)

Имя XML-конфигурации определяется номером КП:

- `DrvFSTJP.xml` для номера `0`;
- `DrvFSTJP_001.xml`, `DrvFSTJP_002.xml` и далее для остальных номеров.

[Пример проекта](../../DemoProjects/DrvFSTJP_001.xml) доступен в репозитории.

Откройте свойства КП в ScadaAdmin, чтобы создать или изменить файл во встроенной форме драйвера. Форма сохраняет его в каталоге конфигурации ScadaComm.

Основные поля XML:

- `MasterAddress` — адрес компьютера/ведущего на шине ФСТ, обычно `0`;
- `DeviceAddress` — адрес ФСТ-03х `1..15`;
- `PollLinkCheck` — отправлять команду `0x00`;
- `PollStatus` — отправлять команду `0x01`;
- `Channels/Channel` — включённые каналы ФСТ `1..8`;
- `Coefficient` и `Offset` — преобразование исходной 12-битной концентрации по формуле `raw * Coefficient + Offset`;
- `RelayDevices` — необязательные блоки расширения реле.

Создаваемые теги:

- `DeviceType`;
- `GlobalErrors`;
- для каждого включённого канала: `<CodePrefix>_Concentration`, `<CodePrefix>_MessageCode`, `<CodePrefix>_AlarmCode`, `<CodePrefix>_SensorType`, `<CodePrefix>_CalibrationRequired`, `<CodePrefix>_Threshold1`, `<CodePrefix>_Threshold2`, `<CodePrefix>_Disabled`;
- для каждого блока реле: `<CodePrefix>_StateLo`, `<CodePrefix>_StateHi`, `<CodePrefix>_Errors`.
