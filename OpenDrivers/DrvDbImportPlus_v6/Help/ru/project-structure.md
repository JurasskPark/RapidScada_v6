# DrvDbImportPlus — Структура проекта

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/project-structure.md)

```text
DrvDbImportPlus_v6/
├── DrvDbImportPlus.sln                  # Файл решения
├── StartСompiling.bat                   # Сборка ZIP-пакета
├── README.md                            # Главная страница
│
├── DrvDbImportPlus.Logic/               # Исполняемый драйвер ScadaComm
│   ├── DrvDbImportPlus.Logic.csproj
│   └── DevDbImportPlusLogic.cs          # Исполняемая логика устройства
│
├── DrvDbImportPlus.View/                # Интерфейс драйвера ScadaAdmin
│   ├── DrvDbImportPlus.View.csproj
│   ├── DevDbImportPlusView.cs           # Точка входа представления устройства
│   ├── Forms/                           # Формы проекта, импорта, экспорта и тегов
│   ├── FastColorTextBox/                # Компонент SQL-редактора
│   └── Lang/                            # Языковые XML-файлы английского и русского
│
├── DrvDbImportPlus.Shared/              # Общая логика для Logic, View и WinForms
│   ├── Client/                          # Клиент опроса
│   ├── Data/                            # Источники данных БД и InfluxDB
│   ├── Database/                        # Выполнение запросов
│   ├── Settings/                        # Классы XML-конфигурации
│   └── Tags/                            # Создание тегов и прототипов каналов
│
└── DrvDbImportPlus.Winform/             # Отдельное WinForms-приложение настройки и проверки
```
