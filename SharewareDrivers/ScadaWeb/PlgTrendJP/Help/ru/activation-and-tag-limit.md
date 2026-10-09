# PlgTrendJP — Активация и лимит каналов

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/activation-and-tag-limit.md)

`PlgTrendJP` использует отдельную лицензию, привязанную к установке. Если действующая лицензия не найдена, плагин создаёт `PlgTrendJP_Activation.bin`, не перезаписывая существующий запрос.

1. Запустите SCADA Web без лицензии TrendJP.
2. Найдите `PlgTrendJP_Activation.bin` в папке лицензий приложения.
3. Передайте запрос поставщику лицензии.
4. Сохраните полученный файл под именем `PlgTrendJP.bin` в той же папке.
5. Перезапустите приложение.

| Приложение | Запрос | Лицензия |
| --- | --- | --- |
| SCADA Web | `ScadaWeb/config/PlgTrendJP_Activation.bin` | `ScadaWeb/config/PlgTrendJP.bin` |
| при использовании | `ScadaAdminWebJP/License/PlgTrendJP_Activation.bin` | `ScadaAdminWebJP/License/PlgTrendJP.bin` |

Подписанная лицензия должна содержать `AppName=PlgTrendJP` и положительный `CountTags`. `CountTags` задаёт максимальное количество уникальных номеров каналов в одном тренде. Диапазоны раскрываются перед подсчётом, дубликаты учитываются один раз, а один канал в нескольких архивных источниках остаётся одним лицензируемым каналом.
