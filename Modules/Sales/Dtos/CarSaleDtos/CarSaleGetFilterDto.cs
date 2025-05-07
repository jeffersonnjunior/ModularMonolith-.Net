using Modules.Sales.Enums;

namespace Modules.Sales.Querys.CarSaleQuerys;

public class CarSaleGetFilterDto
{
    public Guid ProductionOrderIdEqual { get; set; }
    public SaleStatus StatusEqual { get; set; }
    public DateTime? SoldAtEqual { get; set; }
}