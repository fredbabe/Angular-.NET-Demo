using SupermarketProductBoardAPI.Data;
using SupermarketProductBoardAPI.Models;

namespace SupermarketProductBoardAPI.Services.PaperService
{
    public class PaperService : IPaperService
    {
        private readonly AppDbContext context;

        public PaperService(AppDbContext context, IConfiguration configuration)
        {
            this.context = context;

        }

        public async Task<Paper> CreatePaper(Paper paper)
        {
            if (paper == null)
            {
                throw new ArgumentNullException("Paper is null");
            }

            await context.Papers.AddAsync(paper);
            await context.SaveChangesAsync();

            return paper;
        }
    }
}
