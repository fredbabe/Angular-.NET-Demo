using Microsoft.AspNetCore.Mvc;
using SupermarketProductBoardAPI.BusinessLogic.CompanyLogic;
using SupermarketProductBoardAPI.Models;

namespace SupermarketProductBoardAPI.Controller
{
    [ApiController]
    [Route("api/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyBusinessLogic companyBusinessLogic;

        public CompanyController(ICompanyBusinessLogic companyBusinessLogic)
        {
            this.companyBusinessLogic = companyBusinessLogic;
        }

        [HttpPost(Name = "CreateCompany")]
        public async Task<IActionResult> CreateCompany([FromBody] Company company)
        {
            try
            {
                await companyBusinessLogic.CreateCompany(company);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet(Name = "GetCompanies")]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            try
            {
                var companies = await companyBusinessLogic.GetCompanies();
                return Ok(companies);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
