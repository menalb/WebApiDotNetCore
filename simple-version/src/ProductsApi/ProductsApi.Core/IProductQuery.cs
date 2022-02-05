namespace ProductsApi.Core
{
    public interface IProductQuery
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetAsync(int id);
    }
}