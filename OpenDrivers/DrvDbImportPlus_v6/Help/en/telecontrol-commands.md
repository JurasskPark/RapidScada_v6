# DrvDbImportPlus — Telecontrol Commands

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/telecontrol-commands.md)

The driver can receive Rapid SCADA telecontrol commands. A command is matched by command code or command number against enabled export commands and, as a fallback, import command definitions. When a matching database command is found, the driver:

1. Initializes the configured data source.
2. Adds or updates the database command parameter `cmdVal`.
3. Uses `CmdVal` for numeric commands, or converts `CmdData` to string for binary/string commands.
4. Executes the configured SQL command with `ExecuteNonQuery`.
5. Updates the matching command tag in Rapid SCADA if a tag with the same command code exists.

Example:

```sql
insert into operator_commands(command_code, command_value, created_at)
values ('PUMP_SETPOINT', @cmdVal, current_timestamp);
```

Use the parameter syntax supported by the selected database provider. The driver creates the logical parameter name `cmdVal`; the provider decides the final placeholder style.
