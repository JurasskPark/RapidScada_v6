# DrvFSTJP — Overview

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/overview.md)

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
