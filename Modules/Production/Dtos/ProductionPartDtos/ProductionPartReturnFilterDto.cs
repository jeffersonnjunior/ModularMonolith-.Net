namespace Modules.Production.Dtos.ProductionPartDtos;

public class ProductionPartReturnFilterDto
{
    public int TotalRegister { get; set; }

    public int TotalRegisterFilter { get; set; }

    public int TotalPages { get; set; }

    public List<ProductionPartReadDto> ProductionPart { get; set; }
}
