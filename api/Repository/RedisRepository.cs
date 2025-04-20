using Redis.OM;
using StackExchange.Redis;
using System.Linq.Expressions;

namespace api.Repository;

public class RedisRepository<T>(RedisConnectionProvider client) : IRepository<T> where T : notnull
{
    private readonly Redis.OM.Searching.IRedisCollection<T> values = client.RedisCollection<T>();
    public List<T> Get(Expression<Func<T, bool>> predicate) => [.. values.Where(predicate)];

    public async Task UpdateAsync(Expression<Func<T, bool>> predicate, Action<T> updateAction)
    {
        foreach (var item in values.Where(predicate))
        {
            updateAction(item);
        }
        await values.SaveAsync();
    }

    public Task DeleteAsync(Expression<Func<T, bool>> predicate) => values.DeleteAsync(values.Where(predicate));

    public Task InsertAsync(IEnumerable<T> values) => this.values.InsertAsync(values);

    public Task InsertAsync(T value) => this.values.InsertAsync(value);
}
