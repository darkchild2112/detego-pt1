using FluentValidation;

namespace Detego.Api.Endpoints.Hashes.Get;

public class GetHashesValidator : AbstractValidator<GetHashesRequest>
{
    public GetHashesValidator()
    {
        RuleFor(request => request.Page)
            .GreaterThan(0).WithMessage("Page number must be greater than 0");

        RuleFor(request => request.Size)
            .GreaterThan(9).WithMessage("Page size must be greater than or equal to 10")
            .LessThan(1001).WithMessage("Page Size must be less than or equal to 1000");
    }
}
