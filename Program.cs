using Microsoft.Azure.Cosmos;
using SupportWebApp.Components;
using SupportWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();


// ---------------------------
// Cosmos DB konfiguration
// ---------------------------

var connectionString =
    builder.Configuration.GetConnectionString("CosmosDb")
    ?? throw new InvalidOperationException(
        "CosmosDb connection string mangler i appsettings.json."
    );

var databaseName =
    builder.Configuration["CosmosDbSettings:DatabaseName"]
    ?? throw new InvalidOperationException(
        "CosmosDb DatabaseName mangler i appsettings.json."
    );

var containerName =
    builder.Configuration["CosmosDbSettings:ContainerName"]
    ?? throw new InvalidOperationException(
        "CosmosDb ContainerName mangler i appsettings.json."
    );


// CosmosClient skal genbruges
builder.Services.AddSingleton(
    new CosmosClient(connectionString)
);


// Registrer vores egen Cosmos DB service
builder.Services.AddScoped<ICosmosDbService>(serviceProvider =>
{
    var cosmosClient =
        serviceProvider.GetRequiredService<CosmosClient>();

    return new CosmosDbService(
        cosmosClient,
        databaseName,
        containerName
    );
});


var app = builder.Build();


// ---------------------------
// HTTP pipeline
// ---------------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(
        "/Error",
        createScopeForErrors: true
    );

    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute(
    "/not-found",
    createScopeForStatusCodePages: true
);

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
