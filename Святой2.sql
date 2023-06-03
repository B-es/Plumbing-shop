INSERT INTO [dbo].[Entities]
           ([Name]
           ,[Price])
     VALUES
           (N'Тройник', 550)
GO

INSERT INTO [dbo].[Values]
           ([value]
           ,[Id_Attribute]
           ,[Id_Entity])
     VALUES
           (N'Сталь',3,4)
GO

INSERT INTO [dbo].[Values]
           ([value]
           ,[Id_Attribute]
           ,[Id_Entity])
     VALUES
           (N'Неразъёмный',4,4)
GO

INSERT INTO [dbo].[Values]
           ([value]
           ,[Id_Attribute]
           ,[Id_Entity])
     VALUES
           (N'15мм',5,4)
GO

INSERT INTO [dbo].[Values]
           ([value]
           ,[Id_Attribute]
           ,[Id_Entity])
     VALUES
           (N'Чебоксарский трубный завод',7,4)
GO