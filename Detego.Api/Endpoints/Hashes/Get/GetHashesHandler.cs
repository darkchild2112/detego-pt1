using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Detego.Api.Endpoints.Hashes.Get;

public sealed class GetHashesHandler(
    HashesDbContext dbContext, 
    IValidator<GetHashesRequest> validator)
{
    public async Task<Results<Ok<GetHashesResponse>, ValidationProblem>> HandleAsync(GetHashesRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var query = dbContext.Hashes.AsNoTracking();

        if(!string.IsNullOrWhiteSpace(request.Search))
            query = query.Where(x => EF.Functions.Like(x.Code, $"%{request.Search}%"));

        var totalCount = await query.CountAsync(cancellationToken);

        var totalPages = totalCount > request.Size ? totalCount / request.Size : 1;

        // Check that the page they've requested isn't out of range
        if (totalCount > request.Size && request.Page > totalPages)
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                ["Error"] = ["Requested page is greater than the total number of pages"]
            });

        var hashes = await query
                .OrderBy(x => x.Id)
                .Skip((request.Page -1) * request.Size)
                .Take(request.Size)
                .ToArrayAsync(cancellationToken);

        var response = new GetHashesResponse(totalCount, totalPages, hashes.Select(e => new HashDto(e.Id, e.Code)).ToArray());

        return TypedResults.Ok(response);
    }
}
