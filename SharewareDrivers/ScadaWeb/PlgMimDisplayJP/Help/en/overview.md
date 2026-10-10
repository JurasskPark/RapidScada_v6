# PlgMimDisplayJP — Overview

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/overview.md)

PlgMimDisplayJP extends standard Mimic diagrams with six electronic display types and the autonomous `DisplayDemo` component. The editor palette and property descriptions are available in English and Russian. It does not require a JP editor.

| Component | Purpose | Interaction |
| --- | --- | --- |
| [SegmentDisplay](segment-display.md) | Seven-segment, text LED or LCD numeric panel | Read-only |
| [DotMatrixDisplay](dot-matrix-display.md) | Numeric 5 × 7 dot matrix with quality indication | Read-only |
| [MechanicalCounter](mechanical-counter.md) | Numeric wheels with fractional accent | Read-only |
| [MarqueeDisplay](marquee-display.md) | Arbitrary channel text and scrolling | Read-only |
| [DataTableDisplay](data-table-display.md) | Merged cells, measurements, text and images | Per-cell actions and chart selection |
| [ValueTextDisplay](value-text-display.md) | Conditional text, colors and images | Standard component click action |
| [DisplayDemo](demo.md) | Local simulation of all six types | Demo inputs only |

This guide follows source version **6.5.0.3**, targeting **Rapid SCADA 6.5 / .NET 10**. An older Webstation 6.3 requires a matching build and separate compatibility verification. Product metadata, Web and View projects agree on the version.

Editing does not require a local component key. Ordinary components need a valid server license for execution. [Licensing](license.md) explains the boundary; screenshots illustrate the local examples rather than proving deployment compatibility.
