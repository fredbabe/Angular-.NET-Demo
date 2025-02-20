using SupermarketProductBoardAPI.Models.DTOs;

namespace SupermarketProductBoardAPI.Services.MealPlannerService
{
    public interface IMealPlannerService
    {
        Task<MealPlanCreateResponse> CreateMealPlan(MealPlanCreateRequest mealPlanRequest);
    }
}
