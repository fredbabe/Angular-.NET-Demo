namespace SupermarketProductBoardAPI.Models.DTOs
{
    public class PaperWithProductsCreateRequest
    {
        public required string CompanyName { get; set; }

        public required string FilePath { get; set; }

        public required DateOnly PaperStartDate { get; set; }

        public required DateOnly PaperEndDate { get; set; }
    }
}
