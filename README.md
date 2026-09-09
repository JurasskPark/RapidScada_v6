<p align="center" >Rapid SCADA</p>

![RapidScada_v6 downloads](https://jurasskpark.ru/service/budges/?user=JurasskPark&repo=RapidScada_v6&color=006400)

Открытые проекты в `OpenDrivers`, `OpenExtensions`, `OpenModules` и `OpenPlugins`
переведены на **.NET 10**. Для сборки на Windows используйте `Build-OpenSource.ps1`,
для проверки ресурсов WinForms и тестов — `Test-OpenSource.ps1`.
Подробности совместимости и сохранения изображений: [переход на .NET 10](Doc/NET10_MIGRATION.md).

Готовые ZIP с `SCADA` и `readme.txt`: `Build-Release.bat -All`.
Один продукт: `Build-Release.bat -Project DrvFtpJP -Runtime win-x64`.
Архивы создаются в `Releases`; проверка — `Test-Release.ps1`.
Шаблон README, платформы и параметры: [сборка пакетов](Doc/RELEASE_PACKAGING.md).
