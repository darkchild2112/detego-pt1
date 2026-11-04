using Detego.Api;
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
    options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
});

// Handlers
builder.Services.AddScoped<GetHashesHandler>();
builder.Services.AddScoped<IValidator<GetHashesRequest>, GetHashesValidator>();

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.WithOrigins("http://localhost:3000")
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

// Register EF Core with SQLite
builder.Services.AddDbContext<HashesDbContext>(options =>
{
    var connectionString = builder.Configuration
        .GetConnectionString("sqlite");

    options.UseSqlite(connectionString);
});

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
