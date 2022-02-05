using Dapper;
using Microsoft.Data.Sqlite;
using ProductsApi.Core;

namespace ProductsApi.SQLite
{
    public class DatabaseBootstrap
    {
        private readonly SqliteConfiguration _dbConfiguration;

        public DatabaseBootstrap(SqliteConfiguration config)
        {
            _dbConfiguration = config;
        }

        public async Task Setup(IEnumerable<Product> products)
        {
            using var connection = new SqliteConnection(_dbConfiguration.DBConnectionString);

            var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Products';");
            var tableName = table.FirstOrDefault();
            if (!string.IsNullOrEmpty(tableName) && tableName == "Products")
                return;

            await connection.ExecuteAsync("Create Table Products (" +
                "Id INTEGER PRIMARY KEY," +
                "Name VARCHAR(100) NOT NULL," +
                "EAN VARCHAR(13) NOT NULL);");

            await (new ProductsGateway(_dbConfiguration)).SaveProductsAsync(products);
        }
    }
}