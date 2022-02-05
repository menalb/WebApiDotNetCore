namespace ProductsApi.Core
{
    public class ProductService
    {
        private readonly IProductsGateway _productsGateway;

        public ProductService(IProductsGateway productsGateway)
        {
            _productsGateway = productsGateway;
        }

        public async Task CreateNewAsync(Product product)
        {
            if (product == null || string.IsNullOrEmpty(product.Name) || string.IsNullOrEmpty(product.EAN))
            {
                throw new ArgumentException("Invalid product data");
            }

            if(product.Id <=0)
            {
                throw new ArgumentException("Invalid Id");
            }

            await _productsGateway.SaveProductAsync(product);
        }
    }
}