using Dapper;
using Microsoft.Data.Sqlite;
using ProductsApi.Core;

namespace ProductsApi.SQLite
{
    public class ProductQuery : IProductQuery
    {
        private readonly string _dbConnectionString;

        public ProductQuery(SqliteConfiguration configuration)
        {
            _dbConnectionString = configuration.DBConnectionString;
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            var conn = new SqliteConnection(_dbConnectionString);
            return conn.QueryAsync<Product>("SELECT Id, Name, EAN From Products");
        }

        public Task<Product> GetAsync(int id)
        {
            var conn = new SqliteConnection(_dbConnectionString);
            return conn.QuerySingleOrDefaultAsync<Product>("SELECT Id, Name, EAN From Products WHERE Id = @id", new { id });
        }
    }
}