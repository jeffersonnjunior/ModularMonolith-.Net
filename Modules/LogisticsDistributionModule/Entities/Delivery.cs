using Modules.LogisticsDistributionModule.Enums;

namespace Modules.LogisticsDistributionModule.Entities;

public class Delivery
{
    public Guid Id { get; set; }
    public Guid VehicleId { get; set; }
    public Guid DistributorId { get; set; }
    public DateTime DeliveryDate { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
    
    public Vehicle Vehicle { get; set; }
    public Distributor Distributor { get; set; }
}