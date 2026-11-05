using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Text.Json;

namespace Detego.Api.Tests.Integration;

public abstract class IntegrationTest : IDisposable
{
    private bool _disposed = false;
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    protected HttpClient Client { get; }

    public IntegrationTest()
    {
        _webApplicationFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.AddInMemoryCollection(GetConfigurationsToOverride());
                });
            });

        Client = _webApplicationFactory.CreateClient();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual Dictionary<string, string?> GetConfigurationsToOverride()
    {
        var databasePath = Path.GetFullPath(
            Path.Combine(
                AppContext.BaseDirectory,
                "..", "..", "..", "..",
                "Detego.Database",
                "hashes.test.sqlite"
            )
        );

        return new()
        {
            ["ConnectionStrings:sqlite"] =
                $"Data Source={databasePath};Cache=Shared;Mode=ReadWrite"
        };
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _webApplicationFactory.Dispose();
            Client.Dispose();
        }

        _disposed = true;
    }
}
