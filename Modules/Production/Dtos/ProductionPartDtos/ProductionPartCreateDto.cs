namespace Modules.Production.Dtos.ProductionPartDtos;

public class ProductionPartCreateDto
{
    public Guid ProductionOrderId { get; set; }
    public string PartCode { get; set; }
    public int QuantityUsed { get; set; }
}
