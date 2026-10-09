# DrvDebug — Simulation

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/simulation.md)

Tags can work in `Decode`, `Simulate` or `DecodeAndSimulate` mode. Simulation-only tags are updated during normal polling sessions. `DecodeAndSimulate` tags use simulation as fallback if decoding fails and `SimulateOnDecodeError` is enabled.

Simulation kinds:

- `Ramp` - linear growth with step and optional cycle;
- `Sawtooth` - growth with reset value;
- `Sine` - sine wave by amplitude, bias, period and phase;
- `Square` - low/high value by period and duty cycle;
- `StringList` - cyclic or fixed enumeration of configured strings;
- `StringGenerate` - template generation with `{N}` and `{TIME}` placeholders.
