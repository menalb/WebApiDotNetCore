using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProductsApi.Product
{
    [ApiController]
    [Route("product")]
    public class ProductCommandController : ControllerBase
    {
        private readonly ILogger<ProductCommandController> _logger;
        private readonly IProductService _service;
        public ProductCommandController(ILogger<ProductCommandController> logger, IProductService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public ActionResult<Product> Add(Product product) => ParsePostResponse<Product>(_service.Add(product));

        protected ActionResult ParseGetResponse<TItem>(ServiceResponse response) =>
         response switch
         {
             NotFoundServiceResponse => NotFound(),
             FoundServiceResponse<TItem> content => Ok(content.item),
             _ => throw new ArgumentException($"Unhandled case {nameof(response)}")
         };

        [HttpGet("{id}")]
        [ActionName("GetProduct")]
        public ActionResult<Product> Get(int id) => ParseGetResponse<Product>(_service.Get(id));


        protected ActionResult ParsePostResponse<TItem>(ServiceResponse response) where TItem : BaseEntity =>
               response switch
               {
                   InsertConflict => Conflict(),
                   InsertOk<TItem> content => Created("GetProduct", new { id = content.item.Id }),
                   _ => throw new ArgumentException($"Unhandled case {nameof(response)}")
               };
    }
}