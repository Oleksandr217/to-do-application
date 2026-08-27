# To-Do Application

## Технології

Backend: ASP.NET Core 9, EF Core, PostgreSQL, JWT
Frontend: Angular 22, Bootstrap 5

## Запуск (Docker)

Вимоги: встановлений Docker Desktop

    docker compose up --build

Після завершення збірки:

- Застосунок: http://localhost:4200
- Swagger: http://localhost:5051/swagger

Зареєструйте нового користувача через сторінку /register, щоб почати роботу.

Зупинити застосунок:

    docker compose down

Зупинити і видалити дані БД:

    docker compose down -v
