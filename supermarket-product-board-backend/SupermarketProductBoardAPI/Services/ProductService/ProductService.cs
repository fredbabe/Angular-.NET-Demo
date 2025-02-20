using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenAI.Chat;
using SupermarketProductBoardAPI.Data;
using SupermarketProductBoardAPI.Models;
using System.Text.Json;

namespace SupermarketProductBoardAPI.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext context;
        private readonly string openAiApiKey;


        public ProductService(AppDbContext context, IConfiguration configuration)
        {
            this.context = context;
            openAiApiKey = configuration.GetRequiredSection("AppSecrets:OpenApiKey").Value
                ?? throw new ArgumentNullException("OpenAiApiKey is not configured.");
        }

        public async Task CreateProduct(Product product)
        {
            await context.AddAsync(product);
            await context.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await context.Products.ToListAsync();
        }

        public async Task CreateProducts(IEnumerable<Product> products)
        {
            await context.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }

        // Function to find category for a product
        public (string? category, string? newCategory) FindProductCategories(string name, string? description)
        {
            ChatClient client = new(model: "gpt-4o", apiKey: openAiApiKey);

            ChatCompletionOptions options = new()
            {
                ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                    jsonSchemaFormatName: "find_category",
                    jsonSchema: BinaryData.FromBytes("""
                        {
                            "type": "object",
                            "properties": {
                                "category": { "type": "string" },
                                "newCategory": { "type": "string" } 
                            },
                            "required": ["category", "newCategory"],
                            "additionalProperties": false
                        }
                        """u8.ToArray()),
                    jsonSchemaIsStrict: true)
            };


            ChatCompletion completion = client.CompleteChat(
                [
                    new SystemChatMessage("Categorize a product based on its name and potentially its description into predefined categories. Use the provided category list and adjust categories when needed.\n\n- Predefined Categories: Vegetable, Fruit, Meat, Candy, Alcohol, Bread, Dairy, Fish, Vegan, Beverage, Grain.\n\n# Steps\n\n1. **Analyze the Name:** Start by examining the name of the product to identify any keywords or terms that indicate its category.\n2. **Review the Description:** If a description is available, review it for additional context or keywords that might help determine the correct category.\n3. **Match with Categories:** Compare the identified keywords with the predefined categories list to determine the best fit.\n4. **Handle Unmatched Categories:** If no match is found:\n   - Set `category` to an empty string.\n   - Assign the suggested category to `newCategory`.\n\n# Output Format\n\n- Return two variables:\n  - `category` (string): The matching category from the predefined list, or an empty string if none found.\n  - `newCategory` (string): An alternative category suggested by the system, or an empty string if not applicable.\n\n# Examples\n\n**Example 1:**\n\n- **Input:**\n  - **Name:** \"Organic Carrot\"\n  - **Description:** \"A fresh organic root vegetable perfect for salads and cooking.\"\n- **Output:**\n  - `category`: \"Vegetable\"\n  - `newCategory`: \"\"\n\n**Example 2:**\n\n- **Input:**\n  - **Name:** \"Cleaning wipes\"\n  - **Description:** \"\"\n- **Output:**\n  - `category`: \"\"\n  - `newCategory`: \"Cleaning Agent\"\n\n# Notes\n\n- If the product name clearly indicates the category, the description analysis may be skipped.\n- Consider edge cases where a product might belong to more than one category and prioritize based on the most dominant feature.\n- Ensure `newCategory` captures a fitting alternative category when `category` remains empty."),
                    new UserChatMessage($"Name: {name}, Description: {description}")
                ], options);

            using JsonDocument structuredJson = JsonDocument.Parse(completion.Content[0].Text);

            if (completion.Content[0].Text.Contains("error"))
            {
                throw new Exception("Error in finding category");
            }

            var category = structuredJson.RootElement.GetProperty("category").GetString();
            var newCategory = structuredJson.RootElement.GetProperty("newCategory").GetString();

            if (category == null && newCategory == null)
            {
                throw new Exception("Category not found");
            }

            return (category, newCategory);
        }

        public async Task<List<string>> GetAndParseProductsFromSupermarket(Guid supermarketId)
        {
            var newestPaper = await context.Papers
                .Where(x => x.CompanyId == supermarketId)
                .OrderByDescending(x => x.StartDate)
                .FirstOrDefaultAsync();

            if (newestPaper == null)
            {
                throw new Exception("No paper found for the supermarket");
            }

            var products = await context.Products
                .Where(x => x.PaperId == newestPaper.Id)
                .ToListAsync();

            var formattedProducts = products.Select(product =>
            {
                var category = product.Category?.Name ?? "Unknown";
                var supermarket = product.Company?.Name ?? "Unknown";

                return $"- Name: {product.Name}, " +
                       $"Description: {product.Description ?? "N/A"}, " +
                       $"Amount: {product.Amount ?? "N/A"}, " +
                       $"Price: {product.Price} DKK, " +
                       $"Supermarket: {supermarket}, " +
                       $"Category: {category}";
            }).ToList();

            return formattedProducts;
        }
    }
}
