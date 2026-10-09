# PlgMimTankJP — Troubleshooting

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/troubleshooting.md)

| Symptom | Cause and action |
| --- | --- |
| The **TANKS** group is missing | Check `PlgMimTankJP.bin`, plugin registration, DLL and static files, then restart the host. Existing components remain visible without a license, but the toolbox is hidden. |
| `PlgMimTankJP_Activation.bin` is not created | Verify that `LicenseJP.Logic.dll` and its packaged dependencies are installed beside the plugin runtime and that the host can write to its license directory. |
| The group contains fewer than 13 items | The DLL and browser assets are from different versions. Deploy the complete matching package. |
| The editor shows levels, but runtime shows `#.#` | Editor preview is working, but a `Channel` source has channel `0`, missing data or bad quality. |
| A Lite layer is empty | Check `activeLayerCount`, the layer channel number and positive channel status. Lite has no text placeholder. |
| An alarm marker is not visible | In runtime only active alarms are shown. Check `showAlarms` or `alarmsEnabled`, threshold, external channel value and quality. |
| A calculated alarm disappeared after one layer failed | A partial level makes calculated alarms unknown by design. Use a reliable external alarm channel when the alarm must remain authoritative. |
| The vessel body does not use the selected color | `bodyStyle` is `Steel`. Select `Tinted` to apply `bodyColor`. |
| Pressure, temperature, mass or drive is absent | Enable the instrument and verify that the selected vessel type supports it. |
| Multipoint temperature shows `#.#` | Check complete level quality and the channel quality of the nearest selected point. The plugin does not substitute another point. |
| Reactor state is `UNKNOWN` | Verify the drive channel status and use values `0`, `1`, `2` or `-1`. |
| Reactor animation does not run | Animation requires runtime state `1`, good quality and no reduced-motion request. It is intentionally disabled in the editor. |
| The classic Administrator reports an assembly load error | Install the matching packaged `PlgMimTankJP.View.dll`; do not reuse a View DLL built for another Rapid SCADA version. |
| New files are installed, but the old appearance remains | Restart the application or IIS site and perform a hard browser refresh. |
