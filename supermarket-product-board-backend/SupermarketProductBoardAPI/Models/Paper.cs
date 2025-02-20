namespace SupermarketProductBoardAPI.Models
{
    public class Paper
    {
        public required Guid Id { get; set; } = Guid.NewGuid();

        public required string Name { get; set; }

        public required DateOnly StartDate { get; set; }

        public required DateOnly EndDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; }

        public required Guid CompanyId { get; set; }

        public required Company Company { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
