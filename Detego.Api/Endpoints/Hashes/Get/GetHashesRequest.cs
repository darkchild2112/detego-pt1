namespace Detego.Api.Endpoints.Hashes.Get;

public sealed record GetHashesRequest(string? Search, int Page, int Size) { }
