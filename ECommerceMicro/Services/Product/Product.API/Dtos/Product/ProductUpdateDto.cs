﻿namespace Product.API.Dtos.Product;

public class ProductUpdateDto
{
    public string Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Picture { get; set; }
    public decimal Price { get; set; }
    public string CategoryId { get; set; }
}
