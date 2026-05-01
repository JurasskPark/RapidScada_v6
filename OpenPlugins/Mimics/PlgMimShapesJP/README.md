# PlgMimShapesJP

![PlgMimShapesJP](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v6/PlgMimShapesJP_v6.0.1.1/total)
[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](LICENSE)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)

**Mimic Shapes** — a Rapid SCADA plugin that provides geometric shape components for mimic diagrams.  
**Фигуры мнемосхем** — плагин Rapid SCADA, добавляющий геометрические фигуры для мнемосхем.

## Overview / Обзор

PlgMimShapesJP adds 18 geometric shape components to the mimic editor toolbox. Each component allows creating visual elements such as rectangles, circles, polygons, lines, arrows, stars, and more.

Плагин добавляет 18 типов геометрических фигур на панель инструментов редактора мнемосхем. Каждый компонент позволяет создавать визуальные элементы: прямоугольники, круги, многоугольники, линии, стрелки, звезды и многое другое.

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
| **Star** | Star shape | Звезда |
| **Arrow** | Directional arrow | Стрелка |
| **Line** | Line with configurable orientation | Линия с настраиваемой ориентацией |

## Features / Возможности

- **18 shape types** — geometric primitives for any visual need
- **Customizable appearance** — fill color, stroke color, stroke width, dash pattern, opacity, rotation
- **Background image support** — each shape can have a background image with adjustable opacity
- **Polygon point modes** — Auto (regular polygon) and Custom (manual points)
- **Line orientation** — Diagonal, Horizontal, Vertical, Custom
- **Arrow directions** — Right, Left, Up, Down
- **Localization** — English and Russian language support

- **18 типов фигур** — геометрические примитивы для любых визуальных задач
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
├── BuildDeploy_MimShapesJP.bat           # Build and deploy script
├── BuildPublish_MimShapesJP.bat          # Build and publish script
├── README.md                             # This file
│
├── PlgMimShapesJP/                       # Web plugin project
│   ├── PlgMimShapesJP.csproj
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
│           └── shapes-bundle.js          # Bundled JS (all above)
│
├── PlgMimShapesJP.Shared/                # Shared library
│   └── PluginInfo.cs                     # Plugin metadata
│
└── PlgMimShapesJP.View/                  # Admin view plugin
    ├── PlgMimShapesJP.View.csproj
    └── PlgMimShapesJPView.cs             # Plugin view entry point
```

## Build and Deploy / Сборка и развёртывание

### Prerequisites / Требования

- .NET 8.0 SDK
- Rapid SCADA installed to `C:\Program Files\SCADA`
- Administrator rights for deployment

### Quick Deploy / Быстрое развёртывание

Run `BuildDeploy_MimShapesJP.bat` as Administrator. The script will:

1. Build the web plugin (`PlgMimShapesJP`)
2. Build the admin view plugin (`PlgMimShapesJP.View`)
3. Stop the ScadaWeb service
4. Copy binaries to the SCADA installation
5. Deploy language files and web resources
6. Start the ScadaWeb service

Запустите `BuildDeploy_MimShapesJP.bat` от имени Администратора. Скрипт выполнит сборку, развёртывание и перезапуск службы.

### Manual Build / Ручная сборка

```bash
dotnet build PlgMimShapesJP/PlgMimShapesJP.csproj -c Release
dotnet build PlgMimShapesJP.View/PlgMimShapesJP.View.csproj -c Release
```

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
| **Star** | Point count | Number of star points (3-12) | Количество лучей звезды (3-12) |
| | Inner radius | Inner radius percentage (10-50%) | Внутренний радиус в процентах (10-50%) |
| **Arrow** | Direction | Right, Left, Up, Down | Направление: Вправо, Влево, Вверх, Вниз |
| **Line** | Orientation | Diagonal, Horizontal, Vertical, Custom | Ориентация: Диагональная, Горизонтальная, Вертикальная, Произвольная |
| | X1, Y1, X2, Y2 | Custom line endpoints (percentage) | Координаты концов линии (в процентах) |

## Development Notes / Заметки для разработчиков

### Point Handles (Anchor Points) / Точки привязки (якоря)

The plugin contains commented-out code for point handle editing (anchor points). This feature allows dragging shape vertices directly on the canvas. It is currently disabled because the Mimic Editor does not yet support the required point editing API. When the editor adds this support, uncomment the following sections:

Плагин содержит закомментированный код для редактирования точек привязки (якорей). Эта функция позволяет перетаскивать вершины фигур прямо на холсте. В настоящее время она отключена, так как Mimic Editor еще не поддерживает необходимый API для редактирования точек. Когда редактор добавит эту поддержку, раскомментируйте следующие секции:

1. In `shapes-bundle.js` / В `shapes-bundle.js`:
   - `_renderPointHandles()` method in `ShapeRendererBase`
   - `_getEditablePoints()`, `startPointEdit()`, `movePointEdit()`, `finishPointEdit()`, `addPointEdit()`, `removePointEdit()` methods in `ShapePolygonRenderer`, `ShapeLineRenderer`, and `ShapePolylineRenderer`
   - Helper methods: `_clampPercent()`, `_formatPercentPoints()`, `_mimicPointToComponentPercent()`, `_componentPointToPercent()`

2. In `shapes.css` / В `shapes.css`:
   - `.shape-point-handles` and `.jp-point-handle` styles

### Polyline / Полилиния

Polyline is fully functional but without point handles. The commented-out point editing code is ready for future use.

Полилиния полностью функциональна, но без точек привязки. Закомментированный код редактирования точек готов к будущему использованию.

## License / Лицензия

This project is part of the Rapid SCADA ecosystem.  
Данный проект является частью экосистемы Rapid SCADA.

## SAST Tools

[PVS-Studio](https://pvs-studio.ru/ru/pvs-studio/?utm_source=website&utm_medium=github&utm_campaign=open_source) - static analyzer for C, C++, C#, and Java code.
