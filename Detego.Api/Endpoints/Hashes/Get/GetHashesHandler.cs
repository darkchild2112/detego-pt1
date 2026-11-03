using Microsoft.AspNetCore.Http.HttpResults;

namespace Detego.Api.Endpoints.Hashes.Get;

public sealed class GetHashesHandler
{
    public async Task<Ok<HashDto[]>> HandleAsync(CancellationToken cancellationToken)
    {
        HashDto[] results = [new HashDto(1, "iuhsdiuhdiuhd"), new HashDto(2, "ouhsdohusduohsdiuh")];

        return TypedResults.Ok(results);
    }
}
