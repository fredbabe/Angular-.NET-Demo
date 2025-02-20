using Microsoft.AspNetCore.Mvc;
using SupermarketProductBoardAPI.Models;

namespace SupermarketProductBoardAPI.Services.ProductService
{
    public interface IProductService
    {
        Task CreateProduct(Product product);

        Task<ActionResult<IEnumerable<Product>>> GetProducts();

        Task CreateProducts(IEnumerable<Product> products);

        (string? category, string? newCategory) FindProductCategories(string name, string? description);

        Task<List<string>> GetAndParseProductsFromSupermarket(Guid supermarketId);
    }
}
