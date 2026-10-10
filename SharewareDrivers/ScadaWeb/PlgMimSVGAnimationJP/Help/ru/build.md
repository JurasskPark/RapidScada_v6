# PlgMimSVGAnimationJP — Сборка и разработка

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/build.md)

Команды предназначены для отдельной рабочей копии исходников `scada-web-v6-develop`; в этом каталоге документации нет проекта исходников. При оформлении справки команды изучены, но не выполнялись. [Общая справка сборки репозитория](../../../../../Help/ru/build.md)

## Точки входа

Для семейства Mimics запустите из корня исходного репозитория:

~~~powershell
Plugins\Mimics\Build-Release.bat -Project PlgMimSVGAnimationJP -Plan
~~~

`-Plan` показывает план. Для фактического создания пакетов используйте соответствующий режим выпуска; каталог выпусков семейства — `Releases/Mimics`.

Изолированная оболочка плагина из `Plugins/Mimics/PlgMimSVGAnimationJP` создаёт пакет Release:

~~~powershell
.\BuildAll.bat
~~~

Основной скрипт также поддерживает сборку без упаковки, дополнительные браузерные проверки и публикацию:

~~~powershell
powershell -NoProfile -ExecutionPolicy Bypass -File Scripts/BuildPackage.ps1 -Configuration Release
powershell -NoProfile -ExecutionPolicy Bypass -File Scripts/BuildPackage.ps1 -Configuration Release -Package -BrowserTests
powershell -NoProfile -ExecutionPolicy Bypass -File Scripts/BuildPackage.ps1 -Configuration Release -Package -Publish
~~~

## Исходники и требования

Нужны Node.js, .NET 10 SDK и согласованные проекты приложения и общих библиотек Rapid SCADA. Поиск библиотек лицензии: `LICENSEJP_RUNTIME`, затем `LICENSEJP_LITE_RUNTIME`, затем `System/ThirdParty/LicenseJPLite`. Скрипт передаёт нейтральный `-p:RuntimeIdentifier=`.

Редактируйте модули `wwwroot/plugins/MimSVGAnimationJP/js/src` и исходные словари EN/RU. `Scripts/BuildAssets.mjs` собирает JavaScript и формирует JSON словарей. Не редактируйте сгенерированные пакеты и словари вручную. Метаданные View и Web должны совпадать.

Скрипт проверяет синтаксис JavaScript, запускает проверки вычислителя, дизайнера, лицензии и собирает оба проекта. `-BrowserTests` дополнительно запускает проверки открытия, приёмки, регрессий, фейсплейтов и жизненного цикла. Отдельные проверки из каталога плагина:

~~~powershell
node --test Tests/engine.test.cjs Tests/designer.test.cjs Tests/license.test.cjs
~~~

## Упаковка

Изолированный скрипт создаёт `Package/SCADA`, README пакета и версионный ZIP; `-Publish` копирует их в `Plugins/Mimics/Publish/PlgMimSVGAnimationJP` с сохранением старых версионных ZIP. Переносимая структура использует `ScadaAdmin/Lib` и `ScadaWeb/lang` в нижнем регистре.

Обязательные ресурсы: оба пакета `js/svg-animation.bundle.js` и `js/zz-runtime-license.js`, CSS, значок и оба словаря. Защита Web DLL выполняется до архивации/публикации и останавливает процесс при ошибке; View-сборка остаётся без защиты.

Переносимый импорт компонентов использует согласованные общие сборки приложения. Нативные правила лицензирования требуют согласованный `PlgMimic.Common.dll` при нативной установке; не смешивайте это требование с содержимым переносимого импорта и не подменяйте основные библиотеки приложения.

Эти описания не подтверждают новую успешную сборку, защищённую DLL или проверенный ZIP. [Установка](installation.md) · [Границы материалов](sources.md)
