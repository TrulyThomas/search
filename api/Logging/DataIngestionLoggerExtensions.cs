namespace api.Log;

using api.Controllers;
using api.Models;

public static partial class DataIngestionLoggerExtensions
{
    [LoggerMessage(Level = LogLevel.Information, Message = "Recieved request for data ingestion for {product}")]
    public static partial void LogProductRecieved(this ILogger<DataIngestionController> logger, Product product);

    [LoggerMessage(Level = LogLevel.Information, Message = "Data ingested successfully for {id}")]
    public static partial void LogIngestionSuccessful(this ILogger<DataIngestionController> logger, string id);

    [LoggerMessage(Level = LogLevel.Error, Message = "Data ingestion failed for {id}")]
    public static partial void LogIngestionFailed(this ILogger<DataIngestionController> logger, string id);
}
