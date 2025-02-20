using Microsoft.EntityFrameworkCore;
using SupermarketProductBoardAPI.Data;
using SupermarketProductBoardAPI.Models;
using SupermarketProductBoardAPI.Models.DTOs;

namespace SupermarketProductBoardAPI.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext context;

        public CategoryService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Guid> CreateCategory(CategoryCreateRequest request)
        {
            Category category = new Category
            {
                Name = request.Name,
            };

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return category.Id;
        }

        public async Task<Category?> GetCategoryByName(string categoryName)
        {
            return await context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
        }
    }
}
