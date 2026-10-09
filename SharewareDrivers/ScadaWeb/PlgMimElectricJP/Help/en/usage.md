# PlgMimElectricJP — Placement and connections

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/usage.md)

1. Open a mimic in the standard or JP editor and choose a type from the [component catalog](components.md).
2. Place it on the 50 px grid. Set `rotation` to `Deg0`, `Deg90`, `Deg180` or `Deg270`.
3. Add engineering metadata and a reference designation. These values describe the equipment; they do not change its base geometry.
4. Select a [drawing profile](symbol-profiles.md) and configure [channel bindings](channels.md).
5. Save the `.mim` file. Verify state changes in the licensed viewer; the editor shows a static preview.

Atomic symbols are 50 × 50 px. Larger PLC, I/O, panel and terminal-block types have fixed sizes listed in the catalog. Free resizing is disabled and the generic Size property is hidden. Ports are explicitly defined on tile edges; a standard tile uses edge centers 25 px from the corners. Rotation also rotates the port coordinates.

Components expose connection families `electrical.power`, `electrical.signal`, `electrical.fire` and `electrical.data`. The side ports of `ElectricalControlValve` belong to `process.fluid`; its top port is the electrical control connection. Port roles distinguish sources, loads, PLC/I/O directions and passive conductors.

Where the host supports smart connections, metadata validation reports:
- incompatible protective/functional earth or live conductors;
- incompatible AC/DC systems, signal types or cable types;
- nominal voltages differing by more than 10%, or different positive phase/pole counts;
- uncertain compatibility when engineering data is incomplete. Snapping is allowed in this case.

Voltage comparison understands numeric voltage text with `V` or `kV`. A process connection has an unknown diameter and returns an uncertain result. The plugin's electrical validator leaves cross-family checks to the host.

These are editing hints, not circuit calculations or protection coordination. A connected drawing does not propagate a live voltage between ordinary symbols: each indicator uses its own SCADA data.
