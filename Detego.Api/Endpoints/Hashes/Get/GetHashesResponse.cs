namespace Detego.Api.Endpoints.Hashes.Get;

public record GetHashesResponse(int TotalResults, int TotalPages, HashDto[] Hashes) { }
