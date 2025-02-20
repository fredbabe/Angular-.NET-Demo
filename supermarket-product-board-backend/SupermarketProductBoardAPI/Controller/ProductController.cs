using Microsoft.AspNetCore.Mvc;
using SupermarketProductBoardAPI.BusinessLogic.ProductBusinessLogic;
using SupermarketProductBoardAPI.Models;

namespace SupermarketProductBoardAPI.Controller
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductBusinessLogic productBusinessLogic;

        public ProductController(IProductBusinessLogic productBusinessLogic)
        {
            this.productBusinessLogic = productBusinessLogic;
        }

        [HttpPost(Name = "CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            try
            {
                await productBusinessLogic.CreateProduct(product);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An unexpected error occurred: {e.Message}");
            }
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                return await productBusinessLogic.GetProducts();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An unexpected error occurred: {e.Message}");
            }
        }
    }
}
