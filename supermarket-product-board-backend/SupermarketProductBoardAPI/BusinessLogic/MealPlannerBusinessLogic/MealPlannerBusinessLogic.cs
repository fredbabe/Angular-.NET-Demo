using SupermarketProductBoardAPI.Models.DTOs;
using SupermarketProductBoardAPI.Services.MealPlannerService;

namespace SupermarketProductBoardAPI.BusinessLogic.MealPlannerBusinessLogic
{
    public class MealPlannerBusinessLogic : IMealPlannerBusinessLogic
    {
        private readonly IMealPlannerService mealPlannerService;

        public MealPlannerBusinessLogic(IMealPlannerService mealPlannerService)
        {
            this.mealPlannerService = mealPlannerService;
        }

        public async Task<MealPlanCreateResponse> CreateMealPlanner(MealPlanCreateRequest request)
        {
            return await mealPlannerService.CreateMealPlan(request);
        }

    }
}
