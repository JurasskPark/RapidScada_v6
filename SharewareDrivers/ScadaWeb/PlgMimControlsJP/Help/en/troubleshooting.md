# PlgMimControlsJP — Troubleshooting

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/troubleshooting.md)

| Symptom | Cause and action |
| --- | --- |
| The **CONTROLS** group is missing in the editor | Check plugin registration, matching DLLs, dictionaries and static assets, then restart the host. The component runtime license does not hide the authoring palette. |
| Runtime controls show license warnings | Check the server file `PlgMimControlsJP_License.bin`, product `AppName`, UID, validity and licensing dependencies; restart SCADA Web after installing the key. |
| `PlgMimControlsJP_Activation.bin` is not created | Verify that `LicenseJP.Logic.dll` and its packaged dependencies are installed beside the plugin runtime and that the host can write to its license directory. |
| The editor has fewer than 19 ordinary types | Check that the DLL, dictionaries and browser assets belong to the same current package. `ControlsDemo` is additional and may be omitted from a licensed palette. |
| A component displays data but does not send a command | Commands are disabled in the editor. In runtime check `Enabled`, operator control rights, `OutCnlNum` and output-channel permissions. |
| The command was accepted but the visible state did not change | This is expected until the device writes the result to the input feedback channel. Check `InCnlNum` and device feedback. |
| The pending frame is not visible | Check `Show pending frame` and its color; defaults depend on the component. The frame appears only while a command is pending. |
| `BitCheckList` is disabled | The component has not received a good non-negative integer source mask. Check the input channel and its quality. |
| A selection is empty, a checkbox is indeterminate or the slider shows `#.#` | The input channel is zero, missing, bad quality or contains an unsupported value. |
| A numeric value is rejected | Check minimum, maximum, negative-value permission, decimal places and exact step alignment. Exponential notation is not accepted. |
| A text or Hex command is rejected | Check the selected command format. Hex requires pairs of valid hexadecimal digits and no `0x` prefix. |
| A `ValueForm` row is not sent | Apply sends only changed and valid rows. Check the row output channel, validation result and control rights. |
| `OneShotButton` remains locked | Verify a good ready → not ready → ready feedback cycle. Server acknowledgement and a timeout do not confirm PLC completion. |
| `SetpointControl` keeps the old accepted SP | Check `SetpointInCnlNum`; only its matching good feedback confirms the command. PV and transport acknowledgement are separate. |
| The classic Administrator reports an assembly load error | Install the matching packaged `PlgMimControlsJP.View.dll`. It is built against `ScadaWebCommon.Subset` for classic Administrator compatibility. |
| New files are installed but the old appearance remains | Restart the application or IIS site and perform a hard browser refresh. |
