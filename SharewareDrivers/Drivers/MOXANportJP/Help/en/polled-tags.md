# DrvMOXANportJP — Polled Tags

[Contents](index.md) · [Product](../../README.md) · [Русский](../ru/polled-tags.md)

| Tag group | Examples | Description |
| --- | --- | --- |
| Device state | `online`, `device_status` | Device availability and status |
| Device identity | `device_name`, `model`, `mac`, `firmware`, `serial_number` | Device identity |
| Network | `netmask`, `gateway`, `dns1`, `dns2`, `ip_config` | Network settings |
| Uptime | `uptime_days`, `uptime_hours`, `uptime_minutes` | Uptime, if reported by the device |
| Port settings | `portN_settings`, `portN_mode`, `portN_interface`, `portN_fifo` | Serial port settings |
| Port counters | `portN_tx_total`, `portN_rx_total`, `portN_inactivity_alarm` | Transfer counters and inactivity alarm |
| TCP ports | `portN_command_port`, `portN_data_port` | Command/data ports in TCP Server mode |
