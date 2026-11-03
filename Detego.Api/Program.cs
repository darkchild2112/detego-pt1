using Detego.Api.Endpoints.Hashes;
using Detego.Api.Endpoints.Hashes.Get;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();

// Handlers
builder.Services.AddScoped<GetHashesHandler>();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapOpenApi();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "Detego Pt.1");
});

app.UseHttpsRedirection();

app.MapHashesEndpoints();

app.Run();
