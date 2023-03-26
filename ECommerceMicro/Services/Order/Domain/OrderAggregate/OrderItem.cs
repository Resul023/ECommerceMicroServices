using Domain.Core;

namespace Domain.OrderAggregate;
public class OrderItem:Entity
{
    public string ProductId { get; private set; }
    public string ProductName { get; private set; }
    public string PictureUrl { get; private set; }
    public Decimal Price { get; private set; }
    public OrderItem()
    {
    }
    public OrderItem(string productId, string productName, string pictureUrl, decimal price)
    {
        this.ProductId = productId;
        this.ProductName = productName;
        this.PictureUrl = pictureUrl;
        this.Price = price;
    }

    public void UpdateOrderItem(string productName, string pictureUrl, decimal price)
    {
        this.ProductName = productName;
        this.Price = price;
        this.PictureUrl = pictureUrl;
    }
}
