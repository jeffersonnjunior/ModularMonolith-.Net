using Modules.Production.Entities;

namespace Modules.Production.Dtos.ProductionPartDtos;

public class ProductionPartGetFilterDto
{
    public Guid ProductionOrderIdEqual { get; set; }
    public string PartCodeContains { get; set; }
    public int QuantityUsedEqual { get; set; }
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}
