using Modules.Production.Enums;

namespace Modules.Production.Dtos.ProductionOrderDtos;

public class ProductionOrderGetFilterDto
{
    public string ModelContains { get; set; }
    public ProductionStatus ProductionStatusEqual { get; set; }
    public DateTime CreatedAtEqual { get; set; }
    public DateTime? CompletedAtEqual { get; set; }
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}
