using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SupermarketProductBoardAPI.BusinessLogic.CategoryLogic;
using SupermarketProductBoardAPI.BusinessLogic.CompanyLogic;
using SupermarketProductBoardAPI.BusinessLogic.PaperBusinessLogic;
using SupermarketProductBoardAPI.BusinessLogic.ProductBusinessLogic;
using SupermarketProductBoardAPI.Models;
using SupermarketProductBoardAPI.Models.DTOs;
using SupermarketProductBoardAPI.Models.Misc;
using System.Globalization;

namespace SupermarketProductBoardAPI.Controller
{
    [ApiController]
    [Route("api/papers")]
    public class PaperController : ControllerBase
    {
        private readonly IPaperBusinessLogic paperBusinessLogic;
        private readonly ICompanyBusinessLogic companyBusinessLogic;
        private readonly IProductBusinessLogic productBusinessLogic;
        private readonly ICategoryBusinessLogic categoryBusinessLogic;

        public PaperController(
            IPaperBusinessLogic paperBusinessLogic,
            ICompanyBusinessLogic companyBusinessLogic,
            IProductBusinessLogic productBusinessLogic,
            ICategoryBusinessLogic categoryBusinessLogic)
        {
            this.paperBusinessLogic = paperBusinessLogic;
            this.companyBusinessLogic = companyBusinessLogic;
            this.productBusinessLogic = productBusinessLogic;
            this.categoryBusinessLogic = categoryBusinessLogic;
        }

        [HttpPost("create-paper", Name = "CreatePaper")]
        public async Task<IActionResult> CreatePaper([FromBody] Paper paper)
        {
            try
            {
                await paperBusinessLogic.CreatePaper(paper);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create-paper-with-products", Name = "CreatePaperWithProducts")]
        public async Task<IActionResult> CreatePaperWithProducts([FromBody] PaperWithProductsCreateRequest request)
        {
            try
            {
                var products = new List<Product>();

                // Use the raw filePath directly and normalize it
                string filePathString = Path.GetFullPath(request.FilePath);

                // Validate the file path
                if (!System.IO.File.Exists(filePathString))
                {
                    return BadRequest("File not found");
                }

                // Find company by name, stop if non exist.
                var company = await companyBusinessLogic.GetCompanyByName(request.CompanyName);

                if (company == null)
                {
                    return BadRequest("Company not found");
                }

                Paper paper = await paperBusinessLogic.CreatePaper(new Paper
                {
                    Id = new Guid(),
                    Name = request.CompanyName,
                    StartDate = request.PaperStartDate,
                    EndDate = request.PaperEndDate,
                    Company = company,
                    CompanyId = company.Id,
                });

                using (var reader = new StreamReader(filePathString))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    // Assuming the CSV has a header row and maps to a class
                    var records = csv.GetRecords<ProductCSV>();

                    foreach (var record in records)
                    {
                        // Return either the category or a new category
                        var categories = productBusinessLogic.GetProductCategory(record.Title, record.Description);

                        if (categories.newCategory == null && categories.category == null)
                        {
                            return BadRequest("No category found");
                        }

                        // If a new category is found, create it
                        if (categories.newCategory != null && !categories.newCategory.IsNullOrEmpty())
                        {

                            // Check that the category does not already exist
                            var category = await categoryBusinessLogic.GetCategoryByName(categories.newCategory);

                            var categoryId = category == null ? Guid.Empty : category.Id;

                            if (category == null)
                            {
                                categoryId = await categoryBusinessLogic.CreateCategory(new CategoryCreateRequest
                                {
                                    Name = categories.newCategory,
                                });
                            }

                            var product = new Product
                            {
                                Name = record.Title,
                                Description = record.Description,
                                Price = record.Price,
                                CompanyId = company.Id,
                                CategoryId = categoryId,
                                ShowOnline = false,
                                HasNewCategory = true,
                                Amount = record.Amount,
                                PaperId = paper.Id,

                            };

                            products.Add(product);
                        }
                        else
                        {
                            var category = await categoryBusinessLogic.GetCategoryByName(categories.category);

                            var product = new Product
                            {
                                CompanyId = company.Id,
                                Name = record.Title,
                                Description = record.Description,
                                Price = record.Price,
                                Amount = record.Amount,
                                Category = category,
                                HasNewCategory = false,
                                PaperId = paper.Id,
                            };

                            products.Add(product);
                        }
                    }
                }

                await productBusinessLogic.CreateProducts(products);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
