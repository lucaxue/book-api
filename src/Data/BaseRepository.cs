using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace BookApi.Data
{
    public class BaseRepository
    {
        IConfiguration _configuration;

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Generate new connection based on env variables
        private NpgsqlConnection SqlConnection()
        {
            var stringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = _configuration["POSTGRES_HOST"],
                Database = _configuration["POSTGRES_DB"],
                Username = _configuration["POSTGRES_USER"],
                Port = Int32.Parse(_configuration["POSTGRES_PORT"]),
                Password = _configuration["POSTGRES_PASSWORD"],
		// Uncomment below if using a Heroku hosted database
                // SslMode = SslMode.Require,
                // TrustServerCertificate = true
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
}
