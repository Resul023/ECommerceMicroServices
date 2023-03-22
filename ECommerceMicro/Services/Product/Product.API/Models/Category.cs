using System.Text.Json.Serialization;

namespace Product.API.Models;
public class Category
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } 
    public string Name { get; set; }
}
