namespace Product.API.Models;
public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } 
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Picture { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }

    [BsonRepresentation(BsonType.DateTime)] 
    public DateTime? CreatedAt { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string CategoryId { get; set; }
    [BsonIgnore]

    public Category Category { get; set; }
}
