using Microsoft.AspNetCore.Mvc;
using SupermarketProductBoardAPI.BusinessLogic.MealPlannerBusinessLogic;
using SupermarketProductBoardAPI.Models.DTOs;

namespace SupermarketProductBoardAPI.Controller
{
    [ApiController]
    [Route("api/meal-planner")]
    public class MealPlannerController : ControllerBase
    {
        private readonly IMealPlannerBusinessLogic mealPlannerBusinessLogic;

        public MealPlannerController(IMealPlannerBusinessLogic mealPlannerBusinessLogic)
        {
            this.mealPlannerBusinessLogic = mealPlannerBusinessLogic;
        }

        [HttpPost("create-meal-plan", Name = "CreateMealPlan")]
        public async Task<ActionResult<MealPlanCreateResponse>> CreateMealPlan([FromBody] MealPlanCreateRequest mealPlanRequest)
        {
            try
            {
                MealPlanCreateResponse mealPlan = await mealPlannerBusinessLogic.CreateMealPlanner(mealPlanRequest);

                return Ok(mealPlan);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
