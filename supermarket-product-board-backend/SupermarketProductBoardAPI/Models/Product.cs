namespace SupermarketProductBoardAPI.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required string Name { get; set; }

        public required decimal Price { get; set; }

        public string? Amount { get; set; }

        public string? Description { get; set; }

        public Guid? CategoryId { get; set; }

        public Category? Category { get; set; }

        public bool? HasNewCategory { get; set; }

        public Guid? CompanyId { get; set; }

        public Company? Company { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; }

        public bool ShowOnline { get; set; } = true;

        public Guid? PaperId { get; set; }

        public Paper? Paper { get; set; }
    }
}
