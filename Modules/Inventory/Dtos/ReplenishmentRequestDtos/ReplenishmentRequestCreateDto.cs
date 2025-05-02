using Modules.Inventory.Enums;

namespace Modules.Inventory.Dtos.ReplenishmentRequestDtos;

public class ReplenishmentRequestCreateDto
{
    public string PartCode { get; set; }
    public int RequestedQuantity { get; set; }
    public ReplenishmentStatus ReplenishmentStatus { get; set; }
    public DateTime RequestedAt { get; set; }
}
