namespace Detego.Api.Endpoints.Hashes.Get;

public sealed record HashDto(int Id, string Code) { }

public sealed record GetHashesResponse(int TotalResults, int TotalPages, HashDto[] Hashes) { }
