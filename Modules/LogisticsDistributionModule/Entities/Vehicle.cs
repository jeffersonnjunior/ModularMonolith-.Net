using Modules.LogisticsDistributionModule.Enums;
using Modules.ProductionInventoryModule.Entities;

namespace Modules.LogisticsDistributionModule.Entities;

public class Vehicle
{
    public Guid Id { get; set; }
    public string VIN { get; set; }
    public string Model { get; set; }
    public VehicleStatus VehicleStatus { get; set; }
    public Guid ProductionOrderId { get; set; }
    
    public ProductionOrder ProductionOrder { get; set; }
    public ICollection<Delivery> Deliveries { get; set; }
}