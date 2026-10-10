# PlgMimSVGAnimationJP — Examples 21–30: logic and quality

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/examples-conditions.md)

Source templates: `Examples/HelloWorld/01–36.svganim.json`. Open the numbered template in the designer and check its assignments. [Project setup and channel catalog](examples.md).

| No. | Example | Channels | Rule and check |
| --- | --- | --- | --- |
| 21 | Two conditions: AND | 101, 102 | 101 > 0 AND 102 = 1 → green. Both must match. Test (0.5, 1), (−0.5, 1), (0.5, 0). |
| 22 | Two conditions: OR | 101, 103 | 101 > 0.6 OR 103 > 12 → red; either may match. Other states are gray. |
| 23 | Inside / outside an interval | 101 | Green for −0.3 ≤ 101 ≤ +0.3; orange outside. Both boundaries are included in the green interval. |
| 24 | All numeric comparisons | 101 | Six indicators compare 101 with zero. Test −0.1 → 0 → +0.1. Equality refers to the exact number. |
| 25 | Threshold hysteresis | 101 | Enter at 101 > 0; exit at 101 ≤ −0.2. Compare a plain threshold on the left with hysteresis on the right. Test 0.1 → −0.1 → −0.21. |
| 26 | On/off delays | 102 | 102 = 1 turns on after 2 s; 102 = 0 turns off after 1 s. Compare immediate and delayed switching. |
| 27 | Instant and smooth | 102 | 102 = 1 → +170 px offset; 0 returns to base. Upper detail switches instantly, lower detail takes 2 s. New targets replace the current target without a queue. |
| 28 | Group and nested details | 103, 101, 102 | 103: 0…15 moves the group by 100 px. Inside it, 101 ≥ −1 enables rotation and 102 = 1 independently changes the blade color. |
| 29 | No-data isolation | 300, 101 | Channel 300 is disabled in the base: the left detail shows no data. Channel 101 continues changing the right detail's color. Change quality independently in the simulator. |
| 30 | Two devices | 101, 107 | 101 is device 1's sine channel; 107 is device 2's. Geometry is identical, bindings are separate. |

[All examples](examples.md) · [Simulator](simulation.md)
