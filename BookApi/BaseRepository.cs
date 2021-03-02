using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

public class BaseRepository
{
    IConfiguration _configuration;

    public BaseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    // Generate new connection based on connection string
    private NpgsqlConnection SqlConnection()
    {
        var stringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = _configuration["PGHOST"],
            Database = _configuration["PGDATABASE"],
            Username = _configuration["PGUSER"],
            Port = Int32.Parse(_configuration["PGPORT"]),
            Password = _configuration["PGPASSWORD"],
            SslMode = SslMode.Require, // heroku specific setting https://stackoverflow.com/questions/37276821/connecting-to-heroku-postgres-database-with-asp-net
            TrustServerCertificate = true // heroku specific setting 
        };
        return new NpgsqlConnection(stringBuilder.ConnectionString);
    }

    // Open new connection and return it for use
    public IDbConnection CreateConnection()
    {
        var conn = SqlConnection();
        conn.Open();
        return conn;
    }

}