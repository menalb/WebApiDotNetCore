using Bogus;
using ProductsApi.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProductsApi.Tests
{
    public class ProductsServiceTests
    {
        private readonly ProductService _productService;
        private readonly ProductGatewayFake _productsGateway;

        public ProductsServiceTests()
        {
            _productsGateway = new ProductGatewayFake();
            _productService = new ProductService(_productsGateway);
        }

        [Fact]
        public async Task GivenANewProduct_WhenTheNameIsMissing_ItDoesNotSaveTheProduct()
        {
            var emptyProduct = new Product();

            
            await Assert.ThrowsAsync<ArgumentException>(() => _productService.CreateNewAsync(emptyProduct));

            Assert.Empty(_productsGateway.AddedProducts);
        }

        [Fact]
        public async Task GivenANewProduct_WhenTheIdIsInvalid_ItDoesNotSaveTheProduct()
        {
            var invalidProduct = GiveMeAProduct();
            invalidProduct.Id = -1;

            await Assert.ThrowsAsync<ArgumentException>(() => _productService.CreateNewAsync(invalidProduct));

            Assert.Empty(_productsGateway.AddedProducts);
        }

        [Fact]
        public async Task GivenANewProduct_WhenItIsAGoodProduct_ItSaveTheProduct()
        {
            var goodProduct = GiveMeAProduct();

            await _productService.CreateNewAsync(goodProduct);

            var addedProduct = _productsGateway.AddedProducts.Single();
            Assert.Equal(goodProduct,addedProduct);
        }

        private static Product GiveMeAProduct()
        {
            return new Faker<Product>()
            .StrictMode(true)
            .RuleFor(p => p.Id, f => f.Random.Number())
            .RuleFor(p => p.EAN, f => f.Commerce.Ean13())
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .Generate();
        }
    }
}