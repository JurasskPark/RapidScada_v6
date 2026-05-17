# DrvMOXANportJP

![DrvMOXANportJP](https://img.shields.io/badge/Rapid%20SCADA-6.4-blue.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![Platform](https://img.shields.io/badge/platform-Windows-lightgrey.svg)

**MOXA NPort JP** — Rapid SCADA driver for monitoring and administering MOXA NPort serial device servers.  
**MOXA NPort JP** — драйвер Rapid SCADA для контроля и администрирования сетевых преобразователей MOXA NPort.

> This project targets **MOXA NPort** devices. It is intentionally separated from MOXA MGate logic because NPort and MGate command sets differ.
>
> Проект предназначен именно для **MOXA NPort**. Логика отделена от MOXA MGate, потому что наборы команд NPort и MGate отличаются.

## Overview / Обзор

DrvMOXANportJP polls MOXA NPort devices over the network, creates Rapid SCADA tags and channel prototypes, and provides administration actions through the ScadaAdmin UI.

Драйвер опрашивает устройства MOXA NPort по сети, создаёт теги и прототипы каналов Rapid SCADA, а также предоставляет административные действия через интерфейс ScadaAdmin.

## Features / Возможности

- **Network monitoring** — checks device availability and online state
- **Device information** — reads model, MAC address, firmware version, serial number and device status
- **Network settings** — reads netmask, gateway, DNS and IP configuration mode
- **Uptime** — reads system uptime where supported by the model and firmware
- **Serial port data** — reads port settings, interface type, FIFO state, operation mode, command/data ports and RX/TX counters
- **Inactivity alarm** — detects port inactivity by RX/TX counter changes
- **Device discovery** — scans IP ranges and reads MOXA identity data
- **Firmware workflow** — validates firmware images and uploads firmware via the vendor DSCI library
- **Configuration export** — saves device configuration files for backup and administration
- **Device control** — supports locate/beep and device restart operations
- **Command diagnostics** — provides a diagnostic command catalog with read-only, state-change and research commands separated
- **License support** — includes About and activation request export forms
- **Password protection** — stores MOXA passwords in encrypted form in the driver XML configuration
- **Localization** — English and Russian language files are included

- **Контроль сети** — проверка доступности устройства и состояния online
- **Информация об устройстве** — чтение модели, MAC-адреса, версии прошивки, серийного номера и статуса
- **Сетевые настройки** — чтение маски, шлюза, DNS и режима IP-конфигурации
- **Uptime** — чтение времени работы, если это поддерживается моделью и прошивкой
- **Данные COM-портов** — чтение настроек порта, интерфейса, FIFO, режима работы, command/data ports и счётчиков RX/TX
- **Авария неактивности** — контроль остановки обмена по изменению RX/TX счётчиков
- **Поиск устройств** — сканирование диапазона IP и чтение идентификационных данных MOXA
- **Прошивка** — проверка файла прошивки и загрузка через библиотеку производителя DSCI
- **Экспорт конфигурации** — сохранение конфигураций устройств для резервного копирования и администрирования
- **Управление устройством** — locate/beep и перезагрузка устройства
- **Диагностика команд** — каталог команд с разделением read-only, state-change и research команд
- **Лицензирование** — формы About и экспорта запроса активации
- **Защита паролей** — пароль MOXA хранится в XML-конфигурации в зашифрованном виде
- **Локализация** — включены английские и русские языковые файлы

## Polled Tags / Опросные теги

| Tag group | Examples | Описание |
|---|---|---|
| Device state | `online`, `device_status` | Доступность и статус устройства |
| Device identity | `device_name`, `model`, `mac`, `firmware`, `serial_number` | Идентификация устройства |
| Network | `netmask`, `gateway`, `dns1`, `dns2`, `ip_config` | Сетевые параметры |
| Uptime | `uptime_days`, `uptime_hours`, `uptime_minutes` | Время работы, если устройство отдаёт эти данные |
| Port settings | `portN_settings`, `portN_mode`, `portN_interface`, `portN_fifo` | Параметры последовательного порта |
| Port counters | `portN_tx_total`, `portN_rx_total`, `portN_inactivity_alarm` | Счётчики обмена и авария неактивности |
| TCP ports | `portN_command_port`, `portN_data_port` | Command/Data ports для TCP Server mode |

## Firmware Support / Поддержка прошивок

The driver checks firmware images before upload. Supported NPort signatures include:

- modern NPort firmware headers matching `NP[digits]K`, for example `NP6K`;
- legacy NPort firmware headers such as `*FRM`, where the model family can be detected from the file content or name.

Драйвер выполняет первичную проверку файла прошивки перед загрузкой. Поддерживаются:

- современные заголовки NPort вида `NP[цифры]K`, например `NP6K`;
- старые заголовки NPort вида `*FRM`, если семейство модели можно определить по содержимому или имени файла.

The upload itself is performed by the vendor DSCI library.  
Сама загрузка выполняется библиотекой производителя DSCI.

## Project Structure / Структура проекта

```text
DrvMOXANportJP/
├── DrvMOXANportJP.sln                    # Solution file
├── StartСompiling.bat                    # Build and prepare Publish folder
├── ProtectWithReactor.bat                # Build, publish and protect DLLs with .NET Reactor
├── README.md                             # This file
│
├── DrvMOXANportJP.Logic/                 # Runtime driver for ScadaComm
│   ├── DrvMOXANportJP.Logic.csproj
│   └── DevMOXANportJPLogic.cs            # Driver logic entry point
│
├── DrvMOXANportJP.View/                  # ScadaAdmin driver UI
│   ├── DrvMOXANportJP.View.csproj
│   ├── DrvMOXANportJP.View.cs            # Driver view entry point
│   ├── Forms/                            # Configuration, device, search and command forms
│   └── Lang/                             # English and Russian language XML files
│
├── DrvMOXANportJP.Shared/                # Shared code used by Logic, View and tools
│   ├── Moxa/                             # MOXA UDP, DSCI, firmware and command logic
│   ├── Ping/                             # Network scan and polling helpers
│   ├── Project/                          # Driver XML configuration
│   └── Configuration/                    # Rapid SCADA channel prototypes
│
├── DrvMOXANportJP.WinForm/               # Standalone test/admin UI build
├── DrvMOXANportJP.Debug/                 # Console diagnostics and protocol research
├── DrvMOXANportJP.DsciProbe/             # DSCI probing utility
│
├── Libraries/                            # Rapid SCADA, LicenseJP and MOXA native DLLs
├── Publish/                              # Generated publish output
├── Release/                              # Release package output
├── samples/                              # Vendor examples and protocol samples
├── protocol/                             # Protocol notes and research materials
└── screen/                               # Screenshots
```

## Build and Deploy / Сборка и развёртывание

### Prerequisites / Требования

- .NET 8.0 SDK
- Rapid SCADA 6.4
- Windows
- MOXA native libraries in `Libraries`: `Nport.dll`, `DSCI.dll`, `DSCI64.dll`

## Usage / Использование

1. Add the driver `DrvMOXANportJP` to a communication line.
2. Open device properties in ScadaAdmin.
3. Add NPort devices manually or scan an IP range.
4. Configure credentials, timeout and inactivity timeout.
5. Generate channel prototypes for the configured devices.
6. Start the communication line in ScadaComm.

1. Добавьте драйвер `DrvMOXANportJP` в линию связи.
2. Откройте свойства устройства в ScadaAdmin.
3. Добавьте NPort вручную или через сканирование диапазона IP.
4. Настройте учётные данные, таймаут и таймаут неактивности.
5. Создайте прототипы каналов для добавленных устройств.
6. Запустите линию связи в ScadaComm.

## Safety Notes / Замечания по безопасности

- Firmware upload and restart operations are executed through the vendor DSCI library.
- Configuration export is safe for mass backup.
- Configuration import should be used only for a single explicitly selected device, because a copied configuration can contain network settings.
- Diagnostic commands are separated by kind: read-only, state-change and research.
- Some devices or firmware versions may not return all fields. For example, NP6250 firmware 2.3 returns no payload for the UDP uptime command `0x56`; in this case uptime tags remain inactive.

- Прошивка и перезагрузка выполняются через библиотеку производителя DSCI.
- Экспорт конфигурации безопасен для массового резервного копирования.
- Импорт конфигурации следует выполнять только для одного явно выбранного устройства, потому что конфигурация может содержать сетевые настройки.
- Диагностические команды разделены по типам: read-only, state-change и research.
- Некоторые модели или версии прошивок могут не отдавать отдельные поля. Например, NP6250 firmware 2.3 не возвращает payload для UDP-команды uptime `0x56`; в этом случае теги uptime остаются неактивными.

## Screenshots / Скриншоты

![DrvMOXANportJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/Drivers/MOXANportJP/DrvMOXANportJP_001.png)
![DrvMOXANportJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/Drivers/MOXANportJP/DrvMOXANportJP_002.png)
![DrvMOXANportJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/Drivers/MOXANportJP/DrvMOXANportJP_003.png)
![DrvMOXANportJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/SharewareDrivers/Drivers/MOXANportJP/DrvMOXANportJP_004.png)


## License / Лицензия

This project is part of the Rapid SCADA ecosystem.  
Данный проект является частью экосистемы Rapid SCADA.

## SAST Tools

[PVS-Studio](https://pvs-studio.ru/ru/pvs-studio/?utm_source=website&utm_medium=github&utm_campaign=open_source) - static analyzer for C, C++, C#, and Java code.
