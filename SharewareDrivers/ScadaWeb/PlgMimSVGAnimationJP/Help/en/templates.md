# PlgMimSVGAnimationJP — Reusable templates

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/templates.md)

Use the designer's built-in templates as starting drawings, or save your own `.svganim.json`. Template saves preserve geometry, resources, roles, animation and actions while clearing assigned channel numbers.

1. Open or create the drawing.
2. Name roles by purpose, such as feedback, level and command.
3. Save the template.
4. Open it in the destination symbol and assign actual channels in one channel table.
5. Inspect action feedback, availability conditions and host permissions.
6. Test the scene, apply it, then save the mimic or faceplate.

Opening a template creates an independent scene; it does not establish live synchronization with the original file. Channel mappings are not a separate executable script.

The source project's `Examples/HelloWorld` has `01–36.svganim.json` and `UserOriginal.svganim.json`. Those prepared examples already contain HelloWorld assignments; check them after opening. They are not copied into this documentation directory.

[36 examples](examples.md) · [Faceplates](faceplates.md) · [Import/export](import-export.md)
