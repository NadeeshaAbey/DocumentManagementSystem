using DocumentManagementSystem.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Database connection string 'DefaultConnection' was not found.");
}

var storageRoot = builder.Configuration["Storage:RootPath"];

if (string.IsNullOrWhiteSpace(storageRoot))
{
    throw new InvalidOperationException(
        "Storage root path 'Storage:RootPath' was not found.");
}

var repositoryRoot = Path.GetFullPath(
    Path.Combine(
        builder.Environment.ContentRootPath,
        "..",
        ".."));

storageRoot = Path.GetFullPath(
    Path.Combine(repositoryRoot, storageRoot));

builder.Services.AddInfrastructure(
    connectionString,
    storageRoot);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
