# PlgMimSVGAnimationJP — HelloWorld: 36 examples

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/examples.md)

## Materials and setup

The source project's `Examples/HelloWorld` contains `DrawSVG.mim`, `gallery.json`, numbered `01–36.svganim.json` templates and `UserOriginal.svganim.json`. The original line is retained at the bottom of the mimic. This documentation reproduces the catalog; it does not distribute those project files.

The current gallery metadata lists 36 examples, 48 components, 476 shapes, 100 animation rules and 11 actions. These are source-material counts, not newly executed acceptance tests.

Open a numbered template in the designer. Its HelloWorld channel numbers are already assigned; check them in the channel table. In the editor, the base drawing is visible until the local simulator starts. In Webstation, saved symbols use actual SCADA samples and require the runtime license.

To publish `DrawSVG.mim`, connect it as a project view. Opening a template does not install a configuration database or change Server settings.

## Catalog by topic

- [1–10: geometry and appearance](examples-movement.md)
- [11–20: cycles and equipment](examples-equipment.md)
- [21–30: logic and data quality](examples-conditions.md)
- [31–36: commands, charts, links and reuse](examples-interaction.md)

## Sample channel behavior

`101` is a sine from −1 to +1 with a 60-minute period; `102` switches 0/1 every 7.5 minutes; `103` is a triangle from 0 to 15 with a 30-minute period. These live inputs change slowly; cyclic animations have their own shorter periods.

Examples use outputs `110` and `111` for operator commands and input feedback. Before the first value, open the initial-value dialog and enter 0. Fixed-value commands require valid feedback. Outputs `104` / `105` are not used because their tags are duplicated at `302` / `303` in this base. Channel `300` is deliberately disabled.

RA channels are arrays and are excluded from scalar rules. [Complete source channel catalog](example-channels.md)

The eight local Webstation pages `61001–61008` are another set of examples, not these 36 templates. [Webstation examples and PNGs](screenshots.md)
