using Microsoft.AspNetCore.Mvc;
using SupermarketProductBoardAPI.Models;

namespace SupermarketProductBoardAPI.BusinessLogic.ProductBusinessLogic
{
    public interface IProductBusinessLogic
    {
        Task CreateProduct(Product product);

        Task CreateProducts(IEnumerable<Product> products);

        Task<ActionResult<IEnumerable<Product>>> GetProducts();

        (string? category, string? newCategory) GetProductCategory(string name, string? description);

        Task<IEnumerable<string>> GetAndParseProductsFromSupermarket(Guid supermarketId);
    }
}
