# PlgMimElectricJP — Autonomous demonstration

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/demo.md)

Place the `ElectricalDemo` component from the electrical demonstration group. It is one fixed demonstration, default size 1000 × 1200 px, with two connected examples. It remains available without a product runtime license.

The editor shows a static English preview. The running demo starts with English content independently of the host language; the toolbox group uses the host dictionary. English and Russian demo strings are generated from the project's demo localization materials.

## Direct-on-line motor starter

The first example contains QF1, KM1, FR1 and M1. The 24 V control chain has normally closed Stop and overload contacts and parallel normally open Start and holding contacts.

- QF1 must be closed to provide the simulated control supply.
- Start and Stop produce local 700 ms pulses.
- Energized KM1 seals itself in. Stop, an FR1 trip or opening QF1 drops the coil.
- Resetting FR1 or closing QF1 again does not restart the motor without Start.
- The running-current display is an illustrative 6.2 A, not a motor calculation.

## Open-transition source selector

The second example uses main source A, standby source B and interlocked K1/K2.

- Automatic mode prefers healthy source A.
- Switching sources opens both contactors for a 1 s neutral interval, then closes only an available target.
- Manual A/B does not close a contactor for an unavailable source; Off leaves both open.
- A target change during transfer restarts the neutral interval.

The automatic demonstration sequence repeats. A local action pauses the automatic sequence for that example only; the circuit logic, including transfer timing, continues. Restarting the automatic demonstration resets both examples.

Red conductors show simulated voltage, not current direction. Both branches of a supplied common bus may be red even when one source contactor is open. Dashed interlocks are logical links, not wires; text and color distinguish states without requiring animation.

## Isolation and lifetime

The scene uses private SVG slots and per-instance temporary state. It contains no ordinary licensed child components. SCADA channels, command APIs, user scripts and internal circuit settings are not persisted. Legacy bindings and scripts supplied in a saved demo are discarded by its factory.

Only the demo shell is saved. Each instance is independent. Timers are released in edit mode, when the demo or an ancestor is disabled/hidden, or when the component is removed. This is a demonstration of behavior, not an editable circuit simulator or connection to real equipment.
