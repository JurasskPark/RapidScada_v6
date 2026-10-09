# PlgMimElectricJP — Configuration examples

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/examples.md)

The channel numbers below are illustrative. Create or select actual input channels in your project; each test value must have a positive valid data status.

## Breaker position with independent trip

Place `ElectricalBreaker` and set:

| Property | Value |
| --- | --- |
| `referenceDesignation` | `QF1` |
| `inCnlNum` | `1001` |
| `positionEncoding` | `Iec61850` |
| `outCnlNum` | `1002` |

Channel 1001: 0 is intermediate, 1 is open, 2 is closed and 3 is unknown. Channel 1002: 0 means no trip; any valid nonzero value means trip. For example, position 2 with trip 1 displays `Trip`. No SCADA command is sent through 1002.

## Analog measurement

Place `ElectricalPressureTransmitter`, set `referenceDesignation = PT1`, `inCnlNum = 1010` and `signalType = Milliamp4To20`. The component uses the channel value and its display formatting. Set the channel's engineering conversion and units in SCADA; choosing `Milliamp4To20` is metadata and does not scale the measurement.

To display a different calculated measurement, bind `measurementValue` through the host's property binding editor. With good data, this value takes precedence in the displayed number. A missing value produces `?` rather than a fabricated zero.

## Project symbol

Place `ElectricalCustomDevice`, set `symbolProfile = ProjectCustom` and `customSymbolUrl = /plugins/MySymbols/images/pump.svg`. Install that SVG at the corresponding path on the same site. Keep the type's 100 × 50 px tile and declared ports. Confirm loading and rotation in the viewer.

## Optional fire package

Use the XML in [optional groups](configuration.md), then place `ElectricalSmokeDetector`. Its `Protection` state profile maps good 0 to `Normal` and a good nonzero value to `Alarm`. Package visibility and runtime activation are independent settings.
