namespace ProductsApi.Core
{
    public interface IProductsGateway
    {
        Task SaveProductAsync(Product product);

        Task SaveProductsAsync(IEnumerable<Product> products);
    }
}