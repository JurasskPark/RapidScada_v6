# DrvDDEJP - DDE Client Driver for Rapid SCADA v6

[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](LICENSE)
![Platform](https://img.shields.io/badge/platform-Windows-lightgrey.svg)
![.NET](https://img.shields.io/badge/.NET-8.0--windows-purple.svg)

`DrvDDEJP` provides high-performance integration between Rapid SCADA and software supporting the **Dynamic Data Exchange (DDE)** protocol. This driver enables seamless real-time data acquisition from applications such as Microsoft Excel, MetaTrader, specialized industrial terminals, and legacy Windows software.

---

# Инструкция по настройке драйвера DrvDDEJP

`DrvDDEJP` — это специализированный драйвер-клиент DDE (Dynamic Data Exchange) для среды Rapid SCADA v6. Он позволяет получать данные из любых приложений, поддерживающих механизм DDE в ОС Windows (например, Excel, торговые терминалы, специализированное промышленное или лабораторное ПО).

## Основные возможности
- **Универсальность**: Подключение к любому зарегистрированному DDE-сервису в системе.
- **Многопоточность и производительность**: Оптимизированный опрос множества тем (Topics) в рамках одного устройства.
- **Форматы данных**: Поддержка всех основных типов — логические (Bool), целые (16/32/64 бит), числа с плавающей запятой (Float/Double) и строки (Ascii/Unicode/Hex).
- **Отказоустойчивость**: Автоматическое переподключение при сбоях, настраиваемые таймауты и задержки.
- **Диагностика**: Ведение подробного журнала работы, интегрированного в стандартную систему логирования Scada Communicator.
- **Интерфейс**: Полная поддержка русского и английского языков в конфигураторе и документации.

## 1. Настройка драйвера
Настройка осуществляется через форму конфигурации в интерфейсе Администратора Rapid SCADA.

### Параметры соединения
- **Service Name**: Системное имя DDE-сервиса (например, `Excel`).
- **Default Topic**: Тема по умолчанию (например, `Sheet1`). Используется для тегов, у которых не задан собственный Topic.
- **Request Timeout**: Время ожидания ответа от DDE-сервера (мс).
- **Reconnect Delay**: Пауза перед попыткой восстановления связи после ошибки (мс).

### Журналирование
- **Write Log**: Включает запись сообщений драйвера в лог-файл линии связи.
- **Log Type**: Позволяет выбрать уровень детализации (Action, Info, Warning, Error).

## 2. Конфигурация тегов
Каждый тег устройства в SCADA связывается с определенным элементом (Item) DDE-сервера.

### Параметры тега
- **Имя (Name)**: Название тега для идентификации.
- **Канал (Channel)**: Номер канала в системе Rapid SCADA.
- **Topic**: (Опционально) Индивидуальный топик тега, перекрывающий Default Topic.
- **Item Name**: Имя конкретного элемента данных (например, `R1C1` для Excel или имя тикера в торговом терминале).
- **Data Format**: Тип данных тега.
- **Data Length**: Длина буфера для строковых типов данных.

## 3. Системные требования
- **ОС**: Windows (DDE является эксклюзивной технологией Windows).
- **Среда**: .NET 8.0-windows.
- **Rapid SCADA**: Версия 6.x.

---

# DrvDDEJP Configuration Guide

`DrvDDEJP` is a dedicated DDE (Dynamic Data Exchange) client driver for Rapid SCADA v6. It enables real-time data acquisition from any application that supports the DDE protocol on Windows (e.g., MS Excel, trading terminals, specialized industrial or lab software).

## Key Features
- **Universal Connectivity**: Connects to any DDE service registered in the operating system.
- **Performance**: Optimized polling of multiple Topics within a single device context.
- **Data Types**: Full support for Boolean, Integer (16/32/64 bit), Floating point (Float/Double), and String (Ascii/Unicode/Hex) formats.
- **Reliability**: Automatic reconnection, configurable timeouts, and retry delays.
- **Diagnostics**: Detailed logging integrated with the standard Rapid SCADA Communicator logging system.
- **Multilingual**: Comprehensive English and Russian support in the UI and documentation.

## 1. Driver Configuration
Configure the driver using the built-in properties form in the Rapid SCADA Administrator interface.

### Connection Parameters
- **Service Name**: The system name of the DDE service (e.g., `Excel`).
- **Default Topic**: The default topic (e.g., `Sheet1`). Used for tags that don't specify their own Topic.
- **Request Timeout**: Max time to wait for a DDE server response (ms).
- **Reconnect Delay**: Delay before attempting to reconnect after a link failure (ms).

### Logging
- **Write Log**: Enables driver message recording in the communication line log file.
- **Log Type**: Controls message verbosity (Action, Info, Warning, Error).

## 2. Tag Configuration
Each SCADA device tag is mapped to a specific Item on the DDE server.

### Tag Parameters
- **Name**: Identifying tag name.
- **Channel**: Associated Rapid SCADA channel number.
- **Topic**: (Optional) Overrides the Default Topic for this specific tag.
- **Item Name**: Name of the specific data point (e.g., `R1C1` for Excel or a symbol name in trading software).
- **Data Format**: Internal data representation for the tag.
- **Data Length**: Buffer size for string-based data types.

## 3. System Requirements
- **OS**: Windows (DDE is a Windows-only technology).
- **Runtime**: .NET 8.0-windows.
- **Rapid SCADA**: Version 6.x.

---

## SAST Tools

[PVS-Studio](https://pvs-studio.com/en/pvs-studio/?utm_source=website&utm_medium=github&utm_campaign=open_source) - static analyzer for C, C++, C#, and Java code.
