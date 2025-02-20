using SupermarketProductBoardAPI.Models;

namespace SupermarketProductBoardAPI.Services.PaperService
{
    public interface IPaperService
    {
        Task<Paper> CreatePaper(Paper paper);
    }
}
