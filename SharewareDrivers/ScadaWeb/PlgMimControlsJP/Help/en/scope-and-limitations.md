# PlgMimControlsJP — Scope and Limitations

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/scope-and-limitations.md)

- ordinary component confirmed state comes from input feedback, not from an optimistic local write;
- `BitCheckList` requires a valid source mask before the first command;
- `TextCommandInput` has no password mode;
- `ValueForm` does not replace or modify `PlgMimMultiSet`;
- `ProcessValue` and `StateIndicator` are read-only and never send commands;
- `OneShotButton` requires good ready feedback and a completed ready-state cycle;
- the five-position limit applies only to the rotary `ModeSelector`, not to its Button layout;
- `ControlsDemo` uses simulated values and never controls real equipment;
- the ordinary component runtime license is installed on the server; authoring and the editor watermark license are separate;
- command controls use the standard Mimic `mainApi` and require no custom backend endpoint;
- the plugin has no dependency on `PlgMimicJP` or `MimicEditorJP`;
- changing a CSS theme is an administrator file operation, not a runtime user setting.
