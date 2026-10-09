# PlgMimTankJP — Pipe Grid Integration

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/pipe-grid-integration.md)

The vessel SVG layouts use a `400 × N` canvas divided into `100 + 200 + 100` horizontal zones. The center vessel body is 200 pixels wide; alarm indicators occupy the left 100-pixel zone and instrument cards occupy the right 100-pixel zone. Default heights are multiples of 50 or 100 pixels.

This geometry helps place a vessel over a pipeline assembled from `100 × 100` tiles. `PlgMimTankJP` does not automatically snap components and does not require `PlgMimPipesJP`. Keep the default vessel proportions when pipe connection alignment is important.
