# MicrosoftSqlStorage — Reference

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/reference.md)

The storage uses the `project` schema:

| Data | Table or naming rule |
| --- | --- |
| Configuration database tables | Table and column names converted to lowercase |
| Application configuration (`DataCategory.Config`) | `project.app_config` |
| Application storage (`DataCategory.Storage`) | `project.app_storage` |
| View files (`DataCategory.View`) | `project.view_file` |

Application files are selected using `app_id` and a normalized `path`. View files use their path and binary contents. Text views are decoded as UTF-8. A missing file raises `FileNotFoundException`.

This storage handles configuration and files. Runtime archives are handled by the separate [ModArcMicrosoftSqlJP module](../../../../../../OpenModules/ModArcMicrosoftSqlJP/README.md).
