# DrvFSTJP — XML project

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/xml-project.md)

The device configuration file name follows Rapid SCADA driver conventions:

- `DrvFSTJP.xml` for device number `0`;
- `DrvFSTJP_001.xml`, `DrvFSTJP_002.xml`, etc. for regular devices.

The sample project is in `DemoProjects/DrvFSTJP_001.xml`.

In ScadaAdmin, open the device properties to create or edit this XML file using the built-in driver form. The form stores the per-device configuration in the ScadaComm configuration directory.

Important XML fields:

- `MasterAddress` - PC/master address on the FST bus, usually `0`;
- `DeviceAddress` - FST-03x address `1..15`;
- `PollLinkCheck` - send command `0x00`;
- `PollStatus` - send command `0x01`;
- `Channels/Channel` - enabled FST channels `1..8`;
- `Coefficient` and `Offset` - convert raw 12-bit concentration as `raw * Coefficient + Offset`;
- `RelayDevices` - optional relay expansion blocks.

Generated tags:

- `DeviceType`;
- `GlobalErrors`;
- for each enabled channel: `<CodePrefix>_Concentration`, `<CodePrefix>_MessageCode`, `<CodePrefix>_AlarmCode`, `<CodePrefix>_SensorType`, `<CodePrefix>_CalibrationRequired`, `<CodePrefix>_Threshold1`, `<CodePrefix>_Threshold2`, `<CodePrefix>_Disabled`;
- for each relay block: `<CodePrefix>_StateLo`, `<CodePrefix>_StateHi`, `<CodePrefix>_Errors`.
