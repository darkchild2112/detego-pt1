using Detego.Api.Endpoints.Hashes.Get;
using Microsoft.AspNetCore.Mvc;

namespace Detego.Api.Endpoints.Hashes;

public static class EndpointMapping
{
    public static IEndpointRouteBuilder MapHashesEndpoints(this IEndpointRouteBuilder app)
    {
        var jobs = app.MapGroup("/api/v1/hashes").WithTags("Hashes");

        jobs.MapGet("/", async (
            GetHashesHandler handler,
            CancellationToken cancellationToken,
            [FromQuery] string? search,
            [FromQuery]int page = 1, 
            [FromQuery]int size = 10
            )
            => await handler.HandleAsync(new GetHashesRequest(search, page, size), cancellationToken))
            .Produces<HashDto[]>()
            .WithName("GetHashes");

        return app;
    }
}
