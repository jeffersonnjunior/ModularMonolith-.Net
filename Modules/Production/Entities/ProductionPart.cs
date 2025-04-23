namespace Modules.Production.Entities;

public class ProductionPart
{
    public Guid Id { get; set; }
    public Guid ProductionOrderId { get; set; }
    public string PartCode { get; set; }
    public int QuantityUsed { get; set; }

    public ProductionOrder ProductionOrder { get; set; }
}
