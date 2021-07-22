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

### `src/`

- Copy and set up enviroment variables for database
  ```
  cp .env.example .env
  ```
- Run the app on your local port
  ```
  dotnet watch run
  ```

### `tests/`

- Run tests
  ```
  dotnet test
  ```
