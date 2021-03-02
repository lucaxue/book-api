using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Threading.Tasks;
public class BookRepository : BaseRepository, IRepository<Book>
{

    public BookRepository(IConfiguration configuration) : base(configuration) { }

    public async Task<IEnumerable<Book>> GetAll()
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync<Book>("SELECT * FROM Books;");
   
    }


    public void Delete(long id)
    {
        using var connection = CreateConnection();
        connection.Execute("DELETE FROM Books WHERE Id = @Id;", new { Id = id });
    }

    public async Task<Book> Get(long id)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Book>("SELECT * FROM Books WHERE Id = @Id;", new { Id = id });
    }

    public async Task<Book> Update(Book book)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Book>("UPDATE Books SET Title = @Title, Author = @Author WHERE Id = @Id RETURNING *", book);
    }

    public async Task<Book> Insert(Book book)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Book>("INSERT INTO Books (Title, Author) VALUES (@Title, @Author) RETURNING *;", book);
    }

    public async Task<IEnumerable<Book>> Search(string query)
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync<Book>("SELECT * FROM Books WHERE LOWER(Title) LIKE @Query OR LOWER(Author) LIKE @Query;", new {Query= $"%{query}%"});
        
    }

}

