# DrvDDEJP — Конфигурация

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/configuration.md)

| Настройка | По умолчанию | Описание |
| --- | ---: | --- |
| `ServiceName` | `ServiceName` | Имя DDE-сервиса, например имя сервиса приложения |
| `DefaultTopic` | `DefaultTopic` | Topic, который используется, если у тега не задан свой topic |
| `RequestTimeout` | `5000` | Timeout DDE-запроса в миллисекундах, минимум `100` |
| `ReconnectDelay` | `2000` | Минимальный интервал между повторными сообщениями об ошибке одного topic |
| `WriteLogDriver` | `true` | Включает сообщения драйвера в логе ScadaComm |
| `MessageTypeLogDriver` | `Action` | Тип сообщений лога Rapid SCADA, используемый драйвером |

Драйвер хранит конфигурацию КП в XML-файлах, имя которых зависит от номера КП:

- `DrvDDEJP.xml` для номера КП `0`;
- `DrvDDEJP_001.xml`, `DrvDDEJP_002.xml` и далее для обычных номеров КП.

Основные параметры проекта приведены в таблице выше.
