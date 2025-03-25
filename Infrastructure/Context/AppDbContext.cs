using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        var moduleAssemblies = new[]
        {
            typeof(Modules.LogisticsDistributionModule.Mappings.DeliveryMap).Assembly,
            typeof(Modules.ProductionInventoryModule.Mappings.OrderMaterialMap).Assembly,
            typeof(Modules.QualityInspectionModule.Mappings.InspectionFailureMap).Assembly
        };

        foreach (var assembly in moduleAssemblies)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}