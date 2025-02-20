using SupermarketProductBoardAPI.Models.DTOs;

namespace SupermarketProductBoardAPI.BusinessLogic.MealPlannerBusinessLogic
{
    public interface IMealPlannerBusinessLogic
    {
        Task<MealPlanCreateResponse> CreateMealPlanner(MealPlanCreateRequest request);
    }
}
