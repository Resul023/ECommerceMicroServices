using MediatR;
using Infrastructure;
using Application.Dtos;
using Application.Commands;
using Domain.OrderAggregate;

using Shared.Dtos;

namespace Application.Handlers;
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
{
    private readonly OrderDbContext _context;

    public CreateOrderCommandHandler(OrderDbContext context)
    {
        this._context = context;
    }

    public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var newAddress = new Address(
            request.Address.Province,
            request.Address.District,
            request.Address.ZipCode,
            request.Address.Street,
            request.Address.Line);

        Order newOrder = new Order(request.BuyerId, newAddress);

        request.OrderItems.ForEach(x =>
        {
            newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
        });

        await _context.Orders.AddAsync(newOrder);

        await _context.SaveChangesAsync();

        return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id }, 200);
    }
}
