using DuendeIdentityServer.Api.IdentityConfiguration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddInMemoryClients(Configs.clients)
    .AddInMemoryIdentityResources(Configs.identityResources)
    .AddInMemoryApiResources(Configs.apiResources)
    .AddInMemoryApiScopes(Configs.apiScopes)
    .AddTestUsers(Configs.TestUsers)
    .AddDeveloperSigningCredential();

var app = builder.Build();
app.UseIdentityServer();    
app.MapGet("/", () => "Hello World!");

app.Run();
