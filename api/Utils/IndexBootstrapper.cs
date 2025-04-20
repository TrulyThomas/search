using api.Models;
using Redis.OM;

namespace api.Utils;

public sealed class IndexBootstrapper : IHostedService
{
    private readonly RedisConnectionProvider _provider;
    public IndexBootstrapper(RedisConnectionProvider provider) => _provider = provider;

    public async Task StartAsync(CancellationToken _)
    {
        var conn = _provider.Connection;

        if (!conn.IsIndexCurrent(typeof(Product)))
        {
            conn.DropIndex(typeof(Product));
        }

        await conn.CreateIndexAsync(typeof(Product));
    }

    public Task StopAsync(CancellationToken _) => Task.CompletedTask;
}
