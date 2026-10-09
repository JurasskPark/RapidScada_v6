# PlgMimElectricJP — Optional toolbox groups

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/configuration.md)

Six groups are always available in the editor. The `fire`, `security` and `communications` groups are optional and hidden by default.

Create `ScadaWeb/config/PlgMimElectricJP.xml`. This example enables only fire automation:

```xml
<?xml version="1.0" encoding="utf-8"?>
<PlgMimElectricJP>
  <Packages>
    <Package id="fire" enabled="true" />
    <Package id="security" enabled="false" />
    <Package id="communications" enabled="false" />
  </Packages>
</PlgMimElectricJP>
```

Set `enabled="true"` for each required optional group, then restart Webstation and reopen the editor. These are the only three recognized optional package identifiers; their matching is case-insensitive. Boolean values use `true` or `false`.

If the file is absent, all three optional groups are hidden. If loading fails, the plugin logs the error and uses the same defaults. External XML entities and DTDs are prohibited.

The switches control toolbox visibility only. Factories and renderers for all 101 types remain registered, so disabling a group does not remove its components from saved mimics. An enabled group still needs a server license for ordinary runtime execution.

In a host that supplies its own component configuration, use that host's integration settings; the path above is the Webstation configuration path.
