# DrvMOXANportJP — Firmware Support

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/firmware-support.md)

The driver checks firmware images before upload. Supported NPort signatures include:

- modern NPort firmware headers matching `NP[digits]K`, for example `NP6K`;
- legacy NPort firmware headers such as `*FRM`, where the model family can be detected from the file content or name.

The upload itself is performed by the vendor DSCI library.
