using Detego.Api.Endpoints.Hashes.Get;
using Microsoft.AspNetCore.Mvc;

namespace Detego.Api.Endpoints.Hashes;

public static class MapHashesEndpoint
{
    public static IEndpointRouteBuilder MapHashesEndpoints(this IEndpointRouteBuilder app)
    {
        var hashes = app.MapGroup("/api/v1/hashes").WithTags("Hashes");

        hashes.MapGet("/", async (
            GetHashesHandler handler,
            CancellationToken cancellationToken,
            [FromQuery] string? search,
            [FromQuery]int page = 1, 
            [FromQuery]int size = 10
            )
            => await handler.HandleAsync(new GetHashesRequest(search, page, size), cancellationToken))
            .Produces<GetHashesResponse>()
            .WithName("GetHashes");

        return app;
    }
}
