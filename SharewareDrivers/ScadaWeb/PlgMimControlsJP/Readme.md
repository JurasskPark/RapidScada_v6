# PlgMimControlsJP — Operator Controls for Rapid SCADA

![Rapid SCADA](https://img.shields.io/badge/Rapid%20SCADA-6.x-blue.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![Version](https://img.shields.io/badge/version-6.0.1-green.svg)
![Platform](https://img.shields.io/badge/platform-Windows%20%7C%20Linux-lightgrey.svg)

## About This Guide / О руководстве

This guide explains how engineers, operators and administrators use `PlgMimControlsJP` in Rapid SCADA mimic diagrams. It covers the component catalog, channel bindings, command confirmation, data quality, value-entry forms, themes, installation, activation and troubleshooting.

Это руководство предназначено для инженеров, операторов и администраторов, которые используют `PlgMimControlsJP` в мнемосхемах Rapid SCADA. В нём описаны состав компонентов, привязка каналов, подтверждение команд, качество данных, формы ввода значений, темы, установка, активация и устранение неполадок.

`PlgMimControlsJP` version `6.0.1` adds thirteen localized components to the **CONTROLS / УПРАВЛЕНИЕ** toolbox group. The plugin uses the public standard Mimic contract and works with both the standard Mimic Editor and compatible alternative editors. It does not require `PlgMimicJP` or a separate backend API.

`PlgMimControlsJP` версии `6.0.1` добавляет тринадцать локализованных компонентов в группу **CONTROLS / УПРАВЛЕНИЕ**. Плагин использует публичный стандартный контракт Mimic и работает со стандартным Mimic Editor и совместимыми альтернативными редакторами. Зависимость от `PlgMimicJP` и отдельный серверный API не требуются.

## Features / Возможности

English:

- thirteen display, selection, command and multi-value form components;
- standard Rapid SCADA input and output channel bindings;
- numeric, UTF-8 text and exact Hex-byte commands where applicable;
- confirmed-state rendering without optimistic state changes;
- optional command-pending frame with a configurable color;
- explicit handling of missing and bad-quality input data;
- horizontal and vertical option groups, bit lists and discrete sliders;
- three layouts for numeric step buttons;
- built-in numeric and RUS/ENG on-screen keyboards;
- a configurable multi-row value input form with six editor types;
- four complete light and dark CSS themes;
- Russian and English toolbox names, properties and runtime captions;
- safe editor preview: commands are disabled while a mimic is being edited.

Русский:

- тринадцать компонентов отображения, выбора, управления и группового ввода;
- стандартные привязки к входным и выходным каналам Rapid SCADA;
- числовые команды, текст UTF-8 и точные Hex-байты для поддерживаемых компонентов;
- отображение только подтверждённого состояния без оптимистического переключения;
- необязательная рамка ожидания команды с настраиваемым цветом;
- явная обработка отсутствующих данных и плохого качества;
- горизонтальное и вертикальное расположение вариантов, битов и дискретного ползунка;
- три варианта размещения кнопок шага числового ввода;
- встроенные цифровая и экранная RUS/ENG-клавиатуры;
- настраиваемая многострочная форма ввода с шестью типами редакторов;
- четыре полные светлые и тёмные CSS-темы;
- русские и английские названия компонентов, свойств и рабочих надписей;
- безопасное превью в редакторе, в котором отправка команд заблокирована.

## Quick Start / Быстрый старт

English:

1. Install, enable and activate `PlgMimControlsJP`, then restart SCADA Web.
2. Open a mimic in a compatible Mimic Editor.
3. Select a component from the **CONTROLS** group and place it on the canvas.
4. For a display component, set its input channel.
5. For a command component, set both the feedback input channel and the output command channel.
6. Configure captions, values, colors, ranges or options required by that component.
7. Save the mimic and open it in Webstation.
8. Verify the operator has control rights and the output channel accepts commands.
9. Send a command and check that the device writes the resulting state back to the input channel.

Русский:

1. Установите, включите и активируйте `PlgMimControlsJP`, затем перезапустите SCADA Web.
2. Откройте мнемосхему в совместимом редакторе Mimic.
3. Выберите элемент в группе **УПРАВЛЕНИЕ** и поместите его на полотно.
4. Для компонента отображения укажите входной канал.
5. Для компонента управления укажите входной канал обратной связи и выходной канал команды.
6. Настройте требуемые подписи, значения, цвета, диапазоны или варианты.
7. Сохраните мнемосхему и откройте её в Вебстанции.
8. Проверьте наличие у оператора права управления и разрешение команд для выходного канала.
9. Отправьте команду и убедитесь, что устройство возвращает итоговое состояние во входной канал.

Commands are intentionally disabled in edit mode. A command component does not switch its confirmed state immediately after a click: it waits for input-channel feedback. Input and output channel numbers may be different.

В режиме редактирования команды намеренно заблокированы. После нажатия компонент управления не переключает подтверждённое состояние сразу, а ожидает обратную связь входного канала. Номера входного и выходного каналов могут различаться.

## Component Catalog / Каталог компонентов

| Type name | English toolbox name | Русское название | Default size / Размер | Purpose / Назначение |
|---|---|---|---|---|
| `BitCheckList` | Bit check list | Список битов | `170 × 110` | Edit selected bits without losing hidden bits / Изменение выбранных битов без потери скрытых |
| `CheckBox` | Check box | Флажок | `140 × 32` | Binary `0 / 1` command / Двоичная команда `0 / 1` |
| `ComboBox` | Combo box | Выпадающий список | `160 × 34` | Select one configured numeric value / Выбор одного числового значения |
| `DiscreteSlider` | Discrete slider | Дискретный ползунок | `280 × 82` | Select an exact configured division / Выбор точного деления |
| `IlluminatedButton` | Illuminated button | Кнопка с подсветкой | `140 × 64` | Send one fixed command and show feedback / Одна фиксированная команда и индикация обратной связи |
| `LatchedButton` | Latched button | Фиксируемая кнопка | `130 × 42` | Two-state command button / Двухпозиционная командная кнопка |
| `NumericUpDown` | Numeric input | Числовой ввод | `140 × 36` | Validated number and step commands / Проверяемый числовой ввод и команды шага |
| `ProcessValue` | Process value | Текущее значение | `160 × 42` | Read-only formatted process value / Форматированное значение только для чтения |
| `RadioButtonGroup` | Radio button group | Переключатели | `160 × 64` | Visible selection of one configured value / Наглядный выбор одного значения |
| `SquareToggle` | Square toggle | Квадратный переключатель | `60 × 30` | Compact square binary switch / Компактный квадратный переключатель |
| `StateIndicator` | State indicator | Индикатор состояния | `140 × 64` | Read-only state lamp / Лампа состояния только для чтения |
| `TextCommandInput` | Command input | Ввод команды | `250 × 36` | Number, UTF-8 or Hex command entry / Ввод числа, UTF-8 или Hex-команды |
| `ValueForm` | Value input form | Форма ввода значений | `210 × 44` | Multi-row modal value entry / Многострочная модальная форма ввода |

## Channels, Commands and Confirmation / Каналы, команды и подтверждение

Most interactive controls use an input channel for confirmed feedback and an output channel for commands. A command is available only in runtime when the component is enabled, the operator has control rights, the output channel number is greater than zero and the Webstation command API is available.

Большинство интерактивных компонентов используют входной канал для подтверждённой обратной связи и выходной канал для команд. Команда доступна только во время выполнения, если компонент включён, оператор имеет право управления, номер выходного канала больше нуля и доступен командный API Вебстанции.

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

Command controls have an optional `Show pending frame` property. It is disabled by default. When enabled, `Pending frame color` appears; its default is amber `#D97706`.

У командных компонентов есть необязательное свойство `Показывать рамку ожидания`. По умолчанию оно выключено. После включения появляется свойство `Цвет рамки ожидания` с исходным янтарным цветом `#D97706`.

For a control with an input channel, pending state ends when the expected good input value arrives, command sending is rejected, or the ten-second safety timeout expires. Output-only fields clear the pending state after the server acknowledges the command. The frame never replaces the actual channel state.

Для компонента с входным каналом ожидание заканчивается после получения ожидаемого достоверного значения, ошибки отправки или защитного тайм-аута 10 секунд. Поля только с выходным каналом снимают ожидание после подтверждения команды сервером. Рамка никогда не заменяет фактическое состояние канала.

## Selection Controls / Компоненты выбора

### ComboBox

Configure `InCnlNum`, `OutCnlNum` and the `Options` list. Each option contains visible text and a numeric value. The input value selects the matching item; choosing an item sends its value once. If the input is missing, bad or not present in the list, the field is empty and no configured option is selected. Numeric `-1` is not reserved and may be used normally.

Настройте `InCnlNum`, `OutCnlNum` и список `Варианты`. Каждый вариант содержит видимую надпись и числовое значение. Входное значение выбирает совпадающий пункт, а выбор пункта один раз отправляет его значение. При отсутствии данных, плохом качестве или неизвестном значении поле остаётся пустым. Число `-1` не зарезервировано и может использоваться как обычное значение.

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

`BitCheckList` is the only command component disabled before receiving a valid non-negative integer input value. This prevents accidental loss of hidden bits. The list may be vertical or horizontal; horizontal mode uses scrolling when the component is too narrow.

`BitCheckList` — единственный командный компонент, который заблокирован до получения корректного целого неотрицательного входного значения. Это предотвращает случайную потерю скрытых битов. Список может быть вертикальным или горизонтальным; при нехватке ширины горизонтальный режим использует прокрутку.

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

- Rapid SCADA 6.x;
- the .NET 8 runtime used by SCADA Web;
- Mimic diagrams and a compatible Mimic Editor;
- configured input and output channels;
- operator control rights for command components;
- a valid `PlgMimControlsJP` product license for placing new components;
- a package matching the installed Rapid SCADA build.

Требования:

- Rapid SCADA 6.x;
- среда .NET 8, используемая SCADA Web;
- поддержка мнемосхем и совместимый редактор Mimic;
- настроенные входные и выходные каналы;
- право управления у оператора для командных компонентов;
- действующая лицензия продукта `PlgMimControlsJP` для добавления новых элементов;
- пакет, соответствующий установленной сборке Rapid SCADA.

Installation:

1. Copy the supplied `SCADA` package over the Rapid SCADA installation directory while preserving its directory structure. The package includes the required LicenseJPLite runtime files.
2. Enable `PlgMimControlsJP` in the Webstation plugin configuration.
3. On Windows, install the supplied `PlgMimControlsJP.View.dll` in `ScadaAdmin\Lib` when the classic Administrator must recognize the plugin.
4. Restart SCADA Web, its service or the IIS site. A browser refresh alone does not reload plugin assemblies.
5. Open a mimic editor and verify that the **CONTROLS / УПРАВЛЕНИЕ** group contains thirteen components.
6. After an update, perform a hard browser refresh if old scripts or styles remain cached.

Установка:

1. Скопируйте поставляемый пакет `SCADA` поверх каталога установки Rapid SCADA с сохранением структуры папок. Пакет содержит необходимые файлы среды LicenseJPLite.
2. Включите `PlgMimControlsJP` в конфигурации плагинов Вебстанции.
3. Под Windows установите поставляемый `PlgMimControlsJP.View.dll` в `ScadaAdmin\Lib`, если классический Администратор должен распознавать плагин.
4. Перезапустите SCADA Web, соответствующую службу или сайт IIS. Простое обновление браузера не перезагружает сборки плагина.
5. Откройте редактор мнемосхем и убедитесь, что группа **CONTROLS / УПРАВЛЕНИЕ** содержит тринадцать компонентов.
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

The controls plugin uses its own installation-specific license. A `MimicEditorJP`, `PlgMimTankJP`, `PlgMimPipesJP` or another product license does not activate `PlgMimControlsJP`.

Плагин элементов управления использует собственную лицензию, привязанную к установке. Лицензия `MimicEditorJP`, `PlgMimTankJP`, `PlgMimPipesJP` или другого продукта не активирует `PlgMimControlsJP`.

| Host / Приложение | Activation request / Запрос активации | License / Лицензия |
|---|---|---|
| SCADA Web | `C:\Program Files\SCADA\ScadaWeb\config\PlgMimControlsJP_Activation.bin` | `C:\Program Files\SCADA\ScadaWeb\config\PlgMimControlsJP.bin` |
| ScadaAdminWebJP | `C:\Program Files\SCADA\ScadaAdminWebJP\License\PlgMimControlsJP_Activation.bin` | `C:\Program Files\SCADA\ScadaAdminWebJP\License\PlgMimControlsJP.bin` |

English:

1. Start SCADA Web or ScadaAdminWebJP without a ControlsJP license.
2. The plugin creates `PlgMimControlsJP_Activation.bin` in the host license directory. An existing request is not overwritten.
3. Send the activation request to the license provider.
4. The generated license must preserve the request UID and the exact application name `PlgMimControlsJP`.
5. Save the received key as `PlgMimControlsJP.bin` in the same host license directory.
6. Restart SCADA Web or ScadaAdminWebJP. A browser refresh alone is not sufficient.
7. If both hosts are used, install a valid license in each directory because each host reads only its own license location.

Русский:

1. Запустите SCADA Web или ScadaAdminWebJP без лицензии ControlsJP.
2. Плагин создаст `PlgMimControlsJP_Activation.bin` в папке лицензий хоста. Существующий запрос не перезаписывается.
3. Передайте запрос активации поставщику лицензии.
4. При создании лицензии должны быть сохранены UID из запроса и точное имя приложения `PlgMimControlsJP`.
5. Сохраните полученный ключ под именем `PlgMimControlsJP.bin` в той же папке лицензий хоста.
6. Перезапустите SCADA Web или ScadaAdminWebJP. Простого обновления страницы недостаточно.
7. Если используются оба хоста, установите действующую лицензию в каждый каталог, потому что каждый хост читает только собственную папку лицензий.

If the license is missing, invalid or issued for another `AppName`, existing ControlsJP components continue to load and work because their scripts, styles and subtypes remain registered. The **CONTROLS / УПРАВЛЕНИЕ** toolbox group is hidden until a valid license is installed.

Если лицензия отсутствует, недействительна или выдана для другого `AppName`, существующие компоненты ControlsJP продолжают загружаться и работать, потому что их скрипты, стили и подтипы остаются зарегистрированными. Группа **CONTROLS / УПРАВЛЕНИЕ** скрывается до установки действующей лицензии.

## Troubleshooting / Устранение неполадок

| Symptom / Признак | Cause and action / Причина и действие |
|---|---|
| The **CONTROLS / УПРАВЛЕНИЕ** group is missing / Группа отсутствует | Check `PlgMimControlsJP.bin`, plugin registration, DLL and static assets, then restart the host. Without a license existing components remain usable, but the toolbox is hidden. / Проверьте `PlgMimControlsJP.bin`, регистрацию, DLL и статические ресурсы, затем перезапустите хост. Без лицензии существующие компоненты работают, но toolbox скрыт. |
| `PlgMimControlsJP_Activation.bin` is not created / Запрос активации не создаётся | Verify that `LicenseJP.Logic.dll` and its packaged dependencies are installed beside the plugin runtime and that the host can write to its license directory. / Проверьте `LicenseJP.Logic.dll` и пакетные зависимости рядом со средой плагина, а также право хоста на запись в папку лицензий. |
| The group contains fewer than 13 components / В группе меньше 13 компонентов | The DLL and browser assets are from different versions. Deploy the complete matching package. / DLL и браузерные ресурсы относятся к разным версиям. Установите полный согласованный пакет. |
| A component displays data but does not send a command / Данные видны, но команда не отправляется | Commands are disabled in the editor. In runtime check `Enabled`, operator control rights, `OutCnlNum` and output-channel permissions. / В редакторе команды запрещены. Во время выполнения проверьте `Enabled`, право управления, `OutCnlNum` и разрешение команд выходного канала. |
| The command was accepted but the visible state did not change / Команда принята, но вид не изменился | This is expected until the device writes the result to the input feedback channel. Check `InCnlNum` and device feedback. / До обратной связи это ожидаемо. Проверьте `InCnlNum` и возврат состояния устройством. |
| The pending frame is not visible / Рамка ожидания не видна | `Show pending frame` is off by default. Enable it and select its color if visual feedback is required. / `Показывать рамку ожидания` по умолчанию выключено. Включите его и выберите цвет. |
| `BitCheckList` is disabled / `BitCheckList` заблокирован | The component has not received a good non-negative integer source mask. Check the input channel and its quality. / Не получена достоверная целая неотрицательная маска. Проверьте входной канал и качество. |
| A selection is empty, a checkbox is indeterminate or the slider shows `#.#` / Пустой выбор, неопределённый флажок или `#.#` | The input channel is zero, missing, bad quality or contains an unsupported value. / Входной канал равен нулю, отсутствует, имеет плохое качество или неподдерживаемое значение. |
| A numeric value is rejected / Число отклоняется | Check minimum, maximum, negative-value permission, decimal places and exact step alignment. Exponential notation is not accepted. / Проверьте минимум, максимум, отрицательные значения, точность и соответствие шагу. Экспоненциальная запись не принимается. |
| A text or Hex command is rejected / Текстовая или Hex-команда отклоняется | Check the selected command format. Hex requires pairs of valid hexadecimal digits and no `0x` prefix. / Проверьте формат. Для Hex нужны пары допустимых шестнадцатеричных цифр без `0x`. |
| A `ValueForm` row is not sent / Строка `ValueForm` не отправляется | Apply sends only changed and valid rows. Check the row output channel, validation result and control rights. / Общая кнопка отправляет только изменённые и корректные строки. Проверьте выходной канал строки, результат проверки и права. |
| The classic Administrator reports an assembly load error / Классический Администратор сообщает об ошибке загрузки | Install the matching packaged `PlgMimControlsJP.View.dll`. It is built against `ScadaWebCommon.Subset` for classic Administrator compatibility. / Установите соответствующий пакетный `PlgMimControlsJP.View.dll`. Он собран с `ScadaWebCommon.Subset` для совместимости с классическим Администратором. |
| New files are installed but the old appearance remains / Установлены новые файлы, но остался старый вид | Restart the application or IIS site and perform a hard browser refresh. / Перезапустите приложение или IIS и выполните жёсткое обновление страницы. |

## Scope and Limitations / Границы функциональности

English:

- confirmed visual state always comes from the input channel, not from an optimistic local write;
- `BitCheckList` requires a valid source mask before the first command;
- `TextCommandInput` has no password mode;
- `ValueForm` does not replace or modify `PlgMimMultiSet`;
- `ProcessValue` and `StateIndicator` are read-only and never send commands;
- command controls use the standard Mimic `mainApi` and require no custom backend endpoint;
- the plugin has no dependency on `PlgMimicJP` or `MimicEditorJP`;
- changing a CSS theme is an administrator file operation, not a runtime user setting.

Русский:

- подтверждённое визуальное состояние всегда поступает из входного канала, а не из локального оптимистического переключения;
- `BitCheckList` требует корректную исходную маску до первой команды;
- `TextCommandInput` не имеет парольного режима;
- `ValueForm` не заменяет и не изменяет `PlgMimMultiSet`;
- `ProcessValue` и `StateIndicator` предназначены только для чтения и не отправляют команды;
- командные компоненты используют стандартный `mainApi` Mimic и не требуют собственного backend endpoint;
- плагин не зависит от `PlgMimicJP` и `MimicEditorJP`;
- смена CSS-темы является файловой операцией администратора, а не пользовательской настройкой во время выполнения.

## Video / Видео

The demonstration shows the capacitance components on the Rapid SCADA working diagram and their configuration in the editor.

В демонстрации показаны компоненты емкостей на работающей мнемосхеме Rapid SCADA и их настройка в редакторе.

[Watch the PlgMimControlsJP demonstration / Посмотреть демонстрацию PlgMimControlsJP](https://jurasskpark.ru/files/github/PlgMimControlsJP.mp4)

## Screenshots / Скриншоты

### Runtime mimic / Рабочая мнемосхема

![PlgMimControlsJP components in a running Rapid SCADA mimic](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/ScadaWeb/PlgMimControlsJP/Source/PlgMimControlsJP_001.png)
![PlgMimControlsJP components in a running Rapid SCADA mimic](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/ScadaWeb/PlgMimControlsJP/Source/PlgMimControlsJP_002.png)

### Mimic editor / Редактор мнемосхемы

![PlgMimControlsJP components in a running Rapid SCADA mimic](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/ScadaWeb/PlgMimControlsJP/Source/PlgMimControlsJP_003.png)



## License / Лицензия

`PlgMimControlsJP` is distributed as shareware/commercial software. A valid product license is required to place new control components. Existing mimic diagrams remain loadable when a license is temporarily unavailable, but the toolbox is hidden as described above. Do not rename the plugin DLL, activation request or license file.

`PlgMimControlsJP` распространяется как условно-бесплатное/коммерческое программное обеспечение. Для добавления новых компонентов управления требуется действующая лицензия продукта. Существующие мнемосхемы продолжают загружаться при временном отсутствии лицензии, но группа toolbox скрывается, как описано выше. Не переименовывайте DLL плагина, запрос активации и файл лицензии.
