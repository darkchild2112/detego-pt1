namespace Detego.Api.Endpoints.Hashes.Get;

public record GetHashesResponse(int totalResults, HashDto[] hashes) { }
