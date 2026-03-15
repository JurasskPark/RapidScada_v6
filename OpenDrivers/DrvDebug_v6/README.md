	Драйвера для Rapid SCADA.
	Drivers  for Rapid SCADA.


	
### DrvDebug
![DrvDebug](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v6/DrvDebug_v6.4.0.1/total)

# Инструкция по настройке драйвера DrvDebug

`DrvDebug` это универсальный отладочный драйвер. Он не привязан к конкретному протоколу. Драйвер работает с массивами байт:

- может отправлять заранее заданные команды;
- может принимать массив байт и расшифровывать его по тегам;
- может генерировать тестовые значения в режиме симуляции.

## 1. Основная форма проекта

Форма используется для общей настройки драйвера.

### Режим драйвера

Блок `Режим драйвера`:

- `Мастер`
  Драйвер сам отправляет команды.
- `Ведомый`
  Драйвер ожидает входящие запросы и отвечает.
- `Смешанный`
  Драйвер может работать в обоих режимах.

### Условие остановки

Блок `Условие остановки` определяет, как драйвер понимает, что входящий пакет получен полностью.

Режимы:

- `Маркер`
  Драйвер проверяет значение по указанному адресу и завершает прием, когда найден нужный маркер.
- `Длина`
  Драйвер читает поле длины в пакете и по нему определяет полный размер сообщения.

Поля:

- `Адрес проверки`
  Смещение в массиве байт, где искать маркер.
- `Длина проверки`
  Сколько байт читать для проверки маркера.
- `Адрес поля длины`
  Смещение, где находится поле длины.
- `Размер поля длины`
  Размер поля длины в байтах.
- `Формат`
  Тип значения: `Byte`, `UInt16`, `UInt32`, `UInt64`.
- `Hex/Dec значение`
  Значение маркера для режима `Маркер`.
- `Длина включает себя`
  Если включено, поле длины уже содержит полный размер пакета вместе с самим полем длины.

### Команды

Вкладка `Команды` содержит список отправляемых команд.

Столбцы:

- `Вкл.`
  Команда активна или нет.
- `Имя`
  Произвольное имя команды.
- `Тип`
  Формат данных команды:
  - `HEX`
  - `ASCII`
  - `Unicode`
  - `Шаблон`
- `Данные`
  Содержимое команды.
- `Пауза, мс`
  Задержка после выполнения команды.
- `Примечание`
  Комментарий.

Кнопки:

- `Добавить`
  Создает новую команду.
- `Удалить`
  Удаляет выбранную команду.
- `Вверх` / `Вниз`
  Меняют порядок выполнения команд.

### Теги

Вкладка `Теги` показывает список настроенных тегов.

Столбцы:

- `Имя`
- `Канал`
- `Режим`
- `Индекс`
- `Длина`
- `Формат`
- `Симуляция`
- `Предпросмотр`

Кнопка `Редактировать теги` открывает форму полного редактирования тегов.

### Параметры

Блок `Параметры`:

- `Выходные байты`
  Тестовое или справочное значение исходящих данных.
- `Предпросмотр входных байт`
  Тестовое или справочное значение входного массива.
- `Вести лог`
  Включает логирование.
- `Тип лога`
  Уровень логирования:
  - `Действие`
  - `Информация`
  - `Предупреждение`
  - `Ошибка`

## 2. Настройка тегов

Форма `FrmTag` используется для настройки одного тега.

### Основные параметры

- `Имя`
  Имя тега.
- `Активен`
  Тег включен.
- `Канал`
  Номер канала SCADA.
- `Описание`
  Комментарий к тегу.

### Режим тега

`Режим`:

- `Декодирование`
  Тег читает данные из входного массива байт.
- `Симуляция`
  Тег не декодирует входные данные, а генерирует свои.
- `Декодирование и симуляция`
  Основной режим: декодирование. Если декодирование невозможно, может использоваться симуляция.

### Декодирование данных

- `Индекс в массиве`
  Смещение начала данных тега во входном массиве.
- `Длина данных`
  Размер участка данных в байтах.
- `Формат`
  Тип данных тега:
  - `Bool`
  - `Int16`
  - `UInt16`
  - `Int32`
  - `UInt32`
  - `Int64`
  - `UInt64`
  - `Float`
  - `Double`
  - `Ascii`
  - `Unicode`
  - `HexString`
- `Порядок байт`
  Способ перестановки байт:
  - `0123 - Прямой порядок`
  - `3210 - Обратный порядок`
  - `1032 - Смешанный порядок`
  - `2301 - Смешанный порядок`

### Масштабирование значения

- `Коэффициент`
  Умножение результата.
- `Смещение`
  Добавляемое смещение.
- `Точность`
  Количество знаков после запятой.
- `Ед. изм.`
  Единицы измерения.

### Тест декодирования

- `Тестовые байты`
  Тестовый массив байт для проверки настройки.
- `Предпросмотр декодирования`
  Результат расшифровки тестового массива.
- `Симулировать при ошибке декодирования`
  Если декодирование не удалось, брать значение из симуляции.

## 3. Симуляция

Вкладка `Симуляция` на форме тега.

### Тип симуляции

Поле `Тип`:

- `Нет`
- `Линейный рост`
- `Пила`
- `Синусоида`
- `Прямоугольные импульсы`
- `Список строк`
- `Генерация строки`

### Общие параметры

- `Интервал, мс`
  Частота обновления значения.

### Числовая симуляция

Используемые поля зависят от выбранного типа:

- `Мин.`
- `Макс.`
- `Начальное значение`
- `Шаг`
- `Значение сброса`
- `Амплитуда`
- `Смещение`
- `Период, сек`
- `Фаза, град`
- `Нижний уровень`
- `Верхний`
- `Скважность, %`
- `Цикл`

### Строковая симуляция

- `Строковый режим`
  - `Перечисление`
  - `Шаблон`
- `Значения`
  Список строк.
- `Разделитель`
  Символ разделения списка строк.
- `Шаблон`
  Формат генерации строки, например `TAG_{N}`.

### Предпросмотр симуляции

- `Предпросмотр симуляции`
  Показывает, какое значение будет генерировать тег.

## 4. Как настраивать драйвер на практике

Минимальная последовательность:

1. Откройте форму проекта.
2. Выберите режим драйвера: `Мастер`, `Ведомый` или `Смешанный`.
3. Настройте `Условие остановки`, чтобы драйвер понимал конец входящего пакета.
4. Если нужен режим мастера, добавьте команды во вкладке `Команды`.
5. Откройте `Редактировать теги`.
6. Для каждого тега укажите:
   - имя;
   - канал;
   - режим;
   - индекс в массиве;
   - длину;
   - формат;
   - порядок байт.
7. При необходимости задайте симуляцию.
8. Проверьте результат через `Тестовые байты` и `Предпросмотр`.
9. Сохраните конфигурацию.

## 5. Логика работы драйвера

- Входящий пакет читается до выполнения условия остановки.
- После получения полного массива каждый тег берет свои данные по `индексу` и `длине`.
- Значение преобразуется по `формату` и `порядку байт`.
- Если тег работает в симуляции, значение генерируется по выбранному алгоритму.
- Если включен режим `Декодирование и симуляция`, симуляция может использоваться как резерв.


Библиотека DrvDebug (6.4.0.1)
- Релиз драйвера.

---------------------------------------------------------------------------

# DrvDebug Driver Configuration Guide

`DrvDebug` is a universal debugging driver. It is not tied to any specific protocol. The driver works with byte arrays:

- can send predefined commands;
- can receive a byte array and decode it by tags;
- can generate test values in simulation mode.

## 1. Main project form

The form is used for general driver configuration.

### Driver mode

The `Driver mode` block:

- `Master`
  The driver sends commands on its own.
- `Slave`
  The driver waits for incoming requests and responds.
- `Mixed`
  The driver can operate in both modes.

### Stop condition

The `Stop condition` block defines how the driver determines that an incoming packet has been received completely.

Modes:

- `Marker`
  The driver checks the value at the specified address and stops receiving when the required marker is found.
- `Length`
  The driver reads the length field in the packet and uses it to determine the full message size.

Fields:

- `Check address`
  Offset in the byte array where to look for the marker.
- `Check length`
  How many bytes to read to verify the marker.
- `Length field address`
  Offset where the length field is located.
- `Length field size`
  Size of the length field in bytes.
- `Format`
  Value type: `Byte`, `UInt16`, `UInt32`, `UInt64`.
- `Hex/Dec value`
  Marker value for `Marker` mode.
- `Length includes itself`
  If enabled, the length field already contains the full packet size including the length field itself.

### Commands

The `Commands` tab contains the list of outgoing commands.

Columns:

- `En.`
  Whether the command is active.
- `Name`
  Arbitrary command name.
- `Type`
  Command data format:
  - `HEX`
  - `ASCII`
  - `Unicode`
  - `Template`
- `Data`
  Command content.
- `Pause, ms`
  Delay after command execution.
- `Note`
  Comment.

Buttons:

- `Add`
  Creates a new command.
- `Delete`
  Deletes the selected command.
- `Up` / `Down`
  Changes the command execution order.

### Tags

The `Tags` tab shows the list of configured tags.

Columns:

- `Name`
- `Channel`
- `Mode`
- `Index`
- `Length`
- `Format`
- `Simulation`
- `Preview`

The `Edit tags` button opens the full tag editing form.

### Parameters

The `Parameters` block:

- `Output bytes`
  Test or reference value of outgoing data.
- `Input bytes preview`
  Test or reference value of the incoming array.
- `Enable logging`
  Enables logging.
- `Log type`
  Logging level:
  - `Action`
  - `Information`
  - `Warning`
  - `Error`

## 2. Tag configuration

The `FrmTag` form is used to configure a single tag.

### Main parameters

- `Name`
  Tag name.
- `Active`
  Tag is enabled.
- `Channel`
  SCADA channel number.
- `Description`
  Tag comment.

### Tag mode

`Mode`:

- `Decoding`
  The tag reads data from the input byte array.
- `Simulation`
  The tag does not decode incoming data but generates its own.
- `Decoding and simulation`
  Primary mode: decoding. If decoding is not possible, simulation can be used.

### Data decoding

- `Array index`
  Offset of the tag data start in the input array.
- `Data length`
  Size of the data segment in bytes.
- `Format`
  Tag data type:
  - `Bool`
  - `Int16`
  - `UInt16`
  - `Int32`
  - `UInt32`
  - `Int64`
  - `UInt64`
  - `Float`
  - `Double`
  - `Ascii`
  - `Unicode`
  - `HexString`
- `Byte order`
  Byte rearrangement method:
  - `0123 - Direct order`
  - `3210 - Reverse order`
  - `1032 - Mixed order`
  - `2301 - Mixed order`

### Value scaling

- `Factor`
  Multiplies the result.
- `Offset`
  Added offset.
- `Precision`
  Number of digits after the decimal point.
- `Unit`
  Units of measurement.

### Decoding test

- `Test bytes`
  Test byte array for configuration verification.
- `Decoding preview`
  Decoding result of the test array.
- `Simulate on decoding error`
  If decoding fails, take the value from simulation.

## 3. Simulation

The `Simulation` tab on the tag form.

### Simulation type

The `Type` field:

- `None`
- `Linear growth`
- `Sawtooth`
- `Sine wave`
- `Square pulses`
- `String list`
- `String generation`

### Common parameters

- `Interval, ms`
  Value update frequency.

### Numeric simulation

The used fields depend on the selected type:

- `Min.`
- `Max.`
- `Initial value`
- `Step`
- `Reset value`
- `Amplitude`
- `Offset`
- `Period, sec`
- `Phase, deg`
- `Low level`
- `High`
- `Duty cycle, %`
- `Cycle`

### String simulation

- `String mode`
  - `Enumeration`
  - `Template`
- `Values`
  List of strings.
- `Separator`
  Character used to separate the string list.
- `Template`
  String generation format, for example `TAG_{N}`.

### Simulation preview

- `Simulation preview`
  Shows what value the tag will generate.

## 4. How to configure the driver in practice

Minimum sequence:

1. Open the project form.
2. Select the driver mode: `Master`, `Slave`, or `Mixed`.
3. Configure the `Stop condition` so the driver can detect the end of an incoming packet.
4. If master mode is needed, add commands in the `Commands` tab.
5. Open `Edit tags`.
6. For each tag specify:
   - name;
   - channel;
   - mode;
   - array index;
   - length;
   - format;
   - byte order.
7. Configure simulation if needed.
8. Check the result via `Test bytes` and `Preview`.
9. Save the configuration.

## 5. Driver operating logic

- The incoming packet is read until the stop condition is met.
- After receiving the full array, each tag takes its data by `index` and `length`.
- The value is converted according to `format` and `byte order`.
- If the tag operates in simulation mode, the value is generated according to the selected algorithm.
- If `Decoding and simulation` mode is enabled, simulation can be used as a fallback.


DrvDebug library (6.4.0.1)
- Driver release.


Screenshots

![DrvDebug](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/OpenDrivers/Source/DrvDebug_001.png) ![DrvDebug](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/OpenDrivers/Source/DrvDebug_002.png)
![DrvDebug](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/OpenDrivers/Source/DrvDebug_003.png) ![DrvDebug](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/OpenDrivers/Source/DrvDebug_004.png)
![DrvDebug](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/OpenDrivers/Source/DrvDebug_005.png) 


## SAST Tools

[PVS-Studio](https://pvs-studio.ru/ru/pvs-studio/?utm_source=website&utm_medium=github&utm_campaign=open_source) - static analyzer for C, C++, C#, and Java code.
