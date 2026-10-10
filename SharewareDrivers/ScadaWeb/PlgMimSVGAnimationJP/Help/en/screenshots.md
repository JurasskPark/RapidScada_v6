# PlgMimSVGAnimationJP — Webstation examples and PNG gallery

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/screenshots.md)

## Image origin

Images `001–008` are full-mimic PNG captures of local Webstation views `61001–61008`, taken on `2026-10-10`. Each page has three independent cases with Russian/English copies reading the same channels and prepared feedback delays of 1/2/3 seconds. Capture did not operate their controls or send commands.

Image `009` is an unchanged copy of the source project's existing `Tests/Browser/artifacts/standard-designer.png`. It illustrates the designer's Russian interface; its original capture date and tested build are not established by this documentation update.

PNG files remain in `Source` with names `PlgMimSVGAnimationJP_001.png` through `PlgMimSVGAnimationJP_009.png`. No illustrative SVG files were copied.

## What the local project demonstrates

The source guide `Build/Scripts/Demo/README-SVGAnimationJP.md` describes 48 editable scenes across the eight pages, all 11 animated properties and 21 property/mode pairs. Geometry is native to this plugin: groups, paths, gradients, clipping and arrows, without runtime dependence on other equipment-symbol plugins.

Page `61006` uses AND for value >50 and auxiliary ≥30 with 1 s on-delay and 0.7 s off-delay. Hysteresis 5 holds the threshold until value ≤45. OR responds to values outside 20–80 or mode 2. Invalid quality freezes dependent details while independent ones continue; on the actions page it also invalidates mode feedback and blocks commands.

The local demo uses channels/devices `61000–79999` and code prefix `Demo.SVGAnimationJP.*`. Its signal mapping is feedback +0, command +70, counter +71, sine +72 and analog output +73. Assignments are described in `svganimation-demo-manifest.json`; extra scenes can be opened from `Scenes/*.svganim.json`. Charts use `ChartFeature` and require archive history. These project-specific assignments are not installation defaults.

## Pumps and flow — 61001

[![Pumps and flow](../../Source/PlgMimSVGAnimationJP_001.png)](../../Source/PlgMimSVGAnimationJP_001.png)

## Valves and actuator feedback — 61002

[![Valves and actuator feedback](../../Source/PlgMimSVGAnimationJP_002.png)](../../Source/PlgMimSVGAnimationJP_002.png)

## Vessels and level — 61003

[![Vessels and level](../../Source/PlgMimSVGAnimationJP_003.png)](../../Source/PlgMimSVGAnimationJP_003.png)

## Conveyors and movement — 61004

[![Conveyors and movement](../../Source/PlgMimSVGAnimationJP_004.png)](../../Source/PlgMimSVGAnimationJP_004.png)

## Measurement and indication — 61005

[![Measurement and indication](../../Source/PlgMimSVGAnimationJP_005.png)](../../Source/PlgMimSVGAnimationJP_005.png)

## Conditions and alarms — 61006

[![Conditions and alarms](../../Source/PlgMimSVGAnimationJP_006.png)](../../Source/PlgMimSVGAnimationJP_006.png)

## Actions inside SVG — 61007

[![Actions inside SVG](../../Source/PlgMimSVGAnimationJP_007.png)](../../Source/PlgMimSVGAnimationJP_007.png)

## Composite process unit — 61008

[![Composite process unit](../../Source/PlgMimSVGAnimationJP_008.png)](../../Source/PlgMimSVGAnimationJP_008.png)

## Symbol designer

[![Symbol designer](../../Source/PlgMimSVGAnimationJP_009.png)](../../Source/PlgMimSVGAnimationJP_009.png)

[36 HelloWorld templates](examples.md) · [Designer](designer.md) · [Source boundaries](sources.md)
