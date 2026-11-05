using Detego.Api.Data;
using Detego.Api.Endpoints.Hashes;
using Detego.Api.Endpoints.Hashes.Get;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
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
    builder.WithOrigins("http://localhost:3000")
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

builder.Services.AddDbContext<HashesDbContext>(options =>
{
    var connectionString = builder.Configuration
        .GetConnectionString("sqlite");

    options.UseSqlite(connectionString);
});

var connectionString = builder.Configuration
    .GetConnectionString("sqlite");

var app = builder.Build();

//app.MapHealthChecks("/health", new HealthCheckOptions
//{
//    ResultStatusCodes =
//    {
//        [HealthStatus.Healthy] = StatusCodes.Status200OK,
//        [HealthStatus.Degraded] = StatusCodes.Status200OK,
//        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
//    },
//    AllowCachingResponses = false
//});

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
