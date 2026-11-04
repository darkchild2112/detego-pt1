namespace Detego.Api.Endpoints.Hashes.Get;

public record GetHashesRequest(string? Search, int Page, int Size) { }
