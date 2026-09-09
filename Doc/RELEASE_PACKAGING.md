# Сборка ZIP открытых модулей

Для сборки нужны Windows, PowerShell 7.2 или новее и .NET 10 SDK. Библиотеки
Rapid SCADA и FastColoredTextBox берутся из существующих ссылок проектов.
Запуск от имени администратора не требуется.

## Команды

Из корня репозитория:

```cmd
Build-Release.bat -List
Build-Release.bat -Project DrvFtpJP -Runtime win-x64
Build-Release.bat -All
```

Запуск `Build-Release.bat` без аргументов собирает все пакеты для всех платформ,
перечисленных в их `release.json`. BAT в папке продукта делает то же для одного
продукта. Например:

```cmd
OpenDrivers\DrvFtpJP_v6\StartСompilingDrvFtpJP.bat -Runtime win-x64
OpenDrivers\DrvDbImportPlus_v6\StartСompiling.bat
```

Все BAT сборки вызывают корневой `Build-Release.ps1`. Служебные
`FluentFTP/restore.bat` и `SampleServer/uninstall_service.bat` сохраняют свои
отдельные назначения и не используются для выпуска ZIP.

Прямой вызов PowerShell даёт те же возможности:

```powershell
pwsh -NoProfile -File .\Build-Release.ps1 -Project DrvDbImportPlus -Runtime win-x64
pwsh -NoProfile -File .\Build-Release.ps1 -All -Runtime win-x64
pwsh -NoProfile -File .\Build-Release.ps1 -Project DrvFtpJP -Runtime win-x64 -IncludeApp
pwsh -NoProfile -File .\Build-Release.ps1 -Project DrvFtpJP -Runtime win-x64 -OutputDirectory "C:\Temp\SCADA packages" -Date 2026-09-08
```

По умолчанию используется `Release`; для отладки есть `-Configuration Debug`.
Параметр `-KeepStaging` оставляет промежуточные результаты публикации.
Старый `--package-only` принимается для совместимости: теперь каждый запуск
сборки создаёт пакет без установки в работающую SCADA.

## Результат

Готовые файлы находятся в корневой папке `Releases`:

```text
<код>_<AssemblyVersion>_<платформа>.zip
<код>_<AssemblyVersion>_<платформа>.zip.sha256
```

В корне ZIP находятся `SCADA` и `readme.txt`, как в исходных примерах пакетов.
Дополнительных уровней `Release` или папки с именем архива нет.

```text
readme.txt
SCADA/
  ScadaAdmin/
    Lang/<код>.ru-RU.xml
    Lang/<код>.en-GB.xml
    Lib/<код>.View.dll
    Lib/<код>.View/<зависимости>
  ScadaComm/
    Drv/<код>.Logic.dll
    Drv/<код>.Logic/<зависимости>
```

Это структура драйвера. Для других продуктов каталоги назначения указаны в
`release.json`: серверные модули помещаются в `ScadaServer/Mod`, расширения —
в `ScadaAdmin/Lib`, веб-плагины — в `ScadaWeb` вместе с `lang` и `wwwroot`.
`MicrosoftSqlStorage` поставляется для каталогов Сервера, Коммуникатора и
Вебстанции. `DrvDDEJP.DDE.dll` находится рядом с `DrvDDEJP.Logic.dll`.

Основные DLL Rapid SCADA не включаются в пакеты модулей. FastColoredTextBox,
FluentFTP, SQL-клиенты и их зависимости включаются в каталог соответствующего
модуля. Windows DLL настройки входят и в Linux-пакеты: Администратор работает
в Windows, тогда как DLL логики в таком пакете собрана для Linux.

`-IncludeApp` добавляет `App` для продуктов с самостоятельным WinForms-приложением.
В Linux-пакетах WinForms-приложение пропускается. Приложения требуют .NET 10
Desktop Runtime; AnyCPU-приложение запускается через `dotnet <имя>.dll`.

Перед установкой сопоставьте каталоги пакета со своей установкой и подключите
модуль в конфигурации Rapid SCADA. Сборочные скрипты не выполняют развёртывание,
перезапуск служб или изменение пользовательской конфигурации.

## Данные README

Общий шаблон: `Build/readme.template.txt`. Он содержит русскую и английскую
части, авторов, описание, версию, дату, платформу, исходный код и ссылки форума.
Результат записывается в UTF-8 с BOM, чтобы кириллица корректно открывалась в
Windows.

Источники значений:

| Поле | Источник |
| --- | --- |
| Код драйвера | `DriverUtils.DriverCode`, проверяется против `release.json.id` |
| Название и описание драйвера | Константы `NameRu`, `NameEn`, `DescriptionRu`, `DescriptionEn` в `DriverUtils` |
| Название и описание остальных модулей | Раздел `display` в `release.json` |
| Версия | Вычисленное MSBuild свойство `AssemblyVersion` проекта `versionProject` |
| Дата | Дата запуска, либо `-Date` |
| Авторы, GitHub, форум, примечания | `authors`, `sourceUrl`, `forums`, `notes` в `release.json` |

Версию не нужно дублировать в BAT или `DriverUtils`: свойство `DriverUtils.Version`
возвращает версию своей сборки. Если в `.csproj` задан только `Version`, SDK
вычисляет `AssemblyVersion`. Если заданы оба свойства, номер ZIP и README
соответствует именно `AssemblyVersion`, то есть номеру DLL.

Названия и описания не меняются при очередной сборке. Для изменения текста
драйвера отредактируйте соответствующие строковые константы `DriverUtils`:
методы `Name` и `Description` и генератор README используют одни данные.
Константы должны быть обычными строковыми литералами C#.

Темы FTP, Telnet и ФСТ внесены по предоставленным ссылкам. Русская тема Ping
называется `DrvPing`, английская — `DrvPingJP`. Для стандартного `ExtDepAgent`
указан общий раздел форума с явной пометкой «Раздел поддержки». SQL-хранилище
ссылается на тему связанного расширения `ExtDepMicrosoftSqlJP`.

## Каталог продуктов и платформы

| Продукты | Платформы |
| --- | --- |
| DrvDbDataTransferJP, DrvDbImportPlus | win-x64, win-x86, linux-x64 |
| DrvDDEJP, DrvDebug, ExtDepAgent | win-x64, win-x86, anycpu |
| DrvFreeDiskSpaceJP, DrvFSTJP, DrvFtpJP, DrvPingJP, DrvTelnetJP | win-x64, win-x86, linux-x64, anycpu |
| ExtDepMicrosoftSqlJP | win-x64, win-x86 |
| MicrosoftSqlStorage, ModArcMicrosoftSqlJP | win-x64, win-x86, linux-x64 |
| PlgMimCalendarJP, PlgMimShapesJP | win-x64, win-x86, linux-x64, anycpu |

Всего 15 продуктов и 51 сочетание продукта и платформы. Все 44 исходных
`.csproj`, включая вспомогательные библиотеки, конвертеры, тесты и примеры,
по-прежнему проверяются через `Build-OpenSource.ps1`. Вспомогательные проекты
не объявляются самостоятельными модулями Rapid SCADA.

Для SQL-продуктов AnyCPU ZIP отключён по результату проверки: публикация без
RID оставляет в корне переносимую DLL-заглушку `Microsoft.Data.SqlClient`,
а реализации для ОС — во вложенных `runtimes`. Загрузчик Rapid SCADA не
выбирает эти варианты по `.deps.json` модуля. Создание `SqlConnection` из
такой DLL завершается `PlatformNotSupportedException`. Пакеты с явным RID
содержат проверенные реализации SQL-клиента и подходящую нативную SNI DLL.
Это ограничение упаковки SQL-зависимостей, а не запрет конфигурации Any CPU
в Visual Studio для исходных проектов.

## Проверка и ошибки

```powershell
pwsh -NoProfile -File .\Test-Release.ps1
pwsh -NoProfile -File .\Test-Release.ps1 -Project DrvFtpJP -Runtime win-x64
pwsh -NoProfile -File .\Tests\Test-ReleaseFailure.ps1
```

Проверка сопоставляет README с шаблоном, версии DLL с проектами, проверяет SHA-256,
структуру ZIP, языковые файлы, конфигурации и веб-ресурсы. Из распакованного ZIP
в отдельном процессе загружаются типы, FastColoredTextBox и SQL-клиенты;
проверяются ключи и пиксели изображений. Для проверки x86 нужен установленный
.NET 10 Desktop Runtime x86. Соединения с БД, FTP и DDE-серверами не открываются.
Linux-пакеты проверяются по составу и платформенным DLL; запуск логики на Linux
нужно проверять на Linux-хосте.

Проверка от 8 сентября 2026 года: 51 основной ZIP, 88 загрузок Windows-компонентов,
все 128 исходных иконок и восемь дополнительных пакетов с `App` для `win-x64`.
Проверка ожидаемой ошибки публикации подтвердила сохранность предыдущего ZIP
и передачу ненулевого кода выхода через BAT.

Журналы публикации и JSON-результаты сохраняются в `artifacts/release-results`.
Для другого `-OutputDirectory` используется отдельная подпапка результатов.
Если публикация или проверка ZIP не удалась, сценарий возвращает ненулевой код,
показывает журнал ошибки и сохраняет промежуточную папку в
`artifacts/release-builds`. Существующий ZIP заменяется только после успешной
проверки нового архива.
