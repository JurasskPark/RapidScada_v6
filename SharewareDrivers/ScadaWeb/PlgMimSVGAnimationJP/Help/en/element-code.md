# PlgMimSVGAnimationJP — Editing an element's code

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/element-code.md)

The `</>` button opens code for one selected detail. It is a constrained editor for the scene model, rather than an arbitrary DOM/SVG script editor.

- SVG mode accepts one detail of the same type and edits local geometry/style and supported local resources. It preserves placement, angle, pivot, arrows and attached actions.
- JSON mode edits the detail's complete parameters. It cannot change `id`, `parent` or `type`.
- Groups use JSON mode only. A path is edited as a whole; there is no interactive path-node editor.

Validate and preview before applying. `Ctrl+Enter` performs the preview operation. Preview shows the normalized result. Editing the code invalidates the old preview; invalid, unknown or unsupported fields block Apply.

Apply commits the result to the designer draft in one undo operation. Cancel or Esc changes nothing. Apply the outer designer and save the document to persist it.

The validator does not execute submitted markup as raw DOM. SVG restrictions and complexity limits still apply. [SVG import](import-export.md) · [Scene limits](scene-document.md)
