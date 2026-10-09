# PlgMimElectricJP — Drawing profiles

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/symbol-profiles.md)

The document property `electricalSymbolProfile` defaults to `GostIndustrial`. In the JP editor it is selected using the electrical toolbar dropdown; it is hidden in the generic document property grid. Each ordinary component also has a `symbolProfile` property available in the standard editor.

| Value | Meaning |
| --- | --- |
| `Inherit` | Component follows the document profile |
| `GostIndustrial` | Original industrial drawing library |
| `Iec` | IEC-oriented project alternatives where supplied |
| `ProjectCustom` | Project-specific symbol from `customSymbolUrl` |

The effective profile uses an explicit component choice first, then the document choice. `Inherit` is a component option, not a document profile.

The library contains 145 SVG assets: 101 base symbols, 30 original state variants, eight IEC-oriented base alternatives and six IEC-oriented state alternatives. The alternatives cover representative types rather than the complete catalog. Rendering selects a profile-specific state asset, then an original state asset, then a profile base asset, then the original base asset. Missing IEC alternatives therefore fall back to the original library.

The IEC-oriented graphics are a project drawing set, not licensed IEC 60617 database graphics or a certificate of compliance. `standardReference` is an editable drawing note; it does not validate a standard or change geometry.

`ProjectCustom` accepts a relative or same-origin root URL, for example `/plugins/MySymbols/images/pump.svg`. Empty URLs, protocol-relative `//...` URLs, backslash-prefixed paths and URLs with a scheme such as `https:` or `data:` are rejected. If no usable custom URL remains, the ordinary asset is used.

A custom picture replaces the displayed symbol but does not replace its state logic, fixed tile size or port contract. Supply a drawing aligned with those ports and test its rotation.
