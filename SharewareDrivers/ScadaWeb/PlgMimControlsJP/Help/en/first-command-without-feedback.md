# PlgMimControlsJP — First Command Without Feedback

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/first-command-without-feedback.md)

Missing input data is shown honestly and normally does not prevent an explicit operator command:

| Component | Available action |
| --- | --- |
| `ComboBox`, `RadioButtonGroup` | Select a configured value |
| `CheckBox`, `SquareToggle` | First click sends `1` |
| `LatchedButton` | First click sends the configured On command |
| `NumericUpDown` | Enter a number or step from minimum |
| `DiscreteSlider` | Select any configured division |
| `IlluminatedButton`, `TextCommandInput` | Send the explicitly configured command |
| `ValueForm` | Edit and apply ordinary rows |
| `SetpointControl` | Edit and explicitly apply a valid setpoint |
| `ModeSelector`, `SearchableComboBox` | Select an available option, subject to configured permits |
| `MomentaryButton`, `MechanismPanel` | Send configured commands subject to channel rights and permits |
| `OneShotButton` | Blocked until good ready-state feedback arrives |
| `BitCheckList` | Blocked until the first valid source mask |
