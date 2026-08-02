# PlgTrendJP — Interactive Trends for Rapid SCADA

![Rapid SCADA](https://img.shields.io/badge/Rapid%20SCADA-6.x-blue.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![Platform](https://img.shields.io/badge/platform-Windows%20%7C%20Linux-lightgrey.svg)

## About This Guide / О руководстве

This guide explains how operators, engineers and administrators use `PlgTrendJP`. It covers the full TrendJP page, the embedded `TrendWindow` mimic component, view configuration, installation and licensing.

Это руководство предназначено для операторов, инженеров и администраторов, которые работают с `PlgTrendJP`. В нём описаны полная страница TrendJP, встроенный компонент мнемосхемы `TrendWindow`, настройка представления, установка и лицензирование.

`PlgTrendJP` displays current and historical Rapid SCADA channel data as interactive trends. The user can select channels and archives, change the time range, compare several archive sources, choose a chart type, save profiles and export loaded data to Excel.

`PlgTrendJP` отображает текущие и архивные данные каналов Rapid SCADA в виде интерактивных трендов. Пользователь может выбирать каналы и архивы, изменять период, сравнивать несколько архивных источников, выбирать тип графика, сохранять профили и экспортировать загруженные данные в Excel.

## Quick Start / Быстрый старт

English:

1. Open a view of type `TrendJP` in Webstation.
2. Click **Select...** in the **Channels** field.
3. Find channels by object, device or text search, select them and click **Apply**.
4. Select the archive that contains the required data.
5. Set the **From** and **To** date and time.
6. Click **Refresh** to load the trend.
7. Use the mouse wheel to zoom, drag the chart to pan and click **Reset Zoom** to return to the complete loaded range.
8. Open **Actions** to change the chart type, display settings, profiles or Excel export options.

Русский:

1. Откройте в Вебстанции представление типа `TrendJP`.
2. Нажмите **Выбрать...** в поле **Каналы**.
3. Найдите каналы по объекту, устройству или тексту, отметьте их и нажмите **Применить**.
4. Выберите архив, содержащий нужные данные.
5. Укажите дату и время **От** и **До**.
6. Нажмите **Обновить**, чтобы загрузить тренд.
7. Масштабируйте график колесом мыши, перемещайте его перетаскиванием и нажимайте **Сбросить масштаб**, чтобы вернуться ко всему загруженному диапазону.
8. Откройте меню **Действия**, чтобы изменить тип графика, настройки отображения, профили или параметры экспорта Excel.

If no channels are selected, the plot remains empty without showing an error. Select at least one permitted active input channel to begin.

Если каналы не выбраны, область графика остаётся пустой без сообщения об ошибке. Для начала работы выберите хотя бы один доступный активный входной канал.

## Main Page Controls / Элементы основной страницы

| Control / Элемент | Purpose / Назначение |
|---|---|
| **Channels / Каналы** | Opens channel selection. The licensed `CountTags` value limits the number of unique channel numbers. / Открывает выбор каналов. Количество уникальных номеров ограничивается лицензией `CountTags`. |
| **Archive / Архив** | Selects current, minute, hourly, daily or another configured archive. / Выбирает текущий, минутный, часовой, суточный или другой настроенный архив. |
| **Multiple archives / Несколько архивов** | Enables up to four independently configured archive sources. / Включает до четырёх независимо настраиваемых архивных источников. |
| **From / От**, **To / До** | Defines the loaded local time range with second precision. / Задаёт загружаемый диапазон локального времени с точностью до секунды. |
| **Timer / Таймер** | Selects `Auto` or a fixed refresh interval. **Start** and **Stop** control periodic refresh. / Выбирает `Auto` или фиксированный интервал. Кнопки **Старт** и **Стоп** управляют периодическим обновлением. |
| **Tooltip / Подсказка** | Switches between the nearest channel and all channels at the cursor time. / Переключает подсказку ближайшего канала и всех каналов во времени курсора. |
| **Refresh / Обновить** | Reads data for the dates currently shown in the calendar fields. / Загружает данные для дат, указанных в полях календаря. |
| **Reset Zoom / Сбросить масштаб** | Restores the complete loaded time range. / Возвращает полный загруженный диапазон времени. |
| **Actions / Действия** | Opens chart type, display settings, profiles and Excel export. / Открывает выбор типа графика, настройки отображения, профили и экспорт Excel. |
| **Timeline / Временная шкала** | Shows the complete loaded period and the currently visible window. / Показывает полный загруженный период и текущее видимое окно. |

## Selecting Channels and Archives / Выбор каналов и архивов

The channel dialog supports filters by object, device and text. **Selected only** temporarily shows only checked channels. **Available in selected archive** is enabled by default and hides channels that are not written to the selected archive.

Диалог выбора каналов поддерживает фильтры по объекту, устройству и тексту. Фильтр **Только выбранные** временно показывает только отмеченные каналы. Фильтр **Доступные в выбранном архиве** включён по умолчанию и скрывает каналы, которые не записываются в выбранный архив.

An already selected channel that is unavailable in the current archive remains visible and is marked. This lets the user remove it deliberately instead of losing the selection silently. The available objects and channels also depend on the rights of the current Webstation user.

Ранее выбранный канал, отсутствующий в текущем архиве, остаётся видимым и получает отметку. Пользователь может удалить его осознанно, а не потерять выбор незаметно. Доступные объекты и каналы также зависят от прав текущего пользователя Вебстанции.

To compare data from different archives, enable **Multiple archives** and add up to four sources. Each source has its own archive, channel list and enabled switch. Series names include the archive code so that identical channel numbers from different sources remain distinguishable.

Чтобы сравнить данные из разных архивов, включите **Несколько архивов** и добавьте до четырёх источников. У каждого источника есть собственный архив, список каналов и флаг включения. В названия рядов добавляется код архива, поэтому одинаковые номера каналов из разных источников можно различить.

## Working with the Chart / Работа с графиком

### Zoom and Pan / Масштабирование и перемещение

- Rotate the mouse wheel over the plot to zoom the time axis around the cursor.
- Drag the plot horizontally to move through the loaded data.
- Drag the highlighted timeline window to move it.
- Drag a timeline window edge to change the visible interval.
- Click **Reset Zoom** to display the complete loaded range.

- Вращайте колесо мыши над графиком, чтобы масштабировать ось времени относительно курсора.
- Перетаскивайте график по горизонтали, чтобы перемещаться по загруженным данным.
- Перетаскивайте выделенное окно на временной шкале, чтобы перемещать его.
- Перетаскивайте границу окна временной шкалы, чтобы изменить видимый интервал.
- Нажмите **Сбросить масштаб**, чтобы показать весь загруженный диапазон.

Zooming and panning use data that is already loaded. They do not change the **From** and **To** calendar fields and do not request the archive again.

Масштабирование и перемещение используют уже загруженные данные. Они не изменяют календарные поля **От** и **До** и не запрашивают архив повторно.

### Previous and Next / Назад и вперёд

The arrow buttons beside the timeline change the calendar fields first and refresh the trend only after the new range is valid. Configure their behavior in **Actions → Display settings**.

Кнопки со стрелками по сторонам временной шкалы сначала изменяют поля календаря и только после проверки нового диапазона обновляют тренд. Их поведение настраивается через **Действия → Настройки отображения**.

| Setting / Настройка | Values / Значения | Meaning / Значение |
|---|---|---|
| **Step / Шаг** | Positive integer / Положительное целое число | Number of selected time units per click. / Количество выбранных единиц времени за одно нажатие. |
| **Unit / Единица** | Seconds, minutes, hours, days / Секунды, минуты, часы, дни | Unit used by the step. / Единица измерения шага. |
| **Shift / Сдвигать период** | Default / По умолчанию | Previous or Next moves both boundaries and preserves the interval width. / Назад или Вперёд перемещает обе границы, сохраняя ширину периода. |
| **Expand / Расширять период** | Optional / Дополнительно | Previous moves only **From** backward; Next moves only **To** forward. Repeated clicks expand the range. / Назад перемещает только **От** назад; Вперёд перемещает только **До** вперёд. Повторные нажатия расширяют диапазон. |

Example with a one-day step:

- Shift: `23.07 00:00 – 24.07 00:00` → `22.07 00:00 – 23.07 00:00`.
- Expand with Previous: `23.07 00:00 – 24.07 00:00` → `22.07 00:00 – 24.07 00:00`.

Пример с шагом в одни сутки:

- Сдвиг: `23.07 00:00 – 24.07 00:00` → `22.07 00:00 – 23.07 00:00`.
- Расширение кнопкой Назад: `23.07 00:00 – 24.07 00:00` → `22.07 00:00 – 24.07 00:00`.

Day steps use local calendar dates. Second, minute and hour steps preserve the selected precision.

Шаг в сутках использует локальные календарные даты. Шаги в секундах, минутах и часах сохраняют выбранную точность.

### Tooltip / Подсказка

In the default mode, the tooltip shows the nearest channel value. Enable **All channels** to display all available channel values for the cursor time. Every row keeps the individual channel color. The marker shape follows the selected `circle`, `square` or `triangle` setting in both tooltip modes.

По умолчанию подсказка показывает значение ближайшего канала. Включите **Все каналы**, чтобы увидеть все доступные значения во времени курсора. Каждая строка сохраняет индивидуальный цвет канала. Форма маркера `circle`, `square` или `triangle` используется в обоих режимах подсказки.

### Interactive Legend / Интерактивная легенда

The legend of ordinary Cartesian charts can temporarily hide channels without changing the configured channel list:

- click a legend item to hide or show that series;
- `Shift+click` or double-click to show only that series;
- repeat isolation of the only visible series to restore all series.

Легенда обычных декартовых графиков позволяет временно скрывать каналы без изменения настроенного списка:

- щёлкните элемент легенды, чтобы скрыть или показать ряд;
- используйте `Shift+щелчок` или двойной щелчок, чтобы оставить только выбранный ряд;
- повторите изоляцию единственного видимого ряда, чтобы вернуть все ряды.

A hidden item remains dimmed and struck through in the legend. Hidden series are excluded from the plot, tooltip and timeline. Axis ranges stay stable, and temporary visibility is not saved in the URL, last configuration or profile.

Скрытый элемент остаётся в легенде приглушённым и зачёркнутым. Скрытые ряды исключаются из графика, подсказки и временной шкалы. Диапазоны осей не меняются, а временная видимость не сохраняется в URL, последней конфигурации или профиле.

## Actions Menu / Меню «Действия»

### Display Settings / Настройки отображения

| Setting / Настройка | Available behavior / Доступное поведение |
|---|---|
| **Show control panel / Показывать панель управления** | Hides the filters and direct action buttons but keeps the **Actions** menu. / Скрывает фильтры и прямые кнопки, сохраняя меню **Действия**. |
| **Show timeline / Показывать временную шкалу** | Shows or hides the overview and Previous/Next buttons. / Показывает или скрывает обзор и кнопки Назад/Вперёд. |
| **Navigation step and mode / Шаг и режим перехода** | Configures Previous/Next as described above. / Настраивает кнопки Назад/Вперёд, как описано выше. |
| **Legend position / Положение легенды** | Disabled, top, right, bottom or left. / Отключена, сверху, справа, снизу или слева. |
| **Point marker / Маркер точки** | Circle, triangle or square. Also used by tooltips. / Круг, треугольник или квадрат. Также используется в подсказках. |
| **Line width / Толщина линии** | `1`, `1.5`, `2`, `3` or `4` CSS pixels. / `1`, `1.5`, `2`, `3` или `4` CSS-пикселя. |
| **Point size / Размер точки** | `2`, `3`, `4`, `5`, `6` or `8` CSS pixels. / `2`, `3`, `4`, `5`, `6` или `8` CSS-пикселей. |

Display settings are stored in the page URL, the last browser configuration and named profiles. Hiding the control panel or timeline redraws the existing data and does not reload the archive.

Настройки отображения сохраняются в URL страницы, последней конфигурации браузера и именованных профилях. Скрытие панели управления или временной шкалы перерисовывает уже загруженные данные и не перечитывает архив.

### Profiles / Профили

A profile saves the channel list, archive sources, period, chart type, display, tooltip and Excel settings.

Профиль сохраняет список каналов, архивные источники, период, тип графика, отображение, подсказку и настройки Excel.

English:

1. Configure the trend as required.
2. Open **Actions → Profiles**.
3. Enter a profile name and click **Save**.
4. Select a saved profile and click **Apply** to restore it.
5. Use **Delete** to remove a selected profile.
6. Use **Show last** to restore the most recent automatically saved configuration for this view.

Русский:

1. Настройте тренд требуемым образом.
2. Откройте **Действия → Профили тренда**.
3. Введите название и нажмите **Сохранить**.
4. Выберите сохранённый профиль и нажмите **Применить**, чтобы восстановить его.
5. Нажмите **Удалить**, чтобы удалить выбранный профиль.
6. Нажмите **Показать последнее**, чтобы восстановить последнюю автоматически сохранённую конфигурацию этого представления.

Profiles and the last configuration are stored locally in the browser. They are not automatically shared with another browser, computer or user. Use `View.Args` for administrator-defined defaults that must be available to everyone.

Профили и последняя конфигурация хранятся локально в браузере. Они не переносятся автоматически в другой браузер, на другой компьютер или к другому пользователю. Для общих настроек, заданных администратором, используйте `View.Args`.

## Trend Types / Типы тренда

Open **Actions → Trend type** and select the presentation that matches the data.

Откройте **Действия → Тип тренда** и выберите представление, подходящее данным.

| Value / Значение | Recommended use / Рекомендуемое применение |
|---|---|
| `line` | Standard analog values. / Обычные аналоговые значения. |
| `points` | Individual samples without connecting lines. / Отдельные отсчёты без соединяющих линий. |
| `line-markers` | Line with visible sample positions. / Линия с видимыми положениями отсчётов. |
| `stepped` | Discrete, retained and state values. / Дискретные, удерживаемые значения и состояния. |
| `smooth` | Visually smoothed process curve. / Визуально сглаженная технологическая кривая. |
| `area` | Filled area under a curve. / Область с заливкой под кривой. |
| `bar` | Values compared as bars. / Сравнение значений столбиками. |
| `multiple-axes` | Channels with different numeric ranges, grouped into up to four Y axes. / Каналы с разными числовыми диапазонами, объединённые максимум в четыре оси Y. |
| `limits` | Channel trends with configured low and high limits. / Тренды каналов с настроенными нижними и верхними границами. |
| `polar` | Latest values interpreted as angles on a 360° plot. / Последние значения как углы на диаграмме 360°. |
| `pie` | Latest good values shown as pie sectors. / Последние достоверные значения в виде секторов. |
| `radial-gauge` | Latest values of up to the first six series as radial indicators. / Последние значения максимум шести первых рядов в виде радиальных индикаторов. |
| `single-gauge` | Latest value of the first available series. / Последнее значение первого доступного ряда. |
| `normalized-gauge` | Latest values of up to the first six series normalized to `0–100%`. / Последние значения максимум шести первых рядов, нормализованные до `0–100%`. |
| `dynamogram` | The first two channels paired by equal good-quality timestamps. / Первые два канала, объединённые по одинаковому времени достоверных точек. |

`pie`, gauge and polar modes use the latest good values rather than the complete time curve. `dynamogram` requires at least two channels.

Режимы `pie`, индикаторы и полярная диаграмма используют последние достоверные значения, а не всю временную кривую. Для `dynamogram` требуется не менее двух каналов.

## Multiple Axes / Несколько осей

`multiple-axes` creates no more than four real Y-axis groups. It does not create one axis for every channel.

`multiple-axes` создаёт не более четырёх настоящих групп осей Y. Отдельная ось для каждого канала не создаётся.

Automatic grouping uses all valid values in the complete selected **From–To** period and does not change when the user zooms the chart:

1. TrendJP finds the minimum, maximum and midpoint of every channel.
2. The complete numeric range of all channels is divided into four equal high-to-low bins.
3. A channel is assigned by its midpoint. A value exactly on an internal boundary belongs to the lower bin.
4. Empty groups are removed.
5. Every visible group receives an axis covering the actual values of its channels with five-percent padding.

Автоматическая группировка использует все достоверные значения полного выбранного периода **От–До** и не изменяется при масштабировании:

1. TrendJP находит минимум, максимум и середину каждого канала.
2. Общий числовой диапазон всех каналов делится на четыре равные корзины сверху вниз.
3. Канал назначается по середине своего диапазона. Значение точно на внутренней границе относится к нижней корзине.
4. Пустые группы удаляются.
5. Каждая видимая группа получает ось по фактическим значениям своих каналов с запасом пять процентов.

Axes are labelled `Y1`–`Y4`, alternate between the left and right sides and use the same grouping in the main plot, timeline and tooltip. The grid follows only the first visible axis.

Оси обозначаются `Y1`–`Y4`, попеременно размещаются слева и справа и используют одинаковую группировку на основном графике, временной шкале и в подсказке. Сетка строится только по первой видимой оси.

An administrator can pin channels to groups through `axisGroup1`, `axisGroup2`, `axisGroup3` and `axisGroup4`. Unlisted channels remain automatic:

```text
trendType=multiple-axes&axisGroup1=1,2&axisGroup2=3-5
```

In XML:

```xml
<Args>trendType=multiple-axes&amp;axisGroup1=1,2&amp;axisGroup2=3-5</Args>
```

Администратор может закрепить каналы за группами через `axisGroup1`, `axisGroup2`, `axisGroup3` и `axisGroup4`. Неуказанные каналы продолжают распределяться автоматически.

If a channel is listed in several groups, the lowest group number wins and the page displays a warning. Unselected or nonexistent channels are ignored. A manually listed channel number from different archive sources receives the same group. Manual grouping of channels with very different ranges can make the smaller signal look almost flat; this is the expected result of the chosen grouping.

Если канал указан в нескольких группах, действует группа с наименьшим номером, а страница выводит предупреждение. Невыбранные или несуществующие каналы игнорируются. Вручную указанный номер канала из разных архивных источников получает одну группу. Принудительное объединение сильно различающихся диапазонов может сделать меньший сигнал почти прямым — это ожидаемый результат выбранной настройки.

The `axisGroup1`–`axisGroup4` settings are saved in URLs and profiles but affect only `multiple-axes`.

Настройки `axisGroup1`–`axisGroup4` сохраняются в URL и профилях, но влияют только на `multiple-axes`.

## Automatic Refresh / Автоматическое обновление

Select a timer interval and click **Start**. Click **Stop** to disable periodic requests. The timer is not started merely by selecting an interval unless `autoRefresh=true` is configured.

Выберите интервал таймера и нажмите **Старт**. Нажмите **Стоп**, чтобы прекратить периодические запросы. Один только выбор интервала не запускает таймер, если в настройках представления не задано `autoRefresh=true`.

`Auto` uses a short interval for current data and gradually increases it to at most five seconds while values remain unchanged. Historical archives use intervals appropriate to their resolution. Explicit `1`, `5`, `10`, `30` and `60` second modes use the selected cadence.

Режим `Auto` использует короткий интервал для текущих данных и постепенно увеличивает его максимум до пяти секунд, пока значения не меняются. Исторические архивы обновляются с интервалами, соответствующими их разрешению. Явные режимы `1`, `5`, `10`, `30` и `60` секунд используют выбранную периодичность.

A manually zoomed time window is preserved during refresh. A window attached to the newest edge follows incoming current data.

Выбранное пользователем масштабирование сохраняется при обновлении. Окно, закреплённое у новейшей границы, следует за поступающими текущими данными.

## Excel Export / Экспорт Excel

English:

1. Load the required trend first.
2. Open **Actions → Export Excel**.
3. Select **Wide** or **Long** layout.
4. Enable **Split by day** if separate daily worksheets are required.
5. Start the export and keep the page open until the download is ready.

Русский:

1. Сначала загрузите требуемый тренд.
2. Откройте **Действия → Экспорт Excel**.
3. Выберите **Широкий** или **Длинный** формат.
4. Включите **Разделять по суткам**, если нужны отдельные суточные листы.
5. Запустите экспорт и не закрывайте страницу до готовности файла.

| Option / Настройка | Result / Результат |
|---|---|
| **Wide / Широкий** | One shared time column plus value and quality columns for every channel. / Один общий столбец времени и столбцы значения и качества для каждого канала. |
| **Long / Длинный** | Consecutive date/time, tag, value and quality rows. / Последовательные строки даты и времени, тега, значения и качества. |
| **Split by day / Разделять по суткам** | One `yyyy-MM-dd` worksheet for every local calendar day. / Отдельный лист `yyyy-MM-dd` для каждых локальных суток. |
| **Summary / Сводка** | Archive, channel, tag, point count, minimum, maximum and average of good-quality values. / Архив, канал, тег, количество точек, минимум, максимум и среднее достоверных значений. |

The first row of every worksheet is frozen. Microsoft Excel is not required on the SCADA Web server. One export is limited to 10,000,000 points and to the licensed number of unique channels.

Первая строка каждого листа закрепляется. Microsoft Excel не требуется на сервере SCADA Web. Один экспорт ограничен 10 000 000 точек и лицензированным количеством уникальных каналов.

## TrendWindow on a Mimic Diagram / TrendWindow на мнемосхеме

`TrendWindow` is a compact interactive trend placed directly on a mimic diagram. The editor displays demonstration data so that appearance can be configured without archive access. At runtime it loads real data and supports tooltips, wheel zoom, drag panning, Reset Zoom and the interactive legend.

`TrendWindow` — компактный интерактивный тренд, размещаемый непосредственно на мнемосхеме. В редакторе показываются демонстрационные данные, поэтому внешний вид можно настроить без доступа к архиву. Во время выполнения компонент загружает реальные данные и поддерживает подсказки, масштабирование колесом, перемещение, сброс масштаба и интерактивную легенду.

The component intentionally has no title or permanent navigation button. If `openOnClick=true`, an ordinary click opens the full TrendJP page in a new tab. Zooming, dragging, Reset Zoom and legend clicks do not open the page. In edit mode a click only selects the component.

У компонента намеренно нет заголовка и постоянной кнопки перехода. Если `openOnClick=true`, обычный щелчок открывает полную страницу TrendJP в новой вкладке. Масштабирование, перемещение, сброс масштаба и щелчки по легенде страницу не открывают. В режиме редактирования щелчок только выделяет компонент.

### TrendWindow Properties / Свойства TrendWindow

| Property / Свойство | Default / По умолчанию | Purpose / Назначение |
|---|---|---|
| `channelNumbers` / **Channels / Каналы** | `1` | Channel list or ranges. / Список или диапазоны каналов. |
| `archiveCode` / **Archive / Архив** | `Min` | Archive used by the component. / Архив компонента. |
| `periodValue` / **Period / Период** | `1` | Positive rolling-period value. / Положительное значение скользящего периода. |
| `periodUnit` / **Period unit / Единица периода** | `h` | Seconds (`s`), minutes (`m`) or hours (`h`). / Секунды (`s`), минуты (`m`) или часы (`h`). |
| `preset` / **Preset / Шаблон** | `default` | Light `default` or `dark` theme. / Светлая `default` или тёмная тема. |
| `transparentBackground` / **Transparent background / Прозрачный фон** | `true` | Shows the mimic underlay through the plot background. / Показывает подложку мнемосхемы сквозь фон графика. |
| `trendType` / **Trend type / Тип тренда** | `line` | One of the supported trend types. / Один из поддерживаемых типов тренда. |
| `showLegend` / **Show legend / Показывать легенду** | `true` | Enables or disables the legend. / Включает или отключает легенду. |
| `legendPosition` / **Legend position / Положение легенды** | `top` | `none`, `top`, `right`, `bottom` or `left`. / `none`, `top`, `right`, `bottom` или `left`. |
| `markerShape` / **Point marker / Маркер точки** | `circle` | `circle`, `triangle` or `square`; also used by the tooltip. / `circle`, `triangle` или `square`; также используется в подсказке. |
| `lineWidth` / **Line width / Толщина линии** | `2` | Line width in CSS pixels. / Толщина линии в CSS-пикселях. |
| `markerSize` / **Point size / Размер точки** | `3` | Point size in CSS pixels. / Размер точки в CSS-пикселях. |
| `autoRefresh` / **Auto refresh / Автообновление** | `true` | Enables periodic requests at runtime. / Включает периодические запросы во время выполнения. |
| `refreshSeconds` / **Refresh, sec / Обновление, сек** | `30` | Refresh delay; `0` stops periodic refresh. / Задержка обновления; `0` останавливает периодическое обновление. |
| `openOnClick` / **Open trend page on click / Открывать страницу тренда по клику** | `true` | Opens the full page with matching settings. / Открывает полную страницу с соответствующими настройками. |

The legacy `periodHours` property is still accepted when an old mimic is opened. New components use `periodValue` and `periodUnit`.

Старое свойство `periodHours` по-прежнему принимается при открытии существующей мнемосхемы. Новые компоненты используют `periodValue` и `periodUnit`.

## Data Quality and Missing Points / Качество данных и пропуски

A point is drawn only when its SCADA status is positive and its value is a valid finite number. Bad-quality, missing or invalid points break line, stepped, smooth and area paths. TrendJP intentionally does not connect the good values on opposite sides of a bad interval.

Точка рисуется только при положительном статусе SCADA и корректном конечном числовом значении. Недостоверные, отсутствующие или ошибочные точки разрывают линии, ступеньки, сглаженные кривые и области. TrendJP намеренно не соединяет достоверные значения по разные стороны плохого интервала.

Tooltips, group ranges and Excel summary statistics use the same good-quality values. A gap in the chart therefore usually indicates missing or bad-quality archive data rather than a drawing error.

Подсказки, диапазоны групп и сводная статистика Excel используют те же достоверные значения. Поэтому разрыв графика обычно означает отсутствие архивных данных или плохое качество, а не ошибку рисования.

Large periods and many channels require more time and data. Choose an archive resolution appropriate to the operator task: use detailed archives for short diagnostics and coarser archives for long-term analysis.

Большие периоды и большое количество каналов требуют больше времени и данных. Выбирайте разрешение архива по задаче оператора: подробные архивы — для короткой диагностики, более редкие — для длительного анализа.

## Configuring a Standalone View / Настройка отдельного представления

The plugin registers the fileless view type `TrendJP`. A configured view opens `/TrendJP?viewID=<ID>`. The `Args` field contains ordinary query-string parameters without the leading `?`.

Плагин регистрирует тип представления без отдельного файла — `TrendJP`. Настроенное представление открывает `/TrendJP?viewID=<ID>`. Поле `Args` содержит обычные параметры строки запроса без начального `?`.

### Data and Time Arguments / Аргументы данных и времени

| Parameter | Values; default / Значения; по умолчанию | Purpose / Назначение |
|---|---|---|
| `cnlNums` | Channel expression; empty / Выражение каналов; пусто | Input channels. / Входные каналы. |
| `archiveCode` | Archive code; `Min` | Archive in single-source mode. Alias: `archive`. / Архив одиночного режима. Алиас: `archive`. |
| `startTime` | Absolute or relative local time / Абсолютное или относительное локальное время | Start of range. Alias: `from`. / Начало диапазона. Алиас: `from`. |
| `endTime` | Absolute or relative local time / Абсолютное или относительное локальное время | End of range. Alias: `to`. / Конец диапазона. Алиас: `to`. |
| `period` | Positive number plus `s`, `m` or `h` / Положительное число и `s`, `m` или `h` | Creates a range when an endpoint is omitted. / Формирует диапазон при отсутствии границы. |
| `hours` | Positive hours / Положительное число часов | Legacy compatibility alias for an hourly period. / Устаревший совместимый период в часах. |

Without `startTime`, `endTime`, `period` and `hours`, TrendJP opens the complete current local day from `00:00` to the next `00:00`.

Без `startTime`, `endTime`, `period` и `hours` TrendJP открывает полные текущие локальные сутки от `00:00` до следующих `00:00`.

### Display and Behavior Arguments / Аргументы отображения и поведения

| Parameter | Values; default / Значения; по умолчанию | Purpose / Назначение |
|---|---|---|
| `theme` | `default`, `dark`; `default` | Page theme. / Тема страницы. |
| `trendType` | See trend types; `line` / См. типы тренда; `line` | Chart presentation. / Представление графика. |
| `showLegend` | Boolean; `true` | Legacy legend switch; `false` overrides the position. / Совместимый флаг легенды; `false` имеет приоритет. |
| `legendPosition` | `none`, `top`, `right`, `bottom`, `left`; `top` | Legend position. / Положение легенды. |
| `markerShape` | `circle`, `triangle`, `square`; `circle` | Plot and tooltip marker. / Маркер графика и подсказки. |
| `lineWidth` | `1`, `1.5`, `2`, `3`, `4`; `2` | Line width. / Толщина линии. |
| `markerSize` | `2`, `3`, `4`, `5`, `6`, `8`; `3` | Point size. / Размер точки. |
| `tooltip` | `nearest`, `all`; `nearest` | Tooltip mode. / Режим подсказки. |
| `refreshInterval` | `auto`, `1`, `5`, `10`, `30`, `60`; `30` | Timer cadence; does not start it alone. / Период таймера; сам его не запускает. |
| `autoRefresh` | Boolean; `false` | Starts periodic refresh after opening. / Запускает периодическое обновление после открытия. |
| `showToolbar` | Boolean; `true` | `false` hides the complete toolbar including **Actions**. / `false` скрывает всю панель, включая **Действия**. |
| `showControlPanel` | Boolean; `true` | Hides filters but keeps **Actions**. / Скрывает фильтры, сохраняя **Действия**. |
| `showTimeline` | Boolean; `true` | Shows the timeline and Previous/Next buttons. / Показывает временную шкалу и кнопки Назад/Вперёд. |
| `navigationStep` | Positive integer; `1` | Previous/Next step. / Шаг Назад/Вперёд. |
| `navigationUnit` | `s`, `m`, `h`, `d`; `d` | Seconds, minutes, hours or days. / Секунды, минуты, часы или дни. |
| `navigationMode` | `shift`, `expand`; `shift` | Moves both boundaries or expands one boundary. / Сдвигает обе границы или расширяет одну границу. |
| `axisGroup1` ... `axisGroup4` | Channel expressions; empty / Выражения каналов; пусто | Manual Multiple axes assignment. Ignored by other trend types. / Ручное назначение Multiple axes. Другие типы не используют. |
| `exportLayout` | `wide`, `long`; `wide` | Default Excel layout. / Формат Excel по умолчанию. |
| `splitByDay` | Boolean; `false` | Creates separate daily worksheets. / Создаёт отдельные суточные листы. |

Use `true` and `false` for Boolean arguments. The general parser also accepts `1` as true and `0`, `no` or `off` as false.

Для логических аргументов используйте `true` и `false`. Общий обработчик также принимает `1` как истину, а `0`, `no` и `off` — как ложь.

### Multiple Archive Arguments / Аргументы нескольких архивов

| Parameter | Values / Значения | Purpose / Назначение |
|---|---|---|
| `multiArchive` | Boolean; `false` | Enables multiple sources. Alias: `multi`. / Включает несколько источников. Алиас: `multi`. |
| `sourceNArchive` | Archive code, `N=1..4` | Archive of source `N`. Alias: `archiveN`. / Архив источника `N`. Алиас: `archiveN`. |
| `sourceNCnlNums` | Channel expression, `N=1..4` | Channels of source `N`. Alias: `cnlNumsN`. / Каналы источника `N`. Алиас: `cnlNumsN`. |
| `sourceNEnabled` | Boolean, `N=1..4`; enabled | Enables or disables source `N`. Alias: `enabledN`. / Включает или отключает источник `N`. Алиас: `enabledN`. |

Source arguments automatically enable multiple-archive mode. One embedded `TrendWindow` uses only one archive and one channel list.

Аргументы источников автоматически включают режим нескольких архивов. Один встроенный `TrendWindow` использует только один архив и один список каналов.

### Channel Expression Syntax / Синтаксис каналов

Channel expressions accept commas, semicolons and spaces as separators. Ascending and descending ranges are expanded, invalid and non-positive numbers are ignored, and duplicates are removed while preserving the first occurrence.

Выражения каналов принимают запятые, точки с запятой и пробелы как разделители. Возрастающие и убывающие диапазоны раскрываются, ошибочные и неположительные номера игнорируются, а дубликаты удаляются с сохранением первого вхождения.

| Expression / Выражение | Result / Результат |
|---|---|
| `100, 200-203, 310` | `100,200,201,202,203,310` |
| `10; 12 14-16` | `10,12,14,15,16` |
| `5-2, 3, 5` | `5,4,3,2` |

The actual maximum is the positive `CountTags` value in the license. The same channel number repeated in several archive sources counts once.

Фактический максимум задаётся положительным значением `CountTags` в лицензии. Один номер канала, повторённый в нескольких архивных источниках, учитывается один раз.

### Relative Time Expressions / Относительное время

`startTime` and `endTime` accept an absolute local value such as `2026-07-23T00:00:00` or a case-insensitive relative expression.

`startTime` и `endTime` принимают абсолютное локальное значение, например `2026-07-23T00:00:00`, либо регистронезависимое относительное выражение.

| Base / Основа | Meaning / Значение |
|---|---|
| `NOW` | Current local time / Текущее локальное время |
| `SECOND`, `MINUTE`, `HOUR` | Beginning of the current second, minute or hour / Начало текущей секунды, минуты или часа |
| `DAY` | Beginning of the current day / Начало текущих суток |
| `WEEK` | Monday `00:00` of the current week / Понедельник `00:00` текущей недели |
| `MONTH` | First day of the current month / Первое число текущего месяца |
| `YEAR` | January 1 of the current year / 1 января текущего года |

Offsets use `S`, `M`, `H`, `D`, `W`, `MO` and `Y`. Multiple offsets are applied from left to right. In a URL, encode a plus sign as `%2B` because an unescaped `+` is decoded as a space.

Смещения используют `S`, `M`, `H`, `D`, `W`, `MO` и `Y`. Несколько смещений применяются слева направо. В URL кодируйте знак плюса как `%2B`, потому что неэкранированный `+` преобразуется в пробел.

| Expression / Выражение | Meaning / Значение |
|---|---|
| `DAY` ... `DAY%2B1D` | Complete current day / Полные текущие сутки |
| `DAY-1D` ... `DAY` | Complete previous day / Полные предыдущие сутки |
| `NOW-8H` ... `NOW` | Last eight hours / Последние восемь часов |
| `MONTH-1MO` ... `MONTH` | Previous calendar month / Предыдущий календарный месяц |

### Configuration Priority and XML / Приоритет и XML

Configuration priority when a view opens:

1. Direct URL parameters.
2. Corresponding `View.Args` parameters.
3. Last configuration saved in this browser for the current `viewID`.
4. Plugin defaults.

Приоритет конфигурации при открытии представления:

1. Прямые параметры URL.
2. Соответствующие параметры `View.Args`.
3. Последняя конфигурация, сохранённая в этом браузере для текущего `viewID`.
4. Значения по умолчанию.

Relative expressions are recalculated whenever the configured view is opened. In XML, replace `&` between parameters with `&amp;`.

Относительные выражения пересчитываются при каждом открытии настроенного представления. В XML заменяйте разделитель параметров `&` на `&amp;`.

Example:

```xml
<Args>cnlNums=101,103-108&amp;archiveCode=Min&amp;startTime=DAY&amp;endTime=DAY%2B1D&amp;trendType=line&amp;legendPosition=right&amp;navigationStep=1&amp;navigationUnit=d&amp;navigationMode=shift</Args>
```

Do not copy a numeric `ViewTypeID` from another project. Select the registered `TrendJP` view type in the Administrator because every configuration database assigns its own numeric identifier.

Не копируйте числовой `ViewTypeID` из другого проекта. Выберите зарегистрированный тип `TrendJP` в Администраторе, потому что каждая база конфигурации назначает собственный числовой идентификатор.

### Ready-to-Use Examples / Готовые примеры

Replace the example channel numbers with channels available to the current user.

Замените номера каналов в примерах на каналы, доступные текущему пользователю.

Last eight hours with line markers / Последние восемь часов с маркерами:

```xml
<Args>cnlNums=101,103-108&amp;archiveCode=Min&amp;period=8h&amp;trendType=line-markers&amp;legendPosition=top</Args>
```

Two archive sources / Два архивных источника:

```xml
<Args>multiArchive=true&amp;source1Archive=Cur&amp;source1CnlNums=101,103-105&amp;source2Archive=Min&amp;source2CnlNums=101,103-108&amp;period=8h&amp;legendPosition=right</Args>
```

Live 30-second trend without controls / Оперативный тренд за 30 секунд без элементов управления:

```xml
<Args>cnlNums=101,103&amp;archiveCode=Cur&amp;period=30s&amp;trendType=line-markers&amp;showToolbar=false&amp;showTimeline=false&amp;autoRefresh=true&amp;refreshInterval=1</Args>
```

## Installation and Registration / Установка и регистрация

Requirements:

- Rapid SCADA 6.x on Windows or Linux;
- the .NET 8 runtime used by SCADA Web;
- a valid `PlgTrendJP.bin` license with a positive `CountTags` value;
- access rights to the requested objects and active input channels;
- configured SCADA archives.

Требования:

- Rapid SCADA 6.x под Windows или Linux;
- среда .NET 8, используемая SCADA Web;
- действующая лицензия `PlgTrendJP.bin` с положительным значением `CountTags`;
- права на запрашиваемые объекты и активные входные каналы;
- настроенные архивы SCADA.

Installation:

1. Select the package for the target operating system and copy its `SCADA` folder over the Rapid SCADA installation directory while preserving the folder structure.
2. Enable `PlgTrendJP` in the project `ScadaWebConfig.xml`.
3. Assign `PlgTrendJP` as `ChartFeature` if standard Rapid SCADA chart actions must open TrendJP.
4. Install the license and restart SCADA Web or its IIS site. Refreshing the browser alone does not reload assemblies or the license.

Установка:

1. Выберите пакет для целевой операционной системы и скопируйте его папку `SCADA` поверх каталога установки Rapid SCADA с сохранением структуры.
2. Включите `PlgTrendJP` в проектном `ScadaWebConfig.xml`.
3. Назначьте `PlgTrendJP` как `ChartFeature`, если стандартные команды графиков Rapid SCADA должны открывать TrendJP.
4. Установите лицензию и перезапустите SCADA Web или сайт IIS. Обновление браузера не перезагружает сборки и лицензию.

Required configuration:

```xml
<Plugins>
  <Plugin code="PlgTrendJP" />
</Plugins>

<PluginAssignment>
  <ChartFeature>PlgTrendJP</ChartFeature>
</PluginAssignment>
```

The supplied project helper can add this configuration and create a timestamped backup:

```bat
RegisterTrendPluginInProject.bat -ProjectDir "C:\Program Files\SCADA\ProjectSamples\HelloWorld"
```

Use `-KeepChartFeature` to enable the plugin without replacing another chart feature. The helper also accepts `-InstanceName`, `-ConfigFileName` and `-NoBackup`.

Поставляемый помощник может добавить эту конфигурацию и создать резервную копию с отметкой времени. Параметр `-KeepChartFeature` включает плагин без замены другого обработчика графиков. Также поддерживаются `-InstanceName`, `-ConfigFileName` и `-NoBackup`.

On Windows, copy `PlgTrendJP.View.dll` to `ScadaAdmin\Lib` when the classic Administrator must recognize the `TrendJP` view type.

Под Windows скопируйте `PlgTrendJP.View.dll` в `ScadaAdmin\Lib`, если классический Администратор должен распознавать тип представления `TrendJP`.

## Activation and Tag Limit / Активация и лимит каналов

`PlgTrendJP` uses a separate installation-specific license. If no valid license is found, the plugin creates `PlgTrendJP_Activation.bin` without overwriting an existing request.

`PlgTrendJP` использует отдельную лицензию, привязанную к установке. Если действующая лицензия не найдена, плагин создаёт `PlgTrendJP_Activation.bin`, не перезаписывая существующий запрос.

English:

1. Start SCADA Web without the TrendJP license.
2. Find `PlgTrendJP_Activation.bin` in the host license directory.
3. Send the activation request to the license provider.
4. Save the received file as `PlgTrendJP.bin` in the same directory.
5. Restart the host.

Русский:

1. Запустите SCADA Web без лицензии TrendJP.
2. Найдите `PlgTrendJP_Activation.bin` в папке лицензий приложения.
3. Передайте запрос поставщику лицензии.
4. Сохраните полученный файл под именем `PlgTrendJP.bin` в той же папке.
5. Перезапустите приложение.

| Host / Приложение | Activation request / Запрос | License / Лицензия |
|---|---|---|
| SCADA Web | `ScadaWeb/config/PlgTrendJP_Activation.bin` | `ScadaWeb/config/PlgTrendJP.bin` |
| ScadaAdminWebJP, when used / при использовании | `ScadaAdminWebJP/License/PlgTrendJP_Activation.bin` | `ScadaAdminWebJP/License/PlgTrendJP.bin` |

The signed license must contain `AppName=PlgTrendJP` and a positive `CountTags`. `CountTags` is the maximum number of unique channel numbers in one trend. Ranges are expanded before counting, duplicates count once, and one channel used in several archive sources remains one licensed channel.

Подписанная лицензия должна содержать `AppName=PlgTrendJP` и положительный `CountTags`. `CountTags` задаёт максимальное количество уникальных номеров каналов в одном тренде. Диапазоны раскрываются перед подсчётом, дубликаты учитываются один раз, а один канал в нескольких архивных источниках остаётся одним лицензируемым каналом.

## Troubleshooting / Устранение неполадок

| Symptom / Признак | Cause and action / Причина и действие |
|---|---|
| TrendJP is absent from Webstation / TrendJP отсутствует в Вебстанции | Check plugin registration, deployed files and restart SCADA Web. / Проверьте регистрацию, установленные файлы и перезапустите SCADA Web. |
| Standard chart action opens another plugin / Стандартная команда открывает другой плагин | Set `<ChartFeature>PlgTrendJP</ChartFeature>`. / Назначьте `<ChartFeature>PlgTrendJP</ChartFeature>`. |
| License error / Ошибка лицензии | Check the file name, license directory, `AppName=PlgTrendJP`, positive `CountTags` and restart the host. / Проверьте имя, папку лицензии, `AppName=PlgTrendJP`, положительный `CountTags` и перезапустите приложение. |
| Channel limit exceeded / Превышен лимит каналов | Reduce the number of unique channel numbers or use a suitable license. / Сократите количество уникальных номеров или используйте подходящую лицензию. |
| Archive list is empty / Список архивов пуст | Check the project archive configuration. / Проверьте настройку архивов проекта. |
| Selected channel has no data / Выбранный канал не содержит данных | Check that the channel belongs to the selected archive and that its quality is good. / Проверьте принадлежность канала архиву и качество данных. |
| Gaps appear in a line / На линии есть разрывы | Missing, invalid and bad-quality points are intentionally not connected. / Отсутствующие, ошибочные и недостоверные точки намеренно не соединяются. |
| One Multiple axes line looks flat / Одна линия Multiple axes выглядит прямой | Check manual axis groups. Strongly different ranges assigned to one group share one scale. / Проверьте ручные группы осей. Сильно различающиеся диапазоны в одной группе используют общую шкалу. |
| Previous/Next uses an unexpected interval / Назад/Вперёд использует неожиданный интервал | Open **Display settings** and check step, unit and shift/expand mode. / Откройте **Настройки отображения** и проверьте шаг, единицу и режим сдвига/расширения. |
| A hidden series does not return / Скрытый ряд не возвращается | Click its dimmed legend item or isolate the only visible series again. / Щёлкните приглушённый элемент легенды или повторно изолируйте единственный видимый ряд. |
| A profile is missing on another workstation / Профиль отсутствует на другом рабочем месте | Profiles are browser-local. Configure shared defaults in `View.Args`. / Профили локальны для браузера. Общие значения задавайте в `View.Args`. |
| Relative expression containing `+` fails / Не работает выражение с `+` | Encode plus as `%2B`, for example `DAY%2B1D`. / Кодируйте плюс как `%2B`, например `DAY%2B1D`. |
| Automatic refresh does not start / Автообновление не запускается | Click **Start** or configure `autoRefresh=true`; `refreshInterval` only selects the cadence. / Нажмите **Старт** или задайте `autoRefresh=true`; `refreshInterval` только выбирает период. |
| No controls are visible / Элементы управления не видны | `showToolbar=false` hides **Actions** too. Use `showControlPanel=false` to keep **Actions**. / `showToolbar=false` скрывает и **Действия**. Используйте `showControlPanel=false`, чтобы сохранить меню. |
| Excel export is rejected / Экспорт Excel отклонён | Load the trend first, keep the page open and check the point and licensed channel limits. / Сначала загрузите тренд, не закрывайте страницу и проверьте лимиты точек и каналов. |

## Video / Видео

The demonstration shows the capacitance components on the Rapid SCADA working diagram and their configuration in the editor.

В демонстрации показаны компоненты емкостей на работающей мнемосхеме Rapid SCADA и их настройка в редакторе.

[Watch the PlgTrendJP demonstration / Посмотреть демонстрацию PlgTrendJP](https://jurasskpark.ru/files/github/PlgTrendJP.mp4)

## Screenshots / Скриншоты

### Runtime mimic / Рабочая мнемосхема

![PlgTrendJP components in a running Rapid SCADA mimic](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/ScadaWeb/PlgTrendJP/Source/PlgTrendJP_001.png)
![PlgTrendJP components in a running Rapid SCADA mimic](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/ScadaWeb/PlgTrendJP/Source/PlgTrendJP_002.png)
![PlgTrendJP components in a running Rapid SCADA mimic](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/ScadaWeb/PlgTrendJP/Source/PlgTrendJP_003.png)
![PlgTrendJP components in a running Rapid SCADA mimic](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/ScadaWeb/PlgTrendJP/Source/PlgTrendJP_004.png)

## License / Лицензия

`PlgTrendJP` is distributed as shareware/commercial software. A valid product license is required to place new tank components. Existing licensed mimic diagrams remain readable when a license is temporarily unavailable, but editing and new placement are restricted as described above. Do not rename the plugin DLL, activation request or license file.

`PlgTrendJP` распространяется как условно-бесплатное/коммерческое программное обеспечение. Для добавления новых компонентов резервуаров требуется действующая лицензия продукта. Существующие мнемосхемы продолжают открываться при временном отсутствии лицензии, но редактирование и добавление новых компонентов ограничиваются, как описано выше. Не переименовывайте DLL плагина, запрос активации и файл лицензии.

