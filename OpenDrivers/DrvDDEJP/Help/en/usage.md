# DrvDDEJP — Usage

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/usage.md)

1. Add `DrvDDEJP` to a communication line in ScadaAdmin.
2. Open the device properties.
3. Set the DDE `ServiceName`, `DefaultTopic`, `RequestTimeout` and `ReconnectDelay`.
4. Add tags and configure `Topic`, `ItemName`, `DataFormat` and `DataLength`.
5. Generate channel prototypes for the configured device.
6. Make sure the target DDE server application is running in the Windows environment where ScadaComm can access it.
7. Upload the Rapid SCADA project and restart the communication line.
