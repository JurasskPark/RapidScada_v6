# PlgMimSVGAnimationJP — Troubleshooting

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/troubleshooting.md)

| Symptom | Check |
| --- | --- |
| Component absent from the palette | Plugin registration, matching Web/View builds and enabled native entry |
| Designer opens, but viewer shows a placeholder | Runtime license status; free editing does not enable runtime |
| Drawing or locale fails to load | Asset path, both bundles, CSS, icon, dictionaries and cache after update |
| Channel catalog returns 401/403 | Authentication, administrator policy and object View/Control rights |
| Channel cannot be selected | Direction/type, active flag and project assignment |
| Correct JSON channel but no runtime data | Effective `propertyBindings`; runtime never falls back to saved JSON numbers |
| Old faceplate has no subscription | Change an external instance property and save to regenerate bindings |
| Detail freezes with a no-data mark | Used input roles, positive status, finite sample and higher-priority unknown variants |
| Command is unavailable | License, host control right, command channel, feedback/condition quality and pending state |
| Command delivered but indicator unchanged | Read input feedback; delivery is not measured equipment state |
| Action is only logged | Editor simulator is active |
| Imported SVG differs from the original | Exclusions report, supported subset and complexity limits |
| Element-code Apply is disabled | Fresh normalized preview, same type, supported fields and valid resources |
| Group cannot be ungrouped | Explicitly remove group actions first |
| Existing scene opens read-only | Malformed or unsupported schema; export the retained payload before repair |
| Rotation is static | Active condition, data quality and system reduced-motion setting |
| HelloWorld values barely change | The real sine/triangle periods are measured in minutes |
| Chart has no history | Assigned input role, host `ChartFeature` and archive content |

Collect the plugin version, host version, license state, relevant asset/API HTTP status and a minimal saved scene. Do not include an issued license file or activation contents in a public bug report.

Use [simulation](simulation.md) to separate drawing/rule checks from host data and permissions. Documentation validation and PNG capture do not establish actual command delivery, equipment feedback, runtime licensing acceptance or release-package integrity.

[Installation](installation.md) · [Quality](quality.md) · [Activation](activation.md)
