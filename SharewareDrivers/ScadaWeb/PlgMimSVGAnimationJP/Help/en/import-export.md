# PlgMimSVGAnimationJP — SVG import and export

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/import-export.md)

Import converts SVG into an editable scene; raw XML is not inserted into the live DOM. Review the normalized preview and exclusions report before confirming. The original file is not changed.

## Supported material

- Basic shapes, groups, paths and simple text.
- Affine transforms and supported inline style, colors and opacity.
- Local linear/radial gradients and simple local clipping.
- Local `use` references, expanded as independent details.

Paths remain whole objects. Import is a supported subset, so compare complex drawings with their original rendering.

## Excluded or rejected material

Scripts, event attributes, external resources, `foreignObject`, CSS stylesheets, native SVG animation, masks, filters, complex text, nested viewports, transformed/inherited gradients, `symbol` viewBox behavior, complex `clipPath` and unsupported properties are excluded/reported.

Lengths accept plain numbers or `px`. Other units are not interpreted. DTD/entities and malformed XML are rejected. Limits include 2 MiB of input, 2000 nodes after expansion, 32 nesting levels, 100000 complexity tokens and 100 stops per gradient. [Exact limits](scene-document.md)

## Two export formats

| Format | Content |
| --- | --- |
| SVG | Static base drawing; no SCADA rules or live animation |
| `.svganim.json` | Drawing, animation, actions and semantic roles; saved channel numbers are cleared |

A saved template can be opened as an independent drawing and assigned to another project's channels. The supplied HelloWorld files are a deliberate exception: their channel assignments are already populated for that sample project.

[Templates](templates.md) · [Element code](element-code.md)
