namespace Modules.Sales.Dtos.SaleDetailDtos;

public class SaleDetailGetFilterDto
{
    public Guid CarSaleIdEqual { get; set; }
    public string BuyerNameContains { get; set; }
    public decimal PriceContainsEqual { get; set; }
    public decimal DiscountEqual { get; set; }
    public string PaymentMethodContains { get; set; }
    public string NotesContains { get; set; }
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}