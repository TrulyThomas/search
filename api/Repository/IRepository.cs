using System.Linq.Expressions;

namespace api.Repository;

public interface IRepository<T>
{
    public List<T> Get(Expression<Func<T, bool>> predicate);
    public Task InsertAsync(IEnumerable<T> values);
    public Task InsertAsync(T value);
    public Task UpdateAsync(Expression<Func<T, bool>> predicate, Action<T> updateAction);
    public Task DeleteAsync(Expression<Func<T, bool>> predicate);
}
