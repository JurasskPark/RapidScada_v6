# PlgMimControlsJP — Button Images and Text Wrapping

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/button-images-and-text-wrapping.md)

From `6.5.0.15`, `IlluminatedButton`, `LatchedButton`, `MomentaryButton`, `OneShotButton`, `TextCommandInput`, `SetpointControl`, `ValueForm` and `MechanismPanel` support configurable button content. Use the standard Mimic image picker to add PNG, JPG or SVG images to the mimic. Images remain embedded in the `.mim` and do not require an external URL.

| Setting | Behavior |
| --- | --- |
| `ImageName` | Image selected from the mimic collection |
| `ImagePosition` | `Left`, `Right`, `Top` or `Bottom` relative to text |
| `ImageScale` | `1…100%` of the available image area; aspect ratio is preserved |
| `WrapText` | Multiline captions and explicit line breaks |

Use `ButtonContent` for ordinary buttons and the text-command send button, `ApplyButtonContent` for a setpoint's Apply button, and `OpenButtonContent` for a form opener. Mechanism commands have their own button-content settings. State-driven buttons can use different images for On, Off, Unknown and No data; dictionary controls select the image from the confirmed matching state row. If no state image is configured, the common image is used. Images change appearance only, not command routing or feedback rules.

Keep enough space for both the image and caption, especially when Top/Bottom is used on small buttons. The entire caption remains available in the tooltip. Old mimics without these settings retain text-only buttons without wrapping.
