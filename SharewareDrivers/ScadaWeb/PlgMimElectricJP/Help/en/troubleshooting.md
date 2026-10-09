# PlgMimElectricJP — Troubleshooting

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/troubleshooting.md)

| Symptom | Check |
| --- | --- |
| Ordinary symbol shows a license message | Check the server config directory, exact product/key names, UID, expiration and LicenseJPLite dependencies; restart Webstation. Editing and the demo can work while runtime is blocked. |
| Optional group is missing | Check PlgMimElectricJP.xml, the recognized package ID and enabled=true; read the log for XML errors. Reopen the editor after restart. |
| Type cannot load or symbols are blank | Confirm the matching Web DLL, View DLL, PlgMimic.Common.dll and complete assets. In unlicensed runtime, also check js/zz-runtime-license.js. |
| SVG returns 404 | Check /plugins/MimElectricJP/images/symbols/, case-sensitive names and the deployed package version. Reload cached resources. |
| State is unknown | Verify numeric finite data, positive status, primary channel number and configured trip input. A configured bad primary input prevents feedback fallback. |
| Old breaker states changed | Compare Legacy and Iec61850 mappings. In Iec61850, 3 is unknown and trip requires a separate input. |
| Analog value is a question mark | Check the measurement binding and primary input. Signal metadata does not perform engineering conversion. |
| Custom picture is ignored | Select ProjectCustom and use a relative or same-origin root URL; external schemes and // paths are rejected. |
| Component cannot be resized | Ordinary symbol tiles are fixed; choose the required type or rotate it. This restriction does not apply to the demo shell in the same way. |
| Demo stops changing after an action | The automatic sequence pauses for the selected example. Restart the automatic demonstration to reset both; transfer timing still follows local conditions. |

Webstation logs configuration loading and license status. Browser developer tools help locate missing scripts/images and failed resource requests. Include the plugin/package version, host version and relevant log message when reporting a problem.

[Activation](activation.md) · [State contracts](states.md) · [Package requirements](installation.md)
