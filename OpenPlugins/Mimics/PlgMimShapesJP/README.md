# PlgMimShapesJP

![PlgMimShapesJP](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=PlgMimShapesJP&color=4bb60e)

![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=10.0&color=purple)
![Platform](https://jurasskpark.ru/service/budges/?label=platform&message=Windows%20%2F%20Linux&color=lightgrey)

**Mimic Shapes** — a Rapid SCADA plugin that provides geometric shape components for mimic diagrams.  
**Фигуры мнемосхем** — плагин Rapid SCADA, добавляющий геометрические фигуры для мнемосхем.

## Overview / Обзор

PlgMimShapesJP adds 17 general geometric shape components to the mimic editor toolbox. PlgMimicJP additionally exposes an editable polyline because it supports point handles.

Плагин добавляет 17 основных геометрических фигур на панель инструментов редактора мнемосхем. PlgMimicJP дополнительно отображает редактируемую ломаную линию, поскольку поддерживает работу с её точками.

## Components / Компоненты

| Component | Description | Описание |
|---|---|---|
| **Rectangle** | Rectangle shape | Прямоугольник |
| **Square** | Square shape | Квадрат |
| **Ellipse** | Ellipse shape | Эллипс |
| **Circle** | Circle shape | Круг |
| **RoundedRect** | Rectangle with rounded corners | Скругленный прямоугольник |
| **Polygon** | Regular or custom polygon | Правильный или произвольный многоугольник |
| **Triangle** | Triangle shape | Треугольник |
| **Diamond** | Diamond shape | Ромб |
| **Hexagon** | Hexagon shape | Шестиугольник |
| **Parallelogram** | Parallelogram shape | Параллелограмм |
| **Trapezoid** | Trapezoid shape | Трапеция |
| **Cross** | Cross shape | Крест |
| **HalfCircle** | Half circle shape | Полукруг |
| **Donut** | Donut (ring) shape | Пончик (кольцо) |
| **Pie** | Pie (sector) shape | Сектор |
| **Arrow** | Directional arrow | Стрелка |
| **Line** | Line with configurable orientation | Линия с настраиваемой ориентацией |
| **Polyline** | Editable polyline in PlgMimicJP | Редактируемая ломаная линия в PlgMimicJP |

## Features / Возможности

- **17 common shape types** — geometric primitives available in every supported editor
- **PlgMimicJP polyline** — an additional point-editable broken line
- **Customizable appearance** — fill color, stroke color, stroke width, dash pattern, opacity, rotation
- **Background image support** — each shape can have a background image with adjustable opacity
- **Polygon point modes** — Auto (regular polygon) and Custom (manual points)
- **Line orientation** — Diagonal, Horizontal, Vertical, Custom
- **Arrow directions** — Right, Left, Up, Down
- **Localization** — English and Russian language support

- **17 основных типов фигур** — геометрические примитивы во всех поддерживаемых редакторах
- **Ломаная линия PlgMimicJP** — дополнительная фигура с редактируемыми точками
- **Настраиваемый внешний вид** — цвет заливки, цвет обводки, толщина обводки, пунктир, прозрачность, поворот
- **Поддержка фонового изображения** — каждая фигура может иметь фоновое изображение с регулируемой прозрачностью
- **Режимы точек многоугольника** — Авто (правильный многоугольник) и Вручную (произвольные точки)
- **Ориентация линии** — Диагональная, Горизонтальная, Вертикальная, Произвольная
- **Направления стрелки** — Вправо, Влево, Вверх, Вниз
- **Локализация** — поддержка английского и русского языков

## Project Structure / Структура проекта

```
PlgMimShapesJP/
├── PlgMimShapesJP.sln                    # Solution file
├── StartСompiling.bat                    # Build ZIP package
├── ../BuildPublish_PlgMimShapesJP.bat    # Portable package script
├── README.md                             # This file
│
├── PlgMimShapesJP/                       # Web plugin project
│   ├── PlgMimShapesJP.csproj
│   ├── component.json                    # Component manifest
│   ├── PlgMimShapesJPLogic.cs            # Plugin logic entry point
│   ├── Code/
│   │   ├── ShapesComponentGroup.cs       # Toolbox component group
│   │   ├── ShapesComponentSpec.cs        # Component specification
│   │   ├── ShapesSubtypeGroup.cs         # Subtype group registration
│   │   ├── PluginConst.cs                # Plugin constants
│   │   └── PluginPhrases.cs              # Localized phrases
│   ├── lang/                             # Language XML files
│   │   ├── PlgMimShapesJP.en-GB.xml
│   │   └── PlgMimShapesJP.ru-RU.xml
│   └── wwwroot/plugins/MimShapesJP/
│       ├── css/
│       │   ├── shapes.scss               # SCSS source
│       │   ├── shapes.css                # Compiled CSS
│       │   └── shapes.min.css            # Minified CSS
│       ├── images/                       # SVG icons for components
│       └── js/
│           ├── shapes-descr.js           # Property descriptors
│           ├── shapes-factory.js         # Factories and scripts
│           ├── shapes-render.js          # Renderers
│           ├── shapes-subtypes.js        # Subtype definitions
│           ├── shapes-bundle.js          # Runtime bundle (all above)
│           └── shapes-lang.js            # XML-backed browser localization
│
├── PlgMimShapesJP.Shared/                # Shared library
│   └── PluginInfo.cs                     # Plugin metadata
│
└── PlgMimShapesJP.View/                  # Admin view plugin
    ├── PlgMimShapesJP.View.csproj
    └── PlgMimShapesJPView.cs             # Plugin view entry point
```

## Build / Сборка

Requires Windows, PowerShell 7.2+ and the .NET 10 SDK. Run from this product folder:

Нужны Windows, PowerShell 7.2+ и .NET 10 SDK. Запуск из папки продукта:

```cmd
StartСompiling.bat -Runtime win-x64
```

Without `-Runtime`, all platforms listed in `release.json` are built. ZIP archives
and SHA-256 files are written to the repository's `Releases` directory.
Each ZIP contains `SCADA` and an automatically generated `readme.txt`.

Без `-Runtime` собираются все платформы из `release.json`. Готовые ZIP и SHA-256
сохраняются в корневой папке `Releases`. Каждый ZIP содержит `SCADA` и автоматически
сформированный `readme.txt`. Установка в SCADA выполняется отдельно.

Options, package layout and README metadata: [release packaging](../../../Doc/RELEASE_PACKAGING.md).
Параметры, структура пакетов и данные README: [сборка пакетов](../../../Doc/RELEASE_PACKAGING.md).

## Common Properties / Общие свойства

| Property | Description | Описание |
|---|---|---|
| Fill color | Background color of the shape | Цвет заливки фигуры |
| Stroke color | Outline color | Цвет обводки |
| Stroke width | Outline thickness | Толщина обводки |
| Stroke dash | Dash pattern (e.g., "5,3") | Штриховой узор (например, "5,3") |
| Opacity | 0-100% transparency | Прозрачность 0-100% |
| Rotation | Rotation angle in degrees | Угол поворота в градусах |
| Background color | SVG background color | Цвет фона SVG |
| Image | Background image name | Имя фонового изображения |
| Image opacity | Background image transparency | Прозрачность фонового изображения |

## Shape-Specific Properties / Специфические свойства фигур

| Shape | Property | Description | Описание |
|---|---|---|---|
| **Polygon** | Point mode | Auto (regular polygon) or Custom (manual points) | Авто (правильный многоугольник) или Вручную (произвольные точки) |
| | Point count | Number of vertices (2-12) | Количество вершин (2-12) |
| **RoundedRect** | Border radius | Corner rounding radius | Радиус скругления углов |
| **Donut** | Hole size | Inner hole size percentage (10-90%) | Размер внутреннего отверстия в процентах (10-90%) |
| **Pie** | Start angle | Starting angle in degrees | Начальный угол в градусах |
| | Sweep angle | Arc sweep angle in degrees | Угол дуги в градусах |
| **Arrow** | Direction | Right, Left, Up, Down | Направление: Вправо, Влево, Вверх, Вниз |
| **Line** | Orientation | Diagonal, Horizontal, Vertical, Custom | Ориентация: Диагональная, Горизонтальная, Вертикальная, Произвольная |
| | X1, Y1, X2, Y2 | Custom line endpoints (percentage) | Координаты концов линии (в процентах) |

## Development Notes / Заметки для разработчиков

### Point Handles (Anchor Points) / Точки привязки (якоря)

PlgMimicJP supports point handles for polygons, lines and polylines. It enables the polyline through the optional `ConfigureEditor("PlgMimicJP")` component convention. Other editors keep the polyline hidden because they do not provide the required point-editing workflow.

Drag a handle to move it, Alt-click the selected shape to insert a point into the nearest segment, and Shift-click a handle to remove it. Right-click finishes drawing a new polyline. At least two points are required; no artificial upper limit is imposed.

PlgMimicJP поддерживает точки редактирования многоугольников, линий и ломаных. Ломаная включается через необязательное соглашение компонентов `ConfigureEditor("PlgMimicJP")`. В других редакторах она остаётся скрытой, поскольку там нет необходимого сценария редактирования точек.

Перетаскивание маркера перемещает точку, Alt-клик по выбранной фигуре вставляет точку в ближайший сегмент, а Shift-клик по маркеру удаляет её. Правая кнопка мыши завершает рисование новой ломаной. Требуется минимум две точки; искусственного верхнего ограничения нет.

### Polyline / Полилиния

Polyline is available only in PlgMimicJP and supports moving, adding and removing points.

Ломаная линия доступна только в PlgMimicJP и поддерживает перемещение, добавление и удаление точек.

### Browser Assets / Ресурсы браузера

The production runtime loads `shapes-bundle.js` followed by `shapes-lang.js`. The bundle is generated from the four source files listed in `bundleconfig.json`; loading those sources a second time is intentionally avoided.

В рабочем режиме загружается `shapes-bundle.js`, затем `shapes-lang.js`. Bundle формируется из четырёх исходных файлов, перечисленных в `bundleconfig.json`; повторная загрузка этих файлов исключена.

## Screenshots / Скриншоты

![PlgMimShapesJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/master/OpenPlugins/Source/PlgMimShapesJP_001.png)

## License / Лицензия

This project is part of the Rapid SCADA ecosystem.  
Данный проект является частью экосистемы Rapid SCADA.

## SAST Tools

[PVS-Studio](https://pvs-studio.ru/ru/pvs-studio/?utm_source=website&utm_medium=github&utm_campaign=open_source) - static analyzer for C, C++, C#, and Java code.
