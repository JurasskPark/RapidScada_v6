# PlgMimControlsJP — Command Input

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/command-input.md)

`TextCommandInput` has no input channel. Configure an output channel, `Double`, `Text` or `Hex` format, placeholder, optional send button and optional on-screen keyboard button. The send-button caption and keyboard type appear in the property grid only while the corresponding button is enabled.

The numeric keyboard adapts to number or Hex input. The text keyboard provides RUS/ENG layouts, one-shot Shift, digits, space and basic separators. The popup edits a local draft; only Enter validates and sends it. Cancel, Escape or clicking the backdrop closes the keyboard without changing the field. If both buttons are hidden, Enter in the normal input field still sends the value.

Password mode is intentionally not implemented. Use the Web application authentication system for credentials.
