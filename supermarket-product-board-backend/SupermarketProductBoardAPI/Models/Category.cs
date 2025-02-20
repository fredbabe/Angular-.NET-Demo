namespace SupermarketProductBoardAPI.Models
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        private readonly DateTime _now = DateTime.UtcNow;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public required string Name { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();

        public Category()
        {
            CreatedAt = _now;
            UpdatedAt = _now;
        }
    }
}