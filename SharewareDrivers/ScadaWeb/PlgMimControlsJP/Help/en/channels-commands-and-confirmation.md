# PlgMimControlsJP — Channels, Commands and Confirmation

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/channels-commands-and-confirmation.md)

Most interactive controls use an input channel for confirmed feedback and an output channel for commands. A command is available only in licensed runtime when the component is enabled, the operator has control rights, the resolved output channel number is greater than zero and the Webstation command API is available. Ready-state feedback and configured permit channels impose additional component-specific conditions.

The following command formats are available where the component exposes `CommandFormat`:

| Format | Example | Sent value |
| --- | --- | --- |
| `Double` | `12.5` or `12,5` | Numeric command `12.5` |
| `Text` | `START` or `ПУСК` | UTF-8 text |
| `Hex` | `00 AF 10` | Exact bytes `00AF10` |

Hex separators may be spaces, commas, semicolons, colons or hyphens. Every byte must contain exactly two hexadecimal digits; do not use the `0x` prefix.

## Pending Frame

Command controls have an optional `Show pending frame` property. Basic controls default to off; `MomentaryButton`, `OneShotButton`, `MechanismPanel`, `ModeSelector` and `SearchableComboBox` start with it enabled. Existing saved settings are retained. When enabled, `Pending frame color` appears; its default is amber `#D97706`.

For basic controls with an input channel, pending state ends when the expected good input value arrives, command sending is rejected, or the ten-second safety timeout expires. Output-only fields clear the pending state after the server acknowledges the command. The frame never replaces the actual channel state.

This basic ten-second rule is not the completion contract for every type. `SetpointControl`, `ModeSelector` and `SearchableComboBox` have configurable feedback timeouts; `OneShotButton` keeps its PLC handshake lock even if its waiting frame times out. See the corresponding component descriptions below.
