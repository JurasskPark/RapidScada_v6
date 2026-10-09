# MicrosoftSqlStorage — Installation

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/installation.md)

Choose an OS- and architecture-specific package: `win-x64`, `win-x86` or `linux-x64`. An AnyCPU ZIP is not provided because the SQL client runtime implementation must match the host.

The package supplies storage files for `SCADA/ScadaServer`, `SCADA/ScadaComm` and `SCADA/ScadaWeb`. Keep the supplied SQL client dependencies with the storage files. Enable `MicrosoftSqlStorage` in the configuration of each application that uses it.

The library targets `net10.0`; the Rapid SCADA host must run on .NET 10. Prepare and deploy the configuration database using [ExtDepMicrosoftSqlJP](../../../../README.md).
