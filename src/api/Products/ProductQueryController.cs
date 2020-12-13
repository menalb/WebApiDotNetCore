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
        public ActionResult All(int? page = 1, int? pageSize = 5) =>
            OkWithLinksHeader(_query.GetAll().Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value),
                "GetProducts",
                new PaginationInfo(page.Value, pageSize.Value, _query.GetAll().Count()));


        [HttpGet]
        [Route("{id}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> Get(int id)
            => ParseGetResponse<Product>(_query.Get(id));

        private ActionResult OkWithLinksHeader<T>(T content, string actionName, PaginationInfo paginationInfo)
        {
            Response.Headers.Add(
                "Link",
                new HeaderLinksBuilder(paginationInfo, Url.RouteUrl(actionName, new { })).Build()
                );
            return Ok(content);
        }
    }
}