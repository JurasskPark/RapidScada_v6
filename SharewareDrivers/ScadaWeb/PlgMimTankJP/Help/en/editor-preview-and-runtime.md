# PlgMimTankJP — Editor Preview and Runtime

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/editor-preview-and-runtime.md)

The editor intentionally shows configured previews, all enabled instrument positions and level-alarm layout. This makes it possible to arrange a mimic before channels produce live values.

Runtime uses channel values and SCADA quality. Preview values never silently replace a missing channel value in `Channel` mode. Inactive alarm badges and an inactive general alarm are hidden.

All TankJP components preserve the standard green selection frame and resize handles in the editor. The component root receives selection, movement, resizing and the standard click action; internal artwork does not intercept those operations.
