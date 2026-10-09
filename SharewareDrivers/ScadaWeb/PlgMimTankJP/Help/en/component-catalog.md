# PlgMimTankJP — Component Catalog

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/component-catalog.md)

| Type name | English toolbox name | Default size |
| --- | --- | --- |
| [`TankV2`](full-layered-level-indicators.md) | Vertical level | `260 × 420` |
| [`LayerProgress`](full-layered-level-indicators.md) | Horizontal level | `500 × 150` |
| [`LinearGauge`](linear-gauge.md) | Linear gauge | `280 × 86` |
| [`VerticalLevelLite`](lite-level-indicators.md) | Vertical level Lite | `100 × 300` |
| [`HorizontalLevelLite`](lite-level-indicators.md) | Horizontal level Lite | `300 × 100` |
| [`RvsVessel`](industrial-svg-vessels.md) | Vertical storage tank | `400 × 300` |
| [`VerticalProcessVessel`](industrial-svg-vessels.md) | Vertical process vessel | `400 × 300` |
| [`HorizontalProcessVessel`](industrial-svg-vessels.md) | Horizontal process vessel | `400 × 200` |
| [`SiloHopper`](industrial-svg-vessels.md) | Silo / hopper | `400 × 300` |
| [`RectangularClosedTank`](industrial-svg-vessels.md) | Closed rectangular tank | `400 × 250` |
| [`OpenBath`](industrial-svg-vessels.md) | Open bath | `400 × 200` |
| [`SphericalTank`](industrial-svg-vessels.md) | Spherical tank | `400 × 300` |
| [`ReactorMixer`](industrial-svg-vessels.md) | Reactor mixer | `400 × 350` |

The obsolete legacy type `Tank` is not registered or distributed. Existing `.mim` files that contain `typeName: "Tank"` must replace that component with `TankV2` before deployment.
