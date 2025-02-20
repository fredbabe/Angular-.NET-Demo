namespace SupermarketProductBoardAPI.Models
{
    public class Company
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required string Name { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();

        public ICollection<Paper> Papers { get; set; } = new List<Paper>();

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
