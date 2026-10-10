# PlgMimSVGAnimationJP — Examples 31–36: actions and reuse

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/examples-interaction.md)

Source templates: `Examples/HelloWorld/01–36.svganim.json`. Open the numbered template in the designer and check its assignments. [Project setup and channel catalog](examples.md).

| No. | Example | Channels | Rule and check |
| --- | --- | --- | --- |
| 31 | Relay commands 0 / 1 | 110, 110 (command) | ON / OFF menu sends to 110. On first use open the initial-value dialog and enter 0. The indicator follows input feedback 110. |
| 32 | Analog output | 111, 111 (command) | Menu values 0 / 5 / 10 target 111. On first use open the initial-value dialog and enter 0. The 0…10 scale displays feedback 111. |
| 33 | Chart on click | 101, 103 | Each button opens the channel chart through the standard SCADA mechanism. In the simulator the click only reaches the log. |
| 34 | Views and links |  | View 2 is the Simulator table. The second button is an HTTP link to the mimic list. Navigation occurs only after a click. |
| 35 | Path and gradient | 101 | Edit the path with the &lt;/&gt; button. 101: −1…+1 → scale 70…130%. The gradient needs no external files. |
| 36 | Template remapping | 201, 200, 202 | 200 is sine, 201 discrete and 202 level. The same shapes use another mapping. Change channel numbers in one table. |

[All examples](examples.md) · [Simulator](simulation.md)
