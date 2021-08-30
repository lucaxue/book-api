<div align='center'>

# 📚 Books REST API

<div>

[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-2e2e2e?logo=dotnet)](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-5.0)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-2e2e2e?logo=postgresql)](https://www.postgresql.org/)
[![Dapper ORM](https://img.shields.io/badge/Dapper%20ORM-2e2e2e?logo=dapper)](https://dapper-tutorial.net/)
<br>
[![xUnit](https://img.shields.io/badge/xUnit-2e2e2e?logo=xunit)](https://xunit.net/)
[![FluentAssertions](https://img.shields.io/badge/Fluent%20Assertions-2e2e2e?logo=fluentassertions)](https://fluentassertions.com/)
[![NSubstitute](https://img.shields.io/badge/NSubstitute-2e2e2e?logo=nsubstitute)](https://nsubstitute.github.io/)

</div>

A simple REST API, following repository pattern for a books table.

</div>

<br>

## 📄 API Specs

### GET

| HTTP Method | Use                            | Endpoint                |
| ----------- | ------------------------------ | ----------------------- |
| **GET**     | Index all books                | `/books`                |
| **GET**     | Show book by id                | `/books/{id}`           |
| **GET**     | Index books by title or author | `/books?search=foo`     |
| **GET**     | Index books with custom limit  | `/books?limit=5`        |
| **GET**     | Paginate books                 | `/books?limit=5&page=3` |
| **POST**    | Create new book                | `/books`                |
| **PUT**     | Update book                    | `/books/{id}`           |
| **DELETE**  | Destroy book                   | `/books/{id}`           |

<br>

## ⚙️ Setting Up

- [Local](#setting-up-local)
- [Docker](#setting-up-docker)

<h3 id='setting-up-local'>Local 💻</h3>

#### Prerequisites:

- .NET 5.0
- PostgreSQL

#### Running the api locally

1. Copy and set up enviroment variables for the database

   ```bash
   cp .env.example .env
   ```

2. Run the app on your local port

   ```bash
   dotnet watch run --project ./src/BookApi.csproj
   ```

#### Running the tests

1. Run the tests

   ```bash
   cd tests
   ```

   ```bash
   dotnet test
   ```

<h3 id='setting-up-docker'>Docker 🐳</h3>

#### Prerequisites:

- Docker

#### Running the containers

1. Copy environment variables for the database

   ```bash
   cp .env.example .env
   ```

2. Build docker images

   ```bash
   docker compose build
   ```

3. Run docker containers

   ```bash
   docker compose up
   ```

#### Running the tests

1. Run the tests in the api container

   ```bash
   docker compose exec api /bin/sh -c "cd tests; dotnet test;"
   ```
