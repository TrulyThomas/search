using api.Repository;

namespace api.Services;

public class IngestionService<T>(IRepository<T> repository) : IIngenstionService<T>
{
    private readonly IRepository<T> repository = repository;

    public Task Ingest(T data) => repository.InsertAsync(data);
}
