<div align='center'>

# 📚 Books REST API

<div>

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-2e2e2e?logo=dotnet)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-2e2e2e?logo=postgresql)
![Dapper ORM](https://img.shields.io/badge/Dapper%20ORM-2e2e2e?logo=dapper)
<br>
![xUnit](https://img.shields.io/badge/xUnit-2e2e2e?logo=xunit)
![FluentAssertions](https://img.shields.io/badge/Fluent%20Assertions-2e2e2e?logo=fluentassertions)

</div>

A simple REST API, following repository pattern for a books table.

</div>

<br>

## 📄 API Specs

### GET

| Use                            | Endpoint                |
| ------------------------------ | ----------------------- |
| Index all books                | `/books`                |
| Show book by id                | `/books/{id}`           |
| Index books by title or author | `/books?search=foo`     |
| Index books with custom limit  | `books?limit=5`         |
| Paginate books                 | `/books?limit=5&page=3` |

### POST

| Use             | Endpoint |
| --------------- | -------- |
| Create new book | `/books` |

### PUT

| Use         | Endpoint      |
| ----------- | ------------- |
| Update book | `/books/{id}` |

### DELETE

| Use          | Endpoint      |
| ------------ | ------------- |
| Destroy book | `/books/{id}` |

<br>

## ⚙️ Setting Up

- [Local](###Local%20💻)
- [Docker](###Docker%20🐳)

### Local 💻

#### Prerequisites:

- .NET 5.0
- PostgreSQL

#### Running the api locally (in `src/`)

1. Copy and set up enviroment variables for the database

    ```bash
    cp .env.example .env
    ```

2. Run the app on your local port

    ```bash
    dotnet watch run
    ```

#### Running the tests (in `tests/`)

1. Run the tests

    ```bash
    dotnet test
    ```

### Docker 🐳

#### Prerequisites:

- Docker

#### Running the containers

1. Copy enviroment variables for the database

    ```bash
    cp src/.env.example src/.env
    ```

2. Build docker images

    ```bash
    docker compose build
    ```

2. Run docker containers

    ```bash
    docker compose up
    ```

#### Running the tests

1. Shell into the api service container

    ```bash
    docker compose exec api sh
    ```

2. Run the tests

    ```bash
    cd tests
    ```

    ```bash
    dotnet test
    ```
