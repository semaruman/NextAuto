# NextAuto

**NextAuto** — платформа для автосалона: управление каталогом автомобилей, работа с клиентами и публичная витрина. Проект построен на **Clean Architecture** с разделением бизнес-логики, инфраструктуры и представления.

> Автосалон Next Auto — новые и подержанные автомобили. Кредит, страховка, Trade-in.

---

## Проблема

Автосалону нужна единая цифровая система, которая решает сразу несколько задач:

- **Каталог автомобилей** — хранить и обновлять данные о машинах: марка, модель, год, пробег, цена, фото.
- **База клиентов** — вести информацию о клиентах и их автомобилях.
- **Операционная работа** — быстро добавлять, изменять и удалять записи без хаотичной логики в контроллерах.
- **Масштабируемость** — возможность подключать новые интерфейсы (сайт, админ-панель, REST API) к одному ядру приложения.

Без чёткой архитектуры бизнес-логика быстро «размазывается» по UI и слою доступа к данным, усложняя поддержку и развитие продукта.

---

## Решение

NextAuto разделяет систему на независимые слои и использует паттерн **CQRS + MediatR** для всех операций с сущностями `Car` и `Client`.

| Слой | Проект | Назначение |
|------|--------|------------|
| Domain | `NextAuto.Domain` | Сущности (`Car`, `Client`), контракты репозиториев |
| Application | `NextAuto.Application` | Команды, запросы, DTO, `IUnitOfWork`, `ServiceResult<T>` |
| Infrastructure | `NextAuto.Infrastructure` | EF Core, PostgreSQL, репозитории, `UnitOfWork` |
| API | `NextAuto.WebApi` | REST-контроллеры `CarController`, `ClientController` |
| Админ-панель | `NextAuto.AdminPanelMvcApp` | MVC-интерфейс для управления автомобилями |
| Витрина | `NextAuto.Web` | Статический лендинг автосалона |

**Ключевые принципы:**

- **Repository + Unit of Work** — единая точка доступа к данным через `IUnitOfWork` с репозиториями `Cars` и `Clients`.
- **CQRS** — команды (Create, Update, Delete) и запросы (GetById) изолированы в `NextAuto.Application`.
- **Единый результат операций** — `ServiceResult<T>` с HTTP-кодами (200 при успехе, 404 если запись не найдена).
- **PostgreSQL** — надёжное хранение данных с retry-политикой при сбоях подключения.

---

## REST API

Базовый адрес при локальном запуске: `http://localhost:5033`

### Автомобили — `/api/cars`

| Метод | Маршрут | Описание |
|-------|---------|----------|
| `GET` | `/api/cars/{id}` | Получить автомобиль по Id |
| `POST` | `/api/cars` | Добавить автомобиль |
| `PUT` | `/api/cars/{id}` | Обновить автомобиль |
| `DELETE` | `/api/cars/{id}` | Удалить автомобиль |

### Клиенты — `/api/clients`

| Метод | Маршрут | Описание |
|-------|---------|----------|
| `GET` | `/api/clients/{id}` | Получить клиента по Id |
| `POST` | `/api/clients` | Добавить клиента |
| `PUT` | `/api/clients/{id}` | Обновить клиента |
| `DELETE` | `/api/clients/{id}` | Удалить клиента |

---

<!--
## Модель данных

### Car

| Поле | Тип | Описание |
|------|-----|----------|
| `Id` | `int` | Идентификатор |
| `Brand` | `string` | Марка |
| `Model` | `string` | Модель |
| `Year` | `int` | Год выпуска |
| `Mileage` | `int` | Пробег |
| `Price` | `double` | Цена |
| `ImageUrl` | `string` | URL изображения |

### Client

| Поле | Тип | Описание |
|------|-----|----------|
| `Id` | `int` | Идентификатор |
| `CarBrand` | `string` | Марка автомобиля клиента |
| `CarModel` | `string` | Модель автомобиля клиента |
| `ImageUrl` | `string` | URL изображения |

---
-->
## Стек технологий

| Категория | Технология | 
|-----------|------------|
| Платформа | .NET | 
| Язык | C# | 
| Архитектура | Clean Architecture |
| Паттерны | CQRS, MediatR, Repository, Unit of Work |
| ORM | Entity Framework Core | 
| База данных | PostgreSQL |
| REST API | ASP.NET Core Web API | 
| Админ-панель | ASP.NET Core MVC |
| Публичный сайт | HTML + CSS |
| DI | Microsoft.Extensions.DependencyInjection |

---

## Структура решения

```
NextAuto/
├── NextAuto.Domain/              # Сущности и интерфейсы репозиториев
├── NextAuto.Application/         # CQRS: Cars/, Clients/, IUnitOfWork
├── NextAuto.Infrastructure/      # EF Core, DbContext, Repository, UnitOfWork
├── NextAuto.WebApi/              # REST API контроллеры
├── NextAuto.AdminPanelMvcApp/    # MVC админ-панель
├── NextAuto.Web/                 # Лендинг автосалона (wwwroot)
└── NextAuto.sln
```

---

<!--
## Быстрый старт

### Требования

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL

### Запуск API

1. Настройте строку подключения `DefaultConnection` для PostgreSQL (используется в `NextAuto.WebApi/Program.cs`).
2. Выполните миграции EF Core для `ApplicationDbContext`.
3. Запустите проект:

```bash
dotnet run --project NextAuto.WebApi
```

API будет доступен по адресу `http://localhost:5033` (профиль `http` из `launchSettings.json`).

### Запуск админ-панели

```bash
dotnet run --project NextAuto.AdminPanelMvcApp
```

### Запуск лендинга

```bash
dotnet run --project NextAuto.Web
```

---

## Лицензия

Проект находится в разработке. Лицензия не указана.
-->
