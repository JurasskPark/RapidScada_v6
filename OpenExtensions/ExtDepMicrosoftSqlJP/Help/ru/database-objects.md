# ExtDepMicrosoftSqlJP — Объекты БД

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/database-objects.md)

ExtDepMicrosoftSqlJP создаёт и заполняет объекты конфигурации проекта в схеме SQL Server:

```sql
project
```

Типовые объекты включают таблицы конфигурации, представления, внешние ключи и записи конфигурации приложений. Примеры таблиц:

```text
project.app
project.archive
project.cnl
project.comm_line
project.device
project.format
project.obj
project.quantity
project.role
project.unit
```

Это расширение не записывает runtime-архивы. Runtime-данные архивов записывает `ModArcMicrosoftSqlJP`.
