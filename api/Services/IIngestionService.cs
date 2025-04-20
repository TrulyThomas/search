namespace api.Services;

public interface IIngenstionService<T>
{
    public Task Ingest(T data);
}
