# PlgTrendJP — Установка и регистрация

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/installation-and-registration.md)

Требования:

- Rapid SCADA 6.x под Windows или Linux;
- среда .NET 8, используемая SCADA Web;
- действующая лицензия `PlgTrendJP.bin` с положительным значением `CountTags`;
- права на запрашиваемые объекты и активные входные каналы;
- настроенные архивы SCADA.

Установка:

1. Выберите пакет для целевой операционной системы и скопируйте его папку `SCADA` поверх каталога установки Rapid SCADA с сохранением структуры.
2. Включите `PlgTrendJP` в проектном `ScadaWebConfig.xml`.
3. Назначьте `PlgTrendJP` как `ChartFeature`, если стандартные команды графиков Rapid SCADA должны открывать TrendJP.
4. Установите лицензию и перезапустите SCADA Web или сайт IIS. Обновление браузера не перезагружает сборки и лицензию.

```xml
<Plugins>
  <Plugin code="PlgTrendJP" />
</Plugins>

<PluginAssignment>
  <ChartFeature>PlgTrendJP</ChartFeature>
</PluginAssignment>
```

```bat
RegisterTrendPluginInProject.bat -ProjectDir "C:\Program Files\SCADA\ProjectSamples\HelloWorld"
```

Поставляемый помощник может добавить эту конфигурацию и создать резервную копию с отметкой времени. Параметр `-KeepChartFeature` включает плагин без замены другого обработчика графиков. Также поддерживаются `-InstanceName`, `-ConfigFileName` и `-NoBackup`.

Под Windows скопируйте `PlgTrendJP.View.dll` в `ScadaAdmin\Lib`, если классический Администратор должен распознавать тип представления `TrendJP`.
