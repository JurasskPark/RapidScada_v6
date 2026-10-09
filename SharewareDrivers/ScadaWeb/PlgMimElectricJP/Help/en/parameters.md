# PlgMimElectricJP — Parameter reference

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/parameters.md)

These properties belong to ordinary electrical symbols. Generic identity, location, visibility and tooltip properties are supplied by the Mimic host.

| Property | Meaning and default |
| --- | --- |
| `inCnlNum` | State input; zero means unconfigured. Hidden for static types |
| `outCnlNum` | Read-only trip input for breaker/RCD only; zero means unconfigured |
| `propertyBindings` | Additional feedback, alarm, measurement and stored command-value bindings |
| `rotation` | Deg0, Deg90, Deg180, Deg270; default Deg0 |
| `nominalVoltage` | Engineering voltage text; default depends on the type, otherwise empty |
| `currentSystem` | Unspecified, Ac, Dc; initial value depends on the type |
| `phaseCount / poleCount` | Integer counts; default 0 means unspecified; do not redraw poles |
| `signalType` | Signal enumeration below; initial value depends on the type |
| `cableType` | Cable enumeration below; initial value depends on the type |
| `referenceDesignation` | Equipment designation, e.g. QF1; default empty; used in the accessible name |
| `address` | Engineering address text; default empty; not a protocol driver address |
| `symbolProfile` | Inherit, GostIndustrial, Iec, ProjectCustom; default Inherit |
| `standardReference` | Editable note, initialized for the type and default drawing profile |
| `customSymbolUrl` | Relative or same-origin root URL; default empty; used for ProjectCustom |
| `feedbackValue / alarmValue / measurementValue / commandValue` | Bindable numeric properties; default 0; roles described in channel help |
| `positionEncoding` | Legacy or Iec61850; visible only for position types; new types use Iec61850 |
| `stateSchemaVersion` | Hidden persisted migration version; current value 3 |
| `stateProfileEnabled` | Hidden persisted state flag; enabled for new channel-driven types |
| `electricalSymbolProfile` | Document-level profile, not a component property; default GostIndustrial |

## Signal values

| Value | Meaning |
| --- | --- |
| `Unspecified` | Unspecified |
| `DryContact` | Dry contact |
| `V24Dc` | 24 V DC |
| `Milliamp4To20` | 4–20 mA |
| `Volt0To10` | 0–10 V |
| `Rtd` | Resistance temperature detector |
| `Thermocouple` | Thermocouple |
| `Pulse` | Pulse |
| `Ethernet` | Ethernet |
| `Rs485` | RS-485 |
| `FireLoop` | Fire loop |
| `Custom` | Project-specific signal |

## Cable values

| Value | Meaning |
| --- | --- |
| `Unspecified` | Unspecified |
| `Power` | Power |
| `Control` | Control |
| `TwistedPair` | Twisted pair |
| `Shielded` | Shielded |
| `FiberOptic` | Fiber-optic |
| `Coaxial` | Coaxial |
| `FireResistant` | Fire-resistant |
| `Custom` | Project-specific cable |

Engineering fields are persisted hints for drawing and connection validation, not automatic circuit sizing. In particular, phase/pole counts do not create extra geometry. Channel properties and additional roles are described in [bindings](channels.md); numeric encodings are in [states](states.md).

The five static types ignore live state inputs. Breaker/RCD alone expose the inherited trip property. State-profile activation and migration fields are normally managed by the factory rather than edited manually.

`ElectricalDemo` has a separate restricted shell: identity, location, size, enabled, visible and tooltip. Its internal channels, scripts and bindings are unavailable; see [demonstration](demo.md).
