using Bogus;
using ProductsApi.Core;

namespace ProductsApi
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var bootstrapper = serviceProvider.GetService<SQLite.DatabaseBootstrap>();
            var db = serviceProvider.GetService<IProductsGateway>();
            if (bootstrapper != null && db != null)
            {
                var products = GiveMeSomeProducts(1000);
                await bootstrapper.Setup(products);
            }
        }

        private static IEnumerable<Product> GiveMeSomeProducts(int howMany)
        {
            var productId = 0;
            return new Faker<Product>()
            .StrictMode(true)
            .RuleFor(p => p.Id, f => productId++)
            .RuleFor(p => p.EAN, f => f.Commerce.Ean13())
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .Generate(howMany);
        }
    }
}
