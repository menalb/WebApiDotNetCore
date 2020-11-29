using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProductsApi.Product
{
    [ApiController]
    [Route("product")]
    public class ProductQueryController : ControllerBase
    {
        private readonly ILogger<ProductQueryController> _logger;
        private readonly IProductService _service;
        public ProductQueryController(ILogger<ProductQueryController> logger, IProductService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Product> All() => _service.All();


        [HttpGet("{id}")]
        [ActionName("GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> Get(int id) => ParseGetResponse<Product>(_service.Get(id));

        protected ActionResult ParseGetResponse<TItem>(ServiceResponse response) =>
         response switch
         {
             NotFoundServiceResponse => NotFound(),
             FoundServiceResponse<TItem> content => Ok(content.item),
             _ => throw new ArgumentException($"Unhandled case {nameof(response)}")
         };
    }
}