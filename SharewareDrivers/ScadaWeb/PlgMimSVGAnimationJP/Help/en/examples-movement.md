# PlgMimSVGAnimationJP — Examples 1–10: geometry and appearance

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/examples-movement.md)

Source templates: `Examples/HelloWorld/01–36.svganim.json`. Open the numbered template in the designer and check its assignments. [Project setup and channel catalog](examples.md).

| No. | Example | Channels | Rule and check |
| --- | --- | --- | --- |
| 1 | Line: three changes | 101 | 101 > 0: red, 6 px wide, +100 px offset. Otherwise blue, 2 px. Transition 0.6 s. Test −0.01 → 0 → +0.01. |
| 2 | State priority | 101, 102 | First: 101 > 0.7 → red alarm. Next: 102 = 1 → green running state. Otherwise gray. The first matching row wins. |
| 3 | Show / hide | 102 | 102 = 1 → visible; 102 = 0 → hidden. A dashed outline remains as a reference. |
| 4 | Continuous opacity | 101 | 101: −1 → 15% opacity; +1 → 100%. Linear interpolation between the boundaries. |
| 5 | Horizontal travel | 101 | 101: −1…+1 → X offset 0…200 px. The carriage follows the guide. Out-of-range values keep the endpoint position. |
| 6 | Vertical lift | 103 | 103: 0…15 → Y offset 0…−94 px. Negative Y raises the detail; 15 reaches the top. |
| 7 | Before / after position | 102 | 102 = 1 → X +160, Y −65 px; otherwise base position. Both coordinates change over 0.8 s. |
| 8 | Scale on both axes | 103 | 103: 0…15 → scale 50…160%. Proportions stay constant; pivot is at the center. |
| 9 | Stretch on X | 101 | 101: −1…+1 → width 20…180%. Y scale stays 1. The left pivot makes the detail grow to the right. |
| 10 | Needle angle | 101 | 101: −1…+1 → angle −65…+65°. The needle rotates about an explicitly defined bottom pivot. |

[All examples](examples.md) · [Simulator](simulation.md)
