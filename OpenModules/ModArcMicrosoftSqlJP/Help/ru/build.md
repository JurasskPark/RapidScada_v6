# ModArcMicrosoftSqlJP — Сборка

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/build.md)

Нужны Windows, PowerShell 7.2+ и .NET 10 SDK. Запуск из папки продукта:

```cmd
StartСompiling.bat -Runtime win-x64
```

Без `-Runtime` собираются все платформы из `release.json`. Готовые ZIP и SHA-256
сохраняются в корневой папке `Releases`. Каждый ZIP содержит `SCADA` и автоматически
сформированный `readme.txt`. Установка в SCADA выполняется отдельно.

Параметры, структура пакетов и данные README: [сборка пакетов](../../../../Help/ru/release-packaging.md).

## Развёртывание серверного модуля

Скопируйте файлы серверного модуля в Rapid SCADA:

```text
ModArcMicrosoftSqlJP.Logic/bin/Release/net10.0/ModArcMicrosoftSqlJP.Logic.dll
  -> C:\Program Files\SCADA\ScadaServer\Mod\ModArcMicrosoftSqlJP.Logic.dll

ModArcMicrosoftSqlJP.Logic/bin/Release/net10.0/ModArcMicrosoftSqlJP.Logic/
  -> C:\Program Files\SCADA\ScadaServer\Mod\ModArcMicrosoftSqlJP.Logic\

ModArcMicrosoftSqlJP.Shared/Config/ModArcMicrosoftSqlJP.xml
  -> C:\Program Files\SCADA\ScadaServer\Config\ModArcMicrosoftSqlJP.xml
```

Добавьте модуль в `ScadaServerConfig.xml`:

```xml
<Module code="ModArcMicrosoftSqlJP" />
```

Настройте архивы на использование модуля:

```xml
<Archive active="true" code="CurCopy" name="Current data copy" kind="Current" module="ModArcMicrosoftSqlJP">
  <Option name="UseDefaultConn" value="false" />
  <Option name="Connection" value="MicrosoftSqlConn" />
  <Option name="ReadOnly" value="false" />
  <Option name="MaxQueueSize" value="1000" />
  <Option name="BatchSize" value="1000" />
</Archive>
```

Если `UseDefaultConn` равен `false`, модуль использует именованное соединение из `ModArcMicrosoftSqlJP.xml`. Если значение `true`, модуль использует соединение по умолчанию из `ScadaInstanceConfig.xml`.

## Развёртывание в Администраторе

Скопируйте файлы View-модуля и расширения:

```text
ModArcMicrosoftSqlJP.View/bin/Release/net10.0-windows/ModArcMicrosoftSqlJP.View.dll
  -> C:\Program Files\SCADA\ScadaAdmin\Lib\ModArcMicrosoftSqlJP.View.dll

ModArcMicrosoftSqlJP.View/bin/Release/net10.0-windows/ModArcMicrosoftSqlJP.View/
  -> C:\Program Files\SCADA\ScadaAdmin\Lib\ModArcMicrosoftSqlJP.View\

ModArcMicrosoftSqlJP.View/Lang/ModArcMicrosoftSqlJP.*.xml
  -> C:\Program Files\SCADA\ScadaAdmin\Lang\

ExtDepMicrosoftSqlJP/bin/Release/net10.0-windows/ExtDepMicrosoftSqlJP.dll
  -> C:\Program Files\SCADA\ScadaAdmin\Lib\ExtDepMicrosoftSqlJP.dll

ExtDepMicrosoftSqlJP/bin/Release/net10.0-windows/ExtDepMicrosoftSqlJP/
  -> C:\Program Files\SCADA\ScadaAdmin\Lib\ExtDepMicrosoftSqlJP\

ExtDepMicrosoftSqlJP/Config/ExtDepMicrosoftSqlJP.xml
  -> C:\Program Files\SCADA\ScadaAdmin\Config\ExtDepMicrosoftSqlJP.xml

ExtDepMicrosoftSqlJP/Lang/ExtDepMicrosoftSqlJP.*.xml
  -> C:\Program Files\SCADA\ScadaAdmin\Lang\
```

Код расширения должен быть зарегистрирован в конфигурации Администратора:

```xml
<Extension code="ExtDepMicrosoftSqlJP" />
```
