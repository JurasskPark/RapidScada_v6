# DrvDbImportPlus — InfluxDB Notes

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/influxdb-notes.md)

InfluxDB support is implemented through the HTTP `/query` API. The driver builds a connection string from UI fields if a custom connection string is not specified:

- `Server` and `Port` become `Url`, default port is `8086`;
- `Password` is used as the bearer token;
- `Database` is used as `Bucket` for InfluxDB 2.x;
- `Database` is used as `Database` for InfluxDB 3.x;
- `OptionalOptions` are appended to the generated connection string.
