using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Threading.Tasks;
public class BookRepository : BaseRepository, IRepository<Book>
{

  public BookRepository(IConfiguration configuration) : base(configuration) { }

  public async Task<IEnumerable<Book>> Search(string query, int limit, int page)
  {
    using var connection = CreateConnection();
    return await connection.QueryAsync<Book>(
      "SELECT * FROM Books WHERE Title ILIKE @Query OR Author ILIKE @Query LIMIT @Limit OFFSET @Offset;", new { Query = $"%{query}%", Limit = limit, Offset = (page - 1) * limit });
  }

  public async Task<Book> Find(long id)
  {
    using var connection = CreateConnection();
    return await connection.QuerySingleAsync<Book>("SELECT * FROM Books WHERE Id = @Id;", new { Id = id });
  }

  public async Task<Book> Create(Book book)
  {
    using var connection = CreateConnection();
    return await connection.QuerySingleAsync<Book>("INSERT INTO Books (Title, Author) VALUES (@Title, @Author) RETURNING *;", book);
  }

  public async Task<Book> Update(Book book)
  {
    using var connection = CreateConnection();
    return await connection.QuerySingleAsync<Book>("UPDATE Books SET Title = @Title, Author = @Author WHERE Id = @Id RETURNING *", book);
  }

  public void Delete(long id)
  {
    using var connection = CreateConnection();
    connection.Execute("DELETE FROM Books WHERE Id = @Id;", new { Id = id });
  }
}