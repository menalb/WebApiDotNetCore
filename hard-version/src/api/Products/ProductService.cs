using System;

using ProductsApi.Core;

namespace ProductsApi.Product
{
    public class ProductService : ICommandProductService
    {
        private readonly ProductQuery _query;
        public ProductService(ProductQuery query) => _query = query;

        public InsertServiceResponse Add(Product newProduct) =>
            _query.GetByEan(newProduct.EAN) != null ?
                new InsertConflict() :
                new InsertOkResponse<Product>(newProduct);

        public EditServiceResponse Edit(int id, Product product) =>
            CheckCondition<Product, EditServiceResponse>(
                product, p => _query.Get(id) != null,
                new EditOkResponse<Product>(product),
                new EditNotFoundResponse());

        public DeleteServiceResponse Delete(int id) => new DeleteOkResponse();

        private TResponse CheckCondition<TItem, TResponse>(
            TItem something, Func<TItem, Boolean> condition, TResponse ok, TResponse noOk
            ) =>
          condition(something) ? ok : noOk;

    }

    public interface ICommandProductService
    {
        InsertServiceResponse Add(Product product);
        EditServiceResponse Edit(int id, Product product);
        DeleteServiceResponse Delete(int id);
    }
}