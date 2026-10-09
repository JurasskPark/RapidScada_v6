# PlgMimTankJP — Каталог компонентов

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/component-catalog.md)

| Имя типа | Русское название | Размер |
| --- | --- | --- |
| [`TankV2`](full-layered-level-indicators.md) | Уровень вертикальный | `260 × 420` |
| [`LayerProgress`](full-layered-level-indicators.md) | Уровень горизонтальный | `500 × 150` |
| [`LinearGauge`](linear-gauge.md) | Линейная шкала | `280 × 86` |
| [`VerticalLevelLite`](lite-level-indicators.md) | Уровень вертикальный Lite | `100 × 300` |
| [`HorizontalLevelLite`](lite-level-indicators.md) | Уровень горизонтальный Lite | `300 × 100` |
| [`RvsVessel`](industrial-svg-vessels.md) | РВС | `400 × 300` |
| [`VerticalProcessVessel`](industrial-svg-vessels.md) | Вертикальный аппарат | `400 × 300` |
| [`HorizontalProcessVessel`](industrial-svg-vessels.md) | Горизонтальный аппарат | `400 × 200` |
| [`SiloHopper`](industrial-svg-vessels.md) | Силос / бункер | `400 × 300` |
| [`RectangularClosedTank`](industrial-svg-vessels.md) | Закрытая прямоугольная ёмкость | `400 × 250` |
| [`OpenBath`](industrial-svg-vessels.md) | Открытая ванна | `400 × 200` |
| [`SphericalTank`](industrial-svg-vessels.md) | Сферический резервуар | `400 × 300` |
| [`ReactorMixer`](industrial-svg-vessels.md) | Реактор-смеситель | `400 × 350` |

Устаревший тип `Tank` не регистрируется и не поставляется. В существующих файлах `.mim`, содержащих `typeName: "Tank"`, необходимо заменить этот компонент на `TankV2` до развёртывания.
