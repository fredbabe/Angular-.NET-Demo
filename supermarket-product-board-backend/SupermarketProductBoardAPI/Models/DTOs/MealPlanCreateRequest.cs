using SupermarketProductBoardAPI.Models.Enums;

namespace SupermarketProductBoardAPI.Models.DTOs
{
    public class MealPlanCreateRequest
    {
        public required int Days { get; set; }

        public required MealType MealType { get; set; }

        public required int AmountOfPeople { get; set; }

        public required Guid SuperMarketId { get; set; }

        public IEnumerable<string>? PreferedIngredients { get; set; }

        public string? FreshnessPreference { get; set; }
    }
}
