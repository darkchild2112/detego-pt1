namespace Detego.Api.Endpoints.Hashes.Get;

public record GetHashesRequest(string? Search, int Page = 1, int Size = 10) { }
