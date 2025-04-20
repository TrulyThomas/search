namespace api.Log;

using api.Controllers;

public static partial class SearchControllerLoggerExtensions
{
    [LoggerMessage(Level = LogLevel.Debug, Message = "Recieved search request {searchQuery}")]
    public static partial void LogSearch(this ILogger<SearchController> logger, string searchQuery);
}
