using ProductsApi.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsApi.Tests
{
    internal class ProductGatewayFake : IProductsGateway
    {
        public ProductGatewayFake()
        {
            AddedProducts = new List<Product>();
        }

        public Task SaveProductAsync(Product product)
        {
            return Task.Factory.StartNew(() => AddedProducts.Add(product));
        }

        public Task SaveProductsAsync(IEnumerable<Product> products)
        {
            throw new System.NotImplementedException();
        }

        public IList<Product> AddedProducts { get; }
    }
}