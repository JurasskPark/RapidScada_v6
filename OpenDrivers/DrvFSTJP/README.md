# DrvFSTJP

`DrvFSTJP` is a Rapid SCADA 6.x communication driver for FST-03x gas analyzers.

The implementation is based on `Dopolnitelnye-funktsii-FST-03h.-Rukovodstvo-polzovatelya.pdf` and uses the FST RS232/RS485 packet format:

- packet start: `0D 0A`;
- address byte: low nibble is destination address `0..15`, high nibble is source address `0..15`;
- command byte;
- data length byte `N`;
- header checksum: low byte of the sum of the first 5 header bytes;
- data block `N` bytes;
- data checksum: low byte of the sum of data bytes, `00` when `N = 0`.

Implemented polling commands:

- `0x00` link check, response data contains device type: `01` FST-03V, `02` FST-03m, `03` relay expansion block;
- `0x01` status request for FST-03x, response code `01` or `02`, response data length `25`;
- optional `0x01` status request for relay expansion blocks, response code `03`;
- telecommands `ResetDevice`, `ResetChannel`, `RelayOn`, `RelayOff`, `RelaySetMask`, `SendFstPacket`.

## XML project

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

## Build

```powershell
dotnet build .\DrvFSTJP.sln -c Release -v minimal
```

The projects reference Rapid SCADA assemblies from `..\DrvDebug_v6\Libraries` to keep this repository self-contained and avoid modifying the `DrvDebug_v6` sample.
