using SupermarketProductBoardAPI.Models;
using SupermarketProductBoardAPI.Models.DTOs;

namespace SupermarketProductBoardAPI.BusinessLogic.CategoryLogic
{
    public interface ICategoryBusinessLogic
    {
        Task<Guid> CreateCategory(CategoryCreateRequest request);

        Task<Category?> GetCategoryByName(string categoryName);
    }
}
