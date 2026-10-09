# PlgMimControlsJP — Component Catalog

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/component-catalog.md)

The following nineteen types are ordinary components. They are available for authoring without a local component license; their runtime execution is licensed.

| Type name | English toolbox name | Default size | Purpose |
| --- | --- | --- | --- |
| [`BitCheckList`](selection-controls.md) | Bit check list | `170 × 110` | Edit selected bits without losing hidden bits |
| [`CheckBox`](selection-controls.md) | Check box | `140 × 32` | Binary `0 / 1` command |
| [`ComboBox`](selection-controls.md) | Combo box | `160 × 34` | Select one configured numeric value |
| [`DiscreteSlider`](numeric-input-and-slider.md) | Discrete slider | `280 × 82` | Select an exact configured division |
| [`IlluminatedButton`](command-buttons.md) | Illuminated button | `140 × 64` | Send one fixed command and show feedback |
| [`LatchedButton`](command-buttons.md) | Latched button | `130 × 42` | Two-state command button |
| [`MechanismPanel`](command-buttons.md) | Mechanism panel | `130 × 32` | Button or hotspot opening a state and command panel |
| [`ModeSelector`](selection-controls.md) | Mode selector | `220 × 160` | Direct mode selection; default three-position rotary layout |
| [`MomentaryButton`](command-buttons.md) | Hold button | `112 × 32` | Separate commands on press and release |
| [`NumericUpDown`](numeric-input-and-slider.md) | Numeric input | `140 × 36` | Validated number and step commands |
| [`OneShotButton`](command-buttons.md) | One-shot button | `112 × 32` | One command followed by a PLC ready-state cycle |
| [`ProcessValue`](read-only-components.md) | Process value | `160 × 42` | Read-only formatted process value |
| [`RadioButtonGroup`](selection-controls.md) | Radio button group | `160 × 64` | Visible selection of one configured value |
| [`SearchableComboBox`](selection-controls.md) | Searchable selection | `240 × 32` | Searchable Dropdown or ListBox; ListBox starts at `240 × 110` |
| [`SetpointControl`](numeric-input-and-slider.md) | Setpoint control | `390 × 36` | Separate PV, accepted SP and explicit setpoint entry; default Inline |
| [`SquareToggle`](selection-controls.md) | Square toggle | `60 × 30` | Compact square binary switch |
| [`StateIndicator`](read-only-components.md) | State indicator | `140 × 64` | Read-only state lamp |
| [`TextCommandInput`](command-input.md) | Command input | `250 × 36` | Number, UTF-8 or Hex command entry |
| [`ValueForm`](multi-value-form.md) | Value input form | `210 × 44` | Multi-row modal value entry |

The separate demo type is available without a component runtime license. In the standard authoring mode it is offered alongside the ordinary controls, independently of the server license. Saved demos work in both licensed and unlicensed runtime.

| Type name | English toolbox name | Default size | Purpose |
| --- | --- | --- | --- |
| [`ControlsDemo`](autonomous-demo.md) | Demonstration | `920 × 720` | Autonomous simulated controls, no SCADA channels or real commands |
