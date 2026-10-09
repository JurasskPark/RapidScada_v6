# DrvDebug — How It Works

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/how-it-works.md)

The runtime loads the per-device XML configuration from the ScadaComm configuration directory. The file name is based on the device number, for example `DrvDebug_001.xml`. If the file is missing, the project model saves a default configuration.

During a master or mixed polling session, the driver sends each enabled configured command, waits for a response until the stop condition or polling timeout, logs transport data and decodes the response into tags.

During slave or mixed incoming request processing, the driver reads incoming bytes until the stop condition is reached or the timeout expires. It then decodes the received buffer into tags and, in slave/mixed mode, sends the first enabled configured command as the default response payload.
