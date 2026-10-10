# PlgMimSVGAnimationJP — Editor simulator

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/simulation.md)

The designer's test mode uses local synthetic values without a local component license. It is part of the editor; there is no separate autonomous demo component.

Used data roles are grouped by assigned input channel. Roles sharing a positive channel number share one sample; unassigned roles remain independent. You can change each sample's value and quality.

Test these cases:

- Below, exactly at and above each threshold.
- Interval boundaries, overlapping variants and unknown higher-priority conditions.
- Hysteresis entry/exit and sustained values through both delays.
- Range endpoints and values outside the range.
- Loss and restoration of data for one detail while unrelated details continue.
- Command/menu availability and separate feedback roles.

Clicking an action records it in the simulator log without sending SCADA commands, opening host charts or navigating. A simulated success does not establish a runtime license or actual host permissions.

Test-mode changes are not persisted as real input values. Stop testing to edit base geometry, then apply the designer and save the document.

The source-only local example server can be started from `Plugins/Mimics/PlgMimSVGAnimationJP`:

~~~powershell
node Scripts/ServeExamples.mjs
~~~

It serves the examples on port `11140` through the same drawing engine with synthetic samples. This is a development fixture, not a deployed SCADA viewer.

[HelloWorld](examples.md) · [Quality](quality.md) · [License](license.md)
