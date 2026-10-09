# PlgMimTankJP — Quick Start

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/quick-start.md)

1. Install and enable `PlgMimTankJP`, then restart SCADA Web and activate the plugin.
2. Open a mimic in Mimic Editor or Mimic Editor JP.
3. Select a component from the **TANKS** group and place it on the canvas.
4. Set the vessel or indicator height in metres.
5. For each required layer, select **Channel** and enter its input channel number.
6. Set layer names and colors.
7. Configure the dead zone and alarm thresholds if required.
8. For an SVG vessel, separately enable the level indicator and every required instrument.
9. Save the mimic and open it in Webstation to check live values and quality.

New full and vessel components use zero channel numbers by default. The editor still shows preview values, while runtime correctly treats unconfigured channel data as unknown.
