# PlgMimControlsJP — Multi-Value Form

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/multi-value-form.md)

`ValueForm` appears on the mimic as a configurable open button. Its modal window contains the form title, row list, common Apply button and Close button. Each `ValueFormRow` has a name, input channel, output channel and one of six editors:

| Editor | Row settings |
| --- | --- |
| `TextBox` | Command format and placeholder |
| `CheckBox` | Fixed `0 / 1` values |
| `NumericUpDown` | Minimum, maximum, step, negatives and precision |
| `ComboBox` | `ValueOption` list |
| `RadioButtonGroup` | Options and orientation |
| `BitCheckList` | Bits and orientation |

The form has four runtime columns: row name, read-only current value, new value and per-row result. Editing a row sends nothing. The common Apply button sends only changed and valid rows; the form remains open and reports success or failure independently for every row. Close never sends commands. If unsent changes exist, Close or Escape requests confirmation.

The plugin automatically creates hidden standard bindings for all unique row channels. A `BitCheckList` row remains disabled until it has a valid source mask, and hidden bits are preserved from the latest received value. `ValueForm` is independent from `PlgMimMultiSet` and does not replace or modify it.
