# Dapper Npgsql ASP.NET Core API

## A simple REST API for a books table.

### `/books`

- Get all books
- Get book by id
- Post book
- Update book
- Delete book
- Search books by title or author - case insensitive, and any part of the word
  - `/books?search=code`
- Limit books returned
  - `/books?limit=5`
- Paginate books returned, the limit is the amount of books per page
  - `/books?limit=5&page=2`
