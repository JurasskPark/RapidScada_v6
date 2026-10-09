# PlgMimTankJP — Data Quality

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/data-quality.md)

A channel value is considered good only when data exists, its SCADA status is positive and the value is a finite number.

If one full-indicator layer has bad or missing data:

- good and static layers remain visible;
- the affected layer contributes zero and is shown as `#.#` where a value caption exists;
- the total is marked as partial;
- calculated alarms become unknown;
- good external alarm channels remain authoritative and continue to work.

Runtime values and quality fields are transient and are not saved to the `.mim` file.
