using Dapper;
using Microsoft.Data.Sqlite;
using ProductsApi.Core;

namespace ProductsApi.SQLite
{
    public class ProductsGateway : IProductsGateway
    {
        private readonly string _dbConnectionString;

        public ProductsGateway(SqliteConfiguration configuration)
        {
            _dbConnectionString = configuration.DBConnectionString;
        }

        public async Task SaveProductAsync(Product product)
        {
            var conn = new SqliteConnection(_dbConnectionString);
            await conn.ExecuteAsync("INSERT INTO Products VALUES (@Id,@Name,@EAN)", product);
        }

        public async Task SaveProductsAsync(IEnumerable<Product> products)
        {
            var conn = new SqliteConnection(_dbConnectionString);
            await conn.ExecuteAsync("INSERT INTO Products VALUES (@Id,@Name,@EAN)", products);
        }
    }
}