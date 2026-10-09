# MicrosoftSqlStorage — Справочник

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/reference.md)

Хранилище использует схему `project`:

| Данные | Таблица или правило именования |
| --- | --- |
| Таблицы базы конфигурации | Имена таблиц и колонок переводятся в нижний регистр |
| Конфигурация приложения (`DataCategory.Config`) | `project.app_config` |
| Хранилище приложения (`DataCategory.Storage`) | `project.app_storage` |
| Файлы представлений (`DataCategory.View`) | `project.view_file` |

Файлы приложений выбираются по `app_id` и нормализованному `path`. Файлы представлений используют путь и двоичное содержимое. Текст представлений декодируется как UTF-8. Отсутствующий файл вызывает `FileNotFoundException`.

Хранилище работает с конфигурацией и файлами. Архивы текущих и исторических данных обслуживает отдельный [модуль ModArcMicrosoftSqlJP](../../../../../../OpenModules/ModArcMicrosoftSqlJP/README.ru.md).
