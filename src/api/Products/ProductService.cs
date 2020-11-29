using System.Collections.Generic;
using System.Linq;

namespace ProductsApi.Product
{
    public class ProductService : IProductService
    {

        public IEnumerable<Product> All() => products;

        public ServiceResponse Get(int id)
        {
            var product = products.FirstOrDefault(x => x.Id == id);
            return product is null ? new NotFoundServiceResponse() : new FoundServiceResponse<Product>(product);
        }

        private static readonly IEnumerable<Product> products = new List<Product>{
            new Product{Id=1,Name="Pasta", EAN="123456789"},
            new Product{Id=2,Name="Tuna", EAN="987654321"},
            new Product{Id=3,Name="Butter", EAN="63728291"}
        };
    }

    public interface IProductService
    {
        IEnumerable<Product> All();
        ServiceResponse Get(int id);
    }

    public record ServiceResponse { }
    public record NotFoundServiceResponse : ServiceResponse { }
    public record FoundServiceResponse<TItem> : ServiceResponse
    {
        public FoundServiceResponse(TItem foundItem) { item = foundItem; }
        public TItem item { get; init; }
    }
}