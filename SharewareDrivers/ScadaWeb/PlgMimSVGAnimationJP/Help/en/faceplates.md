# PlgMimSVGAnimationJP — Faceplates and effective channels

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/faceplates.md)

`SvgAnimation` supports ordinary mimics and nested faceplates. The adapter creates external standard bindings named `svgFaceplate_<component ID path>_<encoded role>`; the property name contains no dots.

The host resolves these bindings through the ordinary provider and computed-channel mechanisms. Instance assignments, existing user bindings and extra scripts are preserved. Runtime uses the resolved binding, not the scene's saved original number.

For an older `.mim` file with an existing faceplate instance, change an external instance property and save the document to regenerate the external bindings and server subscriptions. Merely opening the document does not perform this migration. A newly inserted instance receives the bindings automatically.

Example: role channel `1` plus instance `cnlOffset = 110` resolves to channel `111`. Verify both feedback and command roles at the actual instance; do not send a trial command based only on the template's number.

Removing a nested faceplate also disposes its SVG animation instances. [Channel roles](channels.md) · [Migration](migration.md)
