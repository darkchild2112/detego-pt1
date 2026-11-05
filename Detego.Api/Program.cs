using Detego.Api.Data;
using Detego.Api.Endpoints.Hashes;
using Detego.Api.Endpoints.Hashes.Get;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi(options =>
{
    // This fixes a problem with the parameters not working in the UI
    // https://github.com/swagger-api/swagger-ui/issues/10646?utm_source=chatgpt.com
    // ChatGPT helped me find this
    options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
});

builder.Services.AddScoped<GetHashesHandler>();
builder.Services.AddScoped<IValidator<GetHashesRequest>, GetHashesValidator>();

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.SetIsOriginAllowed(origin =>
    {
        var uri = new Uri(origin);
        return uri.Host == "localhost"
            && (uri.Scheme == "http" || uri.Scheme == "https");
    })
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

builder.Services.AddDbContext<HashesDbContext>(options =>
{
    var connectionString = builder.Configuration
        .GetConnectionString("sqlite");

    options.UseSqlite(connectionString);
});

var app = builder.Build();

app.UseCors("MyPolicy");

app.MapDefaultEndpoints();

app.MapOpenApi();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "Detego Pt.1");
});

app.UseHttpsRedirection();

app.MapHashesEndpoints();

app.Run();

// Required to expose for testing
public partial class Program { }
