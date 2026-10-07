# PlgMimControlsJP — Operator Controls for Rapid SCADA

![Rapid SCADA](https://img.shields.io/badge/Rapid%20SCADA-6.5-blue.svg)
![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg)
![Version](https://img.shields.io/badge/version-6.5.0.15-green.svg)
![Platform](https://img.shields.io/badge/platform-Windows%20%7C%20Linux-lightgrey.svg)

## About This Guide / О руководстве

This guide explains how engineers, operators and administrators use `PlgMimControlsJP` in Rapid SCADA mimic diagrams. It covers the component catalog, channel bindings, command confirmation, data quality, value-entry forms, themes, installation, activation and troubleshooting.

Это руководство предназначено для инженеров, операторов и администраторов, которые используют `PlgMimControlsJP` в мнемосхемах Rapid SCADA. В нём описаны состав компонентов, привязка каналов, подтверждение команд, качество данных, формы ввода значений, темы, установка, активация и устранение неполадок.

`PlgMimControlsJP` version `6.5.0.15` provides nineteen localized control components and the autonomous `ControlsDemo` component in the **CONTROLS / УПРАВЛЕНИЕ** toolbox. The plugin uses the public standard Mimic contract and works with both the standard Mimic Editor and compatible alternative editors. It does not require `PlgMimicJP` or a separate backend API.

`PlgMimControlsJP` версии `6.5.0.15` предоставляет девятнадцать локализованных компонентов управления и автономный компонент `ControlsDemo` в палитре **CONTROLS / УПРАВЛЕНИЕ**. Плагин использует публичный стандартный контракт Mimic и работает со стандартным Mimic Editor и совместимыми альтернативными редакторами. Зависимость от `PlgMimicJP` и отдельный серверный API не требуются.

Authoring is free of the component runtime license: engineers can place, configure, copy and save ordinary controls without installing a local ControlsJP license. Executing those controls in Webstation requires a valid server-side `PlgMimControlsJP` license. `ControlsDemo` works without that license using only simulated data.

Для проектирования не требуется лицензия исполнения компонента: обычные элементы можно добавлять, настраивать, копировать и сохранять без локальной лицензии ControlsJP. Для их работы в Вебстанции требуется действующая серверная лицензия `PlgMimControlsJP`. `ControlsDemo` работает без этой лицензии исключительно на искусственных данных.

## Features / Возможности

English:

- nineteen display, selection, command and multi-value form components, plus an autonomous demo;
- standard Rapid SCADA input and output channel bindings;
- numeric, UTF-8 text and exact Hex-byte commands where applicable;
- confirmed-state rendering without optimistic state changes;
- optional command-pending frame with a configurable color;
- explicit handling of missing and bad-quality input data;
- horizontal and vertical option groups, bit lists and discrete sliders;
- three layouts for numeric step buttons;
- built-in numeric and RUS/ENG on-screen keyboards;
- a configurable multi-row value input form with six editor types;
- separate press/release commands, a one-shot PLC handshake and a mechanism command panel;
- process value and accepted setpoint with Inline, Stacked and Popup layouts;
- button and rotary mode selection, plus searchable dropdown and list layouts;
- PNG, JPG and SVG button images, four image positions, scaling and multiline captions;
- four complete light and dark CSS themes;
- Russian and English toolbox names, properties and runtime captions;
- safe editor preview: commands are disabled while a mimic is being edited.

Русский:

- девятнадцать компонентов отображения, выбора, управления и группового ввода, а также автономное демо;
- стандартные привязки к входным и выходным каналам Rapid SCADA;
- числовые команды, текст UTF-8 и точные Hex-байты для поддерживаемых компонентов;
- отображение только подтверждённого состояния без оптимистического переключения;
- необязательная рамка ожидания команды с настраиваемым цветом;
- явная обработка отсутствующих данных и плохого качества;
- горизонтальное и вертикальное расположение вариантов, битов и дискретного ползунка;
- три варианта размещения кнопок шага числового ввода;
- встроенные цифровая и экранная RUS/ENG-клавиатуры;
- настраиваемая многострочная форма ввода с шестью типами редакторов;
- отдельные команды нажатия и отпускания, однократная команда с подтверждением цикла ПЛК и панель команд механизма;
- факт и принятая уставка с расположением Inline, Stacked и Popup;
- выбор режима кнопкой или поворотным переключателем, выпадающий список и список с поиском;
- изображения PNG, JPG и SVG в кнопках, четыре положения, масштабирование и многострочные подписи;
- четыре полные светлые и тёмные CSS-темы;
- русские и английские названия компонентов, свойств и рабочих надписей;
- безопасное превью в редакторе, в котором отправка команд заблокирована.

## Quick Start / Быстрый старт

English:

1. Install and enable `PlgMimControlsJP`, then restart SCADA Web. A component license is not required for editing.
2. Open a mimic in a compatible Mimic Editor.
3. Select a component from the **CONTROLS** group and place it on the canvas.
4. For a display component, set its input channel.
5. For a command component, configure its command output and the feedback, ready-state or permit channels it exposes; see its description below.
6. Configure captions, values, colors, ranges or options required by that component.
7. Install the server-side license for ordinary controls, save the mimic, transfer the project to runtime and open it in Webstation.
8. Verify the operator has control rights and the output channel accepts commands.
9. Send a command and check that the device writes the resulting state back to the input channel.

Русский:

1. Установите и включите `PlgMimControlsJP`, затем перезапустите SCADA Web. Для редактирования лицензия компонента не требуется.
2. Откройте мнемосхему в совместимом редакторе Mimic.
3. Выберите элемент в группе **УПРАВЛЕНИЕ** и поместите его на полотно.
4. Для компонента отображения укажите входной канал.
5. Для компонента управления настройте выход команды и предусмотренные им каналы обратной связи, готовности или разрешения; см. описание компонента ниже.
6. Настройте требуемые подписи, значения, цвета, диапазоны или варианты.
7. Установите серверную лицензию обычных компонентов, сохраните мнемосхему, передайте проект в runtime и откройте её в Вебстанции.
8. Проверьте наличие у оператора права управления и разрешение команд для выходного канала.
9. Отправьте команду и убедитесь, что устройство возвращает итоговое состояние во входной канал.

Commands are intentionally disabled in edit mode. A command component does not switch its confirmed state immediately after a click: it waits for input-channel feedback. Input and output channel numbers may be different.

В режиме редактирования команды намеренно заблокированы. После нажатия компонент управления не переключает подтверждённое состояние сразу, а ожидает обратную связь входного канала. Номера входного и выходного каналов могут различаться.

## Component Catalog / Каталог компонентов

The following nineteen types are ordinary components. They are available for authoring without a local component license; their runtime execution is licensed.

Следующие девятнадцать типов являются обычными компонентами. Для их редактирования локальная лицензия компонента не нужна; исполнение в runtime лицензируется.

| Type name | English toolbox name | Русское название | Default size / Размер | Purpose / Назначение |
|---|---|---|---|---|
| `BitCheckList` | Bit check list | Список битов | `170 × 110` | Edit selected bits without losing hidden bits / Изменение выбранных битов без потери скрытых |
| `CheckBox` | Check box | Флажок | `140 × 32` | Binary `0 / 1` command / Двоичная команда `0 / 1` |
| `ComboBox` | Combo box | Выпадающий список | `160 × 34` | Select one configured numeric value / Выбор одного числового значения |
| `DiscreteSlider` | Discrete slider | Дискретный ползунок | `280 × 82` | Select an exact configured division / Выбор точного деления |
| `IlluminatedButton` | Illuminated button | Кнопка с подсветкой | `140 × 64` | Send one fixed command and show feedback / Одна фиксированная команда и индикация обратной связи |
| `LatchedButton` | Latched button | Фиксируемая кнопка | `130 × 42` | Two-state command button / Двухпозиционная командная кнопка |
| `MechanismPanel` | Mechanism panel | Панель механизма | `130 × 32` | Button or hotspot opening a state and command panel / Кнопка или активная область открытия панели состояния и команд |
| `ModeSelector` | Mode selector | Переключатель режимов | `220 × 160` | Direct mode selection; default three-position rotary layout / Прямой выбор режима; по умолчанию поворотный переключатель на три положения |
| `MomentaryButton` | Hold button | Кнопка удержания | `112 × 32` | Separate commands on press and release / Отдельные команды нажатия и отпускания |
| `NumericUpDown` | Numeric input | Числовой ввод | `140 × 36` | Validated number and step commands / Проверяемый числовой ввод и команды шага |
| `OneShotButton` | One-shot button | Однократная команда | `112 × 32` | One command followed by a PLC ready-state cycle / Одна команда с ожиданием цикла готовности ПЛК |
| `ProcessValue` | Process value | Текущее значение | `160 × 42` | Read-only formatted process value / Форматированное значение только для чтения |
| `RadioButtonGroup` | Radio button group | Переключатели | `160 × 64` | Visible selection of one configured value / Наглядный выбор одного значения |
| `SearchableComboBox` | Searchable selection | Выбор с поиском | `240 × 32` | Searchable Dropdown or ListBox; ListBox starts at `240 × 110` / Выпадающий список или список с поиском; исходный размер ListBox `240 × 110` |
| `SetpointControl` | Setpoint control | Уставка процесса | `390 × 36` | Separate PV, accepted SP and explicit setpoint entry; default Inline / Раздельные факт, принятая уставка и явный ввод задания; по умолчанию Inline |
| `SquareToggle` | Square toggle | Квадратный переключатель | `60 × 30` | Compact square binary switch / Компактный квадратный переключатель |
| `StateIndicator` | State indicator | Индикатор состояния | `140 × 64` | Read-only state lamp / Лампа состояния только для чтения |
| `TextCommandInput` | Command input | Ввод команды | `250 × 36` | Number, UTF-8 or Hex command entry / Ввод числа, UTF-8 или Hex-команды |
| `ValueForm` | Value input form | Форма ввода значений | `210 × 44` | Multi-row modal value entry / Многострочная модальная форма ввода |

The separate demo type is available without a component runtime license. In the standard authoring mode it is offered alongside the ordinary controls, independently of the server license. Saved demos work in both licensed and unlicensed runtime.

Отдельный демонстрационный тип доступен без лицензии исполнения компонента. В штатном режиме проектирования он предлагается вместе с обычными элементами независимо от серверной лицензии. Сохранённые демонстрации работают как в лицензированном, так и в нелицензированном runtime.

| Type name | English toolbox name | Русское название | Default size / Размер | Purpose / Назначение |
|---|---|---|---|---|
| `ControlsDemo` | Demonstration | Демонстрация возможностей | `920 × 720` | Autonomous simulated controls, no SCADA channels or real commands / Автономные искусственные значения без каналов SCADA и реальных команд |

## Channels, Commands and Confirmation / Каналы, команды и подтверждение

Most interactive controls use an input channel for confirmed feedback and an output channel for commands. A command is available only in licensed runtime when the component is enabled, the operator has control rights, the resolved output channel number is greater than zero and the Webstation command API is available. Ready-state feedback and configured permit channels impose additional component-specific conditions.

Большинство интерактивных компонентов используют входной канал для подтверждённой обратной связи и выходной канал для команд. Команда доступна только в лицензированном runtime, если компонент включён, оператор имеет право управления, определённый с учётом привязок номер выходного канала больше нуля и доступен командный API Вебстанции. Сигнал готовности и настроенные каналы разрешения добавляют условия конкретного компонента.

The following command formats are available where the component exposes `CommandFormat`:

В компонентах со свойством `CommandFormat` доступны следующие форматы:

| Format / Формат | Example / Пример | Sent value / Что отправляется |
|---|---|---|
| `Double` | `12.5` or `12,5` | Numeric command `12.5` / Числовая команда `12.5` |
| `Text` | `START` or `ПУСК` | UTF-8 text / Текст UTF-8 |
| `Hex` | `00 AF 10` | Exact bytes `00AF10` / Точные байты `00AF10` |

Hex separators may be spaces, commas, semicolons, colons or hyphens. Every byte must contain exactly two hexadecimal digits; do not use the `0x` prefix.

Разделителями Hex-байтов могут быть пробелы, запятые, точки с запятой, двоеточия и дефисы. Каждый байт должен содержать ровно две шестнадцатеричные цифры; префикс `0x` не используется.

### Pending Frame / Рамка ожидания

Command controls have an optional `Show pending frame` property. Basic controls default to off; `MomentaryButton`, `OneShotButton`, `MechanismPanel`, `ModeSelector` and `SearchableComboBox` start with it enabled. Existing saved settings are retained. When enabled, `Pending frame color` appears; its default is amber `#D97706`.

У командных компонентов есть необязательное свойство `Показывать рамку ожидания`. У базовых элементов оно по умолчанию выключено; у `MomentaryButton`, `OneShotButton`, `MechanismPanel`, `ModeSelector` и `SearchableComboBox` изначально включено. Сохранённые настройки сохраняются. После включения появляется свойство `Цвет рамки ожидания` с исходным янтарным цветом `#D97706`.

For basic controls with an input channel, pending state ends when the expected good input value arrives, command sending is rejected, or the ten-second safety timeout expires. Output-only fields clear the pending state after the server acknowledges the command. The frame never replaces the actual channel state.

У базовых компонентов с входным каналом ожидание заканчивается после получения ожидаемого достоверного значения, ошибки отправки или защитного тайм-аута 10 секунд. Поля только с выходным каналом снимают ожидание после подтверждения команды сервером. Рамка никогда не заменяет фактическое состояние канала.

This basic ten-second rule is not the completion contract for every type. `SetpointControl`, `ModeSelector` and `SearchableComboBox` have configurable feedback timeouts; `OneShotButton` keeps its PLC handshake lock even if its waiting frame times out. See the corresponding component descriptions below.

Базовое правило 10 секунд не определяет завершение работы всех типов. У `SetpointControl`, `ModeSelector` и `SearchableComboBox` настраивается тайм-аут обратной связи; `OneShotButton` сохраняет блокировку цикла ПЛК даже после тайм-аута рамки ожидания. Поведение описано в разделах соответствующих компонентов ниже.

## Selection Controls / Компоненты выбора

### ComboBox

Configure `InCnlNum`, `OutCnlNum` and the `Options` list. Each option contains visible text and a numeric value. The input value selects the matching item; choosing an item sends its value once. If the input is missing, bad or not present in the list, the field is empty and no configured option is selected. Numeric `-1` is not reserved and may be used normally.

Настройте `InCnlNum`, `OutCnlNum` и список `Варианты`. Каждый вариант содержит видимую надпись и числовое значение. Входное значение выбирает совпадающий пункт, а выбор пункта один раз отправляет его значение. При отсутствии данных, плохом качестве или неизвестном значении поле остаётся пустым. Число `-1` не зарезервировано и может использоваться как обычное значение.

### ModeSelector

`ModeSelector` has two layouts: `Button` opens a menu for direct selection, while `Rotary` shows a rotary switch with two to five positions. More than five options automatically select the Button layout without losing rows. A rotary drag previews the requested position locally and sends only the final position on release; crossing intermediate positions and cancelling the drag send nothing.

`ModeSelector` имеет два вида: `Button` открывает меню прямого выбора, а `Rotary` показывает поворотный переключатель на два–пять положений. При количестве вариантов больше пяти автоматически используется Button без потери строк. Перетаскивание ручки локально показывает выбранное положение и отправляет только итоговую команду при отпускании; прохождение промежуточных положений и отмена перетаскивания ничего не отправляют.

Each `ModeSelectorOption` can have its own input, output and permit channels, numeric or text feedback, and a Double, Text or Hex command. Zero row input/output channels use the component's common channels. Confirmed position follows good matching feedback; server acknowledgement alone does not confirm a mode. Configure `FeedbackTimeout` and the optional pending frame for command feedback.

Каждый `ModeSelectorOption` может иметь собственные каналы входа, выхода и разрешения, числовую или текстовую обратную связь и команду Double, Text или Hex. Нулевые входной/выходной каналы строки используют общие каналы компонента. Подтверждённое положение следует достоверной совпавшей обратной связи; принятие команды сервером само по себе режим не подтверждает. Для ожидания команды настройте `FeedbackTimeout` и необязательную рамку.

### SearchableComboBox

`SearchableComboBox` provides `Dropdown` and `ListBox` layouts. Configure the `SearchableSelectionOption` list with captions, typed feedback, commands and optional permits; row channels may differ from the common component channels. Search filters captions by a case-insensitive substring and preserves the configured order. Typing or clearing the search and opening the dropdown send no commands.

`SearchableComboBox` поддерживает виды `Dropdown` и `ListBox`. Настройте список `SearchableSelectionOption` с подписями, типизированной обратной связью, командами и необязательными разрешениями; каналы строк могут отличаться от общих каналов компонента. Поиск фильтрует подписи по подстроке без учёта регистра и сохраняет настроенный порядок. Ввод или очистка поиска и открытие списка не отправляют команды.

Clicking a different available row or explicitly pressing Enter sends one configured command. The previous confirmed selection stays visible until matching good feedback arrives. Filtering out the current row does not replace its confirmed value. Configure placeholders, popup dimensions and `FeedbackTimeout`; neither layout limits the number of rows.

Щелчок по другому доступному пункту или явное нажатие Enter отправляет одну настроенную команду. Прежний подтверждённый выбор сохраняется до совпавшей достоверной обратной связи. Исключение текущей строки фильтром не подменяет подтверждённое значение. Настройте подсказки, размеры всплывающего списка и `FeedbackTimeout`; оба вида поддерживают произвольное число строк.

### RadioButtonGroup

`RadioButtonGroup` uses the same `ValueOption` list as `ComboBox` and supports horizontal or vertical orientation. No button is selected for unknown or bad input data. Increase the component width for long captions in horizontal mode.

`RadioButtonGroup` использует тот же список `ValueOption`, что и `ComboBox`, и поддерживает горизонтальное или вертикальное расположение. При неизвестных данных или плохом качестве ни один вариант не выбран. Для длинных надписей в горизонтальном режиме увеличьте ширину компонента.

### CheckBox

`CheckBox` has a configurable caption and fixed values: `1` means checked and `0` means unchecked. Any other value, missing data or bad quality produces an indeterminate state. A click sends the opposite binary value. Before the first valid input, the first click sends `1`.

`CheckBox` имеет настраиваемую подпись и фиксированные значения: `1` — установлен, `0` — снят. Другое значение, отсутствие данных или плохое качество показываются неопределённым состоянием. Нажатие отправляет противоположное двоичное значение. До первого достоверного входа первое нажатие отправляет `1`.

### SquareToggle

`SquareToggle` is a compact switch with a deliberately square track and thumb. A positive input value places the thumb on the right; zero or a negative value places it on the left. Missing or bad data hides the thumb. A click sends `0` from a confirmed active state and `1` otherwise, then waits for feedback.

`SquareToggle` — компактный переключатель с намеренно квадратными корпусом и движком. Положительное входное значение ставит движок вправо, нулевое или отрицательное — влево. При отсутствующих или плохих данных движок скрывается. Нажатие отправляет `0` из подтверждённого включённого состояния и `1` во всех остальных случаях, после чего ожидает обратную связь.

### BitCheckList

Each `BitOption` contains a one-based bit number and caption. Bit 1 corresponds to mask `1`, bit 2 to `2`, bit 8 to `128` and bit 9 to `256`. The component edits only displayed bits and preserves every unlisted bit from the latest good input mask.

Каждый `BitOption` содержит номер бита, начиная с единицы, и подпись. Бит 1 соответствует маске `1`, бит 2 — `2`, бит 8 — `128`, бит 9 — `256`. Компонент изменяет только показанные биты и сохраняет все скрытые биты последней достоверной входной маски.

`BitCheckList` requires a valid non-negative integer input value before sending a command. This prevents accidental loss of hidden bits. The list may be vertical or horizontal; horizontal mode uses scrolling when the component is too narrow.

`BitCheckList` требует корректное целое неотрицательное входное значение до отправки команды. Это предотвращает случайную потерю скрытых битов. Список может быть вертикальным или горизонтальным; при нехватке ширины горизонтальный режим использует прокрутку.

## Numeric Input and Slider / Числовой ввод и ползунок

### NumericUpDown

Configure minimum, maximum, step, negative-value permission, decimal places and one of three button layouts:

Настройте минимум, максимум, шаг, разрешение отрицательных значений, число знаков после запятой и один из трёх вариантов кнопок:

| Layout / Вид | Behavior / Поведение |
|---|---|
| `Native` | Browser up/down arrows on the right / Встроенные стрелки браузера справа |
| `Sides` | Large minus button on the left and plus on the right / Крупные минус слева и плюс справа |
| `Stacked` | Plus above the field and minus below it / Плюс над полем и минус под ним |

`Stacked` automatically raises the component height to at least 84 pixels. Typed input is sent only by Enter. A step button sends exactly one step immediately. Escape restores the last confirmed input value. Text, exponential notation, an out-of-range number, excessive decimal places and values not aligned with the configured step are rejected.

`Stacked` автоматически увеличивает высоту компонента минимум до 84 пикселей. Введённое число отправляется только по Enter. Кнопка шага немедленно отправляет ровно один шаг. Escape восстанавливает последнее подтверждённое входное значение. Текст, экспоненциальная запись, выход за диапазон, лишние дробные знаки и значение не по настроенному шагу отклоняются.

### DiscreteSlider

Set orientation, minimum, maximum and division count. For `0…100` with 10 divisions the selectable values are `0, 10, 20, …, 100`. Dragging, clicking the track or using arrow keys selects only exact divisions and sends each new division once.

Задайте ориентацию, минимум, максимум и количество делений. Для диапазона `0…100` и 10 делений доступны `0, 10, 20, …, 100`. Перетаскивание, щелчок по шкале и стрелки клавиатуры выбирают только точные деления и один раз отправляют каждое новое значение.

During interaction the selected value is shown near the thumb. After release, the slider returns to the confirmed input-channel position until feedback arrives. Missing or bad input data is displayed as `#.#` but does not block selection. Changing orientation exchanges width and height when the current aspect ratio belongs to the previous orientation.

Во время управления рядом с указателем показывается выбранное значение. После отпускания ползунок возвращается к подтверждённому положению входного канала до прихода обратной связи. Отсутствующие или плохие данные отображаются как `#.#`, но не блокируют выбор. При смене ориентации ширина и высота меняются местами, если текущие пропорции соответствуют прежнему направлению.

Command divisions do not round the actual feedback value. For `35…85` with 10 divisions, commands are `35, 40, …, 85`, while feedback such as `43.9` is displayed as `43.9`. An output channel of zero makes the slider an indicator; a missing input is different from a missing command output.

Деления команды не округляют фактическую обратную связь. Для диапазона `35…85` и 10 делений команды равны `35, 40, …, 85`, а факт `43,9` отображается как `43,9`. При выходном канале 0 ползунок работает как индикатор; отсутствие входа и отсутствие выхода команды — разные условия.

### SetpointControl

`SetpointControl` keeps three channels separate: `InCnlNum` supplies the read-only process value (PV), `SetpointInCnlNum` supplies the accepted setpoint (SP), and `OutCnlNum` receives a Double command. It supports `Inline` (`390 × 36`), `Stacked` (`236 × 64`) and `Popup` (`240 × 32`) starting layouts; dimensions remain editable. Set the numeric range, step, precision, unit and captions for the process.

`SetpointControl` разделяет три канала: `InCnlNum` передаёт фактическое значение процесса (PV) только для чтения, `SetpointInCnlNum` — принятую уставку (SP), а `OutCnlNum` получает числовую команду Double. Начальные виды — `Inline` (`390 × 36`), `Stacked` (`236 × 64`) и `Popup` (`240 × 32`); размеры можно изменять. Настройте диапазон, шаг, точность, единицу измерения и подписи процесса.

Editing changes only a local draft. Apply or Enter sends it explicitly; live polling preserves the draft. Only matching good SP feedback confirms the command. PV changes and transport acknowledgement do not replace accepted SP. Failed or timed-out requests retain the draft for an explicit retry; pending requests prevent duplicate sending.

Редактирование меняет только локальный черновик. Кнопка отправки или Enter явно отправляет его; обновление данных сохраняет черновик. Команду подтверждает только совпавшая достоверная обратная связь SP. Изменение факта и подтверждение транспорта не подменяют принятую уставку. После ошибки или тайм-аута черновик сохраняется для явного повтора; во время ожидания повторная отправка блокируется.

## Command Buttons / Командные кнопки

### LatchedButton

`LatchedButton` is a two-state button. Configure the input values, visible texts and commands for transitions into On and Off states, then choose one shared command format.

`LatchedButton` — двухпозиционная кнопка. Настройте входные значения, видимые надписи и команды перехода во включённое и выключенное состояния, затем выберите единый формат команды.

| Confirmed input / Подтверждённый вход | Visible state / Отображение | Next click / Следующее нажатие |
|---|---|---|
| On value, default `1` | On text / Надпись «Включено» | Sends the configured Off command / Отправляет команду выключения |
| Off value, default `0` | Off text / Надпись «Выключено» | Sends the configured On command / Отправляет команду включения |
| Missing, bad or other value / Нет данных, плохое или другое значение | No data or unknown / Нет данных или неизвестно | Sends the configured On command / Отправляет команду включения |

The accessible `aria-pressed` state follows confirmed feedback. The button does not add a separate visible accessibility caption.

Доступное состояние `aria-pressed` следует подтверждённой обратной связи. Отдельная видимая подпись доступности на кнопке не добавляется.

### IlluminatedButton

`IlluminatedButton` sends the same configured command on every click. Its input channel controls only the visible text and color. Configure rectangular, rounded or circular shape, command format and command, plus explicit On and Off values, texts and colors. Good but unmatched input uses the unknown appearance; missing or bad data uses the no-data appearance.

`IlluminatedButton` при каждом нажатии отправляет одну и ту же настроенную команду. Входной канал управляет только видимой надписью и цветом. Настройте прямоугольную, скруглённую или круглую форму, формат и значение команды, а также отдельные значения, надписи и цвета состояний «Включено» и «Выключено». Достоверное, но несовпавшее значение использует вид «Неизвестно», а отсутствующие или плохие данные — вид «Нет данных».

Use `LatchedButton` instead when On and Off require different commands.

Если для включения и выключения нужны разные команды, используйте `LatchedButton`.

### MomentaryButton

`MomentaryButton` has separate `PressOutCnlNum` / `ReleaseOutCnlNum`, command formats and payloads. Holding the pointer, Enter or Space sends the press command once; release, cancellation, focus loss or the finite `MaxHoldTime` sends the release command. A typed `ControlStateOption` dictionary can display independent equipment feedback; the locally pressed button is not confirmation of movement.

`MomentaryButton` имеет отдельные `PressOutCnlNum` / `ReleaseOutCnlNum`, форматы и значения команд. Удержание указателя, Enter или Space один раз отправляет команду нажатия; отпускание, отмена, потеря фокуса или конечный `MaxHoldTime` отправляет команду отпускания. Типизированный словарь `ControlStateOption` может показывать независимую обратную связь оборудования; локальное нажатие не подтверждает движение.

A browser disconnect or closure cannot guarantee delivery of a release command. Configure an equipment-side watchdog or server control timeout for held actions.

При разрыве связи или закрытии браузера доставка команды отпускания не гарантируется. Для действий удержания настройте watchdog оборудования или серверный тайм-аут управления.

### OneShotButton

`OneShotButton` sends one configured command only when good feedback matches `ReadyValue` of `ReadyValueType`. `ReadyInCnlNum=0` uses the common input channel. After sending, the button stays locked until good feedback has left the ready value and returned to it. The caption and state dictionary remain separate from this handshake.

`OneShotButton` отправляет одну настроенную команду только при совпадении достоверной обратной связи с `ReadyValue` типа `ReadyValueType`. При `ReadyInCnlNum=0` используется общий входной канал. После отправки кнопка остаётся заблокированной, пока достоверная обратная связь не выйдет из состояния готовности и не вернётся в него. Подпись и словарь состояния не подменяют этот цикл подтверждения.

Server acceptance and `HandshakeTimeout` do not count as PLC completion or unlock a successfully sent cycle. The timeout reports a diagnostic. An explicit send rejection can restore readiness before PLC busy has been observed.

Принятие команды сервером и `HandshakeTimeout` не означают завершение ПЛК и не снимают блокировку успешно отправленного цикла. Тайм-аут показывает диагностику. Явный отказ отправки может восстановить готовность до наблюдения занятости ПЛК.

### MechanismPanel

`MechanismPanel` opens an adjacent state and command popup through a visible Button or a Hotspot. The hotspot has an editor frame and is transparent in runtime. Configure the title, typed state dictionary and `OperatorCommand` list; each command can use its own output channel, Double/Text/Hex payload, colors, image and optional typed permit channel. Opening or closing the panel never sends commands.

`MechanismPanel` открывает соседнюю панель состояния и команд через видимую кнопку Button или активную область Hotspot. В редакторе область имеет рамку, а в runtime прозрачна. Настройте заголовок, типизированный словарь состояний и список `OperatorCommand`; у каждой команды могут быть собственный выходной канал, значение Double/Text/Hex, цвета, изображение и необязательный типизированный канал разрешения. Открытие и закрытие панели никогда не отправляет команды.

`PopupWidth` and `PopupHeight` are independent of the trigger size. Zero enables automatic sizing on that axis; positive values request fixed dimensions within viewport limits, with wrapping and scrolling for long content. State text and colors follow the first matching dictionary row; missing and unmatched input have separate appearances.

`PopupWidth` и `PopupHeight` не зависят от размера кнопки или активной области. Ноль включает автоматический размер по соответствующей оси; положительное значение задаёт фиксированный размер в пределах окна с переносом и прокруткой длинного содержимого. Текст и цвета состояния берутся из первой совпавшей строки словаря; отсутствующие и неизвестные данные имеют отдельный вид.

## Command Input / Ввод команды

`TextCommandInput` has no input channel. Configure an output channel, `Double`, `Text` or `Hex` format, placeholder, optional send button and optional on-screen keyboard button. The send-button caption and keyboard type appear in the property grid only while the corresponding button is enabled.

`TextCommandInput` не имеет входного канала. Настройте выходной канал, формат `Double`, `Text` или `Hex`, подсказку, необязательную кнопку отправки и необязательную кнопку экранной клавиатуры. Надпись кнопки отправки и тип клавиатуры появляются в свойствах только при включении соответствующей кнопки.

The numeric keyboard adapts to number or Hex input. The text keyboard provides RUS/ENG layouts, one-shot Shift, digits, space and basic separators. The popup edits a local draft; only Enter validates and sends it. Cancel, Escape or clicking the backdrop closes the keyboard without changing the field. If both buttons are hidden, Enter in the normal input field still sends the value.

Цифровая клавиатура адаптируется к числу или Hex. Текстовая клавиатура содержит раскладки RUS/ENG, одноразовый Shift, цифры, пробел и основные разделители. Всплывающее окно изменяет только локальный черновик; проверка и отправка выполняются по Enter. Отмена, Escape или щелчок по затемнённому фону закрывают клавиатуру без изменения поля. Если обе кнопки скрыты, Enter в обычном поле всё равно отправляет значение.

Password mode is intentionally not implemented. Use the Web application authentication system for credentials.

Парольный режим намеренно не реализован. Для учётных данных используйте штатную авторизацию Web-приложения.

## Multi-Value Form / Форма ввода значений

`ValueForm` appears on the mimic as a configurable open button. Its modal window contains the form title, row list, common Apply button and Close button. Each `ValueFormRow` has a name, input channel, output channel and one of six editors:

`ValueForm` отображается на мнемосхеме как настраиваемая кнопка открытия. Модальное окно содержит заголовок, список строк, общую кнопку отправки и кнопку закрытия. Каждая `ValueFormRow` имеет имя, входной канал, выходной канал и один из шести редакторов:

| Editor / Редактор | Row settings / Настройки строки |
|---|---|
| `TextBox` | Command format and placeholder / Формат команды и подсказка |
| `CheckBox` | Fixed `0 / 1` values / Фиксированные значения `0 / 1` |
| `NumericUpDown` | Minimum, maximum, step, negatives and precision / Минимум, максимум, шаг, отрицательные и точность |
| `ComboBox` | `ValueOption` list / Список `ValueOption` |
| `RadioButtonGroup` | Options and orientation / Варианты и расположение |
| `BitCheckList` | Bits and orientation / Биты и расположение |

The form has four runtime columns: row name, read-only current value, new value and per-row result. Editing a row sends nothing. The common Apply button sends only changed and valid rows; the form remains open and reports success or failure independently for every row. Close never sends commands. If unsent changes exist, Close or Escape requests confirmation.

Во время выполнения форма имеет четыре столбца: имя строки, текущее значение только для чтения, новое значение и построчный результат. Изменение строки ничего не отправляет. Общая кнопка отправляет только изменённые и корректные строки; форма остаётся открытой и показывает успех или ошибку отдельно для каждой строки. Закрытие никогда не отправляет команды. При наличии неотправленных изменений закрытие или Escape запрашивают подтверждение.

The plugin automatically creates hidden standard bindings for all unique row channels. A `BitCheckList` row remains disabled until it has a valid source mask, and hidden bits are preserved from the latest received value. `ValueForm` is independent from `PlgMimMultiSet` and does not replace or modify it.

Плагин автоматически создаёт скрытые стандартные привязки для всех уникальных каналов строк. Строка `BitCheckList` заблокирована до получения корректной исходной маски, а скрытые биты сохраняются из последнего принятого значения. `ValueForm` не зависит от `PlgMimMultiSet`, не заменяет и не изменяет его.

## Button Images and Text Wrapping / Изображения и перенос текста в кнопках

From `6.5.0.15`, `IlluminatedButton`, `LatchedButton`, `MomentaryButton`, `OneShotButton`, `TextCommandInput`, `SetpointControl`, `ValueForm` and `MechanismPanel` support configurable button content. Use the standard Mimic image picker to add PNG, JPG or SVG images to the mimic. Images remain embedded in the `.mim` and do not require an external URL.

Начиная с `6.5.0.15`, `IlluminatedButton`, `LatchedButton`, `MomentaryButton`, `OneShotButton`, `TextCommandInput`, `SetpointControl`, `ValueForm` и `MechanismPanel` поддерживают настраиваемое содержимое кнопок. Штатный выбор изображения Mimic позволяет добавить PNG, JPG или SVG в мнемосхему. Изображения сохраняются внутри `.mim` и не требуют внешнего URL.

| Setting / Настройка | Behavior / Поведение |
|---|---|
| `ImageName` | Image selected from the mimic collection / Изображение из коллекции мнемосхемы |
| `ImagePosition` | `Left`, `Right`, `Top` or `Bottom` relative to text / Слева, справа, сверху или снизу относительно текста |
| `ImageScale` | `1…100%` of the available image area; aspect ratio is preserved / `1…100%` доступной области изображения с сохранением пропорций |
| `WrapText` | Multiline captions and explicit line breaks / Многострочные подписи и явные переводы строк |

Use `ButtonContent` for ordinary buttons and the text-command send button, `ApplyButtonContent` for a setpoint's Apply button, and `OpenButtonContent` for a form opener. Mechanism commands have their own button-content settings. State-driven buttons can use different images for On, Off, Unknown and No data; dictionary controls select the image from the confirmed matching state row. If no state image is configured, the common image is used. Images change appearance only, not command routing or feedback rules.

Для обычных кнопок и кнопки отправки текста используется `ButtonContent`, для отправки уставки — `ApplyButtonContent`, для открытия формы — `OpenButtonContent`. Команды механизма имеют собственные настройки содержимого кнопки. Кнопки с индикацией могут использовать разные изображения для состояний «Включено», «Выключено», «Неизвестно» и «Нет данных»; словарные элементы выбирают изображение по подтверждённой совпавшей строке состояния. При отсутствии изображения состояния используется общее изображение. Картинки меняют только оформление, а не маршрутизацию команды или правила обратной связи.

Keep enough space for both the image and caption, especially when Top/Bottom is used on small buttons. The entire caption remains available in the tooltip. Old mimics without these settings retain text-only buttons without wrapping.

Оставляйте место для изображения и подписи, особенно при Top/Bottom на маленьких кнопках. Полная подпись остаётся доступна во всплывающей подсказке. Старые мнемосхемы без этих настроек сохраняют текстовые кнопки без переноса.

## Autonomous Demo / Автономная демонстрация

`ControlsDemo` previews basic operator controls on simulated data and requires no component runtime license, SCADA channels or command outputs. The editor shows a static preview. Save the mimic, transfer it to runtime and open it in Webstation to start the local simulation. Runtime demo controls and its value form update only their owning demo's private values; Reset resumes automatic values. Multiple demos are independent. Demo values and drafts are not saved to the project.

`ControlsDemo` демонстрирует базовые операторские элементы на искусственных данных и не требует лицензии исполнения компонента, каналов SCADA или выходов команд. Редактор показывает статичное превью. Сохраните мнемосхему, передайте её в runtime и откройте в Вебстанции для запуска локальной модели. Элементы и форма демо меняют только собственные временные значения; Reset возвращает автоматическое изменение. Несколько демо независимы. Значения и черновики демо не сохраняются в проект.

The autonomous demo defaults to English independently of the host interface language. It is different from DemoProject views composed of ordinary controls bound to Simulator channels: those views exercise real channel/command routing and require the ordinary component runtime license.

Автономное демо по умолчанию использует английский язык независимо от языка интерфейса хоста. Оно отличается от представлений DemoProject, составленных из обычных элементов с каналами Simulator: такие представления проверяют реальную маршрутизацию каналов и команд и требуют лицензии исполнения обычных компонентов.

## Read-Only Components / Компоненты только для чтения

### ProcessValue

Set an input channel, unit and display template such as `###.###`. In edit mode the template is shown as a width preview. In runtime `1234.5678` becomes `1234.567`, `0.85` becomes `0.850` and `0` becomes `0.000`. Fractional digits are truncated to the template width, trailing fractional zeros are retained and leading integer zeros are not added.

Укажите входной канал, единицу измерения и шаблон, например `###.###`. В редакторе шаблон служит превью ширины. Во время выполнения `1234.5678` отображается как `1234.567`, `0.85` — как `0.850`, `0` — как `0.000`. Дробная часть ограничивается шириной шаблона, конечные нули сохраняются, ведущие нули целой части не добавляются.

The value and optional unit use the same font size on a transparent background. Missing or bad data is shown as the configured template. `ProcessValue` has no output channel and never sends commands.

Значение и необязательная единица измерения имеют одинаковый размер шрифта и прозрачный фон. При отсутствующих или плохих данных показывается настроенный шаблон. `ProcessValue` не имеет выходного канала и никогда не отправляет команды.

### StateIndicator

Configure a caption, rectangular, rounded or circular shape and explicit On and Off groups with input value, text, text color and background color. Good unmatched input uses the configurable Unknown style. Missing or bad data uses the configurable No data style and turns off the state glow.

Настройте подпись, прямоугольную, скруглённую или круглую форму и отдельные группы «Включено» и «Выключено» со входным значением, текстом, цветом текста и фона. Достоверное несовпавшее значение использует настраиваемый вид «Неизвестно». Отсутствующие или плохие данные используют вид «Нет данных» и выключают свечение состояния.

The editor always previews the On state so the selected colors are visible. `StateIndicator` has no output channel and never sends commands.

В редакторе всегда показывается состояние «Включено», чтобы выбранные цвета были видны. `StateIndicator` не имеет выходного канала и никогда не отправляет команды.

## First Command Without Feedback / Первая команда без обратной связи

Missing input data is shown honestly and normally does not prevent an explicit operator command:

Отсутствие входных данных отображается явно и обычно не мешает осознанно отправить первую команду:

| Component / Компонент | Available action / Доступное действие |
|---|---|
| `ComboBox`, `RadioButtonGroup` | Select a configured value / Выбрать настроенное значение |
| `CheckBox`, `SquareToggle` | First click sends `1` / Первое нажатие отправляет `1` |
| `LatchedButton` | First click sends the configured On command / Первое нажатие отправляет команду включения |
| `NumericUpDown` | Enter a number or step from minimum / Ввести число или выполнить шаг от минимума |
| `DiscreteSlider` | Select any configured division / Выбрать любое настроенное деление |
| `IlluminatedButton`, `TextCommandInput` | Send the explicitly configured command / Отправить явно настроенную команду |
| `ValueForm` | Edit and apply ordinary rows / Изменить и отправить обычные строки |
| `SetpointControl` | Edit and explicitly apply a valid setpoint / Ввести и явно отправить корректную уставку |
| `ModeSelector`, `SearchableComboBox` | Select an available option, subject to configured permits / Выбрать доступный вариант с учётом настроенных разрешений |
| `MomentaryButton`, `MechanismPanel` | Send configured commands subject to channel rights and permits / Отправить настроенную команду с учётом прав на канал и разрешений |
| `OneShotButton` | Blocked until good ready-state feedback arrives / Заблокирован до достоверного сигнала готовности |
| `BitCheckList` | Blocked until the first valid source mask / Заблокирован до первой корректной исходной маски |

## Themes / Темы оформления

The active file `css/controls.css` contains the complete light-blue theme. Four complete replaceable presets are supplied:

Активный файл `css/controls.css` содержит полную светло-синюю тему. В комплект входят четыре полных заменяемых набора:

- `css/themes/controls.light-blue.css` — light blue / светлая синяя;
- `css/themes/controls.light-green.css` — light green / светлая зелёная;
- `css/themes/controls.dark-blue.css` — dark blue / тёмная синяя;
- `css/themes/controls.dark-green.css` — dark green / тёмная зелёная.

To change the global theme, back up `ScadaWeb/wwwroot/plugins/MimControlsJP/css/controls.css` and copy the selected preset over it. Restart or hard-refresh the browser after replacement. The plugin does not include a runtime theme selector.

Чтобы изменить общую тему, сохраните резервную копию `ScadaWeb/wwwroot/plugins/MimControlsJP/css/controls.css` и скопируйте выбранный набор поверх него. После замены перезапустите или жёстко обновите браузер. Отдельного переключателя тем во время выполнения нет.

Neutral surfaces use the theme accent for selection, focus and active manipulation. Green and red keep their semantic success and error roles. Explicit indicator colors and the component-level pending-frame color take priority over the theme.

Нейтральные поверхности используют акцент темы для выбора, фокуса и активного управления. Зелёный и красный сохраняют смысл успеха и ошибки. Явно настроенные цвета индикаторов и рамки ожидания имеют приоритет над темой.

## Editor and Runtime Behavior / Редактор и рабочий режим

- Every selected component has a lime editor outline around its complete external size, including controls with clipped or scrollable content.
- Interactive child elements do not intercept moving and resizing in edit mode.
- Runtime values and pending markers are transient and are not saved to `.mim` files.
- Commands are never sent in edit mode.
- The component waits for real input feedback and does not write a new confirmed visual state optimistically.

- Каждый выбранный компонент имеет зелёную рамку редактора по полному внешнему размеру, включая элементы с обрезанным или прокручиваемым содержимым.
- Интерактивные дочерние элементы не мешают перемещению и изменению размера в редакторе.
- Рабочие значения и маркеры ожидания являются временными и не сохраняются в `.mim`.
- В режиме редактирования команды никогда не отправляются.
- Компонент ожидает реальную обратную связь входного канала и не подменяет её оптимистическим состоянием.

## Installation and Registration / Установка и регистрация

Requirements:

- a compatible Rapid SCADA 6.5 build;
- the .NET 10 runtime used by the current SCADA Web package;
- Mimic diagrams and a compatible Mimic Editor;
- configured input and output channels;
- operator control rights for command components;
- a valid server-side `PlgMimControlsJP` license for executing ordinary components;
- a package matching the installed Rapid SCADA build.

Требования:

- совместимая сборка Rapid SCADA 6.5;
- среда .NET 10, используемая текущим пакетом SCADA Web;
- поддержка мнемосхем и совместимый редактор Mimic;
- настроенные входные и выходные каналы;
- право управления у оператора для командных компонентов;
- действующая серверная лицензия `PlgMimControlsJP` для исполнения обычных компонентов;
- пакет, соответствующий установленной сборке Rapid SCADA.

No local component license is required for authoring. Current packages target .NET 10 and the 6.5 branch; compatibility with older Webstation builds, including 6.3, requires a matching build and separate verification.

Для проектирования локальная лицензия компонента не нужна. Текущие пакеты рассчитаны на .NET 10 и ветку 6.5; для старых сборок Вебстанции, включая 6.3, нужна соответствующая сборка и отдельная проверка совместимости.

Installation:

1. Copy the supplied `SCADA` package over the Rapid SCADA installation directory while preserving its directory structure. The package includes the required LicenseJPLite runtime files.
2. Enable `PlgMimControlsJP` in the Webstation plugin configuration.
3. On Windows, install the supplied `PlgMimControlsJP.View.dll` in `ScadaAdmin\Lib` when the classic Administrator must recognize the plugin.
4. Restart SCADA Web, its service or the IIS site. A browser refresh alone does not reload plugin assemblies.
5. Open a mimic editor and verify that **CONTROLS / УПРАВЛЕНИЕ** contains nineteen ordinary component types and the separate `ControlsDemo` entry without requiring a local component license.
6. After an update, perform a hard browser refresh if old scripts or styles remain cached.

Установка:

1. Скопируйте поставляемый пакет `SCADA` поверх каталога установки Rapid SCADA с сохранением структуры папок. Пакет содержит необходимые файлы среды LicenseJPLite.
2. Включите `PlgMimControlsJP` в конфигурации плагинов Вебстанции.
3. Под Windows установите поставляемый `PlgMimControlsJP.View.dll` в `ScadaAdmin\Lib`, если классический Администратор должен распознавать плагин.
4. Перезапустите SCADA Web, соответствующую службу или сайт IIS. Простое обновление браузера не перезагружает сборки плагина.
5. Откройте редактор мнемосхем и убедитесь, что в **CONTROLS / УПРАВЛЕНИЕ** доступны девятнадцать обычных типов и отдельный пункт `ControlsDemo` без локальной лицензии компонента.
6. Если после обновления остались старые скрипты или стили, выполните жёсткое обновление страницы.

Required Webstation plugin entry:

Необходимая запись плагина Вебстанции:

```xml
<Plugins>
  <Plugin code="PlgMimControlsJP" />
</Plugins>
```

The public browser asset path is `/plugins/MimControlsJP`. Do not rename `PlgMimControlsJP.dll`, `PlgMimControlsJP.View.dll` or the `MimControlsJP` static directory. The portable package does not replace a Mimic Editor or host-owned shared assemblies.

Публичный путь браузерных ресурсов — `/plugins/MimControlsJP`. Не переименовывайте `PlgMimControlsJP.dll`, `PlgMimControlsJP.View.dll` и статический каталог `MimControlsJP`. Переносимый пакет не заменяет редактор Mimic и общие сборки, принадлежащие хосту.

## Activation / Активация

The ordinary controls use their own server-side runtime license. A `Single` license is bound to the server installation; it does not have to be copied to the engineer's computer to create or save `.mim` files. A `MimicEditorJP`, `PlgMimTankJP`, `PlgMimPipesJP` or another product license does not activate `PlgMimControlsJP`.

Обычные элементы управления используют собственную серверную лицензию исполнения. Лицензия `Single` привязана к установке сервера; для создания и сохранения `.mim` её не нужно копировать на компьютер проектировщика. Лицензия `MimicEditorJP`, `PlgMimTankJP`, `PlgMimPipesJP` или другого продукта не активирует `PlgMimControlsJP`.

The separate `MimicEditorJP` license governs that editor's free-version watermark on save. It does not replace a ControlsJP runtime license and does not restrict the availability of ControlsJP components for authoring.

Отдельная лицензия `MimicEditorJP` управляет водяным знаком бесплатной версии этого редактора при сохранении. Она не заменяет лицензию исполнения ControlsJP и не ограничивает доступность компонентов ControlsJP для проектирования.

| Host / Приложение | Activation request / Запрос активации | License / Лицензия |
|---|---|---|
| SCADA Web / Webstation | `ScadaWeb/config/PlgMimControlsJP_Activation.bin` | `ScadaWeb/config/PlgMimControlsJP_License.bin` |

Paths are relative to the Rapid SCADA installation directory. For example, a Windows installation may use `C:\Program Files\SCADA\ScadaWeb\config`. A different runtime component host uses its own configured license directory. An authoring-only ScadaAdminWebJP or classic Administrator installation does not require a local ControlsJP runtime license and does not generate an activation request merely by editing a mimic.

Пути указаны относительно каталога установки Rapid SCADA. Например, установка Windows может использовать `C:\Program Files\SCADA\ScadaWeb\config`. Другой хост исполнения компонентов использует свой настроенный каталог лицензий. Для ScadaAdminWebJP или классического Администратора, используемого только для проектирования, локальная лицензия исполнения ControlsJP не нужна; само редактирование мнемосхемы не создаёт запрос активации.

English:

1. Install and enable the plugin on the SCADA Web server, then start or restart the application.
2. If the runtime license is missing or rejected, the plugin creates `PlgMimControlsJP_Activation.bin` in `ScadaWeb/config` when its licensing dependencies are available. An existing request is not overwritten automatically.
3. Send the activation request to the license provider.
4. The generated license must preserve the request UID and the exact application name `PlgMimControlsJP`.
5. Save the received key as `PlgMimControlsJP_License.bin` in the same server directory.
6. Restart SCADA Web so the runtime component specifications are rebuilt. A browser refresh alone is not sufficient.
7. Open an ordinary control in Webstation and verify licensed operation, channel feedback and command rights. No second component license is needed on the authoring computer.

Русский:

1. Установите и включите плагин на сервере SCADA Web, затем запустите или перезапустите приложение.
2. Если лицензия исполнения отсутствует или отклонена, плагин создаст `PlgMimControlsJP_Activation.bin` в `ScadaWeb/config` при наличии зависимостей лицензирования. Существующий запрос автоматически не перезаписывается.
3. Передайте запрос активации поставщику лицензии.
4. При создании лицензии должны быть сохранены UID из запроса и точное имя приложения `PlgMimControlsJP`.
5. Сохраните полученный ключ под именем `PlgMimControlsJP_License.bin` в том же каталоге сервера.
6. Перезапустите SCADA Web для повторного создания спецификаций runtime. Простого обновления страницы недостаточно.
7. Откройте обычный компонент в Вебстанции и проверьте лицензированную работу, обратную связь каналов и права управления. Вторая лицензия компонента на компьютере проектировщика не нужна.

If the server license is missing, invalid, expired, issued for another `AppName`, or cannot be validated, ordinary ControlsJP runtime components are replaced by inert localized license warnings. Their types, IDs, geometry and saved settings remain loadable, but they do not execute component scripts, process bindings/data or send commands. Other plugins and autonomous `ControlsDemo` instances continue to work. The editor palette remains available.

Если серверная лицензия отсутствует, недействительна, просрочена, выдана для другого `AppName` или не может быть проверена, обычные компоненты ControlsJP в runtime заменяются инертными локализованными сообщениями о лицензии. Их типы, ID, геометрия и сохранённые настройки остаются доступными для загрузки, но они не исполняют скрипты компонента, не обрабатывают привязки/данные и не отправляют команды. Другие плагины и автономные экземпляры `ControlsDemo` продолжают работать. Палитра редактора остаётся доступной.

| Context / Контекст | Ordinary controls / Обычные элементы | `ControlsDemo` |
|---|---|---|
| Authoring without a local component license / Проектирование без локальной лицензии компонента | Full palette, properties, copying and saving / Полная палитра, свойства, копирование и сохранение | Available in the standard editor palette; static preview / Доступен в штатной палитре редактора; статичное превью |
| Licensed runtime / Лицензированный runtime | Real feedback and commands with normal operator rights / Реальная обратная связь и команды с обычными правами оператора | Previously saved demos continue to simulate / Ранее сохранённые демо продолжают моделирование |
| Unlicensed runtime / Нелицензированный runtime | Inert license warnings / Инертные сообщения о лицензии | Autonomous simulated values and local actions / Автономные искусственные значения и локальные действия |

## Troubleshooting / Устранение неполадок

| Symptom / Признак | Cause and action / Причина и действие |
|---|---|
| The **CONTROLS / УПРАВЛЕНИЕ** group is missing in the editor / Группа отсутствует в редакторе | Check plugin registration, matching DLLs, dictionaries and static assets, then restart the host. The component runtime license does not hide the authoring palette. / Проверьте регистрацию плагина, согласованные DLL, словари и статические ресурсы, затем перезапустите хост. Лицензия исполнения компонента не скрывает палитру проектирования. |
| Runtime controls show license warnings / Компоненты runtime показывают сообщения о лицензии | Check the server file `PlgMimControlsJP_License.bin`, product `AppName`, UID, validity and licensing dependencies; restart SCADA Web after installing the key. / Проверьте серверный файл `PlgMimControlsJP_License.bin`, `AppName` продукта, UID, срок действия и зависимости лицензирования; после установки ключа перезапустите SCADA Web. |
| `PlgMimControlsJP_Activation.bin` is not created / Запрос активации не создаётся | Verify that `LicenseJP.Logic.dll` and its packaged dependencies are installed beside the plugin runtime and that the host can write to its license directory. / Проверьте `LicenseJP.Logic.dll` и пакетные зависимости рядом со средой плагина, а также право хоста на запись в папку лицензий. |
| The editor has fewer than 19 ordinary types / В редакторе меньше 19 обычных типов | Check that the DLL, dictionaries and browser assets belong to the same current package. `ControlsDemo` is additional and may be omitted from a licensed palette. / Проверьте, что DLL, словари и браузерные ресурсы относятся к одному текущему пакету. `ControlsDemo` является дополнительным пунктом и может отсутствовать в лицензированной палитре. |
| A component displays data but does not send a command / Данные видны, но команда не отправляется | Commands are disabled in the editor. In runtime check `Enabled`, operator control rights, `OutCnlNum` and output-channel permissions. / В редакторе команды запрещены. Во время выполнения проверьте `Enabled`, право управления, `OutCnlNum` и разрешение команд выходного канала. |
| The command was accepted but the visible state did not change / Команда принята, но вид не изменился | This is expected until the device writes the result to the input feedback channel. Check `InCnlNum` and device feedback. / До обратной связи это ожидаемо. Проверьте `InCnlNum` и возврат состояния устройством. |
| The pending frame is not visible / Рамка ожидания не видна | Check `Show pending frame` and its color; defaults depend on the component. The frame appears only while a command is pending. / Проверьте `Показывать рамку ожидания` и её цвет: исходная настройка зависит от компонента. Рамка видна только во время ожидания команды. |
| `BitCheckList` is disabled / `BitCheckList` заблокирован | The component has not received a good non-negative integer source mask. Check the input channel and its quality. / Не получена достоверная целая неотрицательная маска. Проверьте входной канал и качество. |
| A selection is empty, a checkbox is indeterminate or the slider shows `#.#` / Пустой выбор, неопределённый флажок или `#.#` | The input channel is zero, missing, bad quality or contains an unsupported value. / Входной канал равен нулю, отсутствует, имеет плохое качество или неподдерживаемое значение. |
| A numeric value is rejected / Число отклоняется | Check minimum, maximum, negative-value permission, decimal places and exact step alignment. Exponential notation is not accepted. / Проверьте минимум, максимум, отрицательные значения, точность и соответствие шагу. Экспоненциальная запись не принимается. |
| A text or Hex command is rejected / Текстовая или Hex-команда отклоняется | Check the selected command format. Hex requires pairs of valid hexadecimal digits and no `0x` prefix. / Проверьте формат. Для Hex нужны пары допустимых шестнадцатеричных цифр без `0x`. |
| A `ValueForm` row is not sent / Строка `ValueForm` не отправляется | Apply sends only changed and valid rows. Check the row output channel, validation result and control rights. / Общая кнопка отправляет только изменённые и корректные строки. Проверьте выходной канал строки, результат проверки и права. |
| `OneShotButton` remains locked / `OneShotButton` остаётся заблокированным | Verify a good ready → not ready → ready feedback cycle. Server acknowledgement and a timeout do not confirm PLC completion. / Проверьте достоверный цикл готов → не готов → готов. Подтверждение сервера и тайм-аут не подтверждают завершение ПЛК. |
| `SetpointControl` keeps the old accepted SP / `SetpointControl` сохраняет прежнюю принятую уставку | Check `SetpointInCnlNum`; only its matching good feedback confirms the command. PV and transport acknowledgement are separate. / Проверьте `SetpointInCnlNum`; команду подтверждает только совпавшая достоверная обратная связь этого канала. Факт и подтверждение транспорта независимы. |
| The classic Administrator reports an assembly load error / Классический Администратор сообщает об ошибке загрузки | Install the matching packaged `PlgMimControlsJP.View.dll`. It is built against `ScadaWebCommon.Subset` for classic Administrator compatibility. / Установите соответствующий пакетный `PlgMimControlsJP.View.dll`. Он собран с `ScadaWebCommon.Subset` для совместимости с классическим Администратором. |
| New files are installed but the old appearance remains / Установлены новые файлы, но остался старый вид | Restart the application or IIS site and perform a hard browser refresh. / Перезапустите приложение или IIS и выполните жёсткое обновление страницы. |

## Scope and Limitations / Границы функциональности

English:

- ordinary component confirmed state comes from input feedback, not from an optimistic local write;
- `BitCheckList` requires a valid source mask before the first command;
- `TextCommandInput` has no password mode;
- `ValueForm` does not replace or modify `PlgMimMultiSet`;
- `ProcessValue` and `StateIndicator` are read-only and never send commands;
- `OneShotButton` requires good ready feedback and a completed ready-state cycle;
- the five-position limit applies only to the rotary `ModeSelector`, not to its Button layout;
- `ControlsDemo` uses simulated values and never controls real equipment;
- the ordinary component runtime license is installed on the server; authoring and the editor watermark license are separate;
- command controls use the standard Mimic `mainApi` and require no custom backend endpoint;
- the plugin has no dependency on `PlgMimicJP` or `MimicEditorJP`;
- changing a CSS theme is an administrator file operation, not a runtime user setting.

Русский:

- подтверждённое состояние обычного компонента поступает из обратной связи входа, а не из локального оптимистического переключения;
- `BitCheckList` требует корректную исходную маску до первой команды;
- `TextCommandInput` не имеет парольного режима;
- `ValueForm` не заменяет и не изменяет `PlgMimMultiSet`;
- `ProcessValue` и `StateIndicator` предназначены только для чтения и не отправляют команды;
- `OneShotButton` требует достоверного сигнала готовности и завершённого цикла готовности;
- ограничение пяти положений относится только к поворотному `ModeSelector`, а не к виду Button;
- `ControlsDemo` использует искусственные значения и никогда не управляет реальным оборудованием;
- лицензия исполнения обычных компонентов устанавливается на сервере; проектирование и лицензия водяного знака редактора независимы;
- командные компоненты используют стандартный `mainApi` Mimic и не требуют собственного backend endpoint;
- плагин не зависит от `PlgMimicJP` и `MimicEditorJP`;
- смена CSS-темы является файловой операцией администратора, а не пользовательской настройкой во время выполнения.

## Video / Видео

The demonstration shows operator control components on a Rapid SCADA mimic and their configuration in the editor.

В демонстрации показаны операторские элементы управления на мнемосхеме Rapid SCADA и их настройка в редакторе.

[Watch the PlgMimControlsJP demonstration / Посмотреть демонстрацию PlgMimControlsJP](https://jurasskpark.ru/files/github/PlgMimControlsJP.mp4)

## Screenshots / Скриншоты

![PlgMimControlsJP components in a running Rapid SCADA mimic](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/ScadaWeb/PlgMimControlsJP/Source/PlgMimControlsJP_001.png)
![PlgMimControlsJP components in a running Rapid SCADA mimic](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/ScadaWeb/PlgMimControlsJP/Source/PlgMimControlsJP_002.png)
![PlgMimControlsJP components in a running Rapid SCADA mimic](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/ScadaWeb/PlgMimControlsJP/Source/PlgMimControlsJP_003.png)
![PlgMimControlsJP components in a running Rapid SCADA mimic](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/ScadaWeb/PlgMimControlsJP/Source/PlgMimControlsJP_004.png)
![PlgMimControlsJP components in a running Rapid SCADA mimic](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/ScadaWeb/PlgMimControlsJP/Source/PlgMimControlsJP_005.png)
![PlgMimControlsJP components in a running Rapid SCADA mimic](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/ScadaWeb/PlgMimControlsJP/Source/PlgMimControlsJP_006.png)
![PlgMimControlsJP components in a running Rapid SCADA mimic](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/ScadaWeb/PlgMimControlsJP/Source/PlgMimControlsJP_007.png)

## License / Лицензия

`PlgMimControlsJP` is distributed as shareware/commercial software. Creating and saving ordinary control components does not require a local component license. Their execution in Webstation requires a valid server-side product license; otherwise only those runtime components are replaced by inert license warnings. The autonomous `ControlsDemo` remains available on simulated data. A `MimicEditorJP` watermark license is separate. Do not rename the plugin DLL, activation request or `PlgMimControlsJP_License.bin` file.

`PlgMimControlsJP` распространяется как условно-бесплатное/коммерческое программное обеспечение. Создание и сохранение обычных компонентов управления не требует локальной лицензии компонента. Для их исполнения в Вебстанции нужна действующая серверная лицензия продукта; без неё только эти runtime-компоненты заменяются инертными сообщениями о лицензии. Автономный `ControlsDemo` остаётся доступным на искусственных данных. Лицензия водяного знака `MimicEditorJP` независима. Не переименовывайте DLL плагина, запрос активации и файл `PlgMimControlsJP_License.bin`.
