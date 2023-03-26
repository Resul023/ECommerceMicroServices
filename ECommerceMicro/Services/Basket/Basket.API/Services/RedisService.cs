using StackExchange.Redis;
namespace Basket.API.Services;
public class RedisService
{
    private readonly string _host;

    private readonly int _port;

    private ConnectionMultiplexer _ConnectionMultiplexer;

    public RedisService(string host, int port)
    {
        this._host = host;
        this._port = port;
    }

    public void Connect() => _ConnectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");

    public IDatabase GetDb(int db = 1) => _ConnectionMultiplexer.GetDatabase(db);
}
