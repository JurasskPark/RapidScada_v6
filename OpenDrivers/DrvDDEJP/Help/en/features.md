# DrvDDEJP — Features

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/features.md)

- **DDE client mode** - reads values from applications that expose a DDE service
- **Multiple topics** - keeps separate DDE client connections per topic and reuses them between requests
- **Default topic** - tag-specific topic can be empty, in this case the project default topic is used
- **Per-tag item mapping** - each Rapid SCADA tag maps to an individual DDE item name
- **Tag ordering** - enabled tags are polled in configured order
- **Data formats** - supports `Bool`, signed and unsigned integer formats, `Float`, `Double`, `Ascii`, `Unicode` and `HexString`
- **String tags** - ASCII, Unicode and HexString formats are registered as string device tags
- **Automatic reconnect** - failed topics are disconnected and reconnected on the next request
- **Error throttling** - repeated topic errors are logged no more often than `ReconnectDelay`
- **Configurable request timeout** - DDE request timeout is configured in milliseconds
- **Channel prototype generation** - ScadaAdmin view creates Rapid SCADA channel prototypes from configured tags
- **Localization** - English and Russian language files are included
- **Sample DDE server** - the repository includes a `SampleServer` utility for DDE testing
