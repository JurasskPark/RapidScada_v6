# PlgMimElectricJP — Сборка исходников и разработка

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/build.md)

Реализация находится в отдельной рабочей копии `scada-web-v6-develop`, в `Plugins/Mimics/PlgMimElectricJP`. Эта публичная папка продукта содержит документацию и иллюстрации, а не проекты реализации или пакет выпуска.

Веб-проект и модуль представления классического Администратора рассчитаны на .NET 10. `PlgMimElectricJP.Shared` содержит общие метаданные. Общая подготовка описана в [корневой инструкции сборки](../../../../../Help/ru/build.md); правила хоста и зависимостей исходников остаются в `scada-web-v6-develop/Doc/BUILD.md`.

## Проверки ресурсов без изменения файлов

Из `scada-web-v6-develop/Plugins/Mimics/PlgMimElectricJP`:

```powershell
pwsh -NoProfile -File Scripts/BuildElectricalAssets.ps1 -Check
node Scripts/ValidateElectricalPlugin.mjs
pwsh -NoProfile -File PlgMimElectricJP/Scripts/BuildDemoLocalization.ps1 -Check
```

Редактируемые исходные изображения — `Design/ElectricalSymbolsCatalog.svg` и `Design/UniversalSymbolsCatalog.svg`. Второй файл содержит определения символов, а не готовый видимый обзор. Ресурсы исполнения — отдельные SVG в `PlgMimElectricJP/wwwroot/plugins/MimElectricJP/images/symbols`.

## Переносимый пакет

В Windows запустите точку входа из корня репозитория исходников:

```bat
Plugins\Mimics\BuildPublish_PlgMimElectricJP.bat
```

Обёртка использует окружение NuGet и командной строки родительского каталога Mimics, создаёт локализацию демонстрации, проверяет ресурсы, выполняет профильные JavaScript-тесты плагина, собирает Web и View в Release и публикует `Plugins/Mimics/Publish/PlgMimElectricJP/SCADA`.

Для упаковки нужен модуль LicenseJPLite: `LICENSEJP_RUNTIME`, затем `LICENSEJP_LITE_RUNTIME` либо каталог по умолчанию `System/ThirdParty/LicenseJPLite`. Защита лицензируемой Web DLL также требует настроенный инструмент .NET Reactor. View DLL не защищается. Пакет включает необходимые зависимости лицензирования и сгенерированную защиту исполнения.

Изменяйте каталог компонентов, определения, описатели, фабрики, средства отображения и словари EN/RU согласованно. Сохраняйте семантические имена типов и контракты миграции. Пересоздавайте локализацию и ресурсы штатными скриптами исходников; проверяйте все обязательные ресурсы манифеста и включение защиты исполнения в пакет.

Эти команды описывают работу с исходниками. При добавлении документации сборка плагина, создание защищённого пакета и приёмка установленного исполнения не выполнялись.
