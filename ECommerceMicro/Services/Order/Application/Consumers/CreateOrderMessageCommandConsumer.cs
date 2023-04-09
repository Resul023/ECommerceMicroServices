using Domain.OrderAggregate;
using Infrastructure;
using MassTransit;
using Shared.Messages;

namespace Application.Consumers;
public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
{
    private readonly OrderDbContext _orderDbContext;
    public CreateOrderMessageCommandConsumer(OrderDbContext orderDbContext)
    {
        this._orderDbContext = orderDbContext;
    }
    public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
    {
        var newAddress = new Address(
            context.Message.Province,
            context.Message.District,
            context.Message.ZipCode, 
            context.Message.Street,
            context.Message.Line);

        Order order = new Order(context.Message.BuyerId, newAddress);

        context.Message.OrderItems.ForEach(x =>
        {
            order.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
        });

        await _orderDbContext.Orders.AddAsync(order);

        await _orderDbContext.SaveChangesAsync();
    }
}
