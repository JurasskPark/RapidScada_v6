# DrvDDEJP — Tag Configuration

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/tag-configuration.md)

Each tag contains:

- `Enabled` - whether the tag is polled;
- `Name` - tag name and the base for generated Rapid SCADA tag code;
- `Channel` - tag channel value stored in the configuration and used as fallback when a name is empty;
- `Topic` - optional per-tag DDE topic;
- `ItemName` - DDE item name to request;
- `DataFormat` - how the returned DDE text should be converted;
- `DataLength` - buffer/data length, important for string channels.

Tag codes are generated from the tag name by trimming it and replacing spaces with underscores. If the name is empty, the fallback code is built from channel and tag ID.
