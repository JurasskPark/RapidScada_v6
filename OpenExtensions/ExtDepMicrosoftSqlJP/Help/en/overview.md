# ExtDepMicrosoftSqlJP — Overview

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/overview.md)

![ExtDepMicrosoftSqlJP](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&product=ExtDepMicrosoftSqlJP&color=4bb60e)

![Rapid SCADA](https://jurasskpark.ru/service/budges/?label=Rapid%20SCADA&message=6.x&color=blue)
![.NET](https://jurasskpark.ru/service/budges/?label=.NET&message=10.0&color=purple)
[![License](https://jurasskpark.ru/service/budges/?label=license&message=Apache%202.0&color=blue)](../../../License.txt)

**Microsoft SQL Server Deployment JP** — a Rapid SCADA Administrator extension for deploying project configuration to Microsoft SQL Server.

ExtDepMicrosoftSqlJP uploads and downloads a Rapid SCADA project configuration database using Microsoft SQL Server as the target DBMS.

The extension is intended to work together with `MicrosoftSqlStorage` and can be used alongside the `ModArcMicrosoftSqlJP` server module. The extension stores project configuration in the `project` schema, while the archive module writes runtime archive data to the `mod_arc_microsoft_sql` schema.
