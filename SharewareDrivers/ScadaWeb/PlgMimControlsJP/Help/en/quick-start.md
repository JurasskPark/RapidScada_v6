# PlgMimControlsJP — Quick Start

[Contents](index.md) · [Product](../../Readme.md) · [Русский](../ru/quick-start.md)

1. Install and enable `PlgMimControlsJP`, then restart SCADA Web. A component license is not required for editing.
2. Open a mimic in a compatible Mimic Editor.
3. Select a component from the **CONTROLS** group and place it on the canvas.
4. For a display component, set its input channel.
5. For a command component, configure its command output and the feedback, ready-state or permit channels it exposes; see its description below.
6. Configure captions, values, colors, ranges or options required by that component.
7. Install the server-side license for ordinary controls, save the mimic, transfer the project to runtime and open it in Webstation.
8. Verify the operator has control rights and the output channel accepts commands.
9. Send a command and check that the device writes the resulting state back to the input channel.

Commands are intentionally disabled in edit mode. A command component does not switch its confirmed state immediately after a click: it waits for input-channel feedback. Input and output channel numbers may be different.
