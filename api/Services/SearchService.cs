using NRedisStack;
using NRedisStack.RedisStackCommands;
using NRedisStack.Search;
using StackExchange.Redis;
using api.Log;
using api.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace api.Services;

public class SearchService(IDatabase database, ILogger<SearchService> logger) : ISearchService<ProductDTO>
{
    private readonly SearchCommands searchCommandsAsync = database.FT();
    private readonly ILogger<SearchService> logger = logger;
    private static readonly JsonSerializerOptions _jsonOpts = new()
    {
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    public async Task<List<ProductDTO>> Search(string searchString)
    {
        var results = await searchCommandsAsync.SearchAsync(
            Product.GetIndexName,
            Product.GetBasicQuery(searchString)
        );
        logger.LogResultCount(searchString, (int)results.TotalResults);
        return TransformProducts(results.Documents);
    }

    private static List<ProductDTO> TransformProducts(IReadOnlyCollection<Document> docs)
    {
        return [.. docs.Select(static d =>
        {
            var json = (string)d["json"]!;
            var dto = JsonSerializer.Deserialize<ProductDTO>(json, _jsonOpts)
                      ?? throw new JsonException("Could not deserialize product JSON");
            return string.IsNullOrEmpty(dto.ID) ? dto with { ID = d.Id } : dto;
        })];
    }
}
