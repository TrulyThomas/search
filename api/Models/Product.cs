namespace api.Models;

using NRedisStack.Search;
using Redis.OM.Modeling;

[Document(StorageType = StorageType.Json, Prefixes = new[] { "product" })]
public class Product
{
    [RedisIdField, Indexed] public required string ID { get; set; }
    [Searchable(Weight = 5)] public required string Name { get; set; }
    [Searchable(Weight = 3)] public required string Description { get; set; }
    [Indexed(Sortable = true)] public double Price { get; set; }

    public static string GetIndexName => $"{nameof(Product).ToLower()}-idx";
    public static Query GetBasicQuery(string queryString) => new($"@Name: {queryString}");
}

public record ProductDTO(string ID, string Name, double Price);
