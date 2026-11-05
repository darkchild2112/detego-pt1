using Detego.Api.Endpoints.Hashes.Get;
using System.Net;
using System.Net.Http.Json;

namespace Detego.Api.Tests.Integration;

public sealed class GetHashesTests : IntegrationTest
{
    [Fact]
    public async Task GetHashes_WithDefaultParams_ReturnsResult()
    {
        // Arrange

        // Act
        var response = await Client.GetAsync("/api/v1/hashes");

        var result = await response.Content.ReadFromJsonAsync<GetHashesResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.Equal(100000, result.TotalPages);
        Assert.Equal(1000000, result.TotalResults);

        Assert.All(result.Hashes, hash => {
            Assert.True(hash.Id > 0);
            Assert.False(string.IsNullOrWhiteSpace(hash.Code));
        });
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    public async Task GetHashes_WithPageSize_ReturnsTheCorrectNumberOfResults(int hashesPerPage)
    {
        // Arrange

        // Act
        var response = await Client.GetAsync($"/api/v1/hashes?size={hashesPerPage}");

        var result = await response.Content.ReadFromJsonAsync<GetHashesResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.Equal(hashesPerPage, result.Hashes.Length);
    }

    [Fact]
    public async Task GetHashes_NextPage_ReturnsDifferentResults()
    {
        // Arrange
        const int pageSize = 10;

        // Act
        var pageOneResponse = await Client.GetAsync("/api/v1/hashes?page=1");

        pageOneResponse.EnsureSuccessStatusCode();

        var pageOneResult = await pageOneResponse.Content.ReadFromJsonAsync<GetHashesResponse>();

        var pageTwoResponse = await Client.GetAsync("/api/v1/hashes?page=2");

        pageTwoResponse.EnsureSuccessStatusCode();

        var pageTwoResult = await pageTwoResponse.Content.ReadFromJsonAsync<GetHashesResponse>();

        // Assert
        Assert.NotNull(pageOneResult);
        Assert.NotNull(pageTwoResult);

        Assert.NotEmpty(pageOneResult.Hashes);
        Assert.NotEmpty(pageTwoResult.Hashes);

        Assert.Equal(pageSize, pageOneResult.Hashes.Length);
        Assert.Equal(pageSize, pageTwoResult.Hashes.Length);

        Assert.Empty(
            pageOneResult.Hashes
                .Select(e => e.Id)
                .Intersect(pageTwoResult.Hashes.Select(e => e.Id))
        );
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    [InlineData(-10000)]
    public async Task GetHashes_WithInvalidPageNumber_ReturnsBadRequest(int invalidPageNumber)
    {
        // Arrange

        // Act
        var response = await Client.GetAsync($"/api/v1/hashes?page={invalidPageNumber}");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData("test")]
    [InlineData("Ten")]
    [InlineData("Twenty")]
    public async Task GetHashes_WithStringPageNumber_ReturnsBadRequest(string stringPageNumber)
    {
        // Arrange

        // Act
        var response = await Client.GetAsync($"/api/v1/hashes?page={stringPageNumber}");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData("test")]
    [InlineData("Ten")]
    [InlineData("Twenty")]
    public async Task GetHashes_WithStringPageSize_ReturnsBadRequest(string stringPageSize)
    {
        // Arrange

        // Act
        var response = await Client.GetAsync($"/api/v1/hashes?size={stringPageSize}");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(3)]
    [InlineData(7)]
    [InlineData(9)]
    [InlineData(10001)]
    [InlineData(10500)]
    [InlineData(50000)]
    public async Task GetHashes_WithInvalidPageSize_ReturnsBadRequest(int invalidPageSize)
    {
        // Arrange

        // Act
        var response = await Client.GetAsync($"/api/v1/hashes?size={invalidPageSize}");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetHashes_WithPageOutOfRange_ReturnsBadRequest()
    {
        // Arrange

        // Act
        var pageOneResponse = await Client.GetAsync("/api/v1/hashes?page=1");

        pageOneResponse.EnsureSuccessStatusCode();

        var pageOneResult = await pageOneResponse.Content.ReadFromJsonAsync<GetHashesResponse>();

        var pageOutOfRangeResponse = await Client.GetAsync($"/api/v1/hashes?page={pageOneResult!.TotalPages + 1}");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, pageOutOfRangeResponse.StatusCode);
    }

    [Fact]
    public async Task GetHashes_WithSearchParam_ReturnsResults()
    {
        // Arrange
        var searchKeyword = "123";

        // Act
        var response = await Client.GetAsync($"/api/v1/hashes?search={searchKeyword}");

        var result = await response.Content.ReadFromJsonAsync<GetHashesResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.NotEmpty(result.Hashes);

        foreach (var hash in result.Hashes)
        {
            Assert.True(hash.Id > 0);
            Assert.False(string.IsNullOrWhiteSpace(hash.Code));
        }

        Assert.All(result.Hashes, hash =>
        {
            Assert.Contains(searchKeyword, hash.Code, StringComparison.OrdinalIgnoreCase);
        });
    }

    [Fact]
    public async Task GetHashes_WithSearchParamThatDoesntMatch_ReturnsNoResults()
    {
        // Arrange
        var searchKeyword = "Smith45";

        // Act
        var response = await Client.GetAsync($"/api/v1/hashes?search={searchKeyword}");

        var result = await response.Content.ReadFromJsonAsync<GetHashesResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.Empty(result.Hashes);
        Assert.Equal(0, result.TotalResults);
    }

    [Theory]
    [InlineData("' OR 1=1 --")]
    [InlineData("'; DROP TABLE safe_hashes; --")]
    [InlineData("' UNION SELECT 1, 'injected' --")]
    public async Task GetHashes_WithSqlInjection_ReturnsNoResults(string search)
    {
        // Arrange
        var encodedSearch = Uri.EscapeDataString(search);

        // Act
        var response = await Client.GetAsync($"/api/v1/hashes?page=1&size=10&search={encodedSearch}");

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<GetHashesResponse>();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Hashes);
    }
}
