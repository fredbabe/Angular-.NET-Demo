using Microsoft.EntityFrameworkCore;
using SupermarketProductBoardAPI.Data;
using SupermarketProductBoardAPI.Models;

namespace SupermarketProductBoardAPI.Services.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext context;

        public CompanyService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task CreateCompany(Company company)
        {
            await context.Companies.AddAsync(company);
            await context.SaveChangesAsync();
        }

        public async Task<Company?> GetCompanyByName(string companyName)
        {
            return await context.Companies.FirstOrDefaultAsync(c => c.Name == companyName);
        }

        public async Task<IEnumerable<Company>> GetCompaniesById(IEnumerable<Guid> companyIds)
        {
            return await context.Companies.Where(c => companyIds.Contains(c.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await context.Companies.ToListAsync();
        }
    }
}
