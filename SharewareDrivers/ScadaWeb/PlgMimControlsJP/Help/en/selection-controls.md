# PlgMimControlsJP — Selection Controls

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/selection-controls.md)

## ComboBox

Configure `InCnlNum`, `OutCnlNum` and the `Options` list. Each option contains visible text and a numeric value. The input value selects the matching item; choosing an item sends its value once. If the input is missing, bad or not present in the list, the field is empty and no configured option is selected. Numeric `-1` is not reserved and may be used normally.

## ModeSelector

`ModeSelector` has two layouts: `Button` opens a menu for direct selection, while `Rotary` shows a rotary switch with two to five positions. More than five options automatically select the Button layout without losing rows. A rotary drag previews the requested position locally and sends only the final position on release; crossing intermediate positions and cancelling the drag send nothing.

Each `ModeSelectorOption` can have its own input, output and permit channels, numeric or text feedback, and a Double, Text or Hex command. Zero row input/output channels use the component's common channels. Confirmed position follows good matching feedback; server acknowledgement alone does not confirm a mode. Configure `FeedbackTimeout` and the optional pending frame for command feedback.

## SearchableComboBox

`SearchableComboBox` provides `Dropdown` and `ListBox` layouts. Configure the `SearchableSelectionOption` list with captions, typed feedback, commands and optional permits; row channels may differ from the common component channels. Search filters captions by a case-insensitive substring and preserves the configured order. Typing or clearing the search and opening the dropdown send no commands.

Clicking a different available row or explicitly pressing Enter sends one configured command. The previous confirmed selection stays visible until matching good feedback arrives. Filtering out the current row does not replace its confirmed value. Configure placeholders, popup dimensions and `FeedbackTimeout`; neither layout limits the number of rows.

## RadioButtonGroup

`RadioButtonGroup` uses the same `ValueOption` list as `ComboBox` and supports horizontal or vertical orientation. No button is selected for unknown or bad input data. Increase the component width for long captions in horizontal mode.

## CheckBox

`CheckBox` has a configurable caption and fixed values: `1` means checked and `0` means unchecked. Any other value, missing data or bad quality produces an indeterminate state. A click sends the opposite binary value. Before the first valid input, the first click sends `1`.

## SquareToggle

`SquareToggle` is a compact switch with a deliberately square track and thumb. A positive input value places the thumb on the right; zero or a negative value places it on the left. Missing or bad data hides the thumb. A click sends `0` from a confirmed active state and `1` otherwise, then waits for feedback.

## BitCheckList

Each `BitOption` contains a one-based bit number and caption. Bit 1 corresponds to mask `1`, bit 2 to `2`, bit 8 to `128` and bit 9 to `256`. The component edits only displayed bits and preserves every unlisted bit from the latest good input mask.

`BitCheckList` requires a valid non-negative integer input value before sending a command. This prevents accidental loss of hidden bits. The list may be vertical or horizontal; horizontal mode uses scrolling when the component is too narrow.
