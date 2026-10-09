# DrvMOXANportJP — Структура проекта

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/project-structure.md)

```text
DrvMOXANportJP/
├── DrvMOXANportJP.sln                    # Файл решения
├── StartСompiling.bat                    # Сборка и подготовка каталога Publish
├── ProtectWithReactor.bat                # Сборка, публикация и защита DLL через .NET Reactor
├── README.md                             # Главная страница
│
├── DrvMOXANportJP.Logic/                 # Исполняемый драйвер ScadaComm
│   ├── DrvMOXANportJP.Logic.csproj
│   └── DevMOXANportJPLogic.cs            # Точка входа логики драйвера
│
├── DrvMOXANportJP.View/                  # Интерфейс драйвера ScadaAdmin
│   ├── DrvMOXANportJP.View.csproj
│   ├── DrvMOXANportJP.View.cs            # Точка входа представления драйвера
│   ├── Forms/                            # Формы настройки, устройства, поиска и команд
│   └── Lang/                             # Языковые XML-файлы английского и русского
│
├── DrvMOXANportJP.Shared/                # Общий код логики, представления и инструментов
│   ├── Moxa/                             # Логика MOXA UDP, DSCI, прошивок и команд
│   ├── Ping/                             # Сканирование сети и опрос
│   ├── Project/                          # XML-конфигурация драйвера
│   └── Configuration/                    # Прототипы каналов Rapid SCADA
│
├── DrvMOXANportJP.WinForm/               # Отдельное приложение настройки и проверки
├── DrvMOXANportJP.Debug/                 # Консольная диагностика и исследование протокола
├── DrvMOXANportJP.DsciProbe/             # Утилита проверки DSCI
│
├── Libraries/                            # Библиотеки Rapid SCADA, LicenseJP и производителя MOXA
├── Publish/                              # Результат публикации
├── Release/                              # Результат сборки пакета
├── samples/                              # Примеры производителя и образцы протокола
├── protocol/                             # Заметки о протоколе и материалы исследования
└── screen/                               # Скриншоты
```
