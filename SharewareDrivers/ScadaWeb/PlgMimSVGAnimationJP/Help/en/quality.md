# PlgMimSVGAnimationJP — Data quality and missing values

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/quality.md)

A sample is usable when its numeric value is finite and its channel status is positive. A valid zero is data; missing, nonnumeric or invalid-status samples are unknown.

If any animation card of a node cannot evaluate, the node retains its last displayed result and stops motion. It receives a no-data mark; unrelated nodes keep working. The symbol shows a no-data count and affected details receive an exclamation mark. Accessible names retain the quality indication even for hidden or transparent details.

An unknown high-priority condition cannot fall through to a lower variant. A channel used only as a command role does not itself make the drawing unknown. Data roles, configured feedback and action conditions are checked according to their purpose.

Actions are unavailable when the affected node, its descendants or ancestors have unresolved data, or when configured feedback is invalid. A false or unknown availability condition also disables an action.

Use the simulator to test missing samples and restoration, not only normal numeric ranges. Verify that cycles freeze, no-data marks appear, independent nodes continue and commands remain unavailable as expected.

[Conditions](conditions.md) · [Actions](actions.md) · [Simulator](simulation.md)
