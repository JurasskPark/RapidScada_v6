# PlgTrendJP — Activation and Tag Limit

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/activation-and-tag-limit.md)

Activation requires a request file from the server running Webstation.

1. Copy the plugin libraries to the Webstation folder on the server.
2. Restart the Rapid SCADA web service.
3. The `PlgTrendJP_Activation.bin` file will appear automatically in `ScadaWeb\config`. This is the activation request.
4. Download the project from the server using Administrator. The request file will be in the downloaded project's `ScadaWeb\config` folder.
5. Send this file to the email address specified in the plugin archive's README.
6. Put the received `PlgTrendJP_License.bin` file in the project's `ScadaWeb\config` folder in Administrator, then publish the project to the server.
7. If the license matches this server and passes validation, the plugin becomes activated. Open the mimic in Webstation and check the symbol.

Keep the filenames unchanged.

## Channel limit

The signed license must contain `AppName=PlgTrendJP` and a positive `CountTags`. `CountTags` is the maximum number of unique channel numbers in one trend. Ranges are expanded before counting, duplicates count once, and one channel used in several archive sources remains one licensed channel.

[Installation](installation-and-registration.md) · [License](license.md) · [Troubleshooting](troubleshooting.md)
