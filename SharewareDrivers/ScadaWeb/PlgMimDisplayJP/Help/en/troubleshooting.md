# PlgMimDisplayJP — Troubleshooting

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/troubleshooting.md)

| Symptom | Check |
| --- | --- |
| Display types are absent in the editor | Plugin registration, matching host/component package, dictionaries and browser resources |
| Ordinary runtime types show a license warning | Exact product key, running host's configuration directory, LicenseJPLite dependencies and log; restart after replacement |
| Numeric panel shows dashes | Positive input, record/quality, finite numeric value, sign and digit count after rounding |
| Matrix shows bad or no-data quality | Quality and overflow; editor preview is a separate state |
| Marquee does not scroll | Good runtime data, actual text overflow, `scrollEnabled` and reduced-motion setting |
| Long text is cut to its first channel record | Primary input metadata and `InCnlProps.JoinLen` |
| Table cell is missing | One-based position, full span inside the grid, earlier overlapping cells; browser console |
| Table cell formatting differs from its template | Host-provided `df.dispVal` takes precedence at runtime |
| Wrong conditional text | Rule order, comparison bounds, first-match behavior and no-data state |
| Cell colors/font do not change | `useDefaultStyle`, inherited font and rule overrides |
| Chart does not open | Positive input list, assigned chart feature, `trendSelectable` and enabled component |
| Command action is inactive | Explicit action type, positive output, finite immediate value, runtime control rights and host API |
| Digits/wheels animate unexpectedly or stay still | `animateChanges`, transition duration and reduced-motion preference |
| Styling/font is missing | Complete `/plugins/MimDisplayJP` tree, correct case and browser cache |

Inspect Webstation logs for plugin/localization/license messages, then browser Console and Network for failed assets or skipped cells. Verify the complete package before replacing individual dependencies. The runtime and editor are different contexts: a correct preview does not prove a licensed live display.
