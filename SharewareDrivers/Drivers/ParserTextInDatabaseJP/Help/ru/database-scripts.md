# DrvParserTextInDatabaseJP — Скрипты базы данных

[Оглавление](index.md) · [Продукт](../../README.ru.md) · [English](../en/database-scripts.md)

Вкладка словарь служит для замениы в скрипте Запись данных (INSERT) переменных в sql-запросе и для временного хранения данных.

SQL запросы (шаблоны) для работы с файлами. Примеры с MS SQL.

Создание таблицы (CREATE TABLE)

```text
CREATE TABLE [dbo].[MeasurementFiles](
[AU_Id] [uniqueidentifier] NOT NULL,
[AU_Date] [datetime2](7) NULL,
[AU_Name] [nvarchar](150) NULL,
[AU_FileName] [nvarchar](max) NULL,
[AU_FullPath] [nvarchar](max) NULL,
[AU_Content] [nvarchar](max) NULL,
[AU_Owner] [nvarchar](max) NULL,
[AU_IsNeedToRead] [bit] NULL,
[AU_Status] [nvarchar](6) NULL,
[AU_SizeFile] [int] NULL,
[AU_NumberLines] [int] NULL,
 CONSTRAINT [PK_MeasurementFiles] PRIMARY KEY CLUSTERED
(
[AU_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[MeasurementFiles] ADD  CONSTRAINT [DF__MeasurementFiles__ID]  DEFAULT (newid()) FOR [AU_Id]
GO
```

SCRIPT SELECT

```text
SELECT
   AU_Name
  ,AU_FileName
  ,AU_FullPath
FROM MeasurementFiles
WHERE
AU_Name = '@Name' AND
AU_FileName = '@FileName' AND
AU_FullPath = '@FullPath' AND
AU_Owner = '@Owner'
```

SCRIPT INSERT

```text
INSERT INTO MeasurementFiles
   (
AU_Date
   ,AU_Name
   ,AU_FileName
   ,AU_FullPath
   ,AU_Content
   ,AU_Owner
   ,AU_IsNeedToRead
   ,AU_Status
   )
 VALUES
   (
'@Date'
   ,'@Name'
   ,'@FileName'
   ,'@FullPath'
   ,'@Content'
   ,'@Owner'
   ,'0'
   ,'@StatusReceived'
   )
```

SCRIPT UPDATE

```text
MERGE MeasurementFiles AS target
USING (SELECT '@Date', '@Name', '@FileName', '@FullPath', '@Content') AS source (AU_Date, AU_Name, AU_FileName, AU_FullPath, AU_Content)
ON (target.AU_Name = source.AU_Name AND target.AU_FileName = source.AU_FileName AND target.AU_FullPath = source.AU_FullPath)
WHEN MATCHED THEN
  UPDATE SET AU_Date = '@Date', AU_Content = '@Content', AU_IsNeedToRead = '0', AU_Status = 'CS0005'
WHEN NOT MATCHED THEN
  INSERT (AU_Date, AU_Content, AU_IsNeedToRead, AU_Status)
  VALUES ('@Date', '@Content', '0', 'CS0005');
```

SCRIPT DELETE

```text
DELETE FROM MeasurementFiles
WHERE
AU_Name = '@Name' AND
AU_FullPath = '@FullPath'
```

SCRIPT SYNCHRONIZATIOM

```text
SELECT AU_FullPath
FROM MeasurementFiles
WHERE AU_Name = '@Name'
```

Выборка данных (SELECT)

```text
SELECT AU_Id, AU_FileName, AU_Content
FROM MeasurementFiles
WHERE AU_Name = '@Name'
AND AU_Date > DATEADD(HOUR, -24, DATEDIFF(DAY, 0, GETDATE()))
AND ((AU_IsNeedToRead = 0 AND AU_Status = '@StatusReceived') OR (AU_IsNeedToRead = 0 AND AU_Status = '@StatusModified'))
AND AU_Content LIKE '%FIND WORDS%'
ORDER BY AU_Date DESC;
```

Запись данных (INSERT)

```text
UPDATE dbo.MeasurementFiles
SET
 AU_IsNeedToRead = 1
 ,AU_Status = '@StatusError'
WHERE AU_Id = '@ColumnId';

%YOU LOGIC%

UPDATE dbo.MeasurementFiles
SET
 AU_IsNeedToRead = 1
,AU_Status = '@StatusProcessed'
WHERE AU_Id = '@ColumnId';
```
