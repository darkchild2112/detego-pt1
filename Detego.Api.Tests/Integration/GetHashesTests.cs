using Detego.Api.Endpoints.Hashes.Get;
using System.Net;
using System.Text.Json;

namespace Detego.Api.Tests.Integration;

public class GetHashesTests : IntegrationTest
{
    [Fact]
    public async Task GetHashesWithDefaultParams()
    {
        var response = await Client.GetAsync("/api/v1/hashes");

        var content = await response.Content.ReadAsStringAsync();

        var getHashesResponseObject = JsonSerializer.Deserialize<GetHashesResponse>(content, JsonOptions);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(getHashesResponseObject);
        Assert.Equal(100000, getHashesResponseObject.TotalPages);
        Assert.Equal(1000000, getHashesResponseObject.TotalResults);

        foreach (var hash in getHashesResponseObject.Hashes)
        {
            Assert.True(hash.Id > 0);
            Assert.False(string.IsNullOrWhiteSpace(hash.Code));
        }
    }
}
