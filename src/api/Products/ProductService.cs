using System.Collections.Generic;
using System.Linq;

namespace ProductsApi.Product
{
    public class ProductService : IProductService
    {

        public IEnumerable<Product> All() => products;

        public ServiceResponse Get(int id)
        {
            var product = products.FirstOrDefault(product => product.Id == id);
            return product is null ? new NotFoundServiceResponse() : new FoundServiceResponse<Product>(product);
        }

        public InsertServiceResponse Add(Product newProduct)
        {
            if (products.Any(p => p.EAN == newProduct.EAN))
            {
                return new InsertConflict();
            }
            return new InsertOk<Product>(newProduct);
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
        InsertServiceResponse Add(Product product);
    }

    public record ServiceResponse { }
    public record NotFoundServiceResponse : ServiceResponse { }
    public record FoundServiceResponse<TItem> : ServiceResponse
    {
        public FoundServiceResponse(TItem foundItem) { item = foundItem; }
        public TItem item { get; init; }
    }

    public record InsertServiceResponse : ServiceResponse { }

    public record InsertConflict : InsertServiceResponse { }
    public record InsertOk<TItem> : InsertServiceResponse
    {
        public InsertOk(TItem foundItem) { item = foundItem; }
        public TItem item { get; init; }
    }
}