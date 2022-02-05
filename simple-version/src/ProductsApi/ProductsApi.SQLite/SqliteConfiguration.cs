namespace ProductsApi.SQLite
{
    public class SqliteConfiguration
    {
        public SqliteConfiguration(string dbConnectionString)
        {
            DBConnectionString = dbConnectionString;
        }

        public string DBConnectionString { get; }
    }
}