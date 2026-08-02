# PlgMimTankJP — Tank and Vessel Components for Rapid SCADA

![Rapid SCADA](https://img.shields.io/badge/Rapid%20SCADA-6.x-blue.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![Version](https://img.shields.io/badge/version-6.1.0-green.svg)
![Platform](https://img.shields.io/badge/platform-Windows%20%7C%20Linux-lightgrey.svg)

## About This Guide / О руководстве

This guide explains how operators, engineers and administrators use `PlgMimTankJP` in Rapid SCADA mimic diagrams. It covers level indicators, industrial SVG vessels, channel bindings, alarms, data quality, installation and troubleshooting.

Это руководство предназначено для операторов, инженеров и администраторов, которые используют `PlgMimTankJP` в мнемосхемах Rapid SCADA. В нём описаны уровнемеры, промышленные SVG-ёмкости, привязка каналов, аварии, качество данных, установка и устранение неполадок.

`PlgMimTankJP` version `6.1.0` adds thirteen localized components to the **TANKS / РЕЗЕРВУАРЫ** toolbox group. The plugin works with the standard Mimic data contract and does not require a backend API or a dependency on the pipe plugin.

`PlgMimTankJP` версии `6.1.0` добавляет тринадцать локализованных компонентов в группу **TANKS / РЕЗЕРВУАРЫ**. Плагин использует стандартный контракт данных мнемосхемы, не требует отдельного серверного API и не зависит от плагина трубопроводов.

## Features / Возможности

English:

- vertical and horizontal three-layer level indicators;
- compact channel-driven Lite indicators;
- a separate five-zone linear gauge;
- eight industrial SVG vessel types;
- one to three independently configured liquid or product layers;
- channel, static and disabled layer source modes;
- dead-zone compensation and overflow indication;
- calculated or externally controlled `LL`, `L`, `H` and `HH` alarms;
- optional pressure, temperature, mass, general alarm and reactor drive instruments;
- single-point and multipoint temperature measurement with up to 24 points;
- localized Russian and English property names and runtime captions;
- editor previews that do not require live channels.

Русский:

- вертикальный и горизонтальный трёхслойные уровнемеры;
- компактные канальные уровнемеры Lite;
- отдельная пятизонная линейная шкала;
- восемь промышленных SVG-ёмкостей;
- от одного до трёх независимо настраиваемых слоёв жидкости или продукта;
- канальный, статический и отключённый режимы источника слоя;
- учёт мёртвой зоны и индикация переполнения;
- вычисляемые или управляемые внешними каналами аварии `LL`, `L`, `H` и `HH`;
- дополнительные показания давления, температуры, массы, общей аварии и привода реактора;
- одиночное и многоточечное измерение температуры максимум по 24 точкам;
- русские и английские названия свойств и надписи времени выполнения;
- редакторское превью, для которого не требуются работающие каналы.

## Quick Start / Быстрый старт

English:

1. Install and enable `PlgMimTankJP`, then restart SCADA Web and activate the plugin.
2. Open a mimic in Mimic Editor or Mimic Editor JP.
3. Select a component from the **TANKS** group and place it on the canvas.
4. Set the vessel or indicator height in metres.
5. For each required layer, select **Channel** and enter its input channel number.
6. Set layer names and colors.
7. Configure the dead zone and alarm thresholds if required.
8. For an SVG vessel, separately enable the level indicator and every required instrument.
9. Save the mimic and open it in Webstation to check live values and quality.

Русский:

1. Установите и включите `PlgMimTankJP`, затем перезапустите SCADA Web и активируйте плагин.
2. Откройте мнемосхему в Mimic Editor или Mimic Editor JP.
3. Выберите элемент в группе **РЕЗЕРВУАРЫ** и поместите его на полотно.
4. Укажите высоту ёмкости или шкалы в метрах.
5. Для каждого используемого слоя выберите **Канал** и задайте номер входного канала.
6. Настройте названия и цвета слоёв.
7. При необходимости задайте мёртвую зону и аварийные пороги.
8. У SVG-ёмкости отдельно включите уровнемер и каждый требуемый прибор.
9. Сохраните мнемосхему и откройте её в Вебстанции, чтобы проверить текущие значения и качество.

New full and vessel components use zero channel numbers by default. The editor still shows preview values, while runtime correctly treats unconfigured channel data as unknown.

У новых полных уровнемеров и ёмкостей номера каналов по умолчанию равны нулю. В редакторе при этом видно превью, а во время выполнения ненастроенные канальные данные считаются неизвестными.

## Component Catalog / Каталог компонентов

| Type name | English toolbox name | Русское название | Default size / Размер |
|---|---|---|---|
| `TankV2` | Vertical level | Уровень вертикальный | `260 × 420` |
| `LayerProgress` | Horizontal level | Уровень горизонтальный | `500 × 150` |
| `LinearGauge` | Linear gauge | Линейная шкала | `280 × 86` |
| `VerticalLevelLite` | Vertical level Lite | Уровень вертикальный Lite | `100 × 300` |
| `HorizontalLevelLite` | Horizontal level Lite | Уровень горизонтальный Lite | `300 × 100` |
| `RvsVessel` | Vertical storage tank | РВС | `400 × 300` |
| `VerticalProcessVessel` | Vertical process vessel | Вертикальный аппарат | `400 × 300` |
| `HorizontalProcessVessel` | Horizontal process vessel | Горизонтальный аппарат | `400 × 200` |
| `SiloHopper` | Silo / hopper | Силос / бункер | `400 × 300` |
| `RectangularClosedTank` | Closed rectangular tank | Закрытая прямоугольная ёмкость | `400 × 250` |
| `OpenBath` | Open bath | Открытая ванна | `400 × 200` |
| `SphericalTank` | Spherical tank | Сферический резервуар | `400 × 300` |
| `ReactorMixer` | Reactor mixer | Реактор-смеситель | `400 × 350` |

The obsolete legacy type `Tank` is not registered or distributed. Existing `.mim` files that contain `typeName: "Tank"` must replace that component with `TankV2` before deployment.

Устаревший тип `Tank` не регистрируется и не поставляется. В существующих файлах `.mim`, содержащих `typeName: "Tank"`, необходимо заменить этот компонент на `TankV2` до развёртывания.

## Full Layered Level Indicators / Полные многослойные уровнемеры

`TankV2` draws layers vertically from bottom to top. `LayerProgress` draws them horizontally from left to right. Both components use the same physical-level, quality, dead-zone, overflow and alarm calculations.

`TankV2` рисует слои вертикально снизу вверх. `LayerProgress` рисует их горизонтально слева направо. Оба компонента используют одинаковые расчёты физического уровня, качества, мёртвой зоны, переполнения и аварий.

### Layer Source Modes / Режимы источника слоя

| Mode / Режим | Runtime behavior / Поведение во время выполнения |
|---|---|
| `Disabled` / **Отключён** | The layer is completely excluded from totals, alarms and rendering. / Слой полностью исключается из суммы, аварий и отрисовки. |
| `Static` / **Статический** | `liquidNLevelMeters` is used as the real value. No channel is required. / `liquidNLevelMeters` используется как реальное значение. Канал не требуется. |
| `Channel` / **Канал** | Runtime reads `liquidNInCnlNum`; `liquidNLevelMeters` is only the editor preview. / Во время выполнения читается `liquidNInCnlNum`, а `liquidNLevelMeters` используется только для превью в редакторе. |

The fixed layer order is `Liquid 1`, `Liquid 2`, `Liquid 3`. Layer 1 is the lowest or leftmost layer. A disabled layer is omitted without changing the order of the remaining layers.

Фиксированный порядок слоёв: `Слой 1`, `Слой 2`, `Слой 3`. Слой 1 находится снизу или слева. Отключённый слой исключается без изменения порядка остальных слоёв.

### Shared Properties / Общие свойства

| Property / Свойство | Default / По умолчанию | Purpose / Назначение |
|---|---:|---|
| `tankHeightMeters` / **Высота резервуара, м** | `8` | Physical capacity in metres. Must be greater than zero. / Физическая высота в метрах. Должна быть больше нуля. |
| `deadZoneMeters` / **Мёртвая зона, м** | `0` | Sediment or unusable bottom zone. / Осадок или неиспользуемая нижняя зона. |
| `decimalPlaces` / **Знаков после запятой** | `1` | Value precision from `0` to `6`. / Точность значений от `0` до `6`. |
| `showScale` / **Показать шкалу** | `true` | Displays scale ticks and labels. / Показывает деления и подписи шкалы. |
| `showTotalLevel` / **Показать общий уровень** | `true` | Displays the corrected total level. / Показывает скорректированный общий уровень. |
| `showTotalPercent` / **Показать общий процент** | `true` | Displays percentage of useful height. / Показывает процент полезной высоты. |
| `showAlarms` / **Показать аварии** | `true` | Enables alarm markers and external alarm bindings. / Включает аварийные метки и привязки внешних аварий. |
| `showLegend` / **Показать легенду** | `true` for `TankV2`, `false` for `LayerProgress` | Displays layer names, colors and values. / Показывает названия, цвета и значения слоёв. |
| `alarmScale` / **Единицы порогов** | `Percent` | Selects percent or metre thresholds. / Выбирает пороги в процентах или метрах. |
| `emptyColor` / **Цвет пустого объёма** | `#e2e8f0` | Empty scale color. / Цвет незаполненной части. |
| `warningColor` / **Цвет предупреждения** | `#f59e0b` | `L` and `H` warning color. / Цвет предупреждений `L` и `H`. |
| `alarmColor` / **Цвет аварии** | `#dc2626` | `LL`, `HH` and overflow alarm color. / Цвет аварий `LL`, `HH` и переполнения. |
| `clickAction` / **Действие по щелчку** | Empty / Пусто | Standard Mimic action. The plugin does not send its own commands. / Стандартное действие мнемосхемы. Плагин самостоятельно команды не отправляет. |

Each of the three layers has these properties:

Для каждого из трёх слоёв доступны свойства:

| Property pattern / Шаблон свойства | Purpose / Назначение |
|---|---|
| `liquidNSourceMode` | Selects `Disabled`, `Static` or `Channel`. / Выбирает `Отключён`, `Статический` или `Канал`. |
| `liquidNInCnlNum` | Input channel used in `Channel` mode. / Входной канал режима `Канал`. |
| `liquidNName` | Caption shown in the legend or vessel value card. / Название в легенде или карточке значения ёмкости. |
| `liquidNColor` | HTML layer color. / HTML-цвет слоя. |
| `liquidNLevelMeters` | Editor preview and the runtime value in `Static` mode. / Превью редактора и рабочее значение в режиме `Статический`. |

Default preview levels are `2 / 3 / 2 m`. Default colors are blue `#0066cc`, brown `#5c3a21` and dark `#1a1a2e`.

Уровни превью по умолчанию равны `2 / 3 / 2 м`. Исходные цвета: синий `#0066cc`, коричневый `#5c3a21` и тёмный `#1a1a2e`.

### TankV2 Appearance / Оформление TankV2

`TankV2` additionally provides:

- `legendPosition`: `Top`, `Right`, `Bottom` or `Left`; the default is `Right`;
- `showBubbles`: independently enables rising bubbles;
- `animateWaves`: independently enables liquid-wave movement.

`TankV2` дополнительно предоставляет:

- `legendPosition`: `Сверху`, `Справа`, `Снизу` или `Слева`; по умолчанию — `Справа`;
- `showBubbles`: независимо включает поднимающиеся пузырьки;
- `animateWaves`: независимо включает движение волн жидкости.

Animations are disabled in edit mode and when the operating system requests reduced motion.

Анимации отключаются в режиме редактирования и при системной настройке уменьшения движения.

## Dead Zone and Overflow / Мёртвая зона и переполнение

The dead zone represents sediment, sludge or another unusable bottom layer. It is normalized to `0 ≤ deadZoneMeters < tankHeightMeters` and subtracted once from known liquid layers from bottom to top.

Мёртвая зона обозначает осадок, ил или другой неиспользуемый нижний слой. Она нормализуется по условию `0 ≤ deadZoneMeters < tankHeightMeters` и один раз вычитается из достоверных слоёв снизу вверх.

Example: physical level `1.50 m` and dead zone `0.30 m` produce a useful level of `1.20 m`.

Пример: физический уровень `1,50 м` и мёртвая зона `0,30 м` дают полезный уровень `1,20 м`.

The useful height is `tankHeightMeters - deadZoneMeters`. Corrected layer values, total percentage and calculated alarm thresholds use this useful height. The sediment is shown as a neutral hatched strip.

Полезная высота равна `tankHeightMeters - deadZoneMeters`. Скорректированные значения слоёв, общий процент и вычисляемые аварии используют полезную высоту. Осадок показывается нейтральной штрихованной полосой.

Overflow is detected from the physical level before dead-zone subtraction. The renderer preserves actual values, clips only the upper visible part and shows a localized overflow indicator. Therefore, the displayed total percentage may exceed `100%`.

Переполнение определяется по физическому уровню до вычитания мёртвой зоны. Отрисовка сохраняет реальные значения, обрезает только верхнюю видимую часть и показывает локализованный индикатор переполнения. Поэтому итоговый процент может превышать `100%`.

## Level Alarms / Аварийные уровни

Default thresholds are `LL = 5%`, `L = 15%`, `H = 85%` and `HH = 95%`. Threshold order is always normalized:

Пороги по умолчанию: `LL = 5%`, `L = 15%`, `H = 85%`, `HH = 95%`. Порядок всегда нормализуется:

```text
0 ≤ LL ≤ L ≤ H ≤ HH ≤ useful height
```

When `alarmScale` changes between `Percent` and `Meters`, the alternate set is recalculated automatically. The property editor shows the selected unit set.

При переключении `alarmScale` между `Percent` и `Meters` второй набор пересчитывается автоматически. Редактор свойств показывает выбранный набор единиц.

Without an external channel, `LL` and `L` are active when the useful total is at or below the threshold; `H` and `HH` are active when it is at or above the threshold.

Без внешнего канала `LL` и `L` активны, когда полезный общий уровень не выше порога; `H` и `HH` активны, когда уровень не ниже порога.

Each alarm can use an optional external discrete channel:

Каждая авария может использовать отдельный внешний дискретный канал:

| External channel data / Данные внешнего канала | Alarm state / Состояние аварии |
|---|---|
| Good quality, value `0` / Хорошее качество, значение `0` | Inactive / Неактивна |
| Good quality, nonzero value / Хорошее качество, ненулевое значение | Active / Активна |
| Missing or bad quality / Нет данных или плохое качество | Unknown, with no calculated fallback / Неизвестна, без перехода к вычисляемому порогу |

An external channel is authoritative whenever its number is positive. In runtime only active alarm markers are visible; inactive and unknown alarms do not leave gray placeholders. Edit mode displays alarm markers as a layout preview.

Внешний канал является авторитетным, если указан положительный номер. Во время выполнения видны только активные аварийные метки; неактивные и неизвестные аварии не оставляют серых обозначений. В редакторе метки отображаются как превью компоновки.

## Data Quality / Качество данных

A channel value is considered good only when data exists, its SCADA status is positive and the value is a finite number.

Канальное значение считается достоверным, только если данные получены, статус SCADA положительный, а значение является конечным числом.

If one full-indicator layer has bad or missing data:

- good and static layers remain visible;
- the affected layer contributes zero and is shown as `#.#` where a value caption exists;
- the total is marked as partial;
- calculated alarms become unknown;
- good external alarm channels remain authoritative and continue to work.

Если данные одного слоя полного уровнемера отсутствуют или имеют плохое качество:

- достоверные и статические слои продолжают отображаться;
- проблемный слой считается нулевым и обозначается `#.#` там, где выводится значение;
- общий уровень помечается как частичный;
- вычисляемые аварии переходят в неизвестное состояние;
- достоверные внешние каналы аварий продолжают работать.

Runtime values and quality fields are transient and are not saved to the `.mim` file.

Текущие значения и поля качества являются временными и не сохраняются в файле `.mim`.

## Lite Level Indicators / Уровнемеры Lite

`VerticalLevelLite` and `HorizontalLevelLite` are compact indicators for layouts where only the colored fill is required. They intentionally omit values, captions, percentages, quality text, alarms, legend and dead-zone compensation.

`VerticalLevelLite` и `HorizontalLevelLite` — компактные индикаторы для мнемосхем, где требуется только цветное заполнение. В них намеренно отсутствуют цифры, подписи, проценты, текст качества, аварии, легенда и учёт мёртвой зоны.

| Property / Свойство | Default / По умолчанию | Purpose / Назначение |
|---|---:|---|
| `tankHeightMeters` / **Высота, м** | `8` | Total indicator capacity. / Полная высота индикатора. |
| `activeLayerCount` / **Количество активных слоёв** | `3` | Uses the first `1`, `2` or `3` layers. / Использует первые `1`, `2` или `3` слоя. |
| `liquidNInCnlNum` / **Входной канал слоя N** | `0` | Runtime input channel for the layer. / Рабочий входной канал слоя. |
| `liquidNColor` / **Цвет слоя N** | Blue, brown, dark / Синий, коричневый, тёмный | Layer fill color. / Цвет заполнения слоя. |
| `clickAction` / **Действие по щелчку** | Empty / Пусто | Standard Mimic action. / Стандартное действие мнемосхемы. |

The editor always uses the fixed `2 / 3 / 2 m` preview. Runtime reads channel values. A bad or missing layer is drawn as zero because Lite components intentionally have no text status. Overflow is clipped to the configured height.

Редактор всегда использует фиксированное превью `2 / 3 / 2 м`. Во время выполнения читаются значения каналов. Слой с отсутствующими или недостоверными данными рисуется как нулевой, поскольку Lite-компоненты намеренно не имеют текстового статуса. Переполнение обрезается по заданной высоте.

## Linear Gauge / Линейная шкала

`LinearGauge` is a separate read-only zonal scale driven by the standard input channel. It is not a layered liquid indicator and does not calculate tank alarms.

`LinearGauge` — отдельная зональная шкала только для чтения, использующая стандартный входной канал. Она не является многослойным уровнемером и не вычисляет аварии резервуара.

| Property / Свойство | Default / По умолчанию |
|---|---:|
| Standard input channel / Стандартный входной канал | `0` |
| `orientation` / **Ориентация** | `Horizontal` / `Горизонтально` |
| `unit` / **Единица измерения** | Empty / Пусто |
| `decimalPlaces` / **Знаков после запятой** | `1` |
| `minimum` / **Минимум** | `0` |
| `lowAlarmLimit` / **Нижняя граница аварии** | `10` |
| `lowWarningLimit` / **Нижняя граница предупреждения** | `20` |
| `highWarningLimit` / **Верхняя граница предупреждения** | `80` |
| `highAlarmLimit` / **Верхняя граница аварии** | `90` |
| `maximum` / **Максимум** | `100` |
| `workingColor` / **Цвет рабочей зоны** | `#22c55e` |
| `warningColor` / **Цвет предупреждения** | `#f59e0b` |
| `alarmColor` / **Цвет аварии** | `#dc2626` |

Limits are normalized into ascending order between minimum and maximum. Changing orientation exchanges a landscape preview size for a useful portrait size when appropriate. Missing or bad-quality data is shown as `#.#`. The component has no output channel, command sending or click action.

Границы нормализуются по возрастанию между минимумом и максимумом. При подходящем соотношении сторон смена ориентации заменяет горизонтальный размер превью на удобный вертикальный. Отсутствующие или недостоверные данные отображаются как `#.#`. У компонента нет выходного канала, отправки команд и действия по щелчку.

## Industrial SVG Vessels / Промышленные SVG-ёмкости

Every vessel always displays its body and nameplate. The level indicator, alarm badges and instruments are disabled by default and appear only when enabled in properties. Liquid is shown on a wide external level scale and in separate value cards; it is not painted transparently inside the vessel body.

Каждая ёмкость всегда показывает корпус и шильдик. Уровнемер, аварийные метки и приборы по умолчанию выключены и появляются только после включения в свойствах. Жидкость отображается на широкой внешней шкале и в отдельных карточках значений; прозрачная заливка внутри корпуса не рисуется.

### Vessel Capability Matrix / Матрица возможностей ёмкостей

| Vessel / Ёмкость | Layers / Слои | Temperature / Температура | Pressure / Давление | Additional / Дополнительно |
|---|---:|---|---|---|
| `RvsVessel` — РВС | 3 | Single, multipoint / Одиночная, многоточечная | Yes / Да | Alarms / Аварии |
| `VerticalProcessVessel` — вертикальный аппарат | 3 | Single, multipoint / Одиночная, многоточечная | Yes / Да | Alarms / Аварии |
| `HorizontalProcessVessel` — горизонтальный аппарат | 3 | Single / Одиночная | Yes / Да | Alarms / Аварии |
| `SiloHopper` — силос или бункер | 1 product / 1 продукт | Single, multipoint / Одиночная, многоточечная | No / Нет | Direct mass / Прямая масса |
| `RectangularClosedTank` — закрытая прямоугольная | 3 | Single / Одиночная | Yes / Да | Alarms / Аварии |
| `OpenBath` — открытая ванна | 1 | Single / Одиночная | No / Нет | Alarms / Аварии |
| `SphericalTank` — сферический резервуар | 3 | Single / Одиночная | Yes / Да | Alarms / Аварии |
| `ReactorMixer` — реактор-смеситель | 1 | Single, multipoint / Одиночная, многоточечная | Yes / Да | Drive state / Состояние привода |

All vessel types also support `LL/L/H/HH` level alarms and an independent general alarm channel.

Все типы ёмкостей также поддерживают аварийные уровни `LL/L/H/HH` и независимый канал общей аварии.

### Common Vessel Properties / Общие свойства ёмкостей

| Property / Свойство | Default / По умолчанию | Purpose / Назначение |
|---|---:|---|
| `vesselName` / **Название ёмкости** | Type-specific / Зависит от типа | Text on the nameplate. / Текст на шильдике. |
| `bodyStyle` / **Покрытие корпуса** | `Tinted` | Selects a tintable body or fixed steel gradients. / Выбирает тонируемый корпус или фиксированные стальные градиенты. |
| `bodyColor` / **Цвет корпуса** | `#748491` | Body tint used by `Tinted`. / Цвет корпуса в режиме `Tinted`. |
| `levelEnabled` / **Показать уровнемер** | `false` | Shows the external level scale and value cards. / Показывает внешнюю шкалу и карточки значений. |
| `tankHeightMeters` / **Высота ёмкости, м** | `8` | Physical vessel height. / Физическая высота ёмкости. |
| `deadZoneMeters` / **Мёртвая зона, м** | `0` | Bottom sediment compensation. / Компенсация нижнего осадка. |
| `decimalPlaces` / **Знаков уровня после запятой** | `3` | Level value precision from `0` to `6`. / Точность уровня от `0` до `6`. |
| `emptyColor` / **Цвет пустой шкалы** | `#e2e8f0` | Empty part of the external scale. / Пустая часть внешней шкалы. |
| `alarmsEnabled` / **Показать аварийные уровни** | `false` | Enables `LL/L/H/HH` badges and bindings. / Включает метки и привязки `LL/L/H/HH`. |
| `generalAlarmEnabled` / **Общая авария** | `false` | Enables the independent general alarm. / Включает независимую общую аварию. |
| `generalAlarmInCnlNum` / **Канал общей аварии** | `0` | Nonzero good value activates the alarm outline. / Достоверное ненулевое значение включает аварийную рамку. |
| `clickAction` / **Действие по щелчку** | Empty / Пусто | Standard Mimic action. / Стандартное действие мнемосхемы. |

The layer source, color, dead-zone, overflow, quality and alarm rules are the same as for the full level indicators. One-layer vessels expose only the first layer.

Правила источников слоёв, цветов, мёртвой зоны, переполнения, качества и аварий совпадают с полными уровнемерами. Однослойные ёмкости предоставляют только первый слой.

### Body Styles / Покрытия корпуса

| Style / Стиль | Behavior / Поведение |
|---|---|
| `Tinted` / **Тонированный** | Uses `bodyColor` while preserving industrial light and shadow. / Использует `bodyColor`, сохраняя промышленные свет и тень. |
| `Steel` / **Сталь** | Uses the fixed metallic gradients of the original SVG. `bodyColor` does not recolor the steel coating. / Использует фиксированные металлические градиенты исходного SVG. `bodyColor` не перекрашивает стальное покрытие. |

### Pressure and Single Temperature / Давление и одиночная температура

Pressure properties are available only for vessel types marked in the capability matrix:

Свойства давления доступны только для типов, отмеченных в матрице:

| Property group / Группа | Main settings / Основные настройки | Editor preview / Превью редактора |
|---|---|---:|
| Pressure / Давление | `pressureEnabled`, `pressureInCnlNum`, `pressureUnit`, `pressureDecimalPlaces` | `0.62 MPa / МПа` |
| Single temperature / Одиночная температура | `temperatureMode = Single`, `temperatureInCnlNum`, `temperatureUnit`, `temperatureDecimalPlaces` | `54.8 °C` |

At runtime, the configured channel value replaces the preview. Missing or bad-quality values are displayed as `#.#` together with the configured unit.

Во время выполнения значение настроенного канала заменяет превью. При отсутствии данных или плохом качестве отображается `#.#` вместе с заданной единицей измерения.

### Multipoint Temperature / Многоточечная температура

Multipoint mode is available for `RvsVessel`, `VerticalProcessVessel`, `SiloHopper` and `ReactorMixer`. The editable list contains from 1 to 24 `TemperaturePoint` entries.

Многоточечный режим доступен для `RvsVessel`, `VerticalProcessVessel`, `SiloHopper` и `ReactorMixer`. Редактируемый список содержит от 1 до 24 элементов `TemperaturePoint`.

Each point stores:

- `name` — point name;
- `heightMeters` — physical installation height from the bottom;
- `inCnlNum` — input channel.

Каждая точка содержит:

- `name` — название точки;
- `heightMeters` — физическую высоту установки от дна;
- `inCnlNum` — входной канал.

The temperature card selects the point nearest to the current physical liquid level before dead-zone subtraction. If two points are equally distant, the lower point is selected. An unknown or partial physical level, or bad quality of the selected point, produces `#.#` without substituting another point.

Карточка температуры выбирает точку, ближайшую к текущему физическому уровню до вычитания мёртвой зоны. При одинаковом расстоянии выбирается нижняя точка. Неизвестный или частичный физический уровень либо плохое качество выбранной точки даёт `#.#` без автоматической подмены другой точкой.

At runtime, click the `T` card to open a read-only table of all points sorted by height. The table shows name, height, value and quality. This click does not execute the vessel's general `clickAction`.

Во время выполнения щёлкните карточку `T`, чтобы открыть таблицу всех точек только для чтения, отсортированную по высоте. В таблице показаны имя, высота, значение и качество. Этот щелчок не запускает общее `clickAction` ёмкости.

### Silo Mass / Масса силоса

`SiloHopper` can display mass using `massEnabled`, `massInCnlNum`, `massUnit` and `massDecimalPlaces`. The editor preview is `42.5 t / т`.

`SiloHopper` может показывать массу с помощью `massEnabled`, `massInCnlNum`, `massUnit` и `massDecimalPlaces`. Превью редактора равно `42.5 t / т`.

Mass is read directly from the channel. The plugin does not calculate volume, density, temperature correction or derived mass.

Масса читается непосредственно из канала. Плагин не вычисляет объём, плотность, температурную поправку или расчётную массу.

### Reactor Drive / Привод реактора

Enable `driveEnabled` and set `driveInCnlNum` to display the reactor drive state.

Включите `driveEnabled` и задайте `driveInCnlNum`, чтобы показывать состояние привода реактора.

| Channel data / Данные канала | State and display / Состояние и отображение |
|---|---|
| Good quality, value `0` / Хорошее качество, значение `0` | `STOPPED / СТОП`, steady red motor / постоянный красный двигатель |
| Good quality, value `1` / Хорошее качество, значение `1` | `RUNNING / РАБОТА`, green motor and rotating mixer / зелёный двигатель и вращение мешалки |
| Good quality, value `2` or `-1` / Хорошее качество, значение `2` или `-1` | `ALARM / АВАРИЯ`, blinking red motor and card / мигающие красные двигатель и карточка |
| Missing, bad quality or another value / Нет данных, плохое качество или другое значение | `UNKNOWN / НЕИЗВЕСТНО`, amber motor / янтарный двигатель |

Mixer rotation and alarm blinking are disabled in edit mode and when reduced motion is requested by the operating system.

Вращение мешалки и мигание аварии отключаются в редакторе и при системной настройке уменьшения движения.

## Editor Preview and Runtime / Превью редактора и рабочий режим

The editor intentionally shows configured previews, all enabled instrument positions and level-alarm layout. This makes it possible to arrange a mimic before channels produce live values.

Редактор намеренно показывает настроенные превью, расположение всех включённых приборов и компоновку аварийных уровней. Это позволяет оформить мнемосхему до появления реальных данных каналов.

Runtime uses channel values and SCADA quality. Preview values never silently replace a missing channel value in `Channel` mode. Inactive alarm badges and an inactive general alarm are hidden.

Во время выполнения используются значения каналов и качество SCADA. В режиме `Канал` превью никогда незаметно не заменяет отсутствующее рабочее значение. Неактивные аварийные метки и неактивная общая авария скрыты.

All TankJP components preserve the standard green selection frame and resize handles in the editor. The component root receives selection, movement, resizing and the standard click action; internal artwork does not intercept those operations.

Все компоненты TankJP сохраняют стандартную зелёную рамку выделения и маркеры изменения размера в редакторе. Выделение, перемещение, изменение размера и стандартное действие по щелчку принадлежат корневому компоненту; внутренняя графика не перехватывает эти операции.

## Pipe Grid Integration / Совмещение с сеткой трубопроводов

The vessel SVG layouts use a `400 × N` canvas divided into `100 + 200 + 100` horizontal zones. The center vessel body is 200 pixels wide; alarm indicators occupy the left 100-pixel zone and instrument cards occupy the right 100-pixel zone. Default heights are multiples of 50 or 100 pixels.

Макеты SVG-ёмкостей используют полотно `400 × N`, разделённое по горизонтали на зоны `100 + 200 + 100`. Центральный корпус имеет ширину 200 пикселей; аварийные индикаторы занимают левую зону 100 пикселей, а карточки приборов — правую зону 100 пикселей. Высота по умолчанию кратна 50 или 100 пикселям.

This geometry helps place a vessel over a pipeline assembled from `100 × 100` tiles. `PlgMimTankJP` does not automatically snap components and does not require `PlgMimPipesJP`. Keep the default vessel proportions when pipe connection alignment is important.

Такая геометрия помогает размещать ёмкость поверх трубопровода, собранного из блоков `100 × 100`. `PlgMimTankJP` не выполняет автоматическую привязку и не требует `PlgMimPipesJP`. Если важна стыковка с трубой, сохраняйте исходные пропорции ёмкости.

## Installation and Registration / Установка и регистрация

Requirements:

- Rapid SCADA 6.x;
- the .NET 8 runtime used by SCADA Web;
- Mimic diagrams and a compatible Mimic Editor;
- configured input channels for runtime data;
- a valid `PlgMimTankJP` product license for placing new components;
- a package matching the installed Rapid SCADA build.

Требования:

- Rapid SCADA 6.x;
- среда .NET 8, используемая SCADA Web;
- поддержка мнемосхем и совместимый редактор Mimic;
- настроенные входные каналы для рабочих данных;
- действующая лицензия продукта `PlgMimTankJP` для добавления новых компонентов;
- пакет, соответствующий установленной сборке Rapid SCADA.

Installation:

1. Copy the supplied `SCADA` package over the Rapid SCADA installation directory while preserving the directory structure. The package includes the required LicenseJPLite runtime files.
2. Enable `PlgMimTankJP` in the Webstation plugin configuration.
3. On Windows, install the supplied `PlgMimTankJP.View.dll` in `ScadaAdmin\Lib` when the classic Administrator must recognize the plugin.
4. Restart SCADA Web, its service or the IIS site. A browser refresh alone does not reload plugin assemblies.
5. Open a mimic editor and check that the **TANKS / РЕЗЕРВУАРЫ** group contains thirteen components.
6. After an update, perform a hard browser refresh if old styles remain cached.

Установка:

1. Скопируйте поставляемый пакет `SCADA` поверх каталога установки Rapid SCADA с сохранением структуры папок. Пакет содержит необходимые файлы среды LicenseJPLite.
2. Включите `PlgMimTankJP` в конфигурации плагинов Вебстанции.
3. Под Windows установите поставляемый `PlgMimTankJP.View.dll` в `ScadaAdmin\Lib`, если классический Администратор должен распознавать плагин.
4. Перезапустите SCADA Web, соответствующую службу или сайт IIS. Простое обновление браузера не перезагружает сборки плагина.
5. Откройте редактор мнемосхем и убедитесь, что группа **TANKS / РЕЗЕРВУАРЫ** содержит тринадцать компонентов.
6. Если после обновления остались старые стили, выполните жёсткое обновление страницы.

Required Webstation plugin entry:

Необходимая запись плагина Вебстанции:

```xml
<Plugins>
  <Plugin code="PlgMimTankJP" />
</Plugins>
```

The public browser asset path is `/plugins/MimTank`. Do not rename `PlgMimTankJP.dll`, `PlgMimTankJP.View.dll` or the `MimTank` static directory.

Публичный путь браузерных ресурсов — `/plugins/MimTank`. Не переименовывайте `PlgMimTankJP.dll`, `PlgMimTankJP.View.dll` и статический каталог `MimTank`.

## Activation / Активация

The tank plugin uses its own installation-specific license. A `MimicEditorJP`, `PlgMimPipesJP` or another product license does not activate `PlgMimTankJP`.

Плагин резервуаров использует собственную лицензию, привязанную к установке. Лицензия `MimicEditorJP`, `PlgMimPipesJP` или другого продукта не активирует `PlgMimTankJP`.

| Host / Приложение | Activation request / Запрос активации | License / Лицензия |
|---|---|---|
| SCADA Web | `C:\Program Files\SCADA\ScadaWeb\config\PlgMimTankJP_Activation.bin` | `C:\Program Files\SCADA\ScadaWeb\config\PlgMimTankJP.bin` |
| ScadaAdminWebJP | `C:\Program Files\SCADA\ScadaAdminWebJP\License\PlgMimTankJP_Activation.bin` | `C:\Program Files\SCADA\ScadaAdminWebJP\License\PlgMimTankJP.bin` |

English:

1. Start SCADA Web or ScadaAdminWebJP without a TankJP license.
2. The plugin creates `PlgMimTankJP_Activation.bin` in the host license directory. An existing request is not overwritten.
3. Send the activation request to the license provider.
4. The generated license must preserve the request UID and the exact application name `PlgMimTankJP`.
5. Save the received key as `PlgMimTankJP.bin` in the same host license directory.
6. Restart SCADA Web or ScadaAdminWebJP. A browser refresh alone is not sufficient.
7. If both applications are used, place a valid license in each directory because each host reads only its own license location.

Русский:

1. Запустите SCADA Web или ScadaAdminWebJP без лицензии TankJP.
2. Плагин создаст `PlgMimTankJP_Activation.bin` в папке лицензий хоста. Существующий запрос не перезаписывается.
3. Передайте запрос активации поставщику лицензии.
4. При создании лицензии должны быть сохранены UID из запроса и точное имя приложения `PlgMimTankJP`.
5. Сохраните полученный ключ под именем `PlgMimTankJP.bin` в той же папке лицензий хоста.
6. Перезапустите SCADA Web или ScadaAdminWebJP. Простого обновления страницы недостаточно.
7. Если используются оба приложения, поместите действующую лицензию в каждый каталог, потому что каждый хост читает только собственную папку лицензий.

If the license is missing, invalid or issued for another `AppName`, existing TankJP components continue to load and display. The **TANKS / РЕЗЕРВУАРЫ** toolbox group is hidden and direct placement of new components is rejected until a valid license is installed.

Если лицензия отсутствует, недействительна или выдана для другого `AppName`, существующие компоненты TankJP продолжают загружаться и отображаться. Группа **TANKS / РЕЗЕРВУАРЫ** скрывается, а прямое добавление новых компонентов запрещается до установки действующей лицензии.

## Troubleshooting / Устранение неполадок

| Symptom / Признак | Cause and action / Причина и действие |
|---|---|
| The **TANKS / РЕЗЕРВУАРЫ** group is missing / Группа отсутствует | Check `PlgMimTankJP.bin`, plugin registration, DLL and static files, then restart the host. Existing components remain visible without a license, but the toolbox is hidden. / Проверьте `PlgMimTankJP.bin`, регистрацию плагина, DLL и статические файлы, затем перезапустите хост. Без лицензии существующие компоненты видны, но группа инструментов скрыта. |
| `PlgMimTankJP_Activation.bin` is not created / Запрос активации не создаётся | Verify that `LicenseJP.Logic.dll` and its packaged dependencies are installed beside the plugin runtime and that the host can write to its license directory. / Проверьте наличие `LicenseJP.Logic.dll` и пакетных зависимостей рядом со средой плагина, а также право хоста на запись в папку лицензий. |
| The group contains fewer than 13 items / В группе меньше 13 элементов | The DLL and browser assets are from different versions. Deploy the complete matching package. / DLL и браузерные ресурсы относятся к разным версиям. Установите полный согласованный пакет. |
| The editor shows levels, but runtime shows `#.#` / В редакторе есть уровни, а во время выполнения показано `#.#` | Editor preview is working, but a `Channel` source has channel `0`, missing data or bad quality. / Превью работает, но в режиме `Канал` указан канал `0`, нет данных или качество плохое. |
| A Lite layer is empty / Слой Lite пуст | Check `activeLayerCount`, the layer channel number and positive channel status. Lite has no text placeholder. / Проверьте количество активных слоёв, номер канала и положительный статус. У Lite нет текстового заполнителя. |
| An alarm marker is not visible / Аварийная метка не видна | In runtime only active alarms are shown. Check `showAlarms` or `alarmsEnabled`, threshold, external channel value and quality. / Во время выполнения видны только активные аварии. Проверьте включение аварий, порог, значение и качество внешнего канала. |
| A calculated alarm disappeared after one layer failed / Вычисляемая авария исчезла после отказа слоя | A partial level makes calculated alarms unknown by design. Use a reliable external alarm channel when the alarm must remain authoritative. / Частичный уровень намеренно переводит вычисляемые аварии в неизвестное состояние. Для авторитетной аварии используйте достоверный внешний канал. |
| The vessel body does not use the selected color / Корпус не использует выбранный цвет | `bodyStyle` is `Steel`. Select `Tinted` to apply `bodyColor`. / Выбран стиль `Steel`. Для применения `bodyColor` выберите `Tinted`. |
| Pressure, temperature, mass or drive is absent / Нет давления, температуры, массы или привода | Enable the instrument and verify that the selected vessel type supports it. / Включите прибор и убедитесь, что выбранный тип ёмкости его поддерживает. |
| Multipoint temperature shows `#.#` / Многоточечная температура показывает `#.#` | Check complete level quality and the channel quality of the nearest selected point. The plugin does not substitute another point. / Проверьте полный уровень и качество канала ближайшей выбранной точки. Плагин не подставляет другую точку. |
| Reactor state is `UNKNOWN / НЕИЗВЕСТНО` | Verify the drive channel status and use values `0`, `1`, `2` or `-1`. / Проверьте статус канала привода и используйте значения `0`, `1`, `2` или `-1`. |
| Reactor animation does not run / Мешалка не вращается | Animation requires runtime state `1`, good quality and no reduced-motion request. It is intentionally disabled in the editor. / Для анимации нужны рабочее значение `1`, хорошее качество и отсутствие режима уменьшения движения. В редакторе анимация намеренно отключена. |
| The classic Administrator reports an assembly load error / Классический Администратор сообщает об ошибке загрузки сборки | Install the matching packaged `PlgMimTankJP.View.dll`; do not reuse a View DLL built for another Rapid SCADA version. / Установите соответствующий пакетный `PlgMimTankJP.View.dll`; не используйте View DLL от другой версии Rapid SCADA. |
| New files are installed, but the old appearance remains / Установлены новые файлы, но остался старый вид | Restart the application or IIS site and perform a hard browser refresh. / Перезапустите приложение или сайт IIS и выполните жёсткое обновление страницы. |

## Scope and Limitations / Границы функциональности

English:

- all level values are linear heights in metres;
- layer count and order are fixed by the selected component type;
- density, volume, vessel geometry calculations and temperature corrections are not implemented;
- silo mass is a direct preview or channel value, not a calculated value;
- the plugin does not send custom commands;
- the plugin does not automatically connect or snap to pipe components;
- old legacy `Tank` components are not migrated automatically.

Русский:

- все значения уровня являются линейной высотой в метрах;
- количество и порядок слоёв фиксируются выбранным типом компонента;
- плотность, объём, расчёт геометрии ёмкости и температурные поправки не реализованы;
- масса силоса является прямым значением превью или канала, а не результатом вычисления;
- плагин не отправляет собственные команды;
- плагин не выполняет автоматическую стыковку или привязку к трубопроводам;
- старые компоненты `Tank` автоматически не мигрируются.

## Video / Видео

The demonstration shows the capacitance components on the Rapid SCADA working diagram and their configuration in the editor.

В демонстрации показаны компоненты емкостей на работающей мнемосхеме Rapid SCADA и их настройка в редакторе.

[Watch the PlgMimPipesJP demonstration / Посмотреть демонстрацию PlgMimPipesJP](https://jurasskpark.ru/files/github/PlgMimPimpJP.mp4)

## Screenshots / Скриншоты

### Runtime mimic / Рабочая мнемосхема

![PlgMimTankJP components in a running Rapid SCADA mimic](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/ScadaWeb/PlgMimTankJP/Source/PlgMimTankJP_001.png)

### Mimic editor / Редактор мнемосхемы

![PlgMimTankJP components and properties in the mimic editor](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/ScadaWeb/PlgMimTankJP/Source/PlgMimTankJP_002.png)



## License / Лицензия

`PlgMimTankJP` is distributed as shareware/commercial software. A valid product license is required to place new tank components. Existing licensed mimic diagrams remain readable when a license is temporarily unavailable, but editing and new placement are restricted as described above. Do not rename the plugin DLL, activation request or license file.

`PlgMimTankJP` распространяется как условно-бесплатное/коммерческое программное обеспечение. Для добавления новых компонентов резервуаров требуется действующая лицензия продукта. Существующие мнемосхемы продолжают открываться при временном отсутствии лицензии, но редактирование и добавление новых компонентов ограничиваются, как описано выше. Не переименовывайте DLL плагина, запрос активации и файл лицензии.
