using Modules.Production.Enums;

namespace Modules.Production.Entities;

public class ProductionOrder
{
    public Guid Id { get; set; }
    public string Model { get; set; }
    public ProductionStatus ProductionStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    public ICollection<ProductionPart> Parts { get; set; }
}
