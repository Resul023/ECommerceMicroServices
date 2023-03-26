using MediatR;
using Shared.Dtos;
using Infrastructure;
using Application.Dtos;
using Application.Mapping;
using Application.Queries;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers;
public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
{
    private readonly OrderDbContext _context;

    public GetOrdersByUserIdQueryHandler(OrderDbContext context)
    {
        this._context = context;
    }

    public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();

        if (!orders.Any())
        {
            return Response<List<OrderDto>>.Success(new List<OrderDto>(), 200);
        }

        var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

        return Response<List<OrderDto>>.Success(ordersDto, 200);
    }
}
