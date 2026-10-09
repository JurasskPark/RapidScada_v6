# DrvDDEJP — Структура проекта

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/project-structure.md)

```text
DrvDDEJP/
├── DrvDDEJP.sln                    # Файл решения
├── StartСompiling.bat              # Сборка ZIP-пакета
├── README.md                       # Главная страница
│
├── DdeNet/                         # Встроенные исходники клиента и сервера DDE
├── DrvDDEJP.DDE/                   # Обёртка клиента DDE для драйвера
├── DrvDDEJP.Logic/                 # Исполняемый драйвер ScadaComm
├── DrvDDEJP.Shared/                # Общие конфигурация, теги, утилиты и локализация
├── DrvDDEJP.View/                  # Интерфейс настройки ScadaAdmin
│   ├── Forms/                      # Формы проекта и тегов
│   └── Lang/                       # Языковые XML-файлы английского и русского
├── DrvDDEJP.WinForm/               # Отдельный интерфейс настройки и проверки
├── Hex.Shared/                     # Общие функции преобразования
├── Libraries/                      # Библиотеки ссылок Rapid SCADA
└── SampleServer/                   # Пример утилиты сервера DDE
```
