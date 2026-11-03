using Detego.Api.Endpoints.Hashes.Get;

namespace Detego.Api.Endpoints.Hashes;

public static class EndpointMapping
{
    public static IEndpointRouteBuilder MapHashesEndpoints(this IEndpointRouteBuilder app)
    {
        var jobs = app.MapGroup("/api/v1/hashes").WithTags("Hashes");

        jobs.MapGet("/", async (GetHashesHandler handler, CancellationToken cancellationToken, int page = 1, int size = 100) // TODO: move these to the handler
            => await handler.HandleAsync(page, size, cancellationToken))
            .Produces<HashDto[]>()
            .WithName("GetHashes");

        return app;
    }
}
