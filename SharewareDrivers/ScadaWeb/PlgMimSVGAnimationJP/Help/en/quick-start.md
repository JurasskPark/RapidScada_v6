# PlgMimSVGAnimationJP — First animated symbol

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/quick-start.md)

## Minimal example

Use the editor simulator first; the channel assignment below is illustrative. A threshold of 50 is not appropriate for the default HelloWorld sine channel, whose range is −1…1.

1. Add `SvgAnimation` and open the designer. Keep the default canvas `320 × 240`.
2. Draw a horizontal line; give it a recognizable name.
3. Add a role `processValue` with direction `data`. Assign an actual input channel in your project; `101` is only an example.
4. Select the line and add `stroke`, mode `condition`. First variant: `processValue > 50`, red. Keep the base stroke blue.
5. Add `strokeWidth` with the same condition and result `6`; keep base width `2`.
6. Add `position` with the same condition and result `[30, 0]`. Set `duration` to `0.6`.
7. In the simulator enter `49`, `50` and `51`. Only `51` matches `> 50`. Test invalid quality: the line freezes and shows a no-data indication.
8. Stop the simulator, apply the drawing to the component, and save the mimic. Confirm the effective channel assignment and runtime license before opening Webstation.

| Property | Base value | Result when `processValue > 50` |
| --- | --- | --- |
| `stroke` | Blue | Red |
| `strokeWidth` | `2` | `6` |
| `position` | `[0, 0]` | `[30, 0]` |
| `duration` | `0` | `0.6` seconds for the movement card |

Colors and movement are separate cards. A group can move or rotate as a whole while its children keep independent color rules.

To add a command button, use separate input feedback and command roles. Confirmation of a sent command does not replace the measured feedback value. [Actions](actions.md) · [Channel roles](channels.md)
