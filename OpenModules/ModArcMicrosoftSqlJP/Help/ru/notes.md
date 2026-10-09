# ModArcMicrosoftSqlJP — Примечания

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/notes.md)

- Имя схемы архива намеренно оставлено `mod_arc_microsoft_sql` для совместимости с уже созданными таблицами и данными.
- В подпапке зависимостей должна лежать Windows runtime-сборка `Microsoft.Data.SqlClient.dll`. Цель сборки копирует правильный runtime-файл и `Microsoft.Data.SqlClient.SNI.dll`.
- `ExtDepMicrosoftSqlJP` создаёт и обновляет таблицы `project.*`. `ModArcMicrosoftSqlJP` пишет runtime-архивы в таблицы `mod_arc_microsoft_sql.*`.
- Если таблицы не создаются, проверьте `UseDefaultConn`. При `UseDefaultConn=true` модуль использует не `ModArcMicrosoftSqlJP.xml`, а default connection экземпляра.
