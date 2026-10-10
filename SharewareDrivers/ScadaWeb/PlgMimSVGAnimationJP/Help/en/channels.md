# PlgMimSVGAnimationJP — Roles and channel bindings

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/channels.md)

A role separates a symbol's meaning from a project channel number. Each role has `id`, `name`, `direction` and `cnlNum`. Direction `data` reads input values; `command` addresses an output channel. `cnlNum = 0` means unassigned. Positive channel numbers must be safe integers no greater than `2147483647`.

Assign channels manually or through the [catalog](channel-catalog.md). Input feedback and output commands need separate roles even if their channel numbers happen to coincide.

Applying the scene produces standard `propertyBindings` named `svgRole_<id>` with `dataSource = channel` for assigned data and command roles. Existing user bindings are preserved. The scene and binding changes are passed to the host together.

At runtime, channel numbers come exclusively from resolved `component.bindings.propertyBindings`. The JSON's original `cnlNum` is not a fallback. Therefore a missing or stale host binding cannot be fixed just by putting a number in the scene string.

For a faceplate instance, the host resolves the effective channels. For example, a saved channel `1` with `cnlOffset = 110` resolves to `111` for data or command roles. Check the actual instance assignment before runtime testing.

The drawing reads data roles; command-only roles do not require input samples. Feedback and action conditions can introduce their own data requirements.

[Faceplates](faceplates.md) · [Actions](actions.md) · [Scene format](scene-document.md)
