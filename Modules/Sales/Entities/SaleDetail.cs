namespace Modules.Sales.Entities;

public class SaleDetail
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
