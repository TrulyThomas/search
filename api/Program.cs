using api.Models;
using api.Repository;
using api.Services;
using api.Settings;
using api.Utils;
using Redis.OM;
using Scalar.AspNetCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

RedisConnectionProvider provider = new("redis://localhost:6379");
var connection = ConnectionMultiplexer.Connect("localhost:6379");
IDatabase database = connection.GetDatabase();

if (builder.Environment.IsDevelopment())
{
    builder.Logging.SetMinimumLevel(LogLevel.Debug);
}

builder.Services.AddOpenApi();
builder.Services.AddTransient<INormalizationService, NormalizationService>();
builder.Services.AddTransient<IRepository<Product>, RedisRepository<Product>>();
builder.Services.AddTransient<IIngenstionService<Product>, IngestionService<Product>>();
builder.Services.AddTransient<ISearchService<ProductDTO>, SearchService>();
builder.Services.AddSingleton(provider);
builder.Services.AddSingleton(database);

builder.Services.AddHostedService<IndexBootstrapper>();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

if (app.Environment.IsProduction())
{
    var sentryConfig = builder.Configuration.GetSection("Sentry").Get<SentrySettings>();
    SentrySdk.Init(options =>
    {
        options.Dsn = sentryConfig?.DSN ?? "";
        options.Debug = false;
        options.AutoSessionTracking = true;
        options.IsGlobalModeEnabled = false;
        options.TracesSampleRate = 1;
    });
    SentrySdk.ConfigureScope(scope =>
    {
        scope.Level = SentryLevel.Warning;
    });
}

app.UseHttpsRedirection();

app.Run();
