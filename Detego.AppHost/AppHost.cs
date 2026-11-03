var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Detego_Api>("detego-api");

builder.Build().Run();
