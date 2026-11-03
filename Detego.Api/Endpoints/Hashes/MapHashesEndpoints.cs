using Detego.Api.Endpoints.Hashes.Get;

namespace Detego.Api.Endpoints.Hashes;

public static class EndpointMapping
{
    public static IEndpointRouteBuilder MapHashesEndpoints(this IEndpointRouteBuilder app)
    {
        var jobs = app.MapGroup("/hashes").WithTags("Hashes");

        jobs.MapGet("/", async (GetHashesHandler handler, CancellationToken cancellationToken) 
            => await handler.HandleAsync(cancellationToken))
            .Produces<HashDto[]>()
            .WithName("GetHashes");

        return app;
    }
}
