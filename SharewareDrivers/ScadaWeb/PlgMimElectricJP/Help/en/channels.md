# PlgMimElectricJP — Channel bindings

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/channels.md)

The primary state input is `inCnlNum`. Configure an input channel carrying a numeric value and a positive data status. Ordinary electrical components have no built-in command action.

For `ElectricalBreaker` and `ElectricalRcd` only, `outCnlNum` is an **independent read-only trip input**. Despite the inherited property's name, it is not an output command channel. A good zero trip value means normal; a good nonzero value means trip. If a configured trip input has no valid data, the combined state is unknown unless a separate alarm binding overrides it.

Additional roles are configured through `propertyBindings`:

| Property | Role |
| --- | --- |
| `feedbackValue` | State feedback when no primary input is configured |
| `alarmValue` | A finite nonzero bound value overrides the ordinary state with an alarm |
| `measurementValue` | Analog fallback feedback and displayed measurement |
| `commandValue` | Stored bindable metadata; never sends a command |

A configured primary channel takes precedence over additional feedback, even when that channel currently has bad or missing data. Without a primary input, a configured feedback binding is used; analog types can fall back to their measurement binding. The trip state has the highest priority; an alarm does not override a confirmed trip. Otherwise a bound alarm can override the combined state.

Analog display prefers a valid bound `measurementValue`. Otherwise it uses the primary channel's formatted `dispVal`, then its numeric value. Missing or invalid data is shown as `?`.

Bindings are persisted and channel subscriptions are established by the host before client updates. Editing a state profile does not clear existing bindings. The generic click-action, command, rights, blinking and appearance settings are hidden by the descriptor, and the factory disables built-in command behavior.

See [state mappings](states.md) and the [breaker and measurement examples](examples.md).
