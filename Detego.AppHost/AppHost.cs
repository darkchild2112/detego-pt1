var builder = DistributedApplication.CreateBuilder(args);

var sqllite = builder.AddSqlite("sqlite", databasePath: "..\\Database", "hashes.sqlite")
    .WithSqliteWeb();

builder.AddProject<Projects.Detego_Api>("detego-api")
    .WithReference(sqllite);

builder.Build().Run();
