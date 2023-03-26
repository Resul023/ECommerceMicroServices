using Domain.Core;

namespace Domain.OrderAggregate;
public class Order:Entity, IAggregateRoot
{
    public string BuyerId { get; private set; }
    public Address Address { get; private set; }
    public DateTime CreatedDate { get; private set; }

    private readonly List<OrderItem> _orderItems;
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
    public Order()
    {
    }
    public Order(string buyerId, Address address)
    {
        this._orderItems = new List<OrderItem>();
        this.CreatedDate = DateTime.Now;
        this.BuyerId = buyerId;
        this.Address = address;
    }
    public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
    {
        var existProduct = _orderItems.Any(x => x.ProductId == productId);

        if (!existProduct)
        {
            var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);

            _orderItems.Add(newOrderItem);
        }
    }

    public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
}
