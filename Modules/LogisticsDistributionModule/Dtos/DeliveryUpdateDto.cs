using Modules.LogisticsDistributionModule.Enums;

namespace Modules.LogisticsDistributionModule.Dtos;

public class DeliveryUpdateDto
{
    public Guid Id { get; set; }
    public Guid VehicleId { get; set; }
    public Guid DistributorId { get; set; }
    public DateTime DeliveryDate { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
}