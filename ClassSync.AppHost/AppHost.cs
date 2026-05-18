var builder = DistributedApplication.CreateBuilder(args);

// 1. Provision a PostgreSQL server container and database
var postgres = builder.AddPostgres("postgres")
                      .WithImage("ankane/pgvector")
                      .WithImageTag("latest");

var database = postgres.AddDatabase("SyllabusDb");

// 2. Pass the database reference to the API Backend
var apiService = builder.AddProject<Projects.ClassSync_ApiService>("apiservice")
    .WithHttpHealthCheck("/health")
    .WithReference(database);

// 3. Keep the Blazor Web Frontend wired to the API
builder.AddProject<Projects.ClassSync_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
