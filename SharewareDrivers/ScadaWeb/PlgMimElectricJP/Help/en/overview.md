# PlgMimElectricJP — Overview and requirements

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/overview.md)

PlgMimElectricJP adds electrical supply, relay-contactor control, instrumentation, PLC/I/O, cabling and low-current symbols to Rapid SCADA mimic diagrams. The catalog has 101 ordinary component types in nine groups, plus the autonomous `ElectricalDemo` demonstration.

The documented source version is **6.5.0.3**. The Web plugin and Classic Administrator View target **.NET 10** and the Rapid SCADA **6.5** branch. Use the matching `PlgMimic` host and `PlgMimic.Common.dll`. Compatibility with Webstation 6.3 requires a separate matching build and verification.

The Web project uses a platform-neutral .NET target. The Classic Administrator View runs with the Windows Administrator. Deployment still requires the dependencies supplied with the selected package; the target framework alone does not prove an installed host is compatible.

Ordinary components are indicators with channel-driven states and engineering metadata. Their built-in behavior does not send SCADA commands or calculate an electrical circuit. Editing, copying and saving are available without a local component license; server execution requires a valid product license. The demonstration works independently on simulated data.

The interface dictionaries are English and Russian. The demo starts with English content independently of the host language.

See [installation](installation.md), [activation](activation.md), [component groups](components.md), [states](states.md) and [source boundaries](sources.md).
