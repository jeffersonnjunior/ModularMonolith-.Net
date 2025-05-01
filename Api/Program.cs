using Api.Filters;
using Api.Middlewares;
using Infrastructure.DependencyInjection;
using Modules.Inventory.DependencyInjection;
using Common.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

bool isRunningInContainer = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

builder.WebHost.ConfigureKestrel(options =>
{
    if (isRunningInContainer)
    {
        
        options.ListenAnyIP(5000); 
    }
    else
    {

        options.ListenAnyIP(5001, listenOptions =>
        {
            listenOptions.UseHttps(); 
        });
    }
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomCors();
builder.Services.AddNotificationActionFilter();
builder.Services.CommonDependencyInjection(configuration);
builder.Services.DependencyInjectionInfrastructure(configuration);
builder.Services.InventoryDependencyInjection(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomCors();

if (!isRunningInContainer)
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();
app.Run();