# PlgMimDisplayJP — Table cell actions

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/actions.md)

A `DisplayCellAction` has `name` and `icon` (optional), `actionType` (default `DrawChart`), `inCnlNums`, `chartArgs`, `outCnlNum` (default 0), `commandValue` (default 0) and standard `linkArgs`. Empty names use a localized action caption. Unsupported action types fall back to `DrawChart`.

| Type | Parameters | Result |
| --- | --- | --- |
| `DrawChart` | `inCnlNums`, `chartArgs` | Open the host-assigned chart feature |
| `ShowCommand` | Positive `outCnlNum` | Open the host command dialog |
| `SendCommand` | Positive `outCnlNum`, finite `commandValue` | Send an immediate numeric command |
| `OpenLink` | `linkArgs` | Open a view or URL |

An empty `inCnlNums` uses the cell's `inCnlNum`. Channel lists accept positive numbers and ascending ranges, for example `101,102,110-112`; invalid tokens and repeated tokens are omitted. The chart uses the Webstation-assigned `ChartFeature`, so no specific chart plugin is required by the component.

Only explicit `SendCommand` performs immediate sending. `ShowCommand` opens a dialog. Both command actions require an enabled component, runtime control rights, a positive output channel and the appropriate host API. `commandValue` is a requested value, not confirmed channel feedback. Table cell actions have **no script action**.

Ordinary click runs the first action. Right click, Menu or `Shift+F10` opens all actions in configured order; keyboard activation uses `Enter`/`Space`. Actions are inactive in edit mode and when the component is disabled.

For an aggregate chart, enable `showTrendSelection` and mark cells `trendSelectable` with positive input channels. Selection deduplicates channel numbers and opens a comma-separated list using `trendChartArgs`. Selection is transient.

`linkArgs` supports a positive `viewID` or `url`, the standard link target, modal sizing and URL parameters. A view ID takes precedence over the URL. Links require host navigation support; command and chart features likewise depend on the configured Webstation host.
