using SupermarketProductBoardAPI.Models;

namespace SupermarketProductBoardAPI.BusinessLogic.CompanyLogic
{
    public interface ICompanyBusinessLogic
    {
        Task CreateCompany(Company company);

        Task<Company?> GetCompanyByName(string companyName);

        Task<IEnumerable<Company>> GetCompaniesById(IEnumerable<Guid> companyIds);

        Task<IEnumerable<Company>> GetCompanies();
    }
}
