using Modules.Sales.Entities;

namespace Modules.Sales.Dtos.SaleDetailDtos;

public class SaleDetailReadDto
{
    public Guid Id { get; set; }
    public Guid CarSaleId { get; set; }
    public string BuyerName { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public string PaymentMethod { get; set; }
    public string Notes { get; set; }

    public CarSale CarSale { get; set; }
}
