using Modules.Inventory.Enums;

namespace Modules.Inventory.Entities;

public class ReplenishmentRequest
{
    public Guid Id { get; set; }
    public string PartCode { get; set; }
    public int RequestedQuantity { get; set; }
    public ReplenishmentStatus ReplenishmentStatus { get; set; }
    public DateTime RequestedAt { get; set; }
}
