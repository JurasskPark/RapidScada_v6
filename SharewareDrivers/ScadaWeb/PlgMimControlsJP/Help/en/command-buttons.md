# PlgMimControlsJP — Command Buttons

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/command-buttons.md)

## LatchedButton

`LatchedButton` is a two-state button. Configure the input values, visible texts and commands for transitions into On and Off states, then choose one shared command format.

| Confirmed input | Visible state | Next click |
| --- | --- | --- |
| On value, default `1` | On text | Sends the configured Off command |
| Off value, default `0` | Off text | Sends the configured On command |
| Missing, bad or other value | No data or unknown | Sends the configured On command |

The accessible `aria-pressed` state follows confirmed feedback. The button does not add a separate visible accessibility caption.

## IlluminatedButton

`IlluminatedButton` sends the same configured command on every click. Its input channel controls only the visible text and color. Configure rectangular, rounded or circular shape, command format and command, plus explicit On and Off values, texts and colors. Good but unmatched input uses the unknown appearance; missing or bad data uses the no-data appearance.

Use `LatchedButton` instead when On and Off require different commands.

## MomentaryButton

`MomentaryButton` has separate `PressOutCnlNum` / `ReleaseOutCnlNum`, command formats and payloads. Holding the pointer, Enter or Space sends the press command once; release, cancellation, focus loss or the finite `MaxHoldTime` sends the release command. A typed `ControlStateOption` dictionary can display independent equipment feedback; the locally pressed button is not confirmation of movement.

A browser disconnect or closure cannot guarantee delivery of a release command. Configure an equipment-side watchdog or server control timeout for held actions.

## OneShotButton

`OneShotButton` sends one configured command only when good feedback matches `ReadyValue` of `ReadyValueType`. `ReadyInCnlNum=0` uses the common input channel. After sending, the button stays locked until good feedback has left the ready value and returned to it. The caption and state dictionary remain separate from this handshake.

Server acceptance and `HandshakeTimeout` do not count as PLC completion or unlock a successfully sent cycle. The timeout reports a diagnostic. An explicit send rejection can restore readiness before PLC busy has been observed.

## MechanismPanel

`MechanismPanel` opens an adjacent state and command popup through a visible Button or a Hotspot. The hotspot has an editor frame and is transparent in runtime. Configure the title, typed state dictionary and `OperatorCommand` list; each command can use its own output channel, Double/Text/Hex payload, colors, image and optional typed permit channel. Opening or closing the panel never sends commands.

`PopupWidth` and `PopupHeight` are independent of the trigger size. Zero enables automatic sizing on that axis; positive values request fixed dimensions within viewport limits, with wrapping and scrolling for long content. State text and colors follow the first matching dictionary row; missing and unmatched input have separate appearances.
