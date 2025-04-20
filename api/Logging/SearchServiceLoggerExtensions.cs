namespace api.Log;
using api.Services;

public static partial class SearchServiceLoggerExtensions
{
    [LoggerMessage(Level = LogLevel.Debug, Message = "Search with query {query} returned {count} result(s)")]
    public static partial void LogResultCount(this ILogger<SearchService> logger, string query, int count);
}
