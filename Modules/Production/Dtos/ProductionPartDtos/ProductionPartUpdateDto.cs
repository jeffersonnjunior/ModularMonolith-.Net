namespace Modules.Production.Dtos.ProductionPartDtos;

public class ProductionPartUpdateDto
{
    public Guid Id { get; set; }
    public Guid ProductionOrderId { get; set; }
    public string PartCode { get; set; }
    public int QuantityUsed { get; set; }
}
