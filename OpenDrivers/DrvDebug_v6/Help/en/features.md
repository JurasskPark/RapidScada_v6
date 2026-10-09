# DrvDebug — Features

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/features.md)

- **Protocol-neutral byte arrays** - sends and receives raw bytes instead of implementing a fixed protocol
- **Master, Slave and Mixed modes** - supports all Rapid SCADA channel behavior modes
- **Configurable command list** - sends enabled commands in configured order
- **Command payload formats** - sends `Ascii` and `Unicode` as text encodings; other configured command data kinds are sent as HEX bytes by the current runtime
- **Incoming packet stop condition** - detects packet completion by marker or by length field
- **Tag byte decoding** - reads tag data from configured byte offsets and lengths
- **Byte order support** - supports little endian, big endian and mixed byte orders `1032` and `2301`
- **Value scaling** - applies coefficient, offset and precision to decoded numeric values
- **Simulation engine** - supports ramp, sawtooth, sine, square, string list and string template generation
- **Fallback simulation** - `DecodeAndSimulate` tags can simulate a value if decoding fails
- **Telecontrol commands** - supports `SendStr` and `SendBin` command codes for direct output through the current connection
- **Channel prototypes** - generates Rapid SCADA channel prototypes from configured tags
- **Transport logging** - can write sent and received byte dumps to the communication line log
- **Standalone editor host** - includes a WinForms host for opening the configuration UI outside ScadaAdmin
