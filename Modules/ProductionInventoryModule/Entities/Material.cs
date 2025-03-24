namespace Modules.ProductionInventoryModule.Entities;

public class Material
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int StockQuantity { get; set; }
    public int MinimumStock { get; set; }
    public decimal UnitPrice { get; set; }

    public ICollection<OrderMaterial> OrderMaterials { get; set; }
}