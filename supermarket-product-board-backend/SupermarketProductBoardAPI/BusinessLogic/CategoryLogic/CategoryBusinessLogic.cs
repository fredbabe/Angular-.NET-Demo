using SupermarketProductBoardAPI.Models;
using SupermarketProductBoardAPI.Models.DTOs;
using SupermarketProductBoardAPI.Services.CategoryService;

namespace SupermarketProductBoardAPI.BusinessLogic.CategoryLogic
{
    public class CategoryBusinessLogic : ICategoryBusinessLogic
    {
        private readonly ICategoryService categoryService;

        public CategoryBusinessLogic(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<Guid> CreateCategory(CategoryCreateRequest request)
        {
            return await categoryService.CreateCategory(request);
        }

        public async Task<Category?> GetCategoryByName(string categoryName)
        {
            return await categoryService.GetCategoryByName(categoryName);
        }
    }
}
