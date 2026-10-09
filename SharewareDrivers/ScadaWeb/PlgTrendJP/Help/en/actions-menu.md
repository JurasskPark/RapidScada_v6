# PlgTrendJP — Actions Menu

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/actions-menu.md)

## Display Settings

| Setting | Available behavior |
| --- | --- |
| **Show control panel** | Hides the filters and direct action buttons but keeps the **Actions** menu. |
| **Show timeline** | Shows or hides the overview and Previous/Next buttons. |
| **Navigation step and mode** | Configures Previous/Next as described above. |
| **Legend position** | Disabled, top, right, bottom or left. |
| **Point marker** | Circle, triangle or square. Also used by tooltips. |
| **Line width** | `1`, `1.5`, `2`, `3` or `4` CSS pixels. |
| **Point size** | `2`, `3`, `4`, `5`, `6` or `8` CSS pixels. |

Display settings are stored in the page URL, the last browser configuration and named profiles. Hiding the control panel or timeline redraws the existing data and does not reload the archive.

## Profiles

A profile saves the channel list, archive sources, period, chart type, display, tooltip and Excel settings.

1. Configure the trend as required.
2. Open **Actions → Profiles**.
3. Enter a profile name and click **Save**.
4. Select a saved profile and click **Apply** to restore it.
5. Use **Delete** to remove a selected profile.
6. Use **Show last** to restore the most recent automatically saved configuration for this view.

Profiles and the last configuration are stored locally in the browser. They are not automatically shared with another browser, computer or user. Use `View.Args` for administrator-defined defaults that must be available to everyone.
