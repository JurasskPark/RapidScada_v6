# PlgMimSVGAnimationJP — License and execution policy

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/license.md)

The current source distinguishes free authoring from licensed runtime execution.

| Context | Behavior |
| --- | --- |
| Mimic/faceplate editor | Full palette and designer, import/export, animation/action configuration, Apply and simulator without a local component key |
| Webstation with a valid signed server license | Drawing, channel-driven animation and operator actions subject to host rights |
| Missing/invalid license or license-check error | Localized inert placeholder; no component bindings, animation, data/API calls or commands |

Other plugins and the viewer's general polling are not disabled by this component's license state. The shared guard is compiled into the existing plugin assembly; there is no separately installed guard DLL. The generated `js/zz-runtime-license.js` must be present alongside `js/svg-animation.bundle.js`.

The script marker `licensed=1` denotes editor/authoring context in the current component specification. It is not evidence of an issued runtime license. The viewer cannot open the designer.

Statuses include `valid`, `missing`, `invalid` and `error`. An installed runtime key is checked server-side; a visual watermark is a separate mechanism and does not establish activation.

Historical notes for `1.0.3` described a different licensing arrangement. The current manifest, component specification, licensing script and shared policy take precedence for `6.5.0.3`.

Commercial use follows the issued license terms. A price, new video and verified support contact were not found in the inspected materials. [Activation](activation.md) · [Source boundaries](sources.md)
