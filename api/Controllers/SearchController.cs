using api.Log;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
public class SearchController(ISearchService<ProductDTO> productSearchService, ILogger<SearchController> logger) : ControllerBase
{
    private readonly ISearchService<ProductDTO> productSearchService = productSearchService;
    private readonly ILogger<SearchController> logger = logger;

    [HttpPost("Search")]
    public async Task<ActionResult<List<ProductDTO>>> Search(string queryString)
    {
        logger.LogSearch(queryString);
        var results = await productSearchService.Search(queryString);
        return new OkObjectResult(results);
    }
}
