using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProductsApi.Product
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _service;
        public ProductController(ILogger<ProductController> logger, IProductService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Product> All() => _service.All();

        [HttpGet("{id}")]
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