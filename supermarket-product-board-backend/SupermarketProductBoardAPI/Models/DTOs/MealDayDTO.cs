using System.Text.Json.Serialization;

namespace SupermarketProductBoardAPI.Models.DTOs
{
    public class MealDayDTO
    {
        [JsonPropertyName("day")]
        public string Day { get; set; }

        [JsonPropertyName("meal")]
        public string Meal { get; set; }

        [JsonPropertyName("ingredients")]
        public List<IngredientDTO> Ingredients { get; set; }

        [JsonPropertyName("preparation")]
        public string Preparation { get; set; }
    }
}
