namespace Modules.Production.Dtos.ProductionOrderDtos;

public class ProductionOrderReturnFilterDto
{
    public int TotalRegister { get; set; }

    public int TotalRegisterFilter { get; set; }

    public int TotalPages { get; set; }

    public List<ProductionOrderReadDto> ProductionOrder { get; set; }
}
