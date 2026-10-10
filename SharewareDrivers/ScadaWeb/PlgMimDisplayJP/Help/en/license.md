# PlgMimDisplayJP — License and execution

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/license.md)

| Context | Ordinary components | DisplayDemo |
| --- | --- | --- |
| Standard or JP editor without a local component key | Full palette, static preview, properties, copy and save | Available |
| Runtime with a valid server license | Drawing, channel data and configured actions under host permissions | Saved demo continues working |
| Runtime without a valid license, or validation error | Localized inert placeholder | Autonomous synthetic-data simulation |

Blocked ordinary components retain their type, identity and geometry. They do not execute component/extra scripts, data bindings, blinking or actions and do not call data/command APIs. Other plugins are unaffected. The viewer's general data polling remains a host responsibility.

The runtime guard is generated as `js/zz-runtime-license.js` during source build and must be included in the package. It is not a license key. License changes take effect after restarting Webstation.

Editing and saving a diagram do not establish runtime licensing. The separate JP editor's own licensing remains independent. The packaged Russo One font has its own **SIL Open Font License 1.1**; preserve `OFL.txt` when distributing it. See [activation](activation.md) and [installation](installation.md).
