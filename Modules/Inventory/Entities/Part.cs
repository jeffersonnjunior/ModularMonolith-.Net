namespace Modules.Inventory;

public class Part
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public int QuantityInStock { get; set; }
    public int MinimumRequired { get; set; }
    public DateTime CreatedAt { get; set; }
}
