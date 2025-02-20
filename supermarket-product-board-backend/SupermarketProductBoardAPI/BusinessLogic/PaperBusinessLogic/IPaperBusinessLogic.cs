using SupermarketProductBoardAPI.Models;

namespace SupermarketProductBoardAPI.BusinessLogic.PaperBusinessLogic
{
    public interface IPaperBusinessLogic
    {
        Task<Paper> CreatePaper(Paper paper);
    }
}
