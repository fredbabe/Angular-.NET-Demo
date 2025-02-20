using Microsoft.AspNetCore.Mvc;
using SupermarketProductBoardAPI.BusinessLogic.CategoryLogic;
using SupermarketProductBoardAPI.Models.DTOs;

namespace SupermarketProductBoardAPI.Controller
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryBusinessLogic categoryBusinessLogic;

        public CategoryController(ICategoryBusinessLogic categoryBusinessLogic)
        {
            this.categoryBusinessLogic = categoryBusinessLogic;
        }

        [HttpPost(Name = "CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateRequest category)
        {
            try
            {
                await categoryBusinessLogic.CreateCategory(category);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
