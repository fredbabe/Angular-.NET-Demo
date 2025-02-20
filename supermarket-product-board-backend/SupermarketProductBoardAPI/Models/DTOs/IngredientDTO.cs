using System.Text.Json.Serialization;

namespace SupermarketProductBoardAPI.Models.DTOs
{
    public class IngredientDTO
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public string Price { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }
    }
}
