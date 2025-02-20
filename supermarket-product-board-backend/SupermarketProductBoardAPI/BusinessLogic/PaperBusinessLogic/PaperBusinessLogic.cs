using SupermarketProductBoardAPI.Models;
using SupermarketProductBoardAPI.Services.PaperService;

namespace SupermarketProductBoardAPI.BusinessLogic.PaperBusinessLogic
{
    public class PaperBusinessLogic : IPaperBusinessLogic
    {
        private readonly IPaperService paperService;

        public PaperBusinessLogic(IPaperService paperService, IConfiguration configuration)
        {
            this.paperService = paperService;
        }

        public async Task<Paper> CreatePaper(Paper paper)
        {
            return await paperService.CreatePaper(paper);
        }
    }
}
