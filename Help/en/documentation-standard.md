# Rapid SCADA — Documentation standard

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/documentation-standard.md)

## Product pages

Keep an English README and a Russian `README.ru.md`. Preserve existing README filename capitalization. Start with product name and existing compatibility badges, then language/help links, a short description and up to five highlights. Keep the main prose within 250 words.

Use one or two representative existing screenshots with relative paths. Keep video links and clickable existing covers. Omit unavailable media blocks. Summarize known licensing conditions and link to detailed help/support.

## Detailed help

Store English topics in `Help/en` and Russian topics in `Help/ru`, with `index.md` as contents. Both languages use matching filenames and topic structure. Every page links to contents, the product page and its counterpart language.

Use one topic per task or component group. Preserve parameters, commands, examples and important limitations. Translate prose, table headings and captions; keep API identifiers and literal data values unchanged. Never fill missing product facts with guesses.

Shared installation, build and packaging topics belong to the repository help. Product build pages link to the matching language. Category catalogs contain short descriptions and localized product links.

## Maintenance

When behavior changes, update the corresponding topic in both languages and then refresh the short overview if needed. Verify local paths with exact capitalization, anchors, images and language navigation. Preserve existing media directories. Generated package `readme.txt`, binaries and release archives have their own workflow.
