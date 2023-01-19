using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsApi.Data;

namespace ProductsApi.Products
{
    public class ProductDetailsQueryHandler : IRequestHandler<ProductDetailsQuery, ProductDetailsQueryResult>
    {
        private readonly ProductsContext _db;

        public ProductDetailsQueryHandler(ProductsContext db) => _db = db;

        public async Task<ProductDetailsQueryResult> Handle(ProductDetailsQuery request, CancellationToken cancellationToken)
            =>
            await _db.Products.AsNoTracking()
                .Where(p => p.Id == request.Id)
                .Select(p => new ProductDetailsQueryResult(p.Id, p.Name))
                .SingleOrDefaultAsync(cancellationToken);
    }

    public record ProductDetailsQuery : IRequest<ProductDetailsQueryResult>
    {
        public int? Id { get; init; }
    }

    public record ProductDetailsQueryResult(int Id, string Name);
}
