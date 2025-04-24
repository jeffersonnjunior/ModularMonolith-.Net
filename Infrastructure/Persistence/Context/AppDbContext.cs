using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence.Context;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        var moduleAssemblies = new[]
        {
            typeof(Modules.Inventory.Mappings.PartMapping).Assembly,
            typeof(Modules.Production.Mappings.ProductionOrderMapping).Assembly,
            typeof(Modules.Sales.Mappings.SaleDetailMapping).Assembly
        };

        foreach (var assembly in moduleAssemblies)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}