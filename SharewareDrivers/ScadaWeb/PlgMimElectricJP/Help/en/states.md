# PlgMimElectricJP — State reference and migration

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/states.md)

There are 96 channel-driven types and five static types: `ElectricalEarthPE`, `ElectricalEarthFunctional`, `ElectricalCurrentTransformer`, `ElectricalVoltageTransformer` and `ElectricalTerminalBlock`.

For a channel-driven type, good data means a present, finite numeric value and a finite numeric status greater than zero. Numeric strings, missing values and nonpositive status are not good data. They produce `Unknown` rather than a normal zero state.

| State profile / encoding | Mapping for good data |
| --- | --- |
| `Energy` | 0 → `Deenergized`; other → `Energized` |
| `IecDoublePoint / Iec61850` | 0 → `Intermediate`; 1 → `Open`; 2 → `Closed`; other → `Unknown` |
| `IecDoublePoint / Legacy` | 0 → `Open`; 1 → `Closed`; −1 or 2 → `Trip`; 3 → `Intermediate`; other → `Unknown` |
| `Protection` | 0 → `Normal`; other → `Alarm` |
| `Operating` | 0 → `Off`; 1 → `On`; 2 → `Intermediate`; 3 → `Alarm`; other → `Unknown` |
| `Binary` | 0 → `Off`; other → `On` |
| `Contact` | 0 → `Normal`; other → `Actuated` |
| `Selector2` | 0 → `Position0`; 1 → `Position1`; other → `Unknown` |
| `Selector3` | 0 / 1 / 2 → `Position0` / `Position1` / `Position2`; other → `Unknown` |
| `Lamp` | 0 → `Off`; 1 → `On`; −1 or 2 → `Alarm`; other → `Unknown` |
| `Analog` | Good numeric data → `Normal` and a displayed value |
| `None` | Always `Preview`; no channel-driven state |

The identifiers above are persisted state contracts; the interface displays localized names. A document editor uses `Preview` irrespective of live data. The renderer exposes the designation, localized state and analog value as an accessible name.

## Double-point position and trip

New position components use `positionEncoding = Iec61850`. In this encoding, value 3 is unknown; it does not mean trip. Breaker/RCD trip comes from the separate [trip input](channels.md). An unconfigured trip input adds no override. A configured bad trip input yields `Unknown`; a confirmed nonzero trip wins over the position and alarm.

## Saved mimics

The current `stateSchemaVersion` is 3. Components saved without a position schema retain `Legacy` mapping. Older passive symbols keep a conservative `stateProfileEnabled` default; new channel-driven symbols enable the state profile.

The migration preserves channel numbers and existing property bindings. `runtimeState`, `runtimePrimaryState`, `runtimeTripState` and `runtimeValue` are transient and are not serialized into `.mim`. Before switching an old breaker to `Iec61850`, change the source channel's encoding accordingly and verify each position.
