using Microsoft.AspNetCore.Mvc;
using SupermarketProductBoardAPI.Models;
using SupermarketProductBoardAPI.Services.ProductService;

namespace SupermarketProductBoardAPI.BusinessLogic.ProductBusinessLogic
{
    public class ProductBusinessLogic : IProductBusinessLogic
    {
        private readonly IProductService productService;

        public ProductBusinessLogic(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task CreateProduct(Product product)
        {
            await productService.CreateProduct(product);
        }

        public async Task CreateProducts(IEnumerable<Product> products)
        {
            await productService.CreateProducts(products);
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await productService.GetProducts();
        }

        public (string? category, string? newCategory) GetProductCategory(string name, string? description)
        {
            return productService.FindProductCategories(name, description);
        }

        public async Task<IEnumerable<string>> GetAndParseProductsFromSupermarket(Guid supermarketId)
        {
            return await productService.GetAndParseProductsFromSupermarket(supermarketId);
        }
    }
}
