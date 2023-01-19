using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsApi.Data;

namespace ProductsApi.Products
{
    public class ProductsQueryHandler : IRequestHandler<ProductsQuery, ProductsQueryResult>
    {
        private readonly ProductsContext _db;

        public ProductsQueryHandler(ProductsContext db) => _db = db;

        public async Task<ProductsQueryResult> Handle(ProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _db.Products.Select(p => new Product(p.Id, p.Name)).ToListAsync(cancellationToken: cancellationToken);
            return new ProductsQueryResult { Products = products };
        }
    }

    public record ProductsQueryResult
    {
        public List<Product> Products { get; set; } = new();
    }

    public record ProductsQuery : IRequest<ProductsQueryResult> { }

    public record Product(int Id, string Name);
}
