# DrvFreeDiskSpaceJP — Создаваемые теги

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/generated-tags.md)

Для каждой включенной задачи с именем `<TaskName>` драйвер создает следующие коды тегов.

| Код тега | Данные |
| --- | --- |
| `DriverName_<TaskName>` | Имя диска, например `C:\`. |
| `DriverType_<TaskName>` | Тип носителя из .NET `DriveInfo`. |
| `DriverVolumeLabel_<TaskName>` | Метка тома. |
| `DriverTotalSize_<TaskName>` | Общий размер диска в байтах. |
| `DriverTotalSizeString_<TaskName>` | Общий размер диска в виде строки. |
| `DriverCurrentSize_<TaskName>` | Занятый размер диска в байтах. |
| `DriverCurrentSizeString_<TaskName>` | Занятый размер диска в виде строки. |
| `PercentFreeSpaceSetPoint_<TaskName>` | Заданная уставка свободного места. |
| `PercentFreeSpaceCurrent_<TaskName>` | Текущий процент свободного места. |
| `StatusAlarm_<TaskName>` | `0`, если свободное место выше порога; `1`, если условие очистки активно. |
| `ActionTask_<TaskName>` | Название выбранного действия. |
| `ActionDate_<TaskName>` | UTC-время, когда было обнаружено или обновлено условие аварии/действия. |
