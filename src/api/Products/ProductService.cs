using System.Collections.Generic;
using System.Linq;

using ProductsApi.Core;

namespace ProductsApi.Product
{
    public class ProductService : IQueryProductService, ICommandProductService
    {
        public IEnumerable<Product> All() => products;

        public ServiceResponse Get(int id)
        {
            var product = products.FirstOrDefault(product => product.Id == id);
            return product is null ? new NotFoundServiceResponse() : new FoundServiceResponse<Product>(product);
        }

        public InsertServiceResponse Add(Product newProduct) =>
            products.Any(product => product.EAN == newProduct.EAN) ?
                new InsertConflict() :
                new InsertOkResponse<Product>(newProduct);

        public EditServiceResponse Edit(Product product)
        {
            var p = Get(product.Id);

            return p is NotFoundServiceResponse ?
                new EditNotFoundResponse() :
                new EditOkResponse<Product>(product);
        }

        private static readonly IEnumerable<Product> products = new List<Product>{
            new Product{Id=1,Name="Pasta", EAN="123456789"},
            new Product{Id=2,Name="Tuna", EAN="987654321"},
            new Product{Id=3,Name="Butter", EAN="63728291"}
        };
    }

    public interface ICommandProductService
    {
        InsertServiceResponse Add(Product product);
        EditServiceResponse Edit(Product product);
    }

    public interface IQueryProductService
    {
        IEnumerable<Product> All();
        ServiceResponse Get(int id);
    }

}