using System.Net;

namespace Detego.Api.Tests.Integration;

public sealed class GetHealthTests : IntegrationTest
{
    [Fact]
    public async Task GetHealth_ReturnsSuccessResult()
    {
        // Arrange

        // Act
        var response = await Client.GetAsync("/health");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
