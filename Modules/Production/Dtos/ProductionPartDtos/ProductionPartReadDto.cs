using Modules.Production.Entities;

namespace Modules.Production.Dtos.ProductionPartDtos;

public class ProductionPartReadDto
{
    public Guid Id { get; set; }
    public Guid ProductionOrderId { get; set; }
    public string PartCode { get; set; }
    public int QuantityUsed { get; set; }

    public ProductionOrder ProductionOrder { get; set; }
}
