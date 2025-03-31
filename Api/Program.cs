using Api.Filters;
using Infrastructure.DependencyInjection;
using Modules.LogisticsDistributionModule.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddNotificationActionFilter();
builder.Services.DependencyInjectionInfrastructure(configuration);
builder.Services.DependencyInjectionLogisticsDistribution();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();