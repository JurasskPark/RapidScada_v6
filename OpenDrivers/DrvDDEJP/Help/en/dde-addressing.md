# DrvDDEJP — DDE Addressing

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/dde-addressing.md)

A DDE request is built from three fields:

```text
ServiceName|Topic!ItemName
```

`ServiceName` is configured once for the device. `Topic` can be set for each tag; if it is empty, `DefaultTopic` is used. `ItemName` is required for every tag.

Example:

```text
Excel|Sheet1!R1C1
```
