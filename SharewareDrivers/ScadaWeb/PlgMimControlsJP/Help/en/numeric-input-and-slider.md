# PlgMimControlsJP — Numeric Input and Slider

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/numeric-input-and-slider.md)

## NumericUpDown

Configure minimum, maximum, step, negative-value permission, decimal places and one of three button layouts:

| Layout | Behavior |
| --- | --- |
| `Native` | Browser up/down arrows on the right |
| `Sides` | Large minus button on the left and plus on the right |
| `Stacked` | Plus above the field and minus below it |

`Stacked` automatically raises the component height to at least 84 pixels. Typed input is sent only by Enter. A step button sends exactly one step immediately. Escape restores the last confirmed input value. Text, exponential notation, an out-of-range number, excessive decimal places and values not aligned with the configured step are rejected.

## DiscreteSlider

Set orientation, minimum, maximum and division count. For `0…100` with 10 divisions the selectable values are `0, 10, 20, …, 100`. Dragging, clicking the track or using arrow keys selects only exact divisions and sends each new division once.

During interaction the selected value is shown near the thumb. After release, the slider returns to the confirmed input-channel position until feedback arrives. Missing or bad input data is displayed as `#.#` but does not block selection. Changing orientation exchanges width and height when the current aspect ratio belongs to the previous orientation.

Command divisions do not round the actual feedback value. For `35…85` with 10 divisions, commands are `35, 40, …, 85`, while feedback such as `43.9` is displayed as `43.9`. An output channel of zero makes the slider an indicator; a missing input is different from a missing command output.

## SetpointControl

`SetpointControl` keeps three channels separate: `InCnlNum` supplies the read-only process value (PV), `SetpointInCnlNum` supplies the accepted setpoint (SP), and `OutCnlNum` receives a Double command. It supports `Inline` (`390 × 36`), `Stacked` (`236 × 64`) and `Popup` (`240 × 32`) starting layouts; dimensions remain editable. Set the numeric range, step, precision, unit and captions for the process.

Editing changes only a local draft. Apply or Enter sends it explicitly; live polling preserves the draft. Only matching good SP feedback confirms the command. PV changes and transport acknowledgement do not replace accepted SP. Failed or timed-out requests retain the draft for an explicit retry; pending requests prevent duplicate sending.
