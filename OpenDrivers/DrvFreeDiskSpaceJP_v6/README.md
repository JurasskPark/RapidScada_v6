# DrvFreeDiskSpaceJP

![DrvFreeDiskSpaceJP](https://img.shields.io/github/downloads/JurasskPark/RapidScada_v6/DrvFreeDiskSpaceJP_v6.4.0.4/total)

![Rapid SCADA](https://img.shields.io/badge/Rapid%20SCADA-6.x-blue.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![Platform](https://img.shields.io/badge/platform-Windows%20%7C%20Linux-lightgrey.svg)
[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](https://www.apache.org/licenses/LICENSE-2.0)

## Overview / Обзор

DrvFreeDiskSpaceJP is a Rapid SCADA 6 driver for monitoring free disk space and automatically reacting when the free space on a selected drive drops below a configured threshold.

The driver creates one or more tasks. Each task checks a drive, publishes disk status tags to Rapid SCADA, and can optionally delete old folders or compress and move them to another directory.

DrvFreeDiskSpaceJP - драйвер Rapid SCADA 6 для контроля свободного места на дисках и автоматической реакции, когда свободное место на выбранном носителе становится ниже заданного порога.

Драйвер создает одну или несколько задач. Каждая задача проверяет диск, передает теги состояния диска в Rapid SCADA и при необходимости удаляет старые каталоги либо сжимает и переносит их в другой каталог.

## Features / Возможности

English:

- Monitors local drives available to the machine where `ScadaComm` is running.
- Supports multiple independent tasks in one device configuration.
- Publishes disk name, drive type, volume label, total size, used size, free-space threshold, current free-space percentage, alarm status, selected action and last action date.
- Generates Rapid SCADA channel prototypes from enabled tasks.
- Provides a configuration form in ScadaAdmin and a validation button for checking a task before using it in production.
- Can run the communication logic on Windows and Linux. The configuration UI is Windows Forms based.

Русский:

- Контролирует локальные диски, доступные на компьютере, где работает `ScadaComm`.
- Поддерживает несколько независимых задач в одной конфигурации КП.
- Передает название диска, тип носителя, метку тома, общий размер, занятый размер, уставку свободного места, текущий процент свободного места, статус аварии, выбранное действие и дату последнего действия.
- Создает прототипы каналов Rapid SCADA по включенным задачам.
- Предоставляет форму настройки в ScadaAdmin и кнопку проверки задачи перед использованием в рабочем проекте.
- Логическая часть драйвера может работать на Windows и Linux. Интерфейс настройки основан на Windows Forms.

## How It Works / Как это работает

English:

During each communication session the driver uses the loaded task list, processes enabled tasks sequentially, and writes tag values to Rapid SCADA. The default polling period returned by the driver view is 5 seconds.

For each task the driver reads `DriveInfo` for the configured `DiskName`, calculates the current free-space percentage as `AvailableFreeSpace / TotalSize * 100`, and compares it with `ProceentFreeSpace`. If the current percentage is greater than the configured threshold, `StatusAlarm` is set to `0`. If the current percentage is less than or equal to the threshold, `StatusAlarm` is set to `1` and the configured action is executed.

Automatic cleanup scans the configured `Path` recursively and processes only date-named archive folders whose names contain `MIN`, `HOUR`, or `DAY` plus a date in `yyyyMMdd` format. Folders named exactly `CUR`, `MIN`, `HOUR`, or `DAY` are skipped. Matching folders are sorted by date from oldest to newest, and the driver continues deleting or archiving folders until the free-space percentage rises above the threshold.

Русский:

Во время каждого сеанса связи драйвер использует загруженный список задач, последовательно выполняет включенные задачи и записывает значения тегов в Rapid SCADA. Период опроса по умолчанию, который возвращает View-часть драйвера, равен 5 секундам.

Для каждой задачи драйвер читает `DriveInfo` по заданному `DiskName`, рассчитывает текущий процент свободного места как `AvailableFreeSpace / TotalSize * 100` и сравнивает его с `ProceentFreeSpace`. Если текущий процент больше заданного порога, тег `StatusAlarm` получает значение `0`. Если текущий процент меньше или равен порогу, тег `StatusAlarm` получает значение `1` и выполняется выбранное действие.

Автоматическая очистка рекурсивно просматривает заданный `Path` и обрабатывает только архивные каталоги с датой в имени: имя должно содержать `MIN`, `HOUR` или `DAY` и дату в формате `yyyyMMdd`. Каталоги с именами ровно `CUR`, `MIN`, `HOUR` или `DAY` пропускаются. Найденные каталоги сортируются по дате от старых к новым, и драйвер продолжает удаление или архивацию, пока процент свободного места не станет выше порога.

## Task Actions / Действия задач

| Action | English | Русский |
|---|---|---|
| `None` | Only monitors disk space and sets tags. No files are changed. | Только контролирует место на диске и устанавливает теги. Файлы не изменяются. |
| `Delete` | Deletes matching old folders from `Path` until the disk has enough free space. | Удаляет подходящие старые каталоги из `Path`, пока на диске не станет достаточно свободного места. |
| `CompressMove` | Creates a ZIP archive for each matching folder, moves the archive to `PathTo`, then deletes the original folder. | Создает ZIP-архив для каждого подходящего каталога, переносит архив в `PathTo`, затем удаляет исходный каталог. |

## Configuration / Настройка

English:

1. Add `DrvFreeDiskSpaceJP` to a communication line in ScadaAdmin.
2. Open the device properties.
3. Add one or more tasks.
4. For each task, set the task name, description, drive name, free-space percentage threshold, source path and action.
5. For `CompressMove`, also set the destination path for ZIP archives.
6. Use the validation button to check the task and review the generated values and log messages.
7. Save the configuration, generate channel prototypes, upload the project and restart the communication line.

Русский:

1. Добавьте `DrvFreeDiskSpaceJP` в линию связи в ScadaAdmin.
2. Откройте свойства КП.
3. Добавьте одну или несколько задач.
4. Для каждой задачи задайте имя, описание, имя диска, порог свободного места в процентах, исходный путь и действие.
5. Для действия `CompressMove` также задайте путь назначения для ZIP-архивов.
6. Используйте кнопку проверки, чтобы протестировать задачу и посмотреть полученные значения и сообщения журнала.
7. Сохраните конфигурацию, создайте прототипы каналов, загрузите проект и перезапустите линию связи.

## Configuration File / Файл конфигурации

English:

The driver stores settings in the Rapid SCADA configuration directory. The file name is generated from the driver code and device number by Rapid SCADA, for example `DrvFreeDiskSpaceJP_001.xml`.

The main XML blocks are:

- `ListTask` - task list.
- `DeviceTags` - saved tag metadata.
- `DebugerSettings` - logging options.
- `LanguageIsRussian` - language flag used by the configuration tool.

Русский:

Драйвер хранит настройки в каталоге конфигурации Rapid SCADA. Имя файла формируется по коду драйвера и номеру КП средствами Rapid SCADA, например `DrvFreeDiskSpaceJP_001.xml`.

Основные XML-блоки:

- `ListTask` - список задач.
- `DeviceTags` - сохраненные метаданные тегов.
- `DebugerSettings` - параметры журналирования.
- `LanguageIsRussian` - признак языка, используемый утилитой настройки.

## Generated Tags / Создаваемые теги

For every enabled task named `<TaskName>`, the driver creates the following tag codes.

Для каждой включенной задачи с именем `<TaskName>` драйвер создает следующие коды тегов.

| Tag code | Data | Данные |
|---|---|---|
| `DriverName_<TaskName>` | Drive name, for example `C:\`. | Имя диска, например `C:\`. |
| `DriverType_<TaskName>` | Drive type from .NET `DriveInfo`. | Тип носителя из .NET `DriveInfo`. |
| `DriverVolumeLabel_<TaskName>` | Volume label. | Метка тома. |
| `DriverTotalSize_<TaskName>` | Total drive size in bytes. | Общий размер диска в байтах. |
| `DriverTotalSizeString_<TaskName>` | Total drive size as a formatted string. | Общий размер диска в виде строки. |
| `DriverCurrentSize_<TaskName>` | Used drive size in bytes. | Занятый размер диска в байтах. |
| `DriverCurrentSizeString_<TaskName>` | Used drive size as a formatted string. | Занятый размер диска в виде строки. |
| `PercentFreeSpaceSetPoint_<TaskName>` | Configured free-space threshold. | Заданная уставка свободного места. |
| `PercentFreeSpaceCurrent_<TaskName>` | Current free-space percentage. | Текущий процент свободного места. |
| `StatusAlarm_<TaskName>` | `0` when free space is above the threshold, `1` when cleanup condition is active. | `0`, если свободное место выше порога; `1`, если условие очистки активно. |
| `ActionTask_<TaskName>` | Configured action name. | Название выбранного действия. |
| `ActionDate_<TaskName>` | UTC time when the alarm/action condition was detected or updated. | UTC-время, когда было обнаружено или обновлено условие аварии/действия. |

## Safety Notes / Замечания по безопасности

English:

- `Delete` and `CompressMove` can permanently remove directories. Test every task with non-critical data before enabling it on production paths.
- The driver catches many file-operation exceptions without stopping the communication line. Check the driver log when cleanup does not produce the expected result.
- `CompressMove` requires a valid `PathTo` directory and enough permissions to create archives, move files and delete original folders.
- The driver acts with the permissions of the Rapid SCADA communication service process.
- Use unique task names because tag codes are built from the task name.

Русский:

- `Delete` и `CompressMove` могут безвозвратно удалять каталоги. Проверяйте каждую задачу на некритичных данных перед включением на рабочих путях.
- Драйвер перехватывает многие исключения файловых операций без остановки линии связи. Если очистка сработала не так, как ожидалось, проверяйте журнал драйвера.
- Для `CompressMove` нужен корректный каталог `PathTo` и права на создание архивов, перемещение файлов и удаление исходных каталогов.
- Драйвер выполняет действия с правами процесса службы связи Rapid SCADA.
- Используйте уникальные имена задач, потому что коды тегов строятся из имени задачи.

## Project Structure / Структура проекта

| Path | English | Русский |
|---|---|---|
| `DrvFreeDiskSpaceJP.Logic` | Runtime driver logic loaded by `ScadaComm`. | Исполняемая логика драйвера, загружаемая `ScadaComm`. |
| `DrvFreeDiskSpaceJP.View` | Windows Forms configuration UI for ScadaAdmin. | Windows Forms-интерфейс настройки для ScadaAdmin. |
| `DrvFreeDiskSpaceJP.Shared` | Shared settings, task model, tag generation and helper code. | Общие настройки, модель задач, генерация тегов и служебный код. |
| `DrvFreeDiskSpaceJP.Winform` | Standalone WinForms host used for development and testing. | Отдельная WinForms-обертка для разработки и проверки. |
| `StartСompiling.bat` | Publishes release files for Windows and Linux layouts. | Публикует релизные файлы для структур Windows и Linux. |

## Build / Сборка

English:

The solution targets .NET 8. The logic project uses `net8.0`, and the configuration UI uses `net8.0-windows`.

The supplied `StartСompiling.bat` publishes:

- `win-x32` logic and view files;
- `win-x64` logic and view files;
- `linux-x64` logic files, with the ScadaAdmin files copied from the Windows build layout.

Русский:

Решение рассчитано на .NET 8. Проект логики использует `net8.0`, а интерфейс настройки использует `net8.0-windows`.

Поставляемый `StartСompiling.bat` публикует:

- файлы логики и View для `win-x32`;
- файлы логики и View для `win-x64`;
- файлы логики для `linux-x64`, при этом файлы ScadaAdmin копируются из структуры Windows-сборки.

## Screenshots / Скриншоты

![DrvFreeDiskSpaceJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/OpenDrivers/Source/DrvFreeDiskSpaceJP_001.png)

![DrvFreeDiskSpaceJP](https://raw.githubusercontent.com/JurasskPark/RapidScada_v6/refs/heads/master/OpenDrivers/Source/DrvFreeDiskSpaceJP_002.png)

## License / Лицензия

This project is part of the Rapid SCADA ecosystem and is distributed under the Apache License 2.0.

Данный проект является частью экосистемы Rapid SCADA и распространяется под лицензией Apache License 2.0.

## SAST Tools

[PVS-Studio](https://pvs-studio.ru/ru/pvs-studio/?utm_source=website&utm_medium=github&utm_campaign=open_source) - static analyzer for C, C++, C#, and Java code.
