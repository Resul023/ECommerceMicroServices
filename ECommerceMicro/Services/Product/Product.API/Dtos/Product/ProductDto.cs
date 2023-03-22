namespace Product.API.Dtos.Product;
public class ProductDto
{
    public string Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Picture { get; set; }
    public decimal Price { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string CategoryId { get; set; }
    public CategoryDto Category { get; set; }
}
