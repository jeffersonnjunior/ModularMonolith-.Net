using Modules.Inventory.Enums;

namespace Modules.Inventory.Dtos.ReplenishmentRequestDtos;

public class ReplenishmentRequestGetFilterDto
{
    public string? PartCodeContains { get; set; }
    public int? RequestedQuantityEqual { get; set; }
    public ReplenishmentStatus? ReplenishmentStatusEqual { get; set; }
    public DateTime? RequestedAtEqual { get; set; }
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}
