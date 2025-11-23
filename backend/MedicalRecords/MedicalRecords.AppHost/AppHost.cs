var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithImage("postgres:15")
    .WithEnvironment("POSTGRES_USER", "postgres")
    .WithEnvironment("POSTGRES_PASSWORD", "Artur-123")
    .WithEnvironment("POSTGRES_DB", "MedicalRecords")
    .WithDataVolume();

var database = postgres.AddDatabase("MedicalRecordsDB");

var api = builder.AddProject("api", "../MedicalRecords.API/MedicalRecords.API.csproj")
    .WithReference(database);

var frontend = builder.AddExecutable("frontend", "cmd", "../../../fronetnd", "/c", "npm", "run", "dev")
    .WithEnvironment("NODE_ENV", "development")
    .WithReference(api)
    .WithHttpEndpoint(port: 3000, env: "PORT")
    .WithExternalHttpEndpoints();


builder.Build().Run();