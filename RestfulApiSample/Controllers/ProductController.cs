using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestfulApiSample.Filters;
using RestfulApiSample.Models;
using RestfulApiSample.Settings;
using System.Net;
using System.Net.Mime;

namespace RestfulApiSample.Controllers
{
    [Log]
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private MySettings _settings;
        public ProductController(IOptionsSnapshot<MySettings> settings)
        {
            _settings = settings.Value;
        }

        public static List<Product> products = new()
        {
            new() { Id = 1, Name = "Test 1" },
            new() { Id = 2, Name = "Test 2" },
            new() { Id = 3, Name = "Test 3" },
        };

        [HttpGet]
        [Route("ActionWithNoContentResponse")]
        public void ActionWithNoContentResponse()
        {
            Console.WriteLine(_settings.StringSetting + "ActionWithNoContentResponse called");
        }

        [HttpGet]
        [Route("ActionWithDataTypeResponse")]
        public string ActionWithDataTypeResponse()
        {
            return "Products";
        }

        [HttpGet]
        [Route("ActionWithEntityResponse")]
        public Product ActionWithEntityResponse()
        {
            return new Product() { Id = 1, Name = "Parham" };
        }

        [HttpGet]
        [Route("ActionWithHttpResponseMessage")]
        public HttpResponseMessage ActionWithHttpResponseMessage()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("ActionWithStatusCodeResult")]
        public StatusCodeResult ActionWithStatusCodeResult()
        {
            return StatusCode((int)HttpStatusCode.Unauthorized);
        }

        [HttpGet]
        [Route("ActionWithActionResult")]
        public ActionResult<Product> ActionWithActionResult(int id)
        {
            var product = products.FirstOrDefault(x => x.Id == id);
            return product is null ? NotFound() : product;
        }

        [HttpGet]
        [Route("ActionWithActionResultListData")]
        public ActionResult<List<int>> ActionWithActionResultListData()
        {

            List<int> ints = products.Select(x=> x.Id).ToList();
            return products is null ? NotFound() : ints;

        }

        [HttpGet]
        [Route("ActionWithActionResultIEnumerableData")]
        public ActionResult<IEnumerable<Product>> ActionWithActionResultIEnumerableData()
        {
            return products is null ? NotFound() : products;
        }

        [HttpGet]
        [Route("ActionWithIActionResult")]
        public IActionResult ActionWithIActionResult()
        {
            return Ok(products);
        }

        [HttpGet]
        [Route("ActionWithIActionResultWithId")]
        public IActionResult ActionWithIActionResultWithId(int id)
        {
            var product = products.FirstOrDefault(x => x.Id == id);
            return product is null ? NotFound($"There is no product with ID: {id}") : Ok(product);
        }

        //If you are in .Net 6 you will have problem
        //[HttpGet("{id}")]
        //[ProducesResponseType<Product>(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult GetById_IActionResult(int id)
        //{
        //    var product = _productContext.Products.Find(id);
        //    return product == null ? NotFound() : Ok(product);
        //}

        [HttpPost()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
                return BadRequest();

            return CreatedAtAction(nameof(ActionWithIActionResultWithId), new { id = product.Id }, product);
        }
    }
}
