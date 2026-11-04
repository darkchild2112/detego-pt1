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
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var totalCount = await dbContext.Hashes.CountAsync(cancellationToken);

        // Check that the page they've requested isn't out of range
        if(request.Page > totalCount / request.Size)
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
                    {
                        ["Error"] = ["Requested page is greater than the total number of pages"]
                    });

        var hashes = await dbContext.Hashes
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Skip((request.Page -1) * request.Size)
                .Take(request.Size)
                .ToListAsync(cancellationToken);

        // TODO: send the total pages back to the front end for validation
        var response = new GetHashesResponse(totalCount, hashes.Select(e => new HashDto(e.Id, e.Code)).ToArray());

        return TypedResults.Ok(response);
    }
}
