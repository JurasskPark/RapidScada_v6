# PlgMimDisplayJP — Using displays

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/usage.md)

1. Open a Mimic diagram in the standard or a compatible JP editor.
2. Place a component from the display palette and set its size.
3. Choose the input channel, or configure each table cell's input.
4. Use preview values/text to check layout and conditional rules without live data.
5. Save the diagram, transfer the project and open the view in Webstation.
6. Check real input values, data quality and the server license.

Editor preview values are not commands or fallback live readings. Runtime always resolves available input data and quality independently. A successful command action does not replace measured feedback.

For charts in tables, select eligible cells using their selection markers and open the aggregate chart button. A cell click runs its first configured action; right click, the Menu key or `Shift+F10` opens the ordered action menu. Keyboard activation also supports `Enter`/`Space`. See [actions](actions.md).

A stationary marquee can be correct: short text, edit mode, bad/missing data, disabled scrolling and reduced-motion settings all prevent scrolling. Use [DisplayDemo](demo.md) for an autonomous walkthrough.
