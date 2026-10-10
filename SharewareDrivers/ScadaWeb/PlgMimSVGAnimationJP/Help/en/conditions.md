# PlgMimSVGAnimationJP — Conditions, priorities and delays

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/conditions.md)

A condition is a flat list of terms with `join = all` (AND) or `join = any` (OR). Each term uses a channel role and an operator.

| Operator | Match |
| --- | --- |
| `=` / `!=` | Equal / not equal |
| `>` / `>=` | Greater / greater or equal |
| `<` / `<=` | Less / less or equal |
| `between` | Inclusive interval from `value` to `upper` |
| `outside` | Outside that interval, excluding its boundaries |

Comparisons use finite raw numbers. A rounded display of `0` does not imply that the underlying value equals `0`. Unknown data is not converted to zero.

## Ordered variants

Evaluate variants from top to bottom. Put alarm conditions before normal-operation conditions if both can be true. An unknown higher-priority variant prevents selecting a lower variant: the result is unknown.

For `all`, a false term makes the group false even when another term is unknown. For `any`, a true term makes the group true despite another unknown term. Otherwise unresolved terms keep the result unknown.

## Hysteresis

`hysteresis` applies only when leaving an already active threshold. For `>` / `>=`, the exit threshold is reduced by the band; for `<` / `<=`, it is increased. Entry uses the original threshold. Equality and interval operators do not use this band.

Example: `value > 0` with `hysteresis = 0.2` enters at `0.1`, remains active at `-0.1` and exits at `-0.21`.

## Delays

`onDelay` and `offDelay` are seconds and apply to the entire condition group. They delay subsequent true/false transitions; the first known state and the first known state after data is restored are accepted immediately. Unknown data does not produce a false condition or an artificial transition.

A group can contain up to `100` terms and a card up to `100` variants. Default condition: `join = all`, operator `>`, `value = 50`, `upper = 100`, zero hysteresis and delays; a role must be assigned.

[Quality](quality.md) · [Condition examples](examples-conditions.md)
