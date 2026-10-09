# ExtDepAgent — Troubleshooting

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/troubleshooting.md)

- If Agent is disabled, enable it in the selected deployment profile.
- If connection testing fails, check the profile's connection settings and Agent availability.
- If an application's settings are skipped, check both its enabled state in the instance and its include option.
- If service control fails, inspect the transfer log and the Agent service state; service control uses the profile timeout.

Uploading project configuration can restart applications according to the chosen options. Verify the selected instance and profile before transfer.
