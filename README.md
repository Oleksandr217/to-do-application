# To-Do Application

## Technology

Backend: ASP.NET Core 9, EF Core, PostgreSQL, JWT
Frontend: Angular 22, Bootstrap 5

## Project Launch (Docker)

Requirements: Docker Desktop must be installed

    docker compose up --build

After assembly is complete:

- App: http://localhost:4200
- Swagger: http://localhost:5051/swagger

Register a new user to get started.

Stop the app:

    docker compose down

Stop and delete database data:

    docker compose down -v
