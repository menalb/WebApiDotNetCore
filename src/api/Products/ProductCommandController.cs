using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using ProductsApi.Core;

namespace ProductsApi.Product
{
    [ApiController]
    [Route("product")]
    public class ProductCommandController : ApiControllerBase<ProductCommandController>
    {
        private readonly ICommandProductService _service;
        public ProductCommandController(
            ILogger<ProductCommandController> logger,
            ICommandProductService service
            )
            : base(logger)
            => _service = service;


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<Product> Add(Product product)
            => ParsePostResponse<Product>(_service.Add(product));


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> Edit(int id, Product product)
            => ModelState.IsValid ? ParsePutResponse<Product>(_service.Edit(id, product)) : BadRequest(ModelState);
    }
}