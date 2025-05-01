namespace Modules.Inventory.Dtos.PartDtos;

public class PartGetFilterDto
{
    public string CodeContains { get; set; }
    public string DescriptionContains { get; set; }
    public int QuantityInStockEqual { get; set; }
    public int MinimumRequiredEqual { get; set; }
    public DateTime CreatedAtEqual { get; set; }
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}
