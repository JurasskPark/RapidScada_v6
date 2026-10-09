# ExtDepMicrosoftSqlJP — Конфигурация

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/configuration.md)

Файл конфигурации расширения:

```text
ExtDepMicrosoftSqlJP.xml
```

Конфигурация по умолчанию:

```xml
<?xml version="1.0" encoding="utf-8"?>
<ExtDepMicrosoftSqlJP>
  <ClearBaseMethod>DropTables</ClearBaseMethod>
</ExtDepMicrosoftSqlJP>
```

Доступные методы очистки:

| Значение | Описание |
| --- | --- |
| `DropTables` | Удаляет и создаёт заново таблицы конфигурации проекта |
| `TruncateTables` | Очищает существующие таблицы конфигурации проекта |

Используйте `DropTables` для первого развёртывания или полной пересборки базы конфигурации. Используйте `TruncateTables`, когда схема уже существует и нужно обновить только данные таблиц.
