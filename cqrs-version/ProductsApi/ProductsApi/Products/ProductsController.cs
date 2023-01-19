using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProductsApi.Products
{
    [ApiController]
    [Route("product")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IEnumerable<Product>> All()
        {
            var result = await _mediator.Send(new ProductsQuery());

            return result.Products;
        }

        [HttpGet("{id}")]
        public async Task<ProductDetailsQueryResult> GetById(int id)
        {
            var result = await _mediator.Send(new ProductDetailsQuery
            {
                Id = id
            });

            return result;
        }
    }
}
