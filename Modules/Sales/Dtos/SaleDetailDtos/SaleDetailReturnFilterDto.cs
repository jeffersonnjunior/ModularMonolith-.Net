namespace Modules.Sales.Dtos.SaleDetailDtos;

public class SaleDetailReturnFilterDto
{
    public int TotalRegister { get; set; }

    public int TotalRegisterFilter { get; set; }

    public int TotalPages { get; set; }

    public List<SaleDetailReadDto> SaleDetailReadDto { get; set; }
}