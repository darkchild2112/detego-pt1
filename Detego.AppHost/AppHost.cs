var builder = DistributedApplication.CreateBuilder(args);

var sqllite = builder.AddSqlite("Database", databasePath: "..//Detego.Database", "hashes.sqlite")
    .WithSqliteWeb();

var api = builder.AddProject<Projects.Detego_Api>("API")
    .WithReference(sqllite)
    .WithHttpHealthCheck("/health");

builder.AddJavaScriptApp("Frontend", "..//Detego.Web", runScriptName: "start")
    .WithNpm(installCommand: "install")
    .WithReference(api)
    .WaitFor(api)
    .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();
