# PlgMimControlsJP — Themes

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/themes.md)

The active file `css/controls.css` contains the complete light-blue theme. Four complete replaceable presets are supplied:

- `css/themes/controls.light-blue.css` — light blue;
- `css/themes/controls.light-green.css` — light green;
- `css/themes/controls.dark-blue.css` — dark blue;
- `css/themes/controls.dark-green.css` — dark green;

To change the global theme, back up `ScadaWeb/wwwroot/plugins/MimControlsJP/css/controls.css` and copy the selected preset over it. Restart or hard-refresh the browser after replacement. The plugin does not include a runtime theme selector.

Neutral surfaces use the theme accent for selection, focus and active manipulation. Green and red keep their semantic success and error roles. Explicit indicator colors and the component-level pending-frame color take priority over the theme.
