# Plumbing Shop

ASP.NET Core MVC веб-приложение для магазина сантехники. Использует модель EAV (Entity–Attribute–Value) для хранения характеристик товаров, что позволяет гибко описывать разнородные категории продуктов.

## Стек

- **ASP.NET Core** (MVC)
- **Entity Framework Core** + **MS SQL Server**
- Razor Views

## Структура проекта

```
Plumbing shop/
├── Controllers/
│   └── HomeController.cs
├── Models/
│   ├── Entity.cs           # Сущность (товар)
│   ├── Attribute.cs        # Атрибут (название характеристики)
│   ├── Value.cs            # Значение атрибута для сущности
│   ├── Product.cs          # Агрегирует Entity + Attributes + Values
│   ├── PlumbingDbContext.cs # DbContext (EF Core)
│   └── Constants.cs        # Строка подключения к БД
├── Views/
├── wwwroot/
└── Program.cs
```

## Настройка

Строка подключения задаётся в `Models/Constants.cs`:

```csharp
public static string connectMSSQL = "Server=...;Database=...;...";
```

## Запуск

```bash
dotnet run --project "Plumbing shop/Plumbing shop.csproj"
```

Приложение будет доступно по адресу `https://localhost:5001`.

## Требования

- .NET 6+
- MS SQL Server
