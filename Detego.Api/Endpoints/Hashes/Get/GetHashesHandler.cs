using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Detego.Api.Endpoints.Hashes.Get;

public sealed class GetHashesHandler(HashesDbContext dbContext)
{
    public async Task<Ok<GetHashesResponse>> HandleAsync(int page, int size, CancellationToken cancellationToken)
    {
        // TODO: Validate page and size parameters

        var hashes = await dbContext.Hashes
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Skip((page -1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);

        var totalCount = await dbContext.Hashes.CountAsync(cancellationToken);

        var response = new GetHashesResponse(totalCount, hashes.Select(e => new HashDto(e.Id, e.Code)).ToArray());

        return TypedResults.Ok(response);
    }
}
