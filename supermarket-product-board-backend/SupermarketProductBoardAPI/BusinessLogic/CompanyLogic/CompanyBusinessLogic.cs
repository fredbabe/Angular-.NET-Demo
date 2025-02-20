using SupermarketProductBoardAPI.Models;
using SupermarketProductBoardAPI.Services.CompanyService;

namespace SupermarketProductBoardAPI.BusinessLogic.CompanyLogic
{
    public class CompanyBusinessLogic : ICompanyBusinessLogic
    {
        private readonly ICompanyService companyService;

        public CompanyBusinessLogic(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        public async Task CreateCompany(Company company)
        {
            await companyService.CreateCompany(company);
        }

        public async Task<Company?> GetCompanyByName(string companyName)
        {
            return await companyService.GetCompanyByName(companyName);
        }

        public async Task<IEnumerable<Company>> GetCompaniesById(IEnumerable<Guid> companyIds)
        {
            return await companyService.GetCompaniesById(companyIds);
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await companyService.GetCompanies();
        }
    }
}
