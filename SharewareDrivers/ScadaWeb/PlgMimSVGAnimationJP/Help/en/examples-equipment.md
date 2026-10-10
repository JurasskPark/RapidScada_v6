# PlgMimSVGAnimationJP — Examples 11–20: cycles and equipment

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/examples-equipment.md)

Source templates: `Examples/HelloWorld/01–36.svganim.json`. Open the numbered template in the designer and check its assignments. [Project setup and channel catalog](examples.md).

| No. | Example | Channels | Rule and check |
| --- | --- | --- | --- |
| 11 | Continuous rotation | 103 | 103 ≥ 0 rotates a rotor group with a 3 s period. The housing stays fixed; unknown data freezes rotation. |
| 12 | Size pulse | 103 | 103 ≥ 0 pulses with a 1.4 s period about the base scale. The center pivot prevents displacement. |
| 13 | Tank level | 103 | 103: 0…15 → fill 0…100%, growing from bottom to top. The vessel outline stays fixed. |
| 14 | Four level directions | 103 | One channel, 103: 0…15. Bottom, top, left and right fills use separate details. |
| 15 | Moving dashed flow | 103 | 103 ≥ 0 drives two opposing flows with a 0.6 s period and directions +1 / −1. Pipe outline and arrows stay fixed. |
| 16 | Signal blinking | 102 | 102 = 1 blinks opacity with a 0.8 s period; 102 = 0 restores the base appearance. Only the detail blinks. |
| 17 | Pump: independent rules | 101, 102 | 102 = 1 rotates the rotor with a 2 s period. 101 > 0.6 makes the housing red. Stopping retains the current phase. |
| 18 | Valve feedback | 110 | 110 = 1 opens the valve; 110 = 0 closes it. Operator commands change channel 110 in example 31. |
| 19 | Regulating damper | 103 | 103: 0…15 → angle 0…90°. Continuous triangle-driven positioning; housing and labels do not rotate. |
| 20 | Values and text formatting | 101, 103, 111 | 101 uses 3 decimals; 103 uses 2; 111 uses 1. The displayed result is a number, not an expression. Conditions retain raw precision. |

[All examples](examples.md) · [Simulator](simulation.md)
