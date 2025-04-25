using Api.Middlewares;
using Common.DependencyInjection;
using Infrastructure.DependencyInjection;
using Modules.Inventory.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomCors();
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
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
