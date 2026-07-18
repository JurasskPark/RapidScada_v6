# PlgMimPipesJP — Pipe Components for Rapid SCADA

![Rapid SCADA](https://img.shields.io/badge/Rapid%20SCADA-6.x-blue.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![Platform](https://img.shields.io/badge/platform-Windows-lightgrey.svg)

## Overview / Обзор

`PlgMimPipesJP` adds a set of three-dimensional pipe, fitting and equipment components to Rapid SCADA mimic diagrams. Components can be placed manually as `100 × 100` puzzle tiles or generated from route points by the automatic pipe layout tool.

Do not rename the plugin DLL, activation request or license file.

`PlgMimPipesJP` добавляет в мнемосхемы Rapid SCADA набор объёмных труб, фитингов и оборудования. Компоненты можно размещать вручную как блоки-пазлы размером `100 × 100` пикселей или создавать по точкам с помощью авторасстановки трубопровода.

Нельзя переименовывать DLL плагина, файл запроса активации или файл лицензии.

## Features / Возможности

English:

- straight and narrow straight pipes;
- 45° and 90° elbows;
- tee, cross and Y branches;
- U-bend and S-offset;
- concentric reducer, expansion joint and flexible section;
- open end, end cap, blind flange and drain nozzle;
- gate valve, pressure gauge and pump with channel-controlled states;
- pipe diameter, color, orientation and flange properties;
- manual puzzle-style assembly on a fixed `100 × 100` grid;
- automatic layout as separate components or a single editable pipeline.

Русский:

- прямая и узкая прямая трубы;
- отводы 45° и 90°;
- тройник, крестовина и Y-разветвитель;
- U-поворот и S-смещение;
- концентрический переход, компенсатор и гибкий участок;
- открытый торец, заглушка, глухой фланец и сливной патрубок;
- задвижка, манометр и насос с состояниями от входного канала;
- настройка диаметра, цвета, ориентации и фланцев;
- ручная сборка по принципу пазла на фиксированной сетке `100 × 100`;
- авторасстановка отдельными компонентами или одним редактируемым трубопроводом.

## Requirements / Требования

English:

- Rapid SCADA 6.x on Windows;
- an installed and enabled `PlgMimPipesJP` plugin;
- the matching `PlgMimPipesJP` version for automatic layout in ScadaAdminWebJP;
- a separate valid `PlgMimPipesJP` license;
- write permission to the license directory when the activation request is generated.

Русский:

- Rapid SCADA 6.x под Windows;
- установленный и включённый плагин `PlgMimPipesJP`;
- соответствующая версия `PlgMimPipesJP` для авторасстановки в ScadaAdminWebJP;
- отдельная действующая лицензия `PlgMimPipesJP`;
- право записи в папку лицензий при создании запроса активации.

## Installation / Установка

English:

1. Copy the supplied plugin package to the Rapid SCADA installation while preserving its directories.
2. Enable `PlgMimPipesJP` in the Webstation plugin list.
3. When using ScadaAdminWebJP, install the supplied matching versions of both `PlgMimPipesJP`.
4. Restart the corresponding application or IIS site after installing or replacing plugin files.
5. Open a mimic editor and check that the **PIPES** component group is available.

Русский:

1. Скопируйте поставляемый пакет плагина в каталог установки Rapid SCADA с сохранением вложенных папок.
2. Включите `PlgMimPipesJP` в списке плагинов Вебстанции.
3. При использовании ScadaAdminWebJP установите поставляемые совместимые версии `PlgMimPipesJP`.
4. После установки или замены файлов перезапустите соответствующее приложение или сайт IIS.
5. Откройте редактор мнемосхемы и убедитесь, что появилась группа компонентов **ТРУБОПРОВОДЫ**.

## Activation / Активация

The pipe plugin uses its own license. A `MimicEditorJP` license does not activate `PlgMimPipesJP`.

Плагин трубопроводов использует отдельную лицензию. Лицензия `MimicEditorJP` не активирует `PlgMimPipesJP`.

| Host / Приложение | Activation request / Запрос активации | License / Лицензия |
|---|---|---|
| SCADA Web | `C:\Program Files\SCADA\ScadaWeb\config\PlgMimPipesJP_Activation.bin` | `C:\Program Files\SCADA\ScadaWeb\config\PlgMimPipesJP.bin` |
| ScadaAdminWebJP | `C:\Program Files\SCADA\ScadaAdminWebJP\License\PlgMimPipesJP_Activation.bin` | `C:\Program Files\SCADA\ScadaAdminWebJP\License\PlgMimPipesJP.bin` |

English:

1. Start the application without a pipe license.
2. The plugin creates `PlgMimPipesJP_Activation.bin` in the license directory. An existing request is not overwritten.
3. Send this activation request to the license provider.
4. The generated license must preserve the request UID and the exact application name `PlgMimPipesJP`.
5. Save the received key as `PlgMimPipesJP.bin` in the license directory used by the application.
6. Restart SCADA Web or ScadaAdminWebJP. A browser refresh alone is not sufficient.
7. If both applications are used, put a valid license into each directory because each host reads only its own license location.

Русский:

1. Запустите приложение без лицензии трубопроводов.
2. Плагин создаст файл `PlgMimPipesJP_Activation.bin` в папке лицензий. Существующий запрос не перезаписывается.
3. Передайте этот файл поставщику лицензии.
4. При создании лицензии должны быть сохранены UID из запроса и точное имя приложения `PlgMimPipesJP`.
5. Сохраните полученный ключ под именем `PlgMimPipesJP.bin` в папке лицензий используемого приложения.
6. Перезапустите SCADA Web или ScadaAdminWebJP. Простого обновления страницы недостаточно.
7. Если используются оба приложения, положите действующую лицензию в каждую папку, потому что каждый хост читает только свой каталог лицензий.

If the license is missing or invalid, existing pipe components continue to load and display, but the pipe toolbox group is hidden and new components cannot be placed.

Если лицензия отсутствует или недействительна, существующие компоненты труб продолжают загружаться и отображаться, но группа трубопроводов скрыта и добавление новых компонентов запрещено.

## Manual Placement / Ручное размещение

English:

1. Open a mimic in the visual editor.
2. Select a component from the **PIPES** group and place it on the canvas.
3. Keep neighboring component squares aligned. Connection points are located on the edges of the `100 × 100` blocks, so correctly aligned components meet without manually extending the pipe.
4. Use rotation or mirroring in the editor to obtain the required direction.
5. Configure the component properties:
   - **Diameter** — normally from `4` to `200`; the default is `18`;
   - **Pipe color** — presets or an HTML color such as `#748491`, `#f00`, `Red`, `Black`, `Gray`, `Blue`, `Yellow` or `Green`;
   - **Flange** — enables all supported flanges of a manually placed component;
   - **Orientation**, **Rotation** or **Offset side** — depends on the component family;
   - **State input channel** — available for the pump, pressure gauge and gate valve.

Русский:

1. Откройте мнемосхему в визуальном редакторе.
2. Выберите компонент в группе **ТРУБОПРОВОДЫ** и поместите его на полотно.
3. Совмещайте квадраты соседних компонентов. Точки подключения находятся на границах блоков `100 × 100`, поэтому правильно выровненные компоненты стыкуются без ручного удлинения трубы.
4. Используйте поворот или отражение в редакторе, чтобы получить нужное направление.
5. Настройте свойства компонента:
   - **Диаметр** — обычно от `4` до `200`, значение по умолчанию `18`;
   - **Цвет трубы** — готовые варианты или HTML-цвет, например `#748491`, `#f00`, `Red`, `Black`, `Gray`, `Blue`, `Yellow` или `Green`;
   - **Фланец** — включает все поддерживаемые фланцы у вручную размещённого компонента;
   - **Ориентация**, **Поворот** или **Сторона смещения** — зависит от семейства компонента;
   - **Канал состояния** — доступен у насоса, манометра и задвижки.

Changing the diameter changes the visible pipe profile but does not move the `100 × 100` connection points. The narrow straight pipe and the small side of the concentric reducer are intentionally thinner.

Изменение диаметра меняет видимую толщину трубы, но не сдвигает точки подключения блока `100 × 100`. Узкая прямая труба и малая сторона концентрического перехода намеренно имеют меньшую толщину.

## Automatic Pipe Layout / Авторасстановка трубопровода

Automatic layout is supported only by the modern Mimic JP Editor. The original Mimic Editor can place ordinary pipe components but displays a message when automatic layout is selected.

Авторасстановка поддерживается только современным редактором Mimic Editor JP. Оригинальный Mimic Editor позволяет размещать обычные трубные компоненты, но при выборе авторасстановки показывает сообщение о недоступности функции.

English:

1. Select **Automatic element layout** in the **PIPES** group.
2. In the dialog, choose the pipe color, diameter, flanges and result mode. Default values are diameter `18`, gray-blue metal, flanges disabled and **Atomic elements**.
3. Click the route points on the canvas. The first point defines the `100`-pixel lattice; later points snap to horizontal, vertical or 45° diagonal steps.
4. Press `Enter` to finish the route or `Esc` to cancel drawing.
5. Use an intermediate point for a 135° or 180° direction change.

Русский:

1. Выберите **Авторасстановка элементов** в группе **ТРУБОПРОВОДЫ**.
2. В диалоге задайте цвет, диаметр, фланцы и режим результата. По умолчанию используются диаметр `18`, серо-синий металлический цвет, отключённые фланцы и режим **Атомарные элементы**.
3. Щёлкайте по точкам маршрута на полотне. Первая точка задаёт сетку с шагом `100` пикселей, последующие точки привязываются к горизонтальным, вертикальным или диагональным направлениям с шагом 45°.
4. Нажмите `Enter`, чтобы завершить маршрут, или `Esc`, чтобы отменить рисование.
5. Для изменения направления на 135° или 180° добавьте промежуточную точку.

| Result mode / Режим | English description | Русское описание |
|---|---|---|
| **Atomic elements / Атомарные элементы** | Creates separate `100 × 100` straight pipes and elbows. The route is added as one operation and one undo step. | Создаёт отдельные прямые трубы и отводы `100 × 100`. Маршрут добавляется одной операцией и отменяется одним действием. |
| **Single pipeline / Цельный трубопровод** | Creates one `PipeAssembly`. Select it to move, insert or delete route points. Normal component resize is disabled. | Создаёт один `PipeAssembly`. После выделения можно перемещать, добавлять и удалять точки маршрута. Обычное изменение размера отключено. |

The route supports up to 64 points. With flanges enabled, automatic layout creates one flange at each internal joint and one at each route end. The settings confirmed in the dialog are remembered until the editor is closed.

Маршрут поддерживает до 64 точек. При включённых фланцах авторасстановка создаёт по одному фланцу на каждом внутреннем стыке и на концах маршрута. Подтверждённые настройки запоминаются до закрытия редактора.

## Equipment States / Состояния оборудования

The pump, gate valve and pressure gauge obtain their state from the standard input channel binding. The state is calculated at runtime and cannot be selected manually.

Насос, задвижка и манометр получают состояние через стандартную привязку входного канала. Состояние вычисляется во время работы и не выбирается вручную.

### Pump and Gate Valve / Насос и задвижка

| Channel data | Display / Отображение |
|---|---|
| `stat <= 0`, missing channel or non-numeric value | Unknown: amber indicator / Неизвестно: жёлто-оранжевый индикатор |
| `value = 0` | Off: red indicator / Выключено: красный индикатор |
| `value = 1` | Running: green indicator; pump animation is active / Работает: зелёный индикатор; насос анимирован |
| `value = -1` or `value = 2` | Alarm: blinking red indicator / Авария: мигающий красный индикатор |
| any other value | Unknown: amber indicator / Неизвестно: жёлто-оранжевый индикатор |

Only the valve handwheel and pump motor show the equipment state. The pipe body keeps the selected pipe color.

Состояние оборудования отображается только на маховике задвижки и двигателе насоса. Корпус трубы сохраняет выбранный цвет.

### Pressure Gauge / Манометр

| Channel data | Display / Отображение |
|---|---|
| `stat <= 0`, missing channel, `NaN` or negative value | Unknown: housing remains visible and the scale is hidden / Неизвестно: корпус остаётся видимым, шкала скрывается |
| `value = 0` | Zero state with a fixed needle / Нулевое состояние с неподвижной стрелкой |
| `value > 0` | Active state with a white dial and short repeating needle oscillation / Активное состояние с белым циферблатом и коротким повторяющимся движением стрелки |

The current gauge implementation indicates zero, active or unknown state. It does not use the numeric pressure value as a proportional scale position.

Текущая реализация манометра показывает нулевое, активное или неизвестное состояние. Числовое значение давления не используется как пропорциональное положение стрелки на шкале.

## Important Notes / Важные особенности

English:

- Do not resize `PipeAssembly` with the ordinary component resize handles; edit its route points instead.
- The first automatic-layout point fixes the parent container. The route may extend outside that container, including into negative local coordinates.
- Pipe color preserves the original SVG light and shadow. Semantic colors of the pump motor, valve handwheel and gauge mechanism are not replaced by the pipe color.
- A flange is intentionally slightly wider than the pipe.
- A license or plugin update requires an application restart.

Русский:

- Не изменяйте размер `PipeAssembly` обычными маркерами компонента — редактируйте точки маршрута.
- Первая точка авторасстановки фиксирует родительский контейнер. Маршрут может выходить за его границы, в том числе в отрицательные локальные координаты.
- Цвет трубы сохраняет исходные свет и тени SVG. Смысловые цвета двигателя насоса, маховика задвижки и механизма манометра не заменяются цветом трубы.
- Фланец намеренно немного шире трубы.
- После обновления лицензии или плагина требуется перезапуск приложения.

## Troubleshooting / Устранение неполадок

| Symptom / Признак | Cause and action / Причина и действие |
|---|---|
| The **PIPES** group is missing / Нет группы **ТРУБОПРОВОДЫ** | Check that the plugin is enabled, `PlgMimPipesJP.bin` is in the license directory of the current host, and the application was restarted. / Проверьте включение плагина, наличие `PlgMimPipesJP.bin` в папке лицензий текущего приложения и выполните перезапуск. |
| The log says the license is not for this copy / В журнале указано, что лицензия не для этой копии | The UID does not match this computer or the license was generated from another activation request. / UID не соответствует компьютеру либо использован запрос активации от другой системы. |
| The log reports an application-name mismatch / В журнале указано несовпадение имени приложения | Regenerate the license with the exact application name `PlgMimPipesJP`; a default name such as `Demo` is rejected. / Пересоздайте лицензию с точным именем `PlgMimPipesJP`; имя по умолчанию, например `Demo`, отклоняется. |
| No activation request is created / Запрос активации не создаётся | Check write permissions for `config` or `License`, the plugin log and the presence of the supplied licensing runtime. / Проверьте права записи в `config` или `License`, журнал плагина и наличие поставляемого модуля лицензирования. |
| Manual components work, but automatic layout is unavailable / Ручные компоненты работают, но авторасстановка недоступна | Open the mimic in Mimic JP Editor and install the matching `PlgMimicJP` version. Automatic layout is intentionally disabled in the original editor. / Откройте мнемосхему в Mimic JP Editor и установите соответствующую версию `PlgMimicJP`. В оригинальном редакторе авторасстановка намеренно отключена. |
| Equipment always shows Unknown / Оборудование всегда показывает неизвестное состояние | Verify the input channel number, channel status and value. A channel with `stat <= 0` is considered unreliable. / Проверьте номер входного канала, его статус и значение. Канал с `stat <= 0` считается недостоверным. |
| New files are installed but the old UI remains / Установлены новые файлы, но интерфейс не изменился | Restart the application or IIS site, then perform a hard browser refresh. / Перезапустите приложение или сайт IIS, затем выполните жёсткое обновление страницы. |

## Video / Видео

The demonstration shows pipe components in a running Rapid SCADA mimic and their configuration in the editor.

В демонстрации показаны трубопроводные компоненты на работающей мнемосхеме Rapid SCADA и их настройка в редакторе.

[Watch the PlgMimPipesJP demonstration / Посмотреть демонстрацию PlgMimPipesJP](https://jurasskpark.ru/files/github/PlgMimPimpJP.mp4)

## Screenshots / Скриншоты

### Runtime mimic / Рабочая мнемосхема

![PlgMimPipesJP components in a running Rapid SCADA mimic](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/ScadaWeb/PlgMimPipesJP/Source/PlgMimPipesJP_001.png)

### Mimic editor / Редактор мнемосхемы

![PlgMimPipesJP components and properties in the mimic editor](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/ScadaWeb/PlgMimPipesJP/Source/PlgMimPipesJP_002.png)

## License / Лицензия

`PlgMimPipesJP` is distributed as shareware/commercial software. A valid product license is required to place new pipe components. Existing licensed mimic diagrams remain readable when a license is temporarily unavailable, but editing and new placement are restricted as described above.

`PlgMimPipesJP` распространяется как условно-бесплатное/коммерческое программное обеспечение. Для добавления новых трубных компонентов требуется действующая лицензия продукта. Существующие мнемосхемы продолжают открываться при временном отсутствии лицензии, но редактирование и добавление новых компонентов ограничиваются, как описано выше.
