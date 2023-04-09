using Basket.API.Dtos;
using Basket.API.Services;
using MassTransit;
using Shared.Messages;
using Shared.Services;
using StackExchange.Redis;
using System.Text.Json;

namespace Basket.API.Consumers;
public class ProductNameChangedEventConsumer : IConsumer<ProductNameChangedEvent>
{
    private readonly RedisService _redisService;


    public ProductNameChangedEventConsumer(RedisService redisService)
    {
        this._redisService = redisService;
    }
    public async Task Consume(ConsumeContext<ProductNameChangedEvent> context)
    {
        var orderItems =  await _redisService.GetDb().StringGetAsync("f81bd336-3247-46d5-9fee-81eb546923e5");
        var basketItems = JsonSerializer.Deserialize<BasketDto>(orderItems);
        var updatedItem = basketItems.basketItems.Where(x => x.ProductId == context.Message.ProductId).ToList();
        updatedItem.ForEach(x =>
        {
            x.ProductName = context.Message.UpdatedName;
        });

        await _redisService.GetDb().StringSetAsync("f81bd336-3247-46d5-9fee-81eb546923e5", JsonSerializer.Serialize(basketItems));
    }
}
