namespace Modules.Sales.Dtos.CarSaleDtos;

public class CarSaleReturnFilterDto
{
    public int TotalRegister { get; set; }

    public int TotalRegisterFilter { get; set; }

    public int TotalPages { get; set; }

    public List<CarSaleReadDto> CarSaleReadDto { get; set; }
}