using api.Utils.ExtensionMethods;

namespace api.Services;

public partial class NormalizationService(ILogger<NormalizationService> logger) : INormalizationService
{
    private readonly ILogger<NormalizationService> logger = logger;

    public string[] Normalize(string queryString)
    {
        LogInput(logger, queryString);
        var result = queryString
            .Trim()
            .ToLower()
            .RemoveAcentuation()
            .Split(" ");
        LogOutput(logger, result);
        return result;
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Normalizing input query string: {QueryString}")]
    static partial void LogInput(ILogger logger, string queryString);
    [LoggerMessage(Level = LogLevel.Information, Message = "Normalized output query string: {NormalizedQueryString}")]
    static partial void LogOutput(ILogger logger, string[] normalizedQueryString);
}
