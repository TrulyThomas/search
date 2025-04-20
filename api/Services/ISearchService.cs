using api.Models;

namespace api.Services;

public interface ISearchService<T>
{
    public Task<List<T>> Search(string searchString);
}
