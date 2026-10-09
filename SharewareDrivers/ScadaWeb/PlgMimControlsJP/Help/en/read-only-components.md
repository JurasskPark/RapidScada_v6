# PlgMimControlsJP — Read-Only Components

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/read-only-components.md)

## ProcessValue

Set an input channel, unit and display template such as `###.###`. In edit mode the template is shown as a width preview. In runtime `1234.5678` becomes `1234.567`, `0.85` becomes `0.850` and `0` becomes `0.000`. Fractional digits are truncated to the template width, trailing fractional zeros are retained and leading integer zeros are not added.

The value and optional unit use the same font size on a transparent background. Missing or bad data is shown as the configured template. `ProcessValue` has no output channel and never sends commands.

## StateIndicator

Configure a caption, rectangular, rounded or circular shape and explicit On and Off groups with input value, text, text color and background color. Good unmatched input uses the configurable Unknown style. Missing or bad data uses the configurable No data style and turns off the state glow.

The editor always previews the On state so the selected colors are visible. `StateIndicator` has no output channel and never sends commands.
