# ExtDepMicrosoftSqlJP — Диагностика

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/troubleshooting.md)

- Если расширение не отображается в Администраторе, проверьте `ScadaAdminConfig.xml`, `ExtDepMicrosoftSqlJP.dll` и языковые файлы.
- Если проверка подключения не проходит, убедитесь, что выбрана СУБД Microsoft SQL Server, и проверьте сервер, базу данных, пользователя, пароль и параметры строки подключения.
- Если SQL Server использует шифрованное подключение с самоподписанным сертификатом, добавьте `Trust Server Certificate=True` в параметры строки подключения.
- Если развёртывание падает при очистке базы, используйте `DropTables` для первого развёртывания.
- Если `Microsoft.Data.SqlClient` сообщает об ошибке платформы, проверьте наличие Windows-файлов `Microsoft.Data.SqlClient.dll` и `Microsoft.Data.SqlClient.SNI.dll` в подпапке `ExtDepMicrosoftSqlJP`.
