using Modules.Sales.Enums;

namespace Modules.Sales.Entities;

public class CarSale
{
    public Guid Id { get; set; }
    public Guid ProductionOrderId { get; set; }
    public SaleStatus Status { get; set; }
    public DateTime? SoldAt { get; set; }

    public SaleDetail SaleDetail { get; set; }
}
