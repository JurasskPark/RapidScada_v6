# PlgMimSVGAnimationJP — Component and workflow

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/overview.md)

The palette contains one component, `SvgAnimation`. The mimic editor positions the complete symbol; its property editor manages the internal drawing, animation and actions. Use one symbol for a small composite object, rather than replacing the entire mimic diagram.

1. Insert `SvgAnimation` and open its drawing-and-animation property.
2. Draw details or import a supported SVG; name and group the nodes.
3. Create input and command roles and assign project channels.
4. Configure animation cards and actions for individual nodes.
5. Check known, unknown and boundary values in the simulator.
6. Apply the draft to the component, save the mimic or faceplate, and check it in Webstation.

The scene is stored in `sceneDocument`, a JSON string inside the `.mim` or `.fp` document. Live values, animation phases and pending commands are not stored there.

The UI uses English and Russian dictionaries. The designer and simulator are available without a component key. Licensed execution runs within the host's channel and control permissions. There is no separate autonomous demonstration component.

[Designer](designer.md) · [Parameters](parameters.md) · [License](license.md)
