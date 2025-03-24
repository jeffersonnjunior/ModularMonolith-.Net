namespace Modules.ProductionInventoryModule.Entities;

public class OrderMaterial
{
    public Guid Id { get; set; }
    public Guid MaterialId { get; set; }
    public int QuantityUsed { get; set; }
    
    public Material Material { get; set; }
    public ICollection<ProductionOrder> ProductionOrders { get; set; }
}