# PlgMimDisplayJP — Configuration

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/configuration.md)

The plugin exposes the electronic display group and a demonstration group through the host component palette. There is no separate product settings file or custom HTTP route in the inspected source version.

Configure each component in the Mimic property editor:

- `inCnlNum` supplies the primary input for numeric displays, marquee and conditional text.
- `DataTableDisplay` uses `cells[*].inCnlNum`; its root input/output and click action are cleared.
- `previewValue` or `previewText` controls editor previews only.
- Numeric formatting, colors and motion belong to the selected display type.
- Table cells have independent layout, rules and actions.

The four read-only indicators force `outCnlNum = 0` and clear `clickAction`. Their inherited interactive and generic styling properties are hidden; use their own display properties.

Select the host language to use the EN/RU dictionaries. The bundled Russo One font supports Latin and Cyrillic text. If you redistribute its files, retain `OFL.txt`. See [components](components.md), [channels](channels.md) and [table cells](table-cells.md).
