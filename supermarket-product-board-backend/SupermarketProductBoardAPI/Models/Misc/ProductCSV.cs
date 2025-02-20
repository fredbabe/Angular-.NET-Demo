namespace SupermarketProductBoardAPI.Models.Misc
{
    public class ProductCSV
    {
        public decimal Price { get; set; }

        public required string Title { get; set; }

        public string? Amount { get; set; }

        public string? Description { get; set; }
    }
}
