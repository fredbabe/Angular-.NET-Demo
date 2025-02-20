using SupermarketProductBoardAPI.Models;

namespace SupermarketProductBoardAPI.Services.CompanyService
{
    public interface ICompanyService
    {
        Task CreateCompany(Company company);

        Task<Company?> GetCompanyByName(string companyName);

        Task<IEnumerable<Company>> GetCompaniesById(IEnumerable<Guid> companyIds);

        Task<IEnumerable<Company>> GetCompanies();
    }
}
