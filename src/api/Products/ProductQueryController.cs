using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using ProductsApi.Core;

namespace ProductsApi.Product
{
    [ApiController]
    [Route("product")]
    public class ProductQueryController : ApiControllerBase<ProductQueryController>
    {
        private readonly IQueryProductService _service;
        public ProductQueryController(ILogger<ProductQueryController> logger, IQueryProductService service)
            : base(logger)
            => _service = service;


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Product> All() => _service.All();


        [HttpGet]
        [Route("{id}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> Get(int id)
            => ParseGetResponse<Product>(_service.Get(id));
    }
}