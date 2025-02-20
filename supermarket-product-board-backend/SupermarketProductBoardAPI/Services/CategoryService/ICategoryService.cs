using SupermarketProductBoardAPI.Models;
using SupermarketProductBoardAPI.Models.DTOs;

namespace SupermarketProductBoardAPI.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<Guid> CreateCategory(CategoryCreateRequest request);

        Task<Category?> GetCategoryByName(string categoryName);
    }
}
