# PlgMimSVGAnimationJP — License and execution policy

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/license.md)

Creating drawings is free; runtime execution in Webstation requires a license.

| Context | Behavior |
| --- | --- |
| Mimic/faceplate editor | Full palette and designer, import/export, animation/action configuration, Apply and simulator without a local component key |
| Webstation with a valid signed server license | Drawing, channel-driven animation and operator actions subject to host rights |
| Missing/invalid license or license-check error | Localized inert placeholder; no component bindings, animation, data/API calls or commands |

Other plugins and the viewer's general polling are not disabled by this component's license state. Install the complete plugin package. The Webstation viewer cannot open the designer.

Statuses include `valid`, `missing`, `invalid` and `error`. An installed runtime key is checked server-side; a visual watermark is a separate mechanism and does not establish activation.

These licensing rules apply to version `6.5.0.3`. For updates from older versions, see [migration](migration.md).

Commercial use follows the issued license terms. A price, new video and verified support contact were not found in the inspected materials. [Activation](activation.md) · [Source boundaries](sources.md)
