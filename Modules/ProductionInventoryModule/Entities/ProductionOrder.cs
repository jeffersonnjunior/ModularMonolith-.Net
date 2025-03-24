using Modules.ProductionInventoryModule.Enums;
using Modules.LogisticsDistributionModule.Entities;

namespace Modules.ProductionInventoryModule.Entities;

public class ProductionOrder
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ProductionOrderStatus ProductionOrderStatus { get; set; }
    public decimal? TotalCost { get; set; }
    public Guid OrderMaterialId { get; set; }
    
    public OrderMaterial OrderMaterial { get; set; }
    public ICollection<ProductionStage> ProductionStages { get; set; }
    public ICollection<Vehicle> Vehicles { get; set; } 
}