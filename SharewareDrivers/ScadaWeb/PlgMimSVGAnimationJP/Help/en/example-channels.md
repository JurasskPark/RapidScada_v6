# PlgMimSVGAnimationJP — HelloWorld channel catalog

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/example-channels.md)

The names and codes below are preserved from the source `Cnl.xml`; they are identifiers in that sample configuration.

| Channel | Source name | Code | Type | State |
| --- | --- | --- | --- | --- |
| 101 | Simulator - Sine | `Sin` | Input | Enabled |
| 102 | Simulator - Square | `Sqr` | Input | Enabled |
| 103 | Simulator - Triangle | `Tri` | Input | Enabled |
| 104 | Simulator - Relay State | `DO` | Output / feedback | Enabled |
| 105 | Simulator - Analog Output | `AO` | Output / feedback | Enabled |
| 106 | Simulator - Array | `RA` | Input | Enabled |
| 107 | Sine | `Sin` | Input | Enabled |
| 108 | Square | `Sqr` | Input | Enabled |
| 109 | Triangle | `Tri` | Input | Enabled |
| 110 | Relay State | `DO` | Output / feedback | Enabled |
| 111 | Analog Output | `AO` | Output / feedback | Enabled |
| 112 | Array | `RA` | Input | Enabled |
| 200 | Sine | `Sin` | Input | Enabled |
| 201 | Square | `Sqr` | Input | Enabled |
| 202 | Triangle | `Tri` | Input | Enabled |
| 203 | Relay State | `DO` | Output / feedback | Enabled |
| 204 | Analog Output | `AO` | Output / feedback | Enabled |
| 205 | Array | `RA` | Input | Enabled |
| 300 | Sine Web Test | `SinWeb` | Input | Disabled |
| 301 | Triangle | `Tri` | Input | Enabled |
| 302 | Relay State | `DO` | Output / feedback | Enabled |
| 303 | Analog Output | `AO` | Output / feedback | Enabled |
| 304 | Array | `RA` | Input | Enabled |

`RA` values are arrays and are not used by the scalar rules. Readable output feedback and command targets are separate roles. The prepared command examples use `110` / `111` rather than duplicate-tag outputs `104` / `105`.

[Examples](examples.md) · [Channel binding](channels.md)
