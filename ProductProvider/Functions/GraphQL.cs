using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ProductProvider.Functions;

public class GraphQL(ILogger<GraphQL> logger, IGraphQLRequestExecutor executor)
{
    private readonly ILogger<GraphQL> _logger = logger;
    private readonly IGraphQLRequestExecutor _executor = executor;

    [Function("GraphQL")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "products")] HttpRequest req)
    {
        return await _executor.ExecuteAsync(req);
    }
}
