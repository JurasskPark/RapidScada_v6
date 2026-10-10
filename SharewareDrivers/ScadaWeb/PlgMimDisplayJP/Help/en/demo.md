# PlgMimDisplayJP — Autonomous demonstration

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/demo.md)

`DisplayDemo` is one serialized component containing six private display samples. It works without a runtime component license and has no live channel bindings, scripts, cell actions or real operator APIs.

In the editor it shows a static English preview. Move, resize, copy, delete and save use normal Mimic operations. Only shell properties are accepted from a saved document: identity, position/size, visibility, enabled state and tooltip. The initial size is 960 × 1070 px.

At runtime each instance runs its own local simulation. Level changes update numeric displays, the total counter, table level/temperature and conditional text. Each sample shows its simulated input and quality.

Local inputs let you change level **0–100%**, quality, segment style and marquee text. Overrides survive timer ticks until reset or until the corresponding selector returns to Automatic. The automatic cycle demonstrates all three segment styles, good/bad/missing quality, moving wheels, scrolling text and conditional state colors. Input and generated values are not saved.

Demo phrases default to English even with a Russian host; EN/RU phrases come from plugin dictionaries. Palette labels follow the host language. Long marquee input wraps beside the sample; unchanged marquee text does not restart animation on every numeric update.

Each instance owns one timer. Removing, disabling or hiding it, entering edit mode or removing an ancestor DOM node releases its timer and renderer animations. Copies keep independent state. Reduced-motion settings disable animations while synthetic readouts continue updating.

The [PNG gallery](screenshots.md) shows separate local host examples. Their additional test controls belong to those diagrams; they are not part of the read-only display components or of `DisplayDemo`.
