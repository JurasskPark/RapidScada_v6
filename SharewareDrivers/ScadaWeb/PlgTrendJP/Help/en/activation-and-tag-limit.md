# PlgTrendJP — Activation and Tag Limit

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/activation-and-tag-limit.md)

`PlgTrendJP` uses a separate installation-specific license. If no valid license is found, the plugin creates `PlgTrendJP_Activation.bin` without overwriting an existing request.

1. Start SCADA Web without the TrendJP license.
2. Find `PlgTrendJP_Activation.bin` in the host license directory.
3. Send the activation request to the license provider.
4. Save the received file as `PlgTrendJP.bin` in the same directory.
5. Restart the host.

| Host | Activation request | License |
| --- | --- | --- |
| SCADA Web | `ScadaWeb/config/PlgTrendJP_Activation.bin` | `ScadaWeb/config/PlgTrendJP.bin` |
| ScadaAdminWebJP, when used | `ScadaAdminWebJP/License/PlgTrendJP_Activation.bin` | `ScadaAdminWebJP/License/PlgTrendJP.bin` |

The signed license must contain `AppName=PlgTrendJP` and a positive `CountTags`. `CountTags` is the maximum number of unique channel numbers in one trend. Ranges are expanded before counting, duplicates count once, and one channel used in several archive sources remains one licensed channel.
