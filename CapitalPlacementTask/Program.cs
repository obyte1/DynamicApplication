using CapitalPlacementTask.Business;
using CapitalPlacementTask.Data.DataAccess;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
//using System.ComponentModel;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

string accountEndpoint = builder.Configuration.GetConnectionString("connection")!;
string accountKey = builder.Configuration.GetConnectionString("PrimaryKey")!;
string databaseName = builder.Configuration.GetConnectionString("ContainerName")!;
string containerName = builder.Configuration.GetConnectionString("ContainerName")!;


using CosmosClient client = new CosmosClient(databaseName);
Database database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
Microsoft.Azure.Cosmos.Container container = await database.CreateContainerIfNotExistsAsync(containerName, "/id");
// Create CosmosClient instance
//CosmosClient cosmosClient = new CosmosClient(accountEndpoint, accountKey);

//// Create or Get Database and Container instances
//Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName);
//Container container = await database.CreateContainerIfNotExistsAsync(containerName, "/id");

builder.Services.AddSingleton(client);
builder.Services.AddSingleton(database);
builder.Services.AddSingleton(container);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseCosmos(accountEndpoint, accountKey, databaseName));

// Other service registrations
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
CoreServiceRegistration.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
