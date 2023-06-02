delete from Entities;
delete from Attributes;
delete from "Values";

DBCC CHECKIDENT (Entities, RESEED, 0)
DBCC CHECKIDENT (Attributes, RESEED, 0)
DBCC CHECKIDENT ("Values", RESEED, 0)

GO

INSERT INTO [dbo].[Entities]
           ([Name]
           ,[Price])
     VALUES
           (N'Труба', 300)
GO

INSERT INTO [dbo].[Entities]
           ([Name]
           ,[Price])
     VALUES
           (N'Отвод', 150)
GO

GO

INSERT INTO [dbo].[Attributes]
           ([Name])
     VALUES
           (N'Тип трубопровода')
GO

INSERT INTO [dbo].[Attributes]
           ([Name])
     VALUES
           (N'Температурный режим')
GO

INSERT INTO [dbo].[Attributes]
           ([Name])
     VALUES
           (N'Материал')
GO

INSERT INTO [dbo].[Attributes]
           ([Name])
     VALUES
           (N'Способ соединения')
GO

INSERT INTO [dbo].[Attributes]
           ([Name])
     VALUES
           (N'Диаметр')
GO

INSERT INTO [dbo].[Attributes]
           ([Name])
     VALUES
           (N'Длина')
GO

INSERT INTO [dbo].[Attributes]
           ([Name])
     VALUES
           (N'Производитель')
GO

GO

INSERT INTO [dbo].[Values]
           ([value]
           ,[Id_Attribute]
           ,[Id_Entity])
     VALUES
           (N'Для отопления',1,1)
GO

INSERT INTO [dbo].[Values]
           ([value]
           ,[Id_Attribute]
           ,[Id_Entity])
     VALUES
           (N'От 65 до 80 градусов',2,1)
GO

INSERT INTO [dbo].[Values]
           ([value]
           ,[Id_Attribute]
           ,[Id_Entity])
     VALUES
           (N'Чугун',3,1)
GO

INSERT INTO [dbo].[Values]
           ([value]
           ,[Id_Attribute]
           ,[Id_Entity])
     VALUES
           (N'Неразъёмный',4,1)
GO

INSERT INTO [dbo].[Values]
           ([value]
           ,[Id_Attribute]
           ,[Id_Entity])
     VALUES
           (15,5,1)
GO

INSERT INTO [dbo].[Values]
           ([value]
           ,[Id_Attribute]
           ,[Id_Entity])
     VALUES
           (6,6,1)
GO

INSERT INTO [dbo].[Values]
           ([value]
           ,[Id_Attribute]
           ,[Id_Entity])
     VALUES
           (N'Волжский трубный завод',7,1)
GO

INSERT INTO [dbo].[Values]
           ([value]
           ,[Id_Attribute]
           ,[Id_Entity])
     VALUES
           (N'Сталь',3,2)
GO

INSERT INTO [dbo].[Values]
           ([value]
           ,[Id_Attribute]
           ,[Id_Entity])
     VALUES
           (N'Неразъёмный',4,2)
GO

INSERT INTO [dbo].[Values]
           ([value]
           ,[Id_Attribute]
           ,[Id_Entity])
     VALUES
           (15,5,2)
GO

INSERT INTO [dbo].[Values]
           ([value]
           ,[Id_Attribute]
           ,[Id_Entity])
     VALUES
           (N'Мытищинский трубный завод',7,2)
GO