# PlgMimSVGAnimationJP — Sources and remaining gaps

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/sources.md)

Documentation was prepared on `2026-10-10` from the separate local `scada-web-v6-develop` checkout, mainly `Plugins/Mimics/PlgMimSVGAnimationJP`. Its source README was not previously present in this product catalog. No source projects, runtime assets, activation files, binaries or release ZIPs were copied here.

## Evidence used

| Material in the source checkout | Confirmed information |
| --- | --- |
| Product README, `component.json`, Web/View project metadata, component specification | One component, version `6.5.0.3`, .NET 10, identity, assets and palette/editor contract |
| `js/src/00-model.js`, engine, surface, importer and designer modules | Model `1`, typed rules, defaults, limits, geometry, quality, code editor and import/export |
| Channel catalog controller and adapter | Query limits, permissions, paging, provider contract and manual fallback |
| `js/src/89-faceplates.js`, `js/src/90-mimic.js` | Host-resolved bindings, nested faceplates and lifecycle |
| Plugin constants/provider, `js/src/01-license.js`, `Doc/MIMIC_RUNTIME_LICENSING.md` | Free authoring, licensed runtime, exact activation filenames and inert placeholder |
| `Examples/HelloWorld/EXAMPLES.md`, templates and `gallery.json` | All 36 examples, source channel catalog and current rule/action counts |
| `Build/Scripts/Demo/README-SVGAnimationJP.md` and local views | Eight bilingual example pages and PNG capture provenance |
| `Scripts/BuildPackage.ps1`, `Scripts/BuildAssets.mjs`, family release entry and build audit | Actual build/package commands and native versus portable dependency boundaries |

Older `Doc/SVG_ANIMATION_PLUGIN.md` notes retain useful feature details but contain historical `1.0.3` licensing statements. Current implementation and the shared licensing policy govern this version. The former `PlgSVGAnimationJP` name is described in migration, not used for new keys.

## Remaining gaps

- No verified video, price or support contact was found.
- The external download badge from `jurasskpark.ru` did not load during the local preview. Its URL uses the requested product name; the service response was not confirmed.
- Webstation 6.3 compatibility is not established for this .NET 10 version.
- A new protected DLL/package build and ZIP validation were not performed.
- PNG capture did not exercise commands, chart history, license issuance/failure scenarios or physical equipment.
- The designer PNG is an existing source artifact; its original capture date and tested build remain unspecified.
- License-page availability depends on registration of the host's provider contract; portable import should not be assumed to register the native provider automatically.
- Native licensing policy and portable packaging have different shared-dependency boundaries. Use the actual host/package combination and verify its matching dependencies.

[Build](build.md) · [Licensing](license.md) · [Screenshots](screenshots.md)
