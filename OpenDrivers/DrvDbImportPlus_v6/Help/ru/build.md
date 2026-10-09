# DrvDbImportPlus — Сборка

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/build.md)

Нужны Windows, PowerShell 7.2+ и .NET 10 SDK. Запуск из папки продукта:

```cmd
StartСompiling.bat -Runtime win-x64
```

Без `-Runtime` собираются все платформы из `release.json`. Готовые ZIP и SHA-256
сохраняются в корневой папке `Releases`. Каждый ZIP содержит `SCADA` и автоматически
сформированный `readme.txt`. Установка в SCADA выполняется отдельно.

Параметры, структура пакетов и данные README: [сборка пакетов](../../../../Help/ru/release-packaging.md).

При сборке зависимости View остаются рядом с `DrvDbImportPlus.View.dll`, чтобы их находили конструктор WinForms и отдельное приложение. При публикации View зависимости перемещаются в подкаталог `DrvDbImportPlus.View` для Rapid SCADA. При публикации отдельного приложения они остаются рядом с исполняемым файлом.
