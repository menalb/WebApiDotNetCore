using System.Collections.Generic;
using System.Linq;
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
        private readonly ProductQuery _query;
        public ProductQueryController(ILogger<ProductQueryController> logger, ProductQuery query)
            : base(logger)
            => _query = query;

        [HttpGet(Name = "GetProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult All(int? page = 1, int? pageSize = 5) => GetPagedProducts(page.Value, pageSize.Value);

        [HttpGet]
        [Route("{id}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> Get(int id)
            => ParseGetResponse<Product>(_query.Get(id));

        private ActionResult GetPagedProducts(int page = 1, int pageSize = 5)
        {
            var products = _query.GetAll();
            return OkWithLinksHeader(products.Skip((page - 1) * pageSize).Take(pageSize),
                "GetProducts",
                new PaginationInfo(page, pageSize, products.Count()));
        }
    }
}