using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Text.Json;

namespace Detego.Api.Tests.Integration;

public abstract class IntegrationTest
{
    private bool _disposed = false;
    protected static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    protected HttpClient Client { get; }

    public IntegrationTest()
    {
        Client = CreateClient();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected HttpClient CreateClient() =>
        new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.AddInMemoryCollection(GetConfigurationsToOverride());
                });
            })
            .CreateClient();

    protected void ConfigureAppConfig(IConfigurationBuilder builder)
    {
        builder.AddJsonFile(AppConfigPath, optional: false);
        builder.AddInMemoryCollection(GetConfigurationsToOverride());
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

    private static string AppConfigPath => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "appsettings.json");

    private void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            Client.Dispose();
        }

        _disposed = true;
    }
}
