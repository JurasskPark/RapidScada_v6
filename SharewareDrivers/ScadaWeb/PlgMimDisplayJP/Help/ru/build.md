# PlgMimDisplayJP — Сборка исходников и разработка

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/build.md)

Реализация принадлежит отдельной рабочей копии `scada-web-v6-develop` в `Plugins/Mimics/PlgMimDisplayJP`. Эта публичная папка содержит документацию и снимки PNG. Проекты Web и View Классического Администратора используют `net10.0` и общие метаданные продукта.

Общая подготовка описана в [справке сборки репозитория](../../../../../Help/ru/build.md). Контракты исходников находятся в `Doc/BUILD.md`, `Doc/JS_BUILD_RULES.md` и `Doc/MIMIC_RUNTIME_LICENSING.md` той рабочей копии.

Из корня репозитория исходников просмотрите основной план семейства:

~~~bat
Plugins\Mimics\Build-Release.bat -Project PlgMimDisplayJP -Plan
~~~

Уберите `-Plan` для сборки выбранного выпуска семейства. Текущий скрипт семейства по умолчанию помещает проверенные пакеты в `Releases/Mimics` и сохраняет прежние выпуски. Отдельная переносимая обёртка `Plugins/Mimics/BuildPublish_PlgMimDisplayJP.bat` подготавливает `Plugins/Mimics/PlgMimDisplayJP/Publish/SCADA`. Соблюдайте структуру выбранной точки входа.

Профильные проверки исходников:

~~~text
node Tests/Js/PlgMimDisplayJP/index.js
powershell -NoProfile -File Plugins/Mimics/PlgMimDisplayJP/PlgMimDisplayJP/Scripts/BuildDemoLocalization.ps1 -Check
dotnet test Tests/CompiledUnitTests/PlgMimDisplayJP.Tests/PlgMimDisplayJP.Tests.csproj -c Release
node Tests/BrowserSmoke/display-demo-fixture.mjs --serve
~~~

Локализация демо генерируется из XML-словарей EN/RU в `display-demo-lang.js`. Согласованно изменяйте манифест, регистрацию компонентов/подтипов, дескрипторы, фабрики, отрисовку и оба словаря.

LicenseJPLite по умолчанию находится в `System/ThirdParty/LicenseJPLite`; конфигурация сборки может задавать `LICENSEJP_LITE_RUNTIME`. Генерируемый комплект защиты исполнения обязателен. Сохраняйте требуемые файлы зависимости лицензирования `runtimes/win/lib/net8.0`, хотя плагин использует .NET 10. Для защищённого выпуска требуется настроенный инструмент .NET Reactor.

Здесь документированы команды исходников. При подготовке страницы продукта сборка, создание ZIP и приёмка подписанной лицензии не выполнялись.
