# PlgMimTankJP — Dead Zone and Overflow

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/dead-zone-and-overflow.md)

The dead zone represents sediment, sludge or another unusable bottom layer. It is normalized to `0 ≤ deadZoneMeters < tankHeightMeters` and subtracted once from known liquid layers from bottom to top.

Example: physical level `1.50 m` and dead zone `0.30 m` produce a useful level of `1.20 m`.

The useful height is `tankHeightMeters - deadZoneMeters`. Corrected layer values, total percentage and calculated alarm thresholds use this useful height. The sediment is shown as a neutral hatched strip.

Overflow is detected from the physical level before dead-zone subtraction. The renderer preserves actual values, clips only the upper visible part and shows a localized overflow indicator. Therefore, the displayed total percentage may exceed `100%`.
