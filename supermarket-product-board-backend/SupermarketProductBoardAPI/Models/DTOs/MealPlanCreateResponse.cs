using System.Text.Json.Serialization;

namespace SupermarketProductBoardAPI.Models.DTOs
{
    public class MealPlanCreateResponse
    {
        [JsonPropertyName("days")]
        public List<MealDayDTO> Days { get; set; }
    }
}
