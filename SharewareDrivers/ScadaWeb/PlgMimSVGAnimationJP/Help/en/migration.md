# PlgMimSVGAnimationJP — Renaming and document migration

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/migration.md)

The old product name was `PlgSVGAnimationJP`. The current assemblies, metadata and license product name are `PlgMimSVGAnimationJP`. The serialized component type `SvgAnimation`, browser API `SvgAnimationJP`, route `/api/plugins/svg-animation-jp/channels` and scene schema `1` are retained.

1. Back up the mimic/faceplate documents and current plugin installation.
2. Disable the old plugin registration.
3. Install and register the complete current Web/View/assets package for the matching host.
4. Confirm each existing scene opens without validation errors.
5. Regenerate external bindings for older faceplate instances as described in [faceplates](faceplates.md).
6. Save the documents and check resolved channel roles before runtime verification.
7. Generate a current-product activation request if the old key is issued for the previous application name.

Do not load both plugin registrations. Renaming an old signed license file does not produce a current-product key.

The old `1.0.3` archive is historical material, not the current `6.5.0.3` package. Malformed and future-version scenes retain their original payload for recovery; they are not silently converted into blank scenes.

[Installation](installation.md) · [Activation](activation.md) · [Source boundaries](sources.md)
