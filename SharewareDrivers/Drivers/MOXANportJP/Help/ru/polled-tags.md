# DrvMOXANportJP — Опросные теги

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/polled-tags.md)

| Группа тегов | Примеры | Описание |
| --- | --- | --- |
| Состояние устройства | `online`, `device_status` | Доступность и статус устройства |
| Идентификация устройства | `device_name`, `model`, `mac`, `firmware`, `serial_number` | Идентификация устройства |
| Сеть | `netmask`, `gateway`, `dns1`, `dns2`, `ip_config` | Сетевые параметры |
| Время работы | `uptime_days`, `uptime_hours`, `uptime_minutes` | Время работы, если устройство отдаёт эти данные |
| Настройки портов | `portN_settings`, `portN_mode`, `portN_interface`, `portN_fifo` | Параметры последовательного порта |
| Счётчики портов | `portN_tx_total`, `portN_rx_total`, `portN_inactivity_alarm` | Счётчики обмена и авария неактивности |
| TCP-порты | `portN_command_port`, `portN_data_port` | Command/Data ports для TCP Server mode |
