using System;
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

        public EditServiceResponse Edit(int id, Product product) =>
            CheckCondition<Product, EditServiceResponse>(
                product, p => Get(id) is NotFoundServiceResponse,
                new EditOkResponse<Product>(product),
                new EditNotFoundResponse());

        public DeleteServiceResponse Delete(int id) => new DeleteOkResponse();

        private TResponse CheckCondition<TItem, TResponse>(
            TItem something, Func<TItem, Boolean> condition, TResponse ok, TResponse noOk
            ) =>
          condition(something) ? ok : noOk;


        private static readonly IEnumerable<Product> products = new List<Product>{
            new Product{Id=1,Name="Pasta", EAN="123456789"},
            new Product{Id=2,Name="Tuna", EAN="987654321"},
            new Product{Id=3,Name="Butter", EAN="63728291"}
        };
    }

    public interface ICommandProductService
    {
        InsertServiceResponse Add(Product product);
        EditServiceResponse Edit(int id, Product product);
        DeleteServiceResponse Delete(int id);
    }

    public interface IQueryProductService
    {
        IEnumerable<Product> All();
        ServiceResponse Get(int id);
    }

}