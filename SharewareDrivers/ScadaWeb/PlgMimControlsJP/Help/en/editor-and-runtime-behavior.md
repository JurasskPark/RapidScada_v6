# PlgMimControlsJP — Editor and Runtime Behavior

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/editor-and-runtime-behavior.md)

- Every selected component has a lime editor outline around its complete external size, including controls with clipped or scrollable content.
- Interactive child elements do not intercept moving and resizing in edit mode.
- Runtime values and pending markers are transient and are not saved to `.mim` files.
- Commands are never sent in edit mode.
- The component waits for real input feedback and does not write a new confirmed visual state optimistically.
