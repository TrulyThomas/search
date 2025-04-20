using api.Log;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
public class DataIngestionController(
    ILogger<DataIngestionController> logger,
    IIngenstionService<Product> ingenstionService
) : ControllerBase
{
    private readonly ILogger<DataIngestionController> logger = logger;
    private readonly IIngenstionService<Product> ingenstionService = ingenstionService;

    [HttpPost("Product")]
    public ActionResult IngestProduct(Product product)
    {
        logger.LogProductRecieved(product);
        try
        {
            ingenstionService.Ingest(product);
            logger.LogIngestionSuccessful(product.ID);
            return Ok();
        }
        catch (Exception e)
        {
            SentrySdk.CaptureException(e);
            logger.LogIngestionFailed(product.ID);
            return new StatusCodeResult(500);
        }
    }
}
