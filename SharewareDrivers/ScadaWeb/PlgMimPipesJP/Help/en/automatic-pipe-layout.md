# PlgMimPipesJP — Automatic Pipe Layout

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/automatic-pipe-layout.md)

Automatic layout is supported only by the modern Mimic JP Editor. The original Mimic Editor can place ordinary pipe components but displays a message when automatic layout is selected.

1. Select **Automatic element layout** in the **PIPES** group.
2. In the dialog, choose the pipe color, diameter, flanges and result mode. Default values are diameter `18`, gray-blue metal, flanges disabled and **Atomic elements**.
3. Click the route points on the canvas. The first point defines the `100`-pixel lattice; later points snap to horizontal, vertical or 45° diagonal steps.
4. Press `Enter` to finish the route or `Esc` to cancel drawing.
5. Use an intermediate point for a 135° or 180° direction change.

| Result mode | English description |
| --- | --- |
| **Atomic elements** | Creates separate `100 × 100` straight pipes and elbows. The route is added as one operation and one undo step. |
| **Single pipeline** | Creates one `PipeAssembly`. Select it to move, insert or delete route points. Normal component resize is disabled. |

The route supports up to 64 points. With flanges enabled, automatic layout creates one flange at each internal joint and one at each route end. The settings confirmed in the dialog are remembered until the editor is closed.
