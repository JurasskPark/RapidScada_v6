# DrvTelnetJP — Notes

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/notes.md)

Telecontrol commands are enabled at the `DeviceLogic` level, but this driver does not implement a command processing method. The documented and implemented behavior is TCP port polling and writing input channel values.

A closed result means that a TCP connection was not established within the configured timeout. It can be caused by a closed service port, firewall rules, routing problems, DNS problems, or a service that accepts connections slowly.
