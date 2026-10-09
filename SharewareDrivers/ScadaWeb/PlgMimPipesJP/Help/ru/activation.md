# PlgMimPipesJP — Активация

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/activation.md)

Плагин трубопроводов использует отдельную лицензию. Лицензия `MimicEditorJP` не активирует `PlgMimPipesJP`.

| Приложение | Запрос активации | Лицензия |
| --- | --- | --- |
| SCADA Web | `C:\Program Files\SCADA\ScadaWeb\config\PlgMimPipesJP_Activation.bin` | `C:\Program Files\SCADA\ScadaWeb\config\PlgMimPipesJP.bin` |
| ScadaAdminWebJP | `C:\Program Files\SCADA\ScadaAdminWebJP\License\PlgMimPipesJP_Activation.bin` | `C:\Program Files\SCADA\ScadaAdminWebJP\License\PlgMimPipesJP.bin` |

1. Запустите приложение без лицензии трубопроводов.
2. Плагин создаст файл `PlgMimPipesJP_Activation.bin` в папке лицензий. Существующий запрос не перезаписывается.
3. Передайте этот файл поставщику лицензии.
4. При создании лицензии должны быть сохранены UID из запроса и точное имя приложения `PlgMimPipesJP`.
5. Сохраните полученный ключ под именем `PlgMimPipesJP.bin` в папке лицензий используемого приложения.
6. Перезапустите SCADA Web или ScadaAdminWebJP. Простого обновления страницы недостаточно.
7. Если используются оба приложения, положите действующую лицензию в каждую папку, потому что каждый хост читает только свой каталог лицензий.

Если лицензия отсутствует или недействительна, существующие компоненты труб продолжают загружаться и отображаться, но группа трубопроводов скрыта и добавление новых компонентов запрещено.
