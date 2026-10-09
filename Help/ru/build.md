# Rapid SCADA — Сборка

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/build.md)

Открытые продукты используют .NET 10. Сборка выполняется в Windows с PowerShell 7.2+ и стабильным .NET 10 SDK, выбранным через `global.json`. Visual Studio должна поддерживать .NET 10.

Из корня репозитория:

```powershell
pwsh -NoProfile -File .\Build-OpenSource.ps1
pwsh -NoProfile -File .\Test-OpenSource.ps1
```

Оба скрипта по умолчанию используют `Release`. Для отладки добавьте `-Configuration Debug`. Журналы и `results.json` сохраняются в `artifacts/build-net10-Release`.

Создание установочных пакетов:

```cmd
Build-Release.bat -List
Build-Release.bat -Project DrvFtpJP -Runtime win-x64
Build-Release.bat -All
```

[Параметры упаковки и проверки](release-packaging.md) · [Миграция .NET 10 и ранее выполненные проверки](net10-migration.md)

Модуль .NET 10 требует хоста Rapid SCADA на .NET 10. Установка SDK рядом с хостом .NET 8 не меняет платформу этого хоста. Исходники основных приложений Rapid SCADA не входят в репозиторий.

У условно-бесплатных продуктов платформа опубликованной версии указана отдельно. Их бинарные файлы не пересобирались при миграции открытых исходников.
