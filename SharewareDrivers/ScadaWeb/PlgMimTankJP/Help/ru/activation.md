# PlgMimTankJP — Активация

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/activation.md)

Плагин резервуаров использует собственную лицензию, привязанную к установке. Лицензия `MimicEditorJP`, `PlgMimPipesJP` или другого продукта не активирует `PlgMimTankJP`.

| Приложение | Запрос активации | Лицензия |
| --- | --- | --- |
| SCADA Web | `C:\Program Files\SCADA\ScadaWeb\config\PlgMimTankJP_Activation.bin` | `C:\Program Files\SCADA\ScadaWeb\config\PlgMimTankJP.bin` |
| ScadaAdminWebJP | `C:\Program Files\SCADA\ScadaAdminWebJP\License\PlgMimTankJP_Activation.bin` | `C:\Program Files\SCADA\ScadaAdminWebJP\License\PlgMimTankJP.bin` |

1. Запустите SCADA Web или ScadaAdminWebJP без лицензии TankJP.
2. Плагин создаст `PlgMimTankJP_Activation.bin` в папке лицензий хоста. Существующий запрос не перезаписывается.
3. Передайте запрос активации поставщику лицензии.
4. При создании лицензии должны быть сохранены UID из запроса и точное имя приложения `PlgMimTankJP`.
5. Сохраните полученный ключ под именем `PlgMimTankJP.bin` в той же папке лицензий хоста.
6. Перезапустите SCADA Web или ScadaAdminWebJP. Простого обновления страницы недостаточно.
7. Если используются оба приложения, поместите действующую лицензию в каждый каталог, потому что каждый хост читает только собственную папку лицензий.

Если лицензия отсутствует, недействительна или выдана для другого `AppName`, существующие компоненты TankJP продолжают загружаться и отображаться. Группа **РЕЗЕРВУАРЫ** скрывается, а прямое добавление новых компонентов запрещается до установки действующей лицензии.
