using Microsoft.AspNetCore.Mvc;
using ProductsApi.Core;

namespace ProductsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductQuery _query;
        private readonly ProductService _service;

        public ProductsController(ILogger<ProductsController> logger, ProductService service, IProductQuery query)
        {
            _logger = logger;
            _service = service;
            _query = query;
        }

        [HttpGet(Name = "GetProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IEnumerable<Product>> All()
            => _query.GetAllAsync();

        [HttpGet]
        [Route("{id}", Name = "GetProductById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(int id)
            => await _query.GetAsync(id) switch
            {
                null => NotFound(),
                Product p => Ok(p),
            };

        [HttpPost(Name = "AddNewProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<Product> AddNew(Product product)
        {
            await _service.CreateNewAsync(product);
            return await _query.GetAsync(product.Id);
        }
    }
}