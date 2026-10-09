# DrvDebug — Stop Condition

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/stop-condition.md)

The stop condition defines when a packet is considered complete. It is used while reading responses in master mode and incoming requests in slave/mixed mode.

Supported modes:

- marker mode: read until the configured marker value is found at the configured check address;
- length mode: read the length field and use it to determine the expected full packet size.

Main settings:

| Setting | Description |
| --- | --- |
| `StopConditionCheckAddress` | Byte offset used for marker or length check |
| `StopConditionCheckLength` | Number of bytes to check |
| `StopConditionCheckFormat` | Value type: `Byte`, `UInt16`, `UInt32`, `UInt64` |
| `StopConditionCheckValueText` | Marker value, decimal or `0x` hex |
| `StopConditionLengthMode` | Enables length-field mode |
| `StopConditionLengthIncludesItself` | Indicates that the length field already includes the full packet length |
