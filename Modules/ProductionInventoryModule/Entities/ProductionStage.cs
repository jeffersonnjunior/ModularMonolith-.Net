using Modules.QualityInspectionModule.Entities;

namespace Modules.ProductionInventoryModule.Entities;

public class ProductionStage
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public Guid ProductionOrderId { get; set; }
    
    public ProductionOrder ProductionOrder { get; set; }
    public ICollection<Inspection> Inspections { get; set; }
}